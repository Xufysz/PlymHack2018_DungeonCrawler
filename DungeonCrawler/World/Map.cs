using DungeonCrawler.Creatures;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using static DungeonCrawler.World.Tile;

namespace DungeonCrawler.World
{
    sealed class Map
    {
        private static Map _instance = new Map();

        public static Map Instance
        {
            get
            {
                return _instance;
            }
        }

        private Tile[,] tiles;
        private Player player;

        public void load()
        {
            string[] lines = File.ReadAllLines(Environment.CurrentDirectory + @"\World\map.txt");
            tiles = new Tile[lines[0].Length, lines.Length];
            for (int y = 0; y < lines.Length; y++)
            {
                string line = lines[y];
                for (int x = 0; x < line.Length; x++)
                {
                    this.tiles[x, y] = new Tile(new Point(x, y), Utils.ResolveTileType(line[x]));
                }
            }

            //Rand pos player
            Tile randTile = Utils.RandomTile(this.GetSize());
            this.player = new Player(randTile.Position, 100);
            randTile.SetCreature(this.player);
        }

        public Player GetPlayer()
        {
            return this.player;
        }
        
        public Boolean MoveCreature(Creature creature, Direction direction)
        {
            Tile nextTile = null;
            switch (direction)
            {
                case Direction.LEFT:
                    nextTile = getCreatureLeftTile(creature);
                    break;

                case Direction.UP:
                    nextTile = getCreatureUpTile(creature);
                    break;

                case Direction.RIGHT:
                    nextTile = getCreatureRightTile(creature);
                    break;

                case Direction.DOWN:
                    nextTile = getCreatureDownTile(creature);
                    break;
            }

            if (nextTile == null || nextTile.Type == TileType.WALL)
                return false;

            //Add end game if creature

            this.GetTile(creature.GetPosition()).SetDefaultEmpty();
            nextTile.SetCreature(creature);
            return true;
        }

        public List<Tile> RaytracePlayerTiles()
        {
            List<Tile> toReturn = new List<Tile>();
            toReturn.AddRange(this.getAllTilesAroundTile(this.GetTile(this.player.GetPosition())));
            for (int i = 0; i < 12; i++)
            {
                if (i < 3)
                {
                    Tile t = this.GetTile(new Point(this.player.GetPosition().X - i, this.player.GetPosition().Y));
                    if (t != null)
                    {
                        toReturn.Add(t);
                        if (t.Type != TileType.WALL)
                            toReturn.AddRange(this.getAllTilesAroundTile(t));
                    }
                }
                if (i > 3 && i < 6)
                {
                    Tile t = this.GetTile(new Point(this.player.GetPosition().X, this.player.GetPosition().Y - (i - 3)));
                    if (t != null)
                    {
                        toReturn.Add(t);
                        if (t.Type != TileType.WALL)
                            toReturn.AddRange(this.getAllTilesAroundTile(t));
                    }
                }
                if (i > 6 && i < 9)
                {
                    Tile t = this.GetTile(new Point(this.player.GetPosition().X + (i - 6), this.player.GetPosition().Y));
                    if (t != null)
                    {
                        toReturn.Add(t);
                          if (t.Type != TileType.WALL)
                            toReturn.AddRange(this.getAllTilesAroundTile(t));
                    }
                }
                if (i > 9)
                {
                    Tile t = this.GetTile(new Point(this.player.GetPosition().X, this.player.GetPosition().Y + (i - 9)));
                    if (t != null)
                    {
                        toReturn.Add(t);
                        if (t.Type != TileType.WALL)
                            toReturn.AddRange(this.getAllTilesAroundTile(t));
                    }
                }
            }
            return toReturn;
        }

        public List<Tile> getAllTilesAroundTile(Tile tile)
        {
            List<Tile> toReturn = new List<Tile>();
            Tile left = this.getTileLeft(tile);
            if (left != null)
                toReturn.Add(left);

            Tile up = this.getTileUp(tile);
            if (up != null)
                toReturn.Add(up);

            Tile right = this.getTileRight(tile);
            if (right != null)
                toReturn.Add(right);

            Tile down = this.getTileDown(tile);
            if (down != null)
                toReturn.Add(down);

            return toReturn;
        }

        public Tile getCreatureLeftTile(Creature creature)
        {
            Point currentPos = creature.GetPosition();
            return this.GetTile(currentPos.X - 1, currentPos.Y);
        }

        public Tile getTileLeft(Tile tile)
        {
            Point currentPos = tile.Position;
            return this.GetTile(currentPos.X - 1, currentPos.Y);
        }

        public Tile getCreatureUpTile(Creature creature)
        {
            Point currentPos = creature.GetPosition();
            return this.GetTile(currentPos.X, currentPos.Y - 1);
        }

        public Tile getTileUp(Tile tile)
        {
            Point currentPos = tile.Position;
            return this.GetTile(currentPos.X, currentPos.Y - 1);
        }

        public Tile getCreatureRightTile(Creature creature)
        {
            Point currentPos = creature.GetPosition();
            return this.GetTile(currentPos.X + 1, currentPos.Y);
        }

        public Tile getTileRight(Tile tile)
        {
            Point currentPos = tile.Position;
            return this.GetTile(currentPos.X + 1, currentPos.Y);
        }

        public Tile getCreatureDownTile(Creature creature)
        {
            Point currentPos = creature.GetPosition();
            return this.GetTile(currentPos.X, currentPos.Y + 1);
        }

        public Tile getTileDown(Tile tile)
        {
            Point currentPos = tile.Position;
            return this.GetTile(currentPos.X, currentPos.Y + 1);
        }

        public Tile GetTile(int x, int y)
        {
            if (x >= 0 && y >= 0 && x <= this.GetSize().Width && y <= this.GetSize().Height)
                return this.tiles[x, y];
            else
                return null;
        }

        public Tile GetTile(Point point)
        {
            if (point.X >= 0 && point.Y >= 0 && point.X <= this.GetSize().Width && point.Y <= this.GetSize().Height)
                return this.tiles[point.X, point.Y];
            else
                return null;
        }

        public Size GetSize()
        {
            return new Size(this.tiles.GetUpperBound(0), this.tiles.GetUpperBound(1));
        }
    }
}
