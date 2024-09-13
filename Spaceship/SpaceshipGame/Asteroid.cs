using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Spaceship.SpaceshipGame
{
    public class Asteroid
    {
        public Vector2 position = new Vector2(600, 300);

        public int speed;
        public int radius = 59;

        public Asteroid(int speed)
        {
            Random random = new Random();
            this.speed = speed;
            position = new Vector2(1280 + radius, random.Next(0, 721));
        }

        public void asteroidUpdate(GameTime gameTime)
        {
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
            position.X -= speed * dt;
        }
    }
}