using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Spaceship.Content;
using Spaceship.SpaceshipGame;

namespace Spaceship
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Texture2D asteroidSprite;
        private Texture2D shipSprite;
        private Texture2D spaceSprite;
        private Texture2D bulletSprite;
        private SpriteFont spaceFont;
        private SpriteFont timerFont;

        private const int Width = 1280;
        private const int Height = 720;

        SpaceshipGame.Ship player = new SpaceshipGame.Ship();
        SpaceshipGame.Controller gameController = new SpaceshipGame.Controller();

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            Window.Title = "Spaceship";

            _graphics.PreferredBackBufferWidth = Width;
            _graphics.PreferredBackBufferHeight = Height;
            _graphics.ApplyChanges();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            shipSprite = Content.Load<Texture2D>("Assets/ship");
            asteroidSprite = Content.Load<Texture2D>("Assets/asteroid");
            spaceSprite = Content.Load<Texture2D>("Assets/space");
            bulletSprite = Content.Load<Texture2D>("Assets/bullet");

            spaceFont = Content.Load<SpriteFont>("Assets/spaceFont");
            timerFont = Content.Load<SpriteFont>("Assets/timerFont");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (gameController.inGame)
            {
                player.shipUpdate(gameTime);
            }

            gameController.conUpdate(gameTime, player);

            for (int i = 0; i < gameController.asteroids.Count; i++)
            {
                gameController.asteroids[i].asteroidUpdate(gameTime);

                int sum = gameController.asteroids[i].radius + player.radius;
                if (Vector2.Distance(gameController.asteroids[i].position, player.position) < sum)
                {
                    gameController.asteroids.RemoveAt(i);
                    i--;
                    player.health--;

                    if (player.health <= 0)
                    {
                        gameController.inGame = false;
                        player.position = Ship.defaultPosition;
                        gameController.asteroids.Clear();
                        player.health = 3;
                        gameController.destroyedAsteroids = 0;
                    }
                }
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();
            _spriteBatch.Draw(spaceSprite, new Vector2(0, 0), Color.White);
            _spriteBatch.Draw(shipSprite, new Vector2(player.position.X - 30, player.position.Y - 50), Color.White);

            for (int i = 0; i < gameController.asteroids.Count; i++)
            {
                _spriteBatch.Draw(asteroidSprite, new Vector2(gameController.asteroids[i].position.X - gameController.asteroids[i].radius, gameController.asteroids[i].position.Y - gameController.asteroids[i].radius), Color.White);
            }

            // Отрисовка пуль
            foreach (var bullet in player.bullets)
            {
                _spriteBatch.Draw(bulletSprite, new Vector2(bullet.position.X - 60, bullet.position.Y - 72), Color.White);
            }

            if (gameController.inGame == false)
            {
                string menuMessage = "Press Enter to start";
                Vector2 sizeOfText = spaceFont.MeasureString(menuMessage);
                _spriteBatch.DrawString(spaceFont, menuMessage, new Vector2(_graphics.PreferredBackBufferWidth / 2 - sizeOfText.X / 2, 200), Color.White);
            }

            _spriteBatch.DrawString(timerFont, "Time: " + Math.Floor(gameController.totalTime).ToString(), new Vector2(5, 5), Color.White);
            _spriteBatch.DrawString(timerFont, "High Score: " + Math.Floor(gameController.highScore).ToString(), new Vector2(5, 35), Color.White);
            _spriteBatch.DrawString(timerFont, "Health: " + player.health.ToString(), new Vector2(5, 65), Color.White);
            _spriteBatch.DrawString(timerFont, "Asteroids: " + gameController.destroyedAsteroids.ToString(), new Vector2(5, 95), Color.White);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
