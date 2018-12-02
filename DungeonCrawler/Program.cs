using DungeonCrawler.World;
using System;
using System.Collections.Generic;
using System.Drawing;
using static DungeonCrawler.World.Tile;

namespace DungeonCrawler
{
    class Program
    {
        private static object lck = new object();
        private static Random rnd = new Random();

        public static int RandomNum(int min, int max)
        {
            lock (lck)
            {
                return rnd.Next(min, max);
            }
        }

        static void Main(string[] args)
        {
            World.Map.Instance.load();
            while (true)
            {
                gameTick();
            }
        }

        static void gameTick()
        {
            showTiles();
            updateByInput();
        }

        static void showTiles()
        {
            Console.Clear();

            List<Tile> toShow = World.Map.Instance.RaytracePlayerTiles();

            Size size = World.Map.Instance.GetSize();
            for (int y = 0; y <= size.Height; y++)
            {
                for (int x = 0; x <= size.Width; x++)
                {
                    Tile currentT = World.Map.Instance.GetTile(x, y);
                    if (toShow.Contains(currentT))
                        Console.Write(Utils.ResolveTileType(currentT.Type));
                    else
                        Console.Write(' ');
                }

                Console.Write('\n');
            }
        }

        static void updateByInput()
        {
            Direction direction;
            do
            {
                ConsoleKey key = Console.ReadKey(true).Key;
                direction = GetDirectionByKey(key);
            } while (direction == Direction.NONE);

            World.Map.Instance.MoveCreature(World.Map.Instance.GetPlayer(), direction);
        }

        static Direction GetDirectionByKey(ConsoleKey key)
        {
            switch (key)
            {
                case ConsoleKey.LeftArrow:
                    return Direction.LEFT;

                case ConsoleKey.UpArrow:
                    return Direction.UP;

                case ConsoleKey.RightArrow:
                    return Direction.RIGHT;

                case ConsoleKey.DownArrow:
                    return Direction.DOWN;
            }

            return Direction.NONE;
        }
    }
}
