using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Codice.Client.Common;
using UnityEditor;
using UnityEditor.Build.Reporting;
using UnityEngine;

namespace Editor.UnityBuilderAction {

    public static class BuildScript2 {

        public static void BuildProject() {
            // Gather values from args
            var options = ArgumentsParser.GetValidatedOptions();

            int i = 0;
            //todo AZ zmiana
            foreach (var keyValuePair in options) {
                Debug.Log($"OPTION <{i}> {keyValuePair.Key} = {options[keyValuePair.Key]}");
                i++;
            }
            
            
            
            
            // Gather values from project
            var scenes = EditorBuildSettings.scenes.Where(scene => scene.enabled).Select(s => s.path).ToArray();

            // Get all buildOptions from options
            BuildOptions buildOptions = BuildOptions.None;
            foreach (string buildOptionString in Enum.GetNames(typeof(BuildOptions))) {
                if (options.ContainsKey(buildOptionString)) {
                    BuildOptions buildOptionEnum = (BuildOptions) Enum.Parse(typeof(BuildOptions), buildOptionString);
                    buildOptions |= buildOptionEnum;
                }
            }

            // Define BuildPlayer Options
            var buildPlayerOptions = new BuildPlayerOptions {
                scenes = scenes,
                // locationPathName = options["customBuildPath"],
                target = (BuildTarget) Enum.Parse(typeof(BuildTarget), options["buildTarget"]),
                options = buildOptions
            };

            // Set version for this build
            // VersionApplicator.SetVersion(options["buildVersion"]);

            // Apply Android settings
            if (buildPlayerOptions.target == BuildTarget.Android) {
                // VersionApplicator.SetAndroidVersionCode(options["androidVersionCode"]);
                // AndroidSettings.Apply(options);
            }

            // Execute default AddressableAsset content build, if the package is installed.
            // Version defines would be the best solution here, but Unity 2018 doesn't support that,
            // so we fall back to using reflection instead.
            var addressableAssetSettingsType = Type.GetType(
                "UnityEditor.AddressableAssets.Settings.AddressableAssetSettings,Unity.Addressables.Editor"
            );
            if (addressableAssetSettingsType != null) {
                // ReSharper disable once PossibleNullReferenceException, used from try-catch
                try {
                    addressableAssetSettingsType
                        .GetMethod("CleanPlayerContent", BindingFlags.Static | BindingFlags.Public)
                        .Invoke(null, new object[] {null});
                    addressableAssetSettingsType.GetMethod("BuildPlayerContent", new Type[0])
                                                .Invoke(null, new object[0]);
                }
                catch (Exception e) {
                    Debug.LogError($"Failed to run default addressables build:\n{e}");
                }
            }

            // Perform build
            BuildReport buildReport = BuildPipeline.BuildPlayer(buildPlayerOptions);

            // Summary
            BuildSummary summary = buildReport.summary;
            // StdOutReporter.ReportSummary(summary);

            // Result
            BuildResult result = summary.result;
            ExitWithResult(result);
            // StdOutReporter.ExitWithResult(result);
        }
        
        private static void ExitWithResult(BuildResult result)
        {
            switch (result)
            {
                case BuildResult.Succeeded:
                    Console.WriteLine("Build succeeded!");
                    EditorApplication.Exit(0);
                    break;
                case BuildResult.Failed:
                    Console.WriteLine("Build failed!");
                    EditorApplication.Exit(101);
                    break;
                case BuildResult.Cancelled:
                    Console.WriteLine("Build cancelled!");
                    EditorApplication.Exit(102);
                    break;
                case BuildResult.Unknown:
                default:
                    Console.WriteLine("Build result is unknown!");
                    EditorApplication.Exit(103);
                    break;
            }
        }

    }

    public class ArgumentsParser {
        static string EOL = Environment.NewLine;
        static readonly string[] Secrets = {"androidKeystorePass", "androidKeyaliasName", "androidKeyaliasPass"};

        public static Dictionary<string, string> GetValidatedOptions() {
            ParseCommandLineArguments(out var validatedOptions);

            if (!validatedOptions.TryGetValue("projectPath", out var projectPath)) {
                Console.WriteLine("Missing argument -projectPath");
                EditorApplication.Exit(110);
            }

            if (!validatedOptions.TryGetValue("buildTarget", out var buildTarget)) {
                Console.WriteLine("Missing argument -buildTarget");
                EditorApplication.Exit(120);
            }

            if (!Enum.IsDefined(typeof(BuildTarget), buildTarget)) {
                EditorApplication.Exit(121);
            }

            if (!validatedOptions.TryGetValue("customBuildPath", out var customBuildPath)) {
                Console.WriteLine("Missing argument -customBuildPath");
                EditorApplication.Exit(130);
            }

            const string defaultCustomBuildName = "TestBuild";
            if (!validatedOptions.TryGetValue("customBuildName", out var customBuildName)) {
                Console.WriteLine($"Missing argument -customBuildName, defaulting to {defaultCustomBuildName}.");
                validatedOptions.Add("customBuildName", defaultCustomBuildName);
            } else if (customBuildName == "") {
                Console.WriteLine($"Invalid argument -customBuildName, defaulting to {defaultCustomBuildName}.");
                validatedOptions.Add("customBuildName", defaultCustomBuildName);
            }

            return validatedOptions;
        }

        static void ParseCommandLineArguments(out Dictionary<string, string> providedArguments) {
            providedArguments = new Dictionary<string, string>();
            string[] args = Environment.GetCommandLineArgs();

            Console.WriteLine(
                $"{EOL}" +
                $"###########################{EOL}" +
                $"#    Parsing settings     #{EOL}" +
                $"###########################{EOL}" +
                $"{EOL}"
            );

            // Extract flags with optional values
            for (int current = 0, next = 1; current < args.Length; current++, next++) {
                // Parse flag
                bool isFlag = args[current].StartsWith("-");
                if (!isFlag) continue;
                string flag = args[current].TrimStart('-');

                // Parse optional value
                bool flagHasValue = next < args.Length && !args[next].StartsWith("-");
                string value = flagHasValue ? args[next].TrimStart('-') : "";
                bool secret = Secrets.Contains(flag);
                string displayValue = secret ? "*HIDDEN*" : "\"" + value + "\"";

                // Assign
                Console.WriteLine($"Found flag \"{flag}\" with value {displayValue}.");
                providedArguments.Add(flag, value);
            }
        }
    }

}