using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

// Class created by Alexander 11-07
namespace TeamHaddock
{
    /// <summary>
    ///     Draws and does logic for the gameState mainMenu
    /// </summary>
    internal static class MainMenu
    {

        /// <summary>
        ///     String array of menu option names
        /// </summary>
        private static readonly string[] MenuOptionsStr = {"Play", "Tutorial", "High Score", "Credits", "Exit"};

        /// <summary>
        ///     selected menu option
        /// </summary>
        private static Vector2 selected;

        /// <summary>
        ///     MainMenu background image
        /// </summary>
        public static Texture2D Background;

        /// <summary>
        ///     Controls keyboard actions in menus
        /// </summary>
        private static readonly MenuControls MenuControl = new MenuControls(new Vector2(0, MenuOptionsStr.Length - 1));

        /// <summary>
        /// Load MainMenu Textures, e.g the background
        /// </summary>
        /// <param name="content"></param>
        public static void LoadContent(ContentManager content)
        {

            Background = content.Load<Texture2D>(@"Textures/MainMenuBG");
        }

        /// <summary>
        ///     Updates MainMenu gamestate logic
        /// </summary>
        public static void Update()
        {
            MenuControl.UpdateSelected(ref selected); // Updates selected menu option

            // If enter is not pressed
            if (!MenuControl.IsEnterDown)
            {
                // Then return
                return;
            }
            // Else (enter is pressed) Then change gamestate and playstate
            switch ((int) selected.Y)
            {
                // Play
                case 0:
                    Game1.GameState = Game1.GameStates.InGame;
                    InGame.playState = InGame.PlayStates.Normal;
                    break;

                // Tutorial
                case 1:
                    Game1.GameState = Game1.GameStates.InGame;
                    InGame.playState = InGame.PlayStates.Tutorial;
                    break;

                // HighScore 
                case 2:
                    Game1.GameState = Game1.GameStates.HighScore;
                    break;
                // Credits
                case 3:
                    Game1.GameState = Game1.GameStates.Credits;
                    break;
                // Exit
                case 4:
                    Game1.GameState = Game1.GameStates.Exit;
                    break;
                
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        /// <summary>
        ///     Draws the MainMenu gamestate
        /// </summary>
        /// <param name="spriteBatch">Enables a group of sprites to be drawn using the same settings.</param>
        public static void Draw(SpriteBatch spriteBatch)
        {
            // Draw background in whole window
            spriteBatch.Draw(Background, new Rectangle(0, 0, Game1.ScreenBounds.X, Game1.ScreenBounds.Y), Color.White);

            // Iterate through every entry in menuOptionsStr array
            for (int i = 0; i < MenuOptionsStr.Length; i++)
            {
                // If selected menu option is int i have bold font else normal font
                spriteBatch.DrawString((int)selected.Y == i ? Game1.BoldMenuFont : Game1.NormalMenuFont, MenuOptionsStr[i], new Vector2(10, 40 * i), Color.Black);
            }
        }
    }
}