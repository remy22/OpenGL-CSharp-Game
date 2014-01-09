using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;

namespace _2dGameTest
{
    class MyGameWindow : GameWindow
    {
        private Game game;

        private int uniformPositionLocation;

        public MyGameWindow() : base(1280, 720)
        {

        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            VSync = VSyncMode.Off;
            this.CursorVisible = false;

            GL.ClearColor(new Color4(255,255,255,0));

            game = new Game(1280, 720);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            game.screenW = this.Width;
            game.screenH = this.Height;
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);
            game.mouseX = Mouse.X;
            game.mouseY = Mouse.Y;

            game.Keyboard = this.Keyboard;

            game.updateAll(e.Time);
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            this.Title = "FPS: " + (this.RenderFrequency).ToString("F2") + " UPS: " + (this.UpdateFrequency).ToString("F2");
            base.OnRenderFrame(e);

            /*
             * Clear buffer
             */

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);


            /*
             * Calculate Projection & View
             */

            Matrix4 projection = Matrix4.CreateOrthographic(base.Width, base.Height, 0.01f, 100);
            Matrix4 view = Matrix4.LookAt(new Vector3(0, 0, 3), new Vector3(0, 0, 0), new Vector3(0, 1, 0));

            GL.UseProgram(game.getShader());
            
            int projectionUniformLocation = GL.GetUniformLocation(game.getShader(), "projection_matrix");
            int viewUniformLocation = GL.GetUniformLocation(game.getShader(), "view_matrix");

            GL.UniformMatrix4(projectionUniformLocation, false, ref projection);
            GL.UniformMatrix4(viewUniformLocation, false, ref view);

            /*
             * Tell the engine to draw objects
             */

            game.drawAll();

            /*
             * Clean up
             */

            GL.UseProgram(0);
            SwapBuffers();
        }
    }
}
