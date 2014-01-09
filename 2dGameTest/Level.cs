using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace _2dGameTest
{
    class Level : GameObject
    {
        private List<Rectangle> rectangles;
        private Vector3[] vertices;

        private static int vboIndex = -1;
        private Game game;

        public Level()
        {
            rectangles = new List<Rectangle>();
        }


        public void addRectangle(Rectangle rect)
        {
            this.rectangles.Add(rect);
        }

        public void addRectangles(Rectangle[] rects)
        {
            this.rectangles.AddRange(rects);
        }

        public Rectangle[] GetRectangles()
        {
            return rectangles.ToArray();
        }

        public void makeVertices()
        {
            int num = this.rectangles.Count*6;
            this.vertices = new Vector3[num];

            foreach (Rectangle rect in this.rectangles)
            {
                int start = this.rectangles.IndexOf(rect) * 6;
                this.vertices[start + 0] = new Vector3(rect.X             , rect.Y              , 0);
                this.vertices[start + 1] = new Vector3(rect.X             , rect.Y + rect.Height, 0);
                this.vertices[start + 2] = new Vector3(rect.X + rect.Width, rect.Y + rect.Height, 0);

                this.vertices[start + 3] = new Vector3(rect.X             , rect.Y              , 0);
                this.vertices[start + 4] = new Vector3(rect.X + rect.Width, rect.Y              , 0);
                this.vertices[start + 5] = new Vector3(rect.X + rect.Width, rect.Y + rect.Height, 0);
            }
        }

        public override void init(Game game)
        {
            this.game = game;
            if (vboIndex < 0)
            {
                vboIndex = game.GetVboHelper().addToVBO(vertices);
            }
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
            //model = model * Matrix4.CreateTranslation(new Vector3(-game.screenW / 2, -game.screenH / 2, 0));

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
