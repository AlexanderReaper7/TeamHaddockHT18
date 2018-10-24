using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Missile_Master_2
{
    /// <summary>
    ///     Draws and does logic for the gamestate mainMenu
    /// </summary>
    internal static class MainMenu
    {

        /// <summary>
        ///     String array of menu option names
        /// </summary>
        private static readonly string[] MenuOptionsStr = {"Campaign", "Level Select", "Exit"};

        /// <summary>
        ///     Vector2 array of menu options positions
        /// </summary>
        private static readonly Vector2[] MenuOptionsPos = {new Vector2(Game1.ScreenBounds.X / 8, 20), new Vector2(Game1.ScreenBounds.X / 8, 60), new Vector2(Game1.ScreenBounds.X / 8, 100)};

        /// <summary>
        ///     selected menu option
        /// </summary>
        private static Vector2 _selected;

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
            Background = content.Load<Texture2D>(@"images/MainMenuBG");
        }

        /// <summary>
        ///     Updates MainMenu gamestate logic
        /// </summary>
        public static void Update()
        {
            _selected = MenuControl.Update(); // Updates selected menu option

            // If enter is not pressed
            if (!MenuControl.IsEnterDown)
            {
                // Then return
                return;
            }
            // Else (enter is pressed) switch gamestate to
            switch ((int) _selected.Y)
            {
                // Campaign
                case 0:
                    Game1.GameState = Game1.GameStates.Campaign;
                    break;

                // Level select
                case 1:
                    Game1.GameState = Game1.GameStates.LevelSelect;
                    break;

                // Exit
                case 2:
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
                // If selected menu option is int i
                spriteBatch.DrawString((int)_selected.Y == i ? Game1.PixelArt32Bold : Game1.PixelArt32Normal, MenuOptionsStr[i], MenuOptionsPos[i], Color.Black);
            }
        }
    }
}