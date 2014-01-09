using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL;

namespace _2dGameTest
{
    class Shaders
    {
        private string vertexShaderSource = File.ReadAllText("vertex.glsl");
        private string fragmentShaderSoure = File.ReadAllText("fragment.glsl");

        private int fragmentShaderHandle, vertexShaderHandle;
        private int programHandle;

        public void setupShaders()
        {
            vertexShaderHandle = GL.CreateShader(ShaderType.VertexShader);
            GL.ShaderSource(vertexShaderHandle, vertexShaderSource);
            GL.CompileShader(vertexShaderHandle);
            Console.Out.WriteLine("Vertex shader compile status: " + GL.GetShaderInfoLog(vertexShaderHandle).Trim());

            fragmentShaderHandle = GL.CreateShader(ShaderType.FragmentShader);
            GL.ShaderSource(fragmentShaderHandle, fragmentShaderSoure);
            GL.CompileShader(fragmentShaderHandle);
            Console.Out.WriteLine("Fragment shader compile status: " + GL.GetShaderInfoLog(fragmentShaderHandle).Trim());
            

            programHandle = GL.CreateProgram();
            GL.AttachShader(programHandle, vertexShaderHandle);
            GL.AttachShader(programHandle, fragmentShaderHandle);
            GL.LinkProgram(programHandle);
            
        }

        public int getShader()
        {
            return programHandle;
        }
    }
}
