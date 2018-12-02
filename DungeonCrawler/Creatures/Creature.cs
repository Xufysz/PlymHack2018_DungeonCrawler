using DungeonCrawler.World;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace DungeonCrawler.Creatures
{
    abstract class Creature
    {
        private int health;
        private Point position;

        protected Creature(Point position, int health)
        {
            this.position = position;
            this.health = health;
        }

        public Point GetPosition()
        {
            return this.position;
        }

        public void SetPosition(Point position)
        {
            this.position = position;
        }

        public int GetHealth()
        {
            return this.health;
        }

        public void SubtractHealth(int subtract)
        {
            this.health -= subtract;
        }
    }
}
