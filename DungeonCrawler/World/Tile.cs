using DungeonCrawler.Creatures;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace DungeonCrawler.World
{
    class Tile
    {
        public TileType Type { get; private set; }
        public Point Position { get; private set; }
        private Creature creatureOnTile;

        public Tile(Point position, TileType type)
        {
            this.Position = position;
            this.Type = type;
        }

        public Creature GetCreature()
        {
            if (this.Type == TileType.CREATURE)
                return this.creatureOnTile;
            else
                throw new TypeAccessException();
        }

        public void SetCreature(Creature creature)
        {
            if (this.creatureOnTile != null)
                throw new Exception("Tile occupied already!");

            if (!creature.GetPosition().Equals(this.Position))
            {
                creature.SetPosition(this.Position);
            }
            
            this.creatureOnTile = creature;
            this.Type = TileType.CREATURE;
        }

        public void SetDefaultEmpty()
        {
            this.Type = TileType.EMPTY;
            this.creatureOnTile = null;
        }

        public enum TileType
        {
            EMPTY,
            WALL,
            CREATURE,
            ITEM,
        }

        public enum Direction
        {
            LEFT,
            UP,
            RIGHT,
            DOWN,
            NONE,
        }
    }
}
