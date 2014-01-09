using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2dGameTest
{
    abstract class GameObject
    {
        public abstract void init(Game game);
        public abstract void update(double time);
        public abstract void draw();
    }
}
