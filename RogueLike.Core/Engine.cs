using OpenTK.Graphics;
using OpenTK.Input;
using RogueLike.Core.Systems.CameraSystem;
using RogueLike.Core.Systems.ChunkingSystem;
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
        private readonly IChunkingSystem chunkingSystem;
        private readonly MapSystem mapSystem;
        private readonly int ConsoleHeigth =30;
        private readonly int ConsoleWidth =70;

        public Engine(IEntityManager entityManager, IChunkingSystem chunkingSystem, MapSystem mapSystem)
        {
            this.entityManager = entityManager;
            this.chunkingSystem = chunkingSystem;
            this.mapSystem = mapSystem;
        }

        public void Run()
        {
            var stopWatch = new Stopwatch();
            PositionComponent cameraPosition = LoadMapEntitiesAndReturnPlayer();
            var gameConsole = new ConsoleWindow(ConsoleHeigth, ConsoleWidth, "RogueLike");
            chunkingSystem.Initialize(new PositionComponent(0,0));
           
            mapSystem.Initialize();
            var camera = new Camera(cameraPosition, 31);


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

                    chunkingSystem.TrackPosition(camera.Position);
                    mapSystem.CheckForUpdate();

                    var view = camera.GetCurrentView(mapSystem);
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
                    Console.WriteLine(stopWatch.ElapsedMilliseconds);
                    stopWatch.Reset();
                    gameConsole.Write(0,40,$"CameraPos Y:{camera.Position.YCoord},X:{camera.Position.XCoord}",Color4.White);
                    gameConsole.Write(1, 40, $"TopLeftChunk Y:{mapSystem.TopLeftCorner.YCoord},X:{mapSystem.TopLeftCorner.XCoord}", Color4.White);
                    gameConsole.Write(2, 40, $"Current Chunk Y:{chunkingSystem.PositionInChunk(cameraPosition).ToString()}", Color4.White);


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
