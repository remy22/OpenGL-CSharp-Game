using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;

namespace _2dGameTest
{
    class Game
    {
        private GameObject[] gameObjects;
        private Level currentLevel;

        private VBOHelper vboHelper = new VBOHelper();
        private Shaders shaders = new Shaders();

        public int mouseX;
        public int mouseY;

        public KeyboardDevice Keyboard;

        public int screenW, screenH;

        public Game(int screenw, int screenh)
        {
            this.screenW = screenw;
            this.screenH = screenh;

            currentLevel = new LevelParser(this).constructLevel("level1.lvl");

            gameObjects = new GameObject[] {
            new Player(),
            new Crosshair(), 
            currentLevel
            
        };

            createVBO();
            createShaders();
        }

        public GameObject[] getObjects()
        {
            return gameObjects;
        }

        public Level GetCurrentLevel()
        {
            return currentLevel;
        }

        private void createVBO()
        {
            vboHelper.initVBO();
            Console.Out.WriteLine("VBO created: " + GL.GetError());
            foreach (GameObject obj in gameObjects)
            {
                obj.init(this);
            }
            vboHelper.setVBO();

            Console.Out.WriteLine("VBO data set: " + GL.GetError());
        }

        private void createShaders()
        {
            shaders.setupShaders();
            Console.Out.WriteLine("Shaders created: " + GL.GetError());
        }

        public VBOHelper GetVboHelper()
        {
            return vboHelper;
        }

        public int getShader()
        {
            return shaders.getShader();
        }

        public void drawAll()
        {
            foreach (GameObject gameObject in gameObjects)
            {
                gameObject.draw();
            }
        }

        public void updateAll(double time)
        {
            foreach (GameObject gameObject in gameObjects)
            {
                gameObject.update(time);
            }
        }
    }
}
