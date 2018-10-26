using OpenTK.Graphics;
using OpenTK.Input;
using RogueLike.Core.Systems.CameraSystem;
using RogueLike.Core.Systems.WorldSystem;
using RogueLike.Data.Abstract;
using RogueLike.Data.Components.GeneralComponents;
using RogueLike.Data.Entities;
using SunshineConsole;
using System;
using System.Diagnostics;
using System.IO;
using RogueLike.Core.ComponentExtensions;
using RogueLike.Core.Systems;
using RogueLike.Core.Systems.DrawingSystem;
using RogueLike.Core.Systems.MovementSystem;

namespace RogueLike.Core
{
    class Engine
    {
        private readonly IEntityManager entityManager;
        private readonly WorldSystem worldSystem;
        private readonly MovementSystem movementSystem;
        private readonly DrawingSystem drawingSystem;
        private readonly VisionSystem visionSystem;
        private readonly int ConsoleHeigth =30;
        private readonly int ConsoleWidth =70;

        public Engine(IEntityManager entityManager, WorldSystem worldSystem, MovementSystem movementSystem, DrawingSystem drawingSystem,VisionSystem visionSystem)
        {
            this.entityManager = entityManager;
            this.worldSystem = worldSystem;
            this.movementSystem = movementSystem;
            this.drawingSystem = drawingSystem;
            this.visionSystem = visionSystem;
        }

        public void Run()
        {
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            PositionComponent cameraPosition = LoadMapEntitiesAndReturnPlayer();
            var gameConsole = new ConsoleWindow(ConsoleHeigth, ConsoleWidth, "RogueLike"); 
            var camera = new Camera(cameraPosition, 31,visionSystem);
            worldSystem.InitializeLocalMap(new PositionComponent(0, 0));
            stopWatch.Stop();
            Console.WriteLine("setup");
            Console.WriteLine($"ms:{stopWatch.ElapsedMilliseconds} ticks:{stopWatch.ElapsedTicks}");
            stopWatch.Reset();
            while (gameConsole.WindowUpdate())
            {
                if (gameConsole.KeyPressed)
                {
                    PositionComponent newPos = camera.Position.GetCopy();
                    //Input
                    switch (gameConsole.GetKey())
                    {
                        case Key.Down:
                            newPos.MoveBy(1, 0);
                            break;
                        case Key.Up:
                            newPos.MoveBy(-1, 0);
                            break;
                        case Key.Left:
                            newPos.MoveBy(0, -1);
                            break;
                        case Key.Right:
                            newPos.MoveBy(0, 1);
                            break;

                    }

                      
                    
                    stopWatch.Start();
                    if (movementSystem.CanBeMovedTo(newPos))
                    {
                        camera.Position.MoveTo(newPos); 
                    }
                    stopWatch.Stop();
                    Console.Write("CanMove:");
                    Console.WriteLine($"ms:{stopWatch.ElapsedMilliseconds} ticks:{stopWatch.ElapsedTicks}");
                    stopWatch.Reset();

                    stopWatch.Start();

                     
                    worldSystem.CheckAndMoveChunks(camera.Position);
                    visionSystem.ClearMask();
                    visionSystem.SetVisiblePositions(camera.Position,13);

                    var view = camera.GetCurrentViewWithLight(worldSystem);
                    drawingSystem.Render(gameConsole, view);
                    
                    stopWatch.Stop();
                    Console.Write("ViewMask:");
                    Console.WriteLine($" ms:{stopWatch.ElapsedMilliseconds} ticks:{stopWatch.ElapsedTicks}");
                    stopWatch.Reset();
                    gameConsole.Write(0,40,$"CameraPos Y:{camera.Position.YCoord},X:{camera.Position.XCoord}",Color4.White);
                    gameConsole.Write(1, 40, $"TopLeftChunk Y:{worldSystem.TopLeftCorner.YCoord},X:{worldSystem.TopLeftCorner.XCoord}", Color4.White);
                    gameConsole.Write(2, 40, $"Current Chunk Y:{worldSystem.PositionInChunk(cameraPosition).ToString()}", Color4.White);


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
                            entityToAdd = new FloorTileEntity(y, x);
                            break;
                           default:
                               entityToAdd = new WallTileEntity(y, x);
                               break;
                    }
                    entityManager.WorldEntities.Add(entityToAdd);
                }
            }

            return playerPosition;
        }

    }
}
