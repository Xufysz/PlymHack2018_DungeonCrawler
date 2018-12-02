using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using static DungeonCrawler.World.Tile;

namespace DungeonCrawler.World
{
    class Utils
    {
        public static Tile RandomTile(Size s)
        {
            Tile emptyRandTile = null;
            do
            {
                Point point = new Point(Program.RandomNum(0, s.Width), Program.RandomNum(0, s.Height));
                emptyRandTile = Map.Instance.GetTile(point);
            } while (emptyRandTile == null && emptyRandTile.Type != TileType.EMPTY);
            return emptyRandTile;
        }

        public static TileType ResolveTileType(char c)
        {
            switch (c)
            {
                case ' ':
                    return TileType.EMPTY;

                case '+':
                    return TileType.WALL;

                default:
                    return TileType.EMPTY;
            }
        }

        public static char ResolveTileType(TileType t)
        {
            switch (t)
            {
                case TileType.EMPTY:
                    return ' ';

                case TileType.WALL:
                    return '+';

                case TileType.CREATURE:
                    return 'X';

                default:
                    return 'e';
            }
        }
    }
}
