using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

using Config = OpenTK.Configuration;
namespace _2dGameTest
{
    class Start
    {
        static void Main(string[] args)
        {
            Console.Out.WriteLine("Starting!");
            MyGameWindow window = new MyGameWindow();
            window.Run();
            
        }
    }
}
