using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace _2dGameTest
{
    class VBOHelper
    {
        private Vector3[] vertices;
        private int vboId;

        public void initVBO()
        {
            GL.GenBuffers(1, out vboId);

            vertices = new Vector3[0];
        }

        public int addToVBO(Vector3[] newVertices)
        {
            int startIndex = vertices.Length;
            
            Vector3[] newarr = new Vector3[vertices.Length + newVertices.Length];
            vertices.CopyTo(newarr, 0);
            newVertices.CopyTo(newarr, vertices.Length);
            vertices = newarr;

            return startIndex;
        }

        public int getVBO()
        {
            return vboId;
        }

        public Vector3[] getVBOArray()
        {
            return vertices;
        }

        public void setVBO()
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, vboId);
            GL.BufferData(BufferTarget.ArrayBuffer, new IntPtr(vertices.Length * Vector3.SizeInBytes), vertices, BufferUsageHint.StaticDraw);
        }
    }
}
