using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

// Class created by Alexander 11-07
namespace TeamHaddock
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        /// <summary>
        /// List of menu states
        /// </summary>
        public enum GameStates
        {
            MainMenu,
            InGame,
            HighScore,
            // Options, // Might add later
            Credits,
            Exit
        }
        /// <summary>
        /// Current GameState
        /// </summary>
        public static GameStates GameState = GameStates.InGame;

        /// <summary>
        /// Size of window
        /// </summary>
        public static readonly Point ScreenBounds = new Point(1280, 720);

        // TODO : Add these fonts
        public static SpriteFont NormalMenuFont;
        public static SpriteFont BoldMenuFont;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // Set window size to ScreenBounds
            graphics.PreferredBackBufferWidth = ScreenBounds.X;
            graphics.PreferredBackBufferHeight = ScreenBounds.Y;
            graphics.ApplyChanges();

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            NormalMenuFont = Content.Load<SpriteFont>(@"Fonts/NormalMenuFont");
            BoldMenuFont = Content.Load<SpriteFont>(@"Fonts/BoldMenuFont");

            MainMenu.LoadContent(Content);
            InGame.LoadContent(Content);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || UtilityClass.SingleActivationKey(Keys.End)) this.Exit();
                
            UtilityClass.Update();

            switch (GameState)
            {
                case GameStates.MainMenu:
                    MainMenu.Update();
                    break;
                case GameStates.InGame:
                    InGame.Update(gameTime);
                    break;
                case GameStates.HighScore:
                    HighScore.Update();
                    break;
                case GameStates.Credits:
                    break;
                case GameStates.Exit:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            switch (GameState)
            {
                case GameStates.MainMenu:
                    MainMenu.Draw(spriteBatch);
                    break;
                case GameStates.InGame:
                    InGame.Draw(spriteBatch);
                    break;
                case GameStates.HighScore:
                    HighScore.Draw(spriteBatch);
                    break;
                case GameStates.Credits:
                    break;
                case GameStates.Exit:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
