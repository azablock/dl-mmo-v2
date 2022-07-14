using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.Build.Reporting;

namespace Editor.UnityBuilderAction {

    public static class BuildScript {

        private static readonly string Eol = Environment.NewLine;

        private static readonly string[] Secrets =
            {"androidKeystorePass", "androidKeyaliasName", "androidKeyaliasPass"};

        public static void Build() {
            // Gather values from args
            Dictionary<string, string> options = GetValidatedOptions();

            var buildTarget = (BuildTarget) Enum.Parse(typeof(BuildTarget), options["buildTarget"]);

            switch (buildTarget) {
                case BuildTarget.StandaloneWindows64:
                    PlayerSettings.SetScriptingBackend(BuildTargetGroup.Standalone, ScriptingImplementation.Mono2x);
                    break;
                    
                case BuildTarget.StandaloneLinux64:
                    PlayerSettings.SetScriptingBackend(BuildTargetGroup.Standalone, ScriptingImplementation.Mono2x);
                    break;
            }

            Build(buildTarget);
        }


        //without options["customBuildPath"]
        private static void Build(BuildTarget buildTarget) {
            var scenes = EditorBuildSettings.scenes
                                            .Where(scene => scene.enabled)
                                            .Select(s => s.path)
                                            .ToArray();

            var buildPlayerOptions = new BuildPlayerOptions {
                scenes = scenes,
                target = buildTarget,
//                targetGroup = BuildPipeline.GetBuildTargetGroup(buildTarget),
                // locationPathName = filePath, (method arg)
//                options = UnityEditor.BuildOptions.Development
            };

            var buildSummary = BuildPipeline.BuildPlayer(buildPlayerOptions).summary;
            // ReportSummary(buildSummary);
            ExitWithResult(buildSummary.result);
        }
    
        private static void Build(BuildTarget buildTarget, string filePath) {
            var scenes = EditorBuildSettings.scenes
                                            .Where(scene => scene.enabled)
                                            .Select(s => s.path)
                                            .ToArray();

            var buildPlayerOptions = new BuildPlayerOptions {
                scenes = scenes,
                target = buildTarget,
//                targetGroup = BuildPipeline.GetBuildTargetGroup(buildTarget),
                // locationPathName = filePath,
//                options = UnityEditor.BuildOptions.Development
            };

            var buildSummary = BuildPipeline.BuildPlayer(buildPlayerOptions).summary;
            // ReportSummary(buildSummary);
            ExitWithResult(buildSummary.result);
        }

        private static Dictionary<string, string> GetValidatedOptions() {
            ParseCommandLineArguments(out Dictionary<string, string> validatedOptions);

            // if (!validatedOptions.TryGetValue("projectPath", out string _)) {
            //     Console.WriteLine("Missing argument -projectPath");
            //     EditorApplication.Exit(110);
            // }
            //
            // if (!validatedOptions.TryGetValue("buildTarget", out string buildTarget)) {
            //     Console.WriteLine("Missing argument -buildTarget");
            //     EditorApplication.Exit(120);
            // }
            //
            // if (!Enum.IsDefined(typeof(BuildTarget), buildTarget ?? string.Empty)) {
            //     EditorApplication.Exit(121);
            // }
            //
            // if (!validatedOptions.TryGetValue("customBuildPath", out string _)) {
            //     Console.WriteLine("Missing argument -customBuildPath");
            //     EditorApplication.Exit(130);
            // }
            //
            // const string defaultCustomBuildName = "TestBuild";
            // if (!validatedOptions.TryGetValue("customBuildName", out string customBuildName)) {
            //     Console.WriteLine($"Missing argument -customBuildName, defaulting to {defaultCustomBuildName}.");
            //     validatedOptions.Add("customBuildName", defaultCustomBuildName);
            // } else if (customBuildName == "") {
            //     Console.WriteLine($"Invalid argument -customBuildName, defaulting to {defaultCustomBuildName}.");
            //     validatedOptions.Add("customBuildName", defaultCustomBuildName);
            // }

            return validatedOptions;
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

        private static void ParseCommandLineArguments(out Dictionary<string, string> providedArguments) {
            providedArguments = new Dictionary<string, string>();
            string[] args = Environment.GetCommandLineArgs();

            Console.WriteLine(
                $"{Eol}" +
                $"###########################{Eol}" +
                $"#    Parsing settings     #{Eol}" +
                $"###########################{Eol}" +
                $"{Eol}"
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