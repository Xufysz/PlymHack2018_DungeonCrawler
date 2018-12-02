using DungeonCrawler.Creatures;
using DungeonCrawler.World;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace DungeonCrawler.Creatures
{
    class Player : Creature
    {
        public Player(Point position, int health) : base(position, health)
        {
        }
    }
}
