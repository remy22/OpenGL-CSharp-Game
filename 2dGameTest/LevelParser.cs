using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _2dGameTest
{
    class LevelParser
    {
        private Game game;

        public LevelParser(Game game)
        {
            this.game = game;
        }

        public Level constructLevel(String path)
        {
            Level newLevel = parseLevel(path);

            return newLevel;
        }

        private Level parseLevel(String path)
        {
            Level level = new Level();

            String[] lines = System.IO.File.ReadAllLines(path);
            foreach (string line in lines)
            {
                string trimmed = line.Trim();
                if (trimmed.StartsWith("G"))
                {
                    string[] args = trimmed.Split(' ');

                    Rectangle rect = new Rectangle(-(game.screenW/2) + int.Parse(args[1]), -(game.screenH/2) + int.Parse(args[2]), int.Parse(args[3]), int.Parse(args[4]));
                    level.addRectangle(rect);
                }
            }
            level.makeVertices();

            return level;
        }
    }
}
