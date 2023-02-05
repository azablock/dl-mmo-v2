using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Darkland.Sources.Models.Ai {

    public class Bresenham3Dv2 : IEnumerable {

        private Vector3Int _start;
        private Vector3Int _target;

        public Bresenham3Dv2(Vector3Int start, Vector3Int target) {
            _start = start;
            _target = target;
        }

        public List<Vector3Int> Result() {
            var result = new List<Vector3Int>();
            
            var x1 = _target.x;
            var y1 = _target.y;
            var z1 = _target.z;

            var x0 = _start.x;
            var y0 = _start.y;
            var z0 = _start.z;

            int dx = Math.Abs(x1 - x0), sx = x0 < x1 ? 1 : -1;
            int dy = Math.Abs(y1 - y0), sy = y0 < y1 ? 1 : -1;
            int dz = Math.Abs(z1 - z0), sz = z0 < z1 ? 1 : -1;
            int dm = Math.Max(dx, Math.Max(dy, dz)), i = dm; /* maximum difference */
            x1 = y1 = z1 = dm / 2; /* error offset */

            for (;;) {
                /* loop */

                result.Add(new Vector3Int(x0, y0, z0));

                if (i-- == 0) break;
                x1 -= dx;
                if (x1 < 0) {
                    x1 += dm;
                    x0 += sx;
                }

                y1 -= dy;
                if (y1 < 0) {
                    y1 += dm;
                    y0 += sy;
                }

                z1 -= dz;
                if (z1 < 0) {
                    z1 += dm;
                    z0 += sz;
                }
            }

            return result;
        }

        public IEnumerator GetEnumerator() {
            var x1 = _target.x;
            var y1 = _target.y;
            var z1 = _target.z;

            var x0 = _start.x;
            var y0 = _start.y;
            var z0 = _start.z;

            int dx = Math.Abs(x1 - x0), sx = x0 < x1 ? 1 : -1;
            int dy = Math.Abs(y1 - y0), sy = y0 < y1 ? 1 : -1;
            int dz = Math.Abs(z1 - z0), sz = z0 < z1 ? 1 : -1;
            int dm = Math.Max(dx, Math.Max(dy, dz)), i = dm; /* maximum difference */
            x1 = y1 = z1 = dm / 2; /* error offset */

            for (;;) {
                /* loop */

                yield return new Vector3(x0, y0, z0);

                if (i-- == 0) yield break;
                x1 -= dx;
                if (x1 < 0) {
                    x1 += dm;
                    x0 += sx;
                }

                y1 -= dy;
                if (y1 < 0) {
                    y1 += dm;
                    y0 += sy;
                }

                z1 -= dz;
                if (z1 < 0) {
                    z1 += dm;
                    z0 += sz;
                }
            }
        }

    }


    //https://wiki.unity3d.com/index.php/Bresenham3D#CSharp_-_Bresenham3D.cs
    public class Bresenham3D : IEnumerable {

        private readonly Vector3 _start;
        private readonly Vector3 _end;
        private readonly float _steps;

        public Bresenham3D(Vector3 start, Vector3 end) {
            _start = start;
            _end = end;
            _steps = 1;
        }

        public Bresenham3D(Vector3 start, Vector3 end, float steps) {
            _steps = steps;
            _start = start * _steps;
            _end = end * _steps;
        }

        public IEnumerator GetEnumerator() {
            Vector3 result;

            int xd, yd, zd;
            int x, y, z;
            int ax, ay, az;
            int sx, sy, sz;
            int dx, dy, dz;

            dx = (int)(_end.x - _start.x);
            dy = (int)(_end.y - _start.y);
            dz = (int)(_end.z - _start.z);

            ax = Mathf.Abs(dx) << 1;
            ay = Mathf.Abs(dy) << 1;
            az = Mathf.Abs(dz) << 1;

            sx = (int)Mathf.Sign((float)dx);
            sy = (int)Mathf.Sign((float)dy);
            sz = (int)Mathf.Sign((float)dz);

            x = (int)_start.x;
            y = (int)_start.y;
            z = (int)_start.z;

            if (ax >= Mathf.Max(ay, az)) // x dominant
            {
                yd = ay - (ax >> 1);
                zd = az - (ax >> 1);
                for (;;) {
                    result.x = (int)(x / _steps);
                    result.y = (int)(y / _steps);
                    result.z = (int)(z / _steps);
                    yield return result;

                    if (x == (int)_end.x)
                        yield break;

                    if (yd >= 0) {
                        y += sy;
                        yd -= ax;
                    }

                    if (zd >= 0) {
                        z += sz;
                        zd -= ax;
                    }

                    x += sx;
                    yd += ay;
                    zd += az;
                }
            }
            else if (ay >= Mathf.Max(ax, az)) // y dominant
            {
                xd = ax - (ay >> 1);
                zd = az - (ay >> 1);
                for (;;) {
                    result.x = (int)(x / _steps);
                    result.y = (int)(y / _steps);
                    result.z = (int)(z / _steps);
                    yield return result;

                    if (y == (int)_end.y)
                        yield break;

                    if (xd >= 0) {
                        x += sx;
                        xd -= ay;
                    }

                    if (zd >= 0) {
                        z += sz;
                        zd -= ay;
                    }

                    y += sy;
                    xd += ax;
                    zd += az;
                }
            }
            else if (az >= Mathf.Max(ax, ay)) // z dominant
            {
                xd = ax - (az >> 1);
                yd = ay - (az >> 1);
                for (;;) {
                    result.x = (int)(x / _steps);
                    result.y = (int)(y / _steps);
                    result.z = (int)(z / _steps);
                    yield return result;

                    if (z == (int)_end.z)
                        yield break;

                    if (xd >= 0) {
                        x += sx;
                        xd -= az;
                    }

                    if (yd >= 0) {
                        y += sy;
                        yd -= az;
                    }

                    z += sz;
                    xd += ax;
                    yd += ay;
                }
            }
        }

    }

}