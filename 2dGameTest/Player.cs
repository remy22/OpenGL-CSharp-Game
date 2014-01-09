using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;

namespace _2dGameTest
{
    class Player : GameObject
    {

        private static Vector3[] vertices =
        {
            new Vector3(-16, -12, -1),
            new Vector3( 16, -12, -1),
            new Vector3( 16,  4, -1),
 
            new Vector3(-16, -12, -1),
            new Vector3(-16,  4, -1),
            new Vector3( 16,  4, -1),
 
            new Vector3( 12,  0,  -1),
            new Vector3( 12,  16, -1), 
            new Vector3( 14,  16, -1),
 
            new Vector3( 12, 0,  -1),
            new Vector3( 14, 0 , -1),
            new Vector3( 14, 16, -1),  
        };

        private static int vboIndex = -1;
        private Game game;

        private float x, y;
        private float angle;

        public override void init(Game game)
        {
            this.game = game;
            if (vboIndex < 0)
            {
                vboIndex = game.GetVboHelper().addToVBO(vertices);
            }

            x = 0;
            y = 0;
            angle = 0;
        }

        public override void update(double time)
        {
            float oldx = x;
            float oldy = y;
            
            if(game.Keyboard[Key.A])
            {
                x -= (float)(750*time);
            }
            if (game.Keyboard[Key.D])
            {
                x += (float)(750 * time);
            }
            if (game.Keyboard[Key.W])
            {
                y += (float)(750 * time);
            }
            if (game.Keyboard[Key.S])
            {
                y -= (float)(750 * time);
            }

            checkCollision(oldx, oldy, x, y);

            float dx = (game.mouseX - game.screenW / 2) - x;
            float dy = -(game.mouseY - game.screenH / 2) - y;

            angle = (float)(Math.Atan2(dy,dx) - (90/(180/Math.PI)));
        }

        private void checkCollision(float x1, float y1, float x2, float y2)
        {

            Rectangle myRect = new Rectangle((int)x2-16, (int)y2-16, 32, 32);
            Rectangle myRect_x = new Rectangle((int)x2 - 16, (int)y1 - 16, 32, 32);
            Rectangle myRect_y = new Rectangle((int)x1 - 16, (int)y2 - 16, 32, 32);

            foreach (Rectangle rect in this.game.GetCurrentLevel().GetRectangles())
            {
                if (myRect.IntersectsWith(rect))
                {
                    if (myRect_x.IntersectsWith(rect))
                    {
                        this.x = x1;
                    }
                    if (myRect_y.IntersectsWith(rect))
                    {
                        this.y = y1;
                    }
                }
            }
        }

        public override void draw()
        {
            int positionAttribLocation = GL.GetAttribLocation(game.getShader(), "in_position");
            GL.VertexAttribPointer(positionAttribLocation, 3, VertexAttribPointerType.Float, false, Vector3.SizeInBytes, 0);
            GL.EnableVertexAttribArray(positionAttribLocation);

            Matrix4 model = Matrix4.Identity;
            model = model*Matrix4.CreateRotationZ(angle);
            model = model*Matrix4.CreateTranslation(new Vector3(x, y, 0));
            

            int modelUniformLocation = GL.GetUniformLocation(game.getShader(), "model_matrix");
            GL.UniformMatrix4(modelUniformLocation, false, ref model);


            GL.DrawArrays(PrimitiveType.Triangles, vboIndex, vertices.Length);

            GL.DisableVertexAttribArray(positionAttribLocation);
        }
    }
}
