using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Spaceship.Content;

namespace Spaceship.SpaceshipGame
{
    public class Controller
    {
        public List<Asteroid> asteroids = new List<Asteroid>();
        public double timer = 2;
        public double maxTime = 2;
        public int nextSpeed = 240;
        public bool inGame = false;
        public double totalTime = 0;
        public double highScore = HighScoreManager.LoadHighScore();
        public int destroyedAsteroids = 0;

        public Controller()
        {
        }

        public void conUpdate(GameTime gameTime, Ship player)
        {
            if (inGame)
            {
                timer -= gameTime.ElapsedGameTime.TotalSeconds;
                totalTime += gameTime.ElapsedGameTime.TotalSeconds;

                if (totalTime > highScore)
                {
                    highScore = totalTime;
                    HighScoreManager.SaveHighScore(totalTime);
                }
            }
            else
            {
                KeyboardState kState = Keyboard.GetState();
                if (kState.IsKeyDown(Keys.Enter))
                {
                    inGame = true;
                    totalTime = 0;
                    timer = 2;
                    maxTime = 2;
                    nextSpeed = 240;
                }
            }

            if (timer <= 0)
            {
                asteroids.Add(new Asteroid(250));
                timer = 2;

                if (maxTime > 0.5)
                {
                    maxTime -= 0.1;
                }

                if (nextSpeed < 720)
                {
                    nextSpeed += 5;
                }
            }

            for (int i = 0; i < asteroids.Count; i++)
            {
                for (int j = 0; j < player.bullets.Count; j++)
                {
                    if (player.bullets[j].CheckCollision(asteroids[i]))
                    {
                        asteroids.RemoveAt(i);
                        player.bullets.RemoveAt(j);
                        i--;
                        destroyedAsteroids++;
                        break;
                    }
                }
            }
        }
    }
}