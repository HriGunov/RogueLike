using OpenTK.Graphics;
using OpenTK.Input;
using RogueLike.Core.Systems.CameraSystem;
using RogueLike.Core.Systems.MapSystem;
using RogueLike.Data.Abstract;
using RogueLike.Data.Components.GeneralComponents;
using RogueLike.Data.Entities;
using SunshineConsole;
using System;
using System.Diagnostics;
using System.IO;

namespace RogueLike.Core
{
    class Engine
    {
        private readonly IEntityManager entityManager;
        private readonly MapSystem mapSystem;
        private readonly int ConsoleHeigth =30;
        private readonly int ConsoleWidth =70;

        public Engine(IEntityManager entityManager, MapSystem mapSystem)
        {
            this.entityManager = entityManager;
           
            this.mapSystem = mapSystem;
        }

        public void Run()
        {
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            PositionComponent cameraPosition = LoadMapEntitiesAndReturnPlayer();
            var gameConsole = new ConsoleWindow(ConsoleHeigth, ConsoleWidth, "RogueLike");
           
            mapSystem.InitializeChunking(new PositionComponent(0,0));
            mapSystem.InitializeLocalMap();
            var camera = new Camera(cameraPosition, 31);

            stopWatch.Stop();
            Console.WriteLine("setup");
            Console.WriteLine($"ms:{stopWatch.ElapsedMilliseconds} ticks:{stopWatch.ElapsedTicks}");
            stopWatch.Reset();
            while (gameConsole.WindowUpdate())
            {
                if (gameConsole.KeyPressed)
                {
                    //Input
                    switch (gameConsole.GetKey())
                    {
                        case Key.Down:
                            cameraPosition.YCoord++;
                            break;
                        case Key.Up:
                            cameraPosition.YCoord--;
                            break;
                        case Key.Left:
                            cameraPosition.XCoord--;
                            break;
                        case Key.Right:
                            cameraPosition.XCoord++;
                            break;

                    }
                    stopWatch.Start();

                     
                    mapSystem.CheckAndMoveChunks(camera.Position);

                    var view = camera.GetCurrentView(mapSystem);
                    view[view.Length / 2][view.Length / 2] = '@';
                    for (int y = 0; y < view.Length; y++)
                    {
                        for (int x = 0; x < view[y].Length; x++)
                        {
                            if (view[y][x] != '!')
                            {
                                gameConsole.Write(y, x, view[y][x], Color4.White);

                            }
                            else
                            {
                                gameConsole.Write(y, x, view[y][x], Color4.Red);
                            }

                        }
                    }
                    stopWatch.Stop();
                    Console.WriteLine($"ms:{stopWatch.ElapsedMilliseconds} ticks:{stopWatch.ElapsedTicks}");
                    stopWatch.Reset();
                    gameConsole.Write(0,40,$"CameraPos Y:{camera.Position.YCoord},X:{camera.Position.XCoord}",Color4.White);
                    gameConsole.Write(1, 40, $"TopLeftChunk Y:{mapSystem.TopLeftCorner.YCoord},X:{mapSystem.TopLeftCorner.XCoord}", Color4.White);
                    gameConsole.Write(2, 40, $"Current Chunk Y:{mapSystem.PositionInChunk(cameraPosition).ToString()}", Color4.White);


                }

            }
        }

        public PositionComponent LoadMapEntitiesAndReturnPlayer()
        {
            var mapPath = AppContext.BaseDirectory + "map.txt";
            var mapLines = File.ReadAllLines(mapPath);
            PositionComponent playerPosition = null;
            for (int y = 0; y < mapLines.Length; y++)
            {
                for (int x = 0; x < mapLines[y].Length; x++)
                {
                    Entity entityToAdd = null;
                    switch (mapLines[y][x])
                    {
                        case '#':
                            entityToAdd = new WallTileEntity(y, x);
                            break;
                        case '.':
                        case ' ':
                            entityToAdd = new FloorTileEntity(y, x);
                            break;
                        case '@':
                            playerPosition = new PositionComponent(y, x);
                            entityToAdd = new WallTileEntity(y, x);
                            break;
                           default:
                               entityToAdd = new WallTileEntity(y, x);
                               break;
                    }
                    entityManager.Entities.Add(entityToAdd);
                }
            }

            return playerPosition;
        }

    }
}
