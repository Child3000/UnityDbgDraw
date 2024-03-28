// DbgDraw for Unity. Copyright (c) 2019 Peter Schraut (www.console-dev.de). See LICENSE.md
// https://github.com/pschraut/UnityDbgDraw
using System.Collections.Generic;
using UnityEngine;
#pragma warning disable IDE0018 // Variable declaration can be inlined
#pragma warning disable IDE0017 // Object initialization can be simplified

namespace Oddworm.Framework
{
    public partial class DbgDraw
    {
        static Mesh s_WireCubeMesh = null;

        [System.Diagnostics.Conditional("UNITY_EDITOR")]
        [System.Diagnostics.Conditional("DEVELOPMENT_BUILD")]
        public static void WireCube(Vector3 position, Quaternion rotation, Vector3 scale, Vector3 extents, Color color, float duration = 0, bool depthTest = true)
        {
            MeshJob job;
            if (!TryAllocMeshJob(out job, duration, depthTest, UnityEngine.Rendering.CullMode.Off, true))
                return;

            if (s_WireCubeMesh == null)
            {
                s_WireCubeMesh = CreateWireCubeMesh(extents);
                ReleaseOnDestroy(s_WireCubeMesh);
            }

            job.mesh = s_WireCubeMesh;
            job.matrix = Matrix4x4.TRS(position, rotation, scale);
            job.color = color;

            job.Submit();
        }

        static Mesh CreateWireCubeMesh(Vector3 extents)
        {
            var mesh = new Mesh();
            mesh.name = "DbgDraw-WireCube-Mesh";

            var vertices = new List<Vector3>(24);

            var s = extents;
            vertices.Add(new Vector3(-s.x, -s.y, -s.z)); // bottom near left
            vertices.Add(new Vector3(-s.x, -s.y, +s.z)); // bottom far left
            vertices.Add(new Vector3(-s.x, -s.y, +s.z)); // bottom far left
            vertices.Add(new Vector3(+s.x, -s.y, +s.z)); // bottom far right
            vertices.Add(new Vector3(+s.x, -s.y, +s.z)); // bottom far right
            vertices.Add(new Vector3(+s.x, -s.y, -s.z)); // bottom near right
            vertices.Add(new Vector3(+s.x, -s.y, -s.z)); // bottom near right
            vertices.Add(new Vector3(-s.x, -s.y, -s.z)); // bottom near left

            vertices.Add(new Vector3(-s.x, +s.y, -s)); // top near left
            vertices.Add(new Vector3(-s.x, +s.y, +s)); // top far left
            vertices.Add(new Vector3(-s.x, +s.y, +s)); // top far left
            vertices.Add(new Vector3(+s.x, +s.y, +s)); // top far right
            vertices.Add(new Vector3(+s.x, +s.y, +s)); // top far right
            vertices.Add(new Vector3(+s.x, +s.y, -s)); // top near right
            vertices.Add(new Vector3(+s.x, +s.y, -s)); // top near right
            vertices.Add(new Vector3(-s.x, +s.y, -s)); // top near left

            vertices.Add(new Vector3(+s.x +s.y, +s.z)); // top far right
            vertices.Add(new Vector3(+s.x -s.y, +s.z)); // bottom far right
            vertices.Add(new Vector3(+s.x +s.y, -s.z)); // top near right
            vertices.Add(new Vector3(+s.x -s.y, -s.z)); // bottom near right

            vertices.Add(new Vector3(-s.x, +s.y +s.z); // top far left
            vertices.Add(new Vector3(-s.x, -s.y +s.z); // bottom far left
            vertices.Add(new Vector3(-s.x, +s.y -s.z); // top near left
            vertices.Add(new Vector3(-s.x, -s.y -s.z); // bottom near left

            mesh.SetVertices(vertices);

            var indices = new int[vertices.Count];
            for (var n = 0; n < indices.Length; ++n)
                indices[n] = n;
            mesh.SetIndices(indices, MeshTopology.Lines, 0);

            return mesh;
        }
    }
}
