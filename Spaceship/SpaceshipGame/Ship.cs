using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Spaceship.SpaceshipGame
{
    public class Ship
    {
        static public Vector2 defaultPosition = new Vector2(640, 360);
        public Vector2 position = defaultPosition;
        public int speed = 180;
        public int radius = 30;
        public int health = 3;

        public List<Bullet> bullets = new List<Bullet>();
        private double shootCooldown = 0.25;
        private double shootTimer = 0;

        public Ship()
        {
        }

        public void shipUpdate(GameTime gameTime)
        {
            KeyboardState kState = Keyboard.GetState();
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (kState.IsKeyDown(Keys.LeftShift))
            {
                speed = 300;
            }
            else
            {
                speed = 180;
            }

            if (kState.IsKeyDown(Keys.Right) && position.X < 1280)
            {
                position.X += speed * dt;
            }

            if (kState.IsKeyDown(Keys.Left) && position.X > 0)
            {
                position.X -= speed * dt;
            }

            if (kState.IsKeyDown(Keys.Down) && position.Y < 720)
            {
                position.Y += speed * dt;
            }

            if (kState.IsKeyDown(Keys.Up) && position.Y > 0)
            {
                position.Y -= speed * dt;
            }

            shootTimer -= dt;

            if (kState.IsKeyDown(Keys.Space) && shootTimer <= 0)
            {
                bullets.Add(new Bullet(new Vector2(position.X, position.Y - radius)));
                shootTimer = shootCooldown;
            }

            for (int i = 0; i < bullets.Count; i++)
            {
                bullets[i].Update(gameTime);
                if (!bullets[i].isActive)
                {
                    bullets.RemoveAt(i);
                    i--;
                }
            }
        }

        private void Shoot()
        {
            bullets.Add(new Bullet(new Vector2(position.X + radius, position.Y)));
        }
    }
}