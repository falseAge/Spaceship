using Microsoft.Xna.Framework;

namespace Spaceship.SpaceshipGame
{
    public class Bullet
    {
        public Vector2 position;
        public float speed = 500f;
        public bool isActive = true;
        private int radius = 5;

        public Bullet(Vector2 startPosition)
        {
            position = startPosition;
        }

        public void Update(GameTime gameTime)
        {
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
            position.X += speed * dt;

            if (position.X > 1280)
            {
                isActive = false;
            }
        }

        public bool CheckCollision(Asteroid asteroid)
        {
            int sum = radius + asteroid.radius;
            if (Vector2.Distance(position, new Vector2(asteroid.position.X - 130, asteroid.position.Y - 50)) < sum)
            {
                isActive = false;
                return true;
            }
            return false;
        }
    }
}
