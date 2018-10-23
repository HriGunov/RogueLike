using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics;
using SunshineConsole;

namespace RogueLike.Core.Systems.DrawingSystem
{
    public class DrawingSystem
    {
        public DrawingSystem()
        {
            
        }

        public void Render(ConsoleWindow console,char[][] view)
        {
            view[view.Length / 2][view.Length / 2] = '@';
            for (int y = 0; y < view.Length; y++)
            {
                for (int x = 0; x < view[y].Length; x++)
                {
                    if (view[y][x] != '!')
                    {
                        console.Write(y, x, view[y][x], Color4.White);

                    }
                    else
                    {
                        console.Write(y, x, view[y][x], Color4.Red);
                    }

                }
            }
        }
    }
}
