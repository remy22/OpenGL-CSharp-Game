using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenTK;
using OpenTK.Input;
using OpenTK.Graphics.OpenGL;

namespace _2dGameTest
{
    class Crosshair : GameObject
    {

        Vector3[] vertices =
        {
            new Vector3( -5,  0, 0),
            new Vector3(-15, -3, 0),
            new Vector3(-15,  3, 0),
 
            new Vector3( 5,  0, 0),
            new Vector3(15,  3, 0),
            new Vector3(15, -3, 0),
 
            new Vector3( 0,  5, 0),
            new Vector3(-3, 15, 0),
            new Vector3( 3, 15, 0),
 
            new Vector3( 0, -5,  0),
            new Vector3(-3, -15, 0),
            new Vector3( 3, -15, 0), 
        };

        private static int vboIndex = -1;
        private Game game;

        private int x, y;
        public override void init(Game game)
        {
            this.game = game;
            if (vboIndex < 0)
            {
                vboIndex = game.GetVboHelper().addToVBO(vertices);
            }

            x = 0;
            y = 0;
        }

        public override void update(double time)
        {
 
        }

        public override void draw()
        {

            /*
             * Vertex positions (in-model)
             */

            int positionAttribLocation = GL.GetAttribLocation(game.getShader(), "in_position");
            GL.VertexAttribPointer(positionAttribLocation, 3, VertexAttribPointerType.Float, false, Vector3.SizeInBytes, 0);
            GL.EnableVertexAttribArray(positionAttribLocation);

            /*
             * Model position (outside-model)
             */

            Matrix4 model = Matrix4.Identity;
            model = model * Matrix4.CreateTranslation(new Vector3(game.mouseX - game.screenW/2, -(game.mouseY-game.screenH/2), 0));

            int modelUniformLocation = GL.GetUniformLocation(game.getShader(), "model_matrix");
            GL.UniformMatrix4(modelUniformLocation, false, ref model);

            /*
             * Draw
             */

            GL.DrawArrays(PrimitiveType.Triangles, vboIndex, vertices.Length);

            /*
             * Clean up
             */

            GL.DisableVertexAttribArray(positionAttribLocation);
        }
    }
}
