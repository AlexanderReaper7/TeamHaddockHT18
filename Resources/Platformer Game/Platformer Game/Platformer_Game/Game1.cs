#region Information

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Generic_Platformer_Game
{
    /// <summary>
    ///     This is the main type for your game
    /// </summary>
    public class Game1 : Game
    {
        private readonly GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        #endregion

        #region Definitioner

        #region Bilder och dess positioner

        // Min brick
        private Texture2D brick;

        // Brick Position
        private readonly Vector2 brick_position = new Vector2(450, 400);

        // Spelkarakt�ren som man spelar som
        private Texture2D player_character;

        // Spelkarakt�rens position
        private Vector2 player_character_position = new Vector2(80, 620);

        // Spelkarakt�rens snabbhet
        private Vector2 player_character_speed = new Vector2(0, 0);

        // Marken som spelaren och fiender kommer att st� p�
        private Texture2D ground;

        // Markens position
        private readonly Vector2 ground_position = new Vector2(0, 680);

        // Fiende nummer ett
        private Texture2D enemy_one_bricky;

        // Fiende nummber ett position
        private readonly Vector2 enemy_one_bricky_position = new Vector2(960, 620);

        // Position f�r menyer
        private Vector2 menu_position = Vector2.Zero;

        // Min font f�r titeln p� spelet i main menu
        private SpriteFont main_menu_game_name_font;

        // Min font f�r Play Game, Credits och Exit Game
        private SpriteFont main_menu_font;

        // Positionen f�r main menu game name texten
        private readonly Vector2 main_menu_game_name_position = new Vector2(1920 / 2, 1080 / 6);

        // Positionen f�r main menu play game texten
        private readonly Vector2 main_menu_text_play_game_position = new Vector2(1920 / 2, 1080 / 4 * 2);

        // Positionen f�r main menu credits texten
        private readonly Vector2 main_menu_text_credits_position = new Vector2(1920 / 2, 1080 / 4 * 3.5f);

        // Positionen f�r main menu exit game texten
        private readonly Vector2 main_menu_text_exit_game_position = new Vector2(1920 / 2, 1080 / 4 * 5);

        // Positionen f�r choose a level level one texten
        private readonly Vector2 choose_a_level_text_level_one_position = new Vector2(1920 / 6 * 1, 1080 / 2);

        // Positionen f�r choose a level level two texten
        private readonly Vector2 choose_a_level_text_level_two_position = new Vector2(1920 / 6 * 2.5f, 1080 / 2);

        // Positionen f�r choose a level level three texten
        private readonly Vector2 choose_a_level_text_level_three_position = new Vector2(1920 / 6 * 4, 1080 / 2);

        #endregion

        #region Ints, Bools, Floats & Annat

        // Detta anv�nds s� att man inte kan zooma igenom menyerna
        private int menu_delay = 250;

        //int[] test = new int[5] { 1, 2, 3, 4, 5 };
        // Detta anv�nds s� att man kan scrolla i main menu f�r att v�lja olika saker, som credits, play game, exit game osv.   
        private int menu_scrolling;

        // Detta anv�nds s� att man kan scrolla i "Choose A Level" f�r att v�lja olika saker, som level 1, level 2, level 3 osv 
        private int choose_a_level_scrolling;

        // Generic Platformer Game texten i main menu
        private readonly string main_menu_game_name_text = "Generic Platformer Game";

        // Play Game texten i main menu
        private readonly string play_game = "Play Game";

        // Credits texten i main menu
        private readonly string credits = "Credits";

        // Exit game texten i main menu
        private readonly string exit_game = "Exit Game";

        // Choose a level texten i choose a level menyn
        private readonly string choose_a_level_text = "Choose A Level";

        // Level one texten i choose a level
        private readonly string choose_a_level_level_one_text = "Level One";

        // Level two texten i choose a level
        private readonly string choose_a_level_level_two_text = "Level Two";

        // Level three texten i choose a level
        private readonly string choose_a_level_level_three_text = "Level Three";

        // Detta g�r s� att jag kan f� min karakt�r att g� snabbare med tiden
        private int player_sprint_speed = 1;

        // Detta g�r s� att min karakt�rs hopp ser b�ttre ut
        private float player_jump_and_fall_speed = 750f;

        // Dessa tv� anv�nds f�r att g�ra hopp koden, den f�rsta anv�nds f�r att starta hoppkoden, och den andra anv�nds f�r att starta den andra delen av hoppkoden, att falla ner
        private bool player_is_jumping;

        private bool player_is_falling;

        // Alla dessa anv�nds f�r att n�got ska h�nda n�r man n�r kanten p� ett ut av dessa saker
        private bool wall_collision_up = false;
        private bool wall_collision_down = false;
        private bool wall_collision_left;

        private bool wall_collision_right = false;

        // Dessa tv� anv�nds f�r att g�ra s� att jag kan resetta player_sprint_speed
        private bool key_right;
        private bool key_left;

        #endregion

        #region GameStates

        // Mina olika gamestates som kan anv�ndas f�r att komma igenom olika menyer
        private enum GameStates
        {
            main_menu,
            choose_a_level,
            credits,
            level_one,
            level_two,
            level_three
        }

        // GameStates defineras som gameState och spelet b�rjar i GameStates.main_menu
        private GameStates gameState = GameStates.level_one;

        #endregion

        #endregion

        #region Information

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        ///     Allows the game to perform any initialization it needs to before starting to run.
        ///     This is where it can query for any required services and load any non-graphic
        ///     related content.  Calling base.Initialize will enumerate through any components
        ///     and initialize them as well.
        /// </summary>

        #endregion

        #region F�nster storleken f�r mitt spel
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            // S�tter spelets borders till 1920 (height) och 1080, (width) vilket �r min monitors resolution. 
            graphics.PreferredBackBufferWidth = 1920;
            graphics.PreferredBackBufferHeight = 1080;
            graphics.ApplyChanges();

            base.Initialize();
        }

        #endregion

        #region Information

        /// <summary>
        ///     LoadContent will be called once per game and is the place to load
        ///     all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            #endregion

            #region Bilduppladning

            // Alla Bilder
            // All Text
            main_menu_game_name_font = Content.Load<SpriteFont>(@"Alla Bilder/All Text/Main menu game name");
            main_menu_font = Content.Load<SpriteFont>(@"Alla Bilder/All Text/Main menu text");
            // Alla Bakgrundsbilder
            // Alla Karakt�rer
            enemy_one_bricky = Content.Load<Texture2D>(@"Alla Bilder/Alla Karakt�rer/Enemy One (Bricky)");
            player_character = Content.Load<Texture2D>(@"Alla Bilder/Alla Karakt�rer/Player Character");
            // Alla Objekt
            brick = Content.Load<Texture2D>(@"Alla Bilder/Alla Objekt/Brick");
            ground = Content.Load<Texture2D>(@"Alla Bilder/Alla Objekt/Ground");

            #endregion

            #region Information

        }

        /// <summary>
        ///     UnloadContent will be called once per game and is the place to unload
        ///     all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        ///     Allows the game to run logic such as updating the world,
        ///     checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {

            #endregion

            #region Update

            #region St�ng ner spelet och Fullscreen kod

            // Tryck p� Back eller Esc f�r att st�nga ner spelet
            var gamePad = GamePad.GetState(PlayerIndex.One);
            var keyboard = Keyboard.GetState();
            // Back eller escape quittar spelet
            if (keyboard.IsKeyDown(Keys.F1))
                Exit();
            // Tryck p� F f�r att g� Full-Screen
            if (keyboard.IsKeyDown(Keys.F))
            {
                graphics.IsFullScreen = !graphics.IsFullScreen;
                graphics.ApplyChanges();
            }

            #endregion

            #region Saker som defineras och saker som g�r hela tiden

            menu_delay += gameTime.ElapsedGameTime.Milliseconds;
            player_character_speed += player_character_position;
            // Spelkarakt�rn som ett objekt
            var player_character_object = new Rectangle((int) player_character_position.X,
                (int) player_character_position.Y, player_character.Width, player_character.Height);
            // Fiende nummber ett som ett objekt
            var enemy_one_bricky_object = new Rectangle((int) enemy_one_bricky_position.X,
                (int) enemy_one_bricky_position.Y, enemy_one_bricky.Width, enemy_one_bricky.Height);
            // Marken som ett objekt 
            var ground_object = new Rectangle((int) ground_position.X, (int) ground_position.Y, ground.Width,
                ground.Height);

            #endregion

            #region Alla de olika menyerna, gameStatesen 

            // Fr�ga mattias varf�r inte det fungerade n�r jag gjorde att n�r du trycker p� en knapp (t.ex upp) och det blev += 1 menu_scroll, varf�r det glitchade och varf�r det inte fungerade
            switch (gameState)
            {
                #region Main Menu

                case GameStates.main_menu:
                    if (keyboard.IsKeyDown(Keys.Up) && menu_delay >= 250 && menu_scrolling == 0)
                    {
                        menu_scrolling = 2;
                        menu_delay = 0;
                    }

                    if (keyboard.IsKeyDown(Keys.Up) && menu_delay >= 250 && menu_scrolling == 1)
                    {
                        menu_scrolling = 0;
                        menu_delay = 0;
                    }

                    if (keyboard.IsKeyDown(Keys.Up) && menu_delay >= 250 && menu_scrolling == 2)
                    {
                        menu_scrolling = 1;
                        menu_delay = 0;
                    }

                    if (keyboard.IsKeyDown(Keys.Down) && menu_delay >= 250 && menu_scrolling == 2)
                    {
                        menu_scrolling = 0;
                        menu_delay = 0;
                    }

                    if (keyboard.IsKeyDown(Keys.Down) && menu_delay >= 250 && menu_scrolling == 1)
                    {
                        menu_scrolling = 2;
                        menu_delay = 0;
                    }

                    if (keyboard.IsKeyDown(Keys.Down) && menu_delay >= 250 && menu_scrolling == 0)
                    {
                        menu_scrolling = 1;
                        menu_delay = 0;
                    }

                    if (keyboard.IsKeyDown(Keys.Enter) && menu_delay >= 250 && menu_scrolling == 0)
                    {
                        gameState = GameStates.choose_a_level;
                        menu_delay = 0;
                    }

                    if (keyboard.IsKeyDown(Keys.Enter) && menu_delay >= 250 && menu_scrolling == 1)
                    {
                        gameState = GameStates.credits;
                        menu_delay = 0;
                    }

                    if (keyboard.IsKeyDown(Keys.Enter) && menu_delay >= 250 && menu_scrolling == 2) Exit();
                    break;

                #endregion

                #region Choose A Level

                case GameStates.choose_a_level:
                    if (keyboard.IsKeyDown(Keys.Left) && menu_delay >= 250 && choose_a_level_scrolling == 0)
                    {
                        choose_a_level_scrolling = 2;
                        menu_delay = 0;
                    }

                    if (keyboard.IsKeyDown(Keys.Left) && menu_delay >= 250 && choose_a_level_scrolling == 1)
                    {
                        choose_a_level_scrolling = 0;
                        menu_delay = 0;
                    }

                    if (keyboard.IsKeyDown(Keys.Left) && menu_delay >= 250 && choose_a_level_scrolling == 2)
                    {
                        choose_a_level_scrolling = 1;
                        menu_delay = 0;
                    }

                    if (keyboard.IsKeyDown(Keys.Right) && menu_delay >= 250 && choose_a_level_scrolling == 2)
                    {
                        choose_a_level_scrolling = 0;
                        menu_delay = 0;
                    }

                    if (keyboard.IsKeyDown(Keys.Right) && menu_delay >= 250 && choose_a_level_scrolling == 1)
                    {
                        choose_a_level_scrolling = 2;
                        menu_delay = 0;
                    }

                    if (keyboard.IsKeyDown(Keys.Right) && menu_delay >= 250 && choose_a_level_scrolling == 0)
                    {
                        choose_a_level_scrolling = 1;
                        menu_delay = 0;
                    }

                    if (keyboard.IsKeyDown(Keys.Enter) && menu_delay >= 250 && choose_a_level_scrolling == 0)
                    {
                        gameState = GameStates.level_one;
                        menu_delay = 0;
                    }

                    if (keyboard.IsKeyDown(Keys.Enter) && menu_delay >= 250 && choose_a_level_scrolling == 1)
                    {
                        gameState = GameStates.level_two;
                        menu_delay = 0;
                    }

                    if (keyboard.IsKeyDown(Keys.Enter) && menu_delay >= 250 && choose_a_level_scrolling == 2)
                    {
                        gameState = GameStates.level_three;
                        menu_delay = 0;
                    }

                    if (keyboard.IsKeyDown(Keys.Escape) && menu_delay >= 250)
                    {
                        gameState = GameStates.main_menu;
                        menu_delay = 0;
                    }

                    break;

                #endregion

                #region Credits

                case GameStates.credits:
                    if (keyboard.IsKeyDown(Keys.Escape) && menu_delay >= 250) gameState = GameStates.main_menu;
                    break;

                #endregion

                #region Level One

                case GameStates.level_one:

                    #region Definitioner i level one

                        // Anv�nds f�r att min karakt�r inte ska ramla utanf�r sk�rmen
                        var max_x_player = graphics.GraphicsDevice.Viewport.Width - player_character.Width;
                        var max_y_player = graphics.GraphicsDevice.Viewport.Height - player_character.Height;
                        var min_x_player = 0;
                        var min_y_player = 0;

                        #endregion

                        if (keyboard.IsKeyDown(Keys.Escape) && menu_delay >= 250)
                        {
                            gameState = GameStates.choose_a_level;
                            menu_delay = 0;
                        }

                        if (player_sprint_speed <= 10) player_sprint_speed += gameTime.ElapsedGameTime.Milliseconds / 100;

                        #region Min Spelkarakt�r

                        // G�r s� att om min karakt�r g�r till v�nster sidan utav sk�rmen, att g� v�nster koden slutar att fungera
                        if (player_character_position.X < min_x_player)
                            wall_collision_left = true;
                        else
                            wall_collision_left = false;

                        // G�r s� att min karakt�r kan g� v�nster
                        if (keyboard.IsKeyDown(Keys.Left) && wall_collision_left == false)
                        {
                            player_character_position.X -= 5 * player_sprint_speed / 500;
                            key_left = true;
                            if (player_sprint_speed < 500) player_sprint_speed += gameTime.ElapsedGameTime.Milliseconds;
                        }

                        // Detta g�r s� att jag kan resetta player_sprint_speed 
                        if (keyboard.IsKeyUp(Keys.Left) && key_right == false) player_sprint_speed = 1;
                        // G�r s� att min karakt�r kan g� h�ger
                        if (keyboard.IsKeyDown(Keys.Right))
                        {
                            player_character_position.X += 5 * player_sprint_speed / 500;
                            key_right = true;
                            if (player_sprint_speed < 500) player_sprint_speed += gameTime.ElapsedGameTime.Milliseconds;
                        }

                        // Detta g�r s� att jag kan resetta player_sprint_speed
                        if (keyboard.IsKeyUp(Keys.Right) && key_left == false) player_sprint_speed = 1;


                        // Detta �r gravitationen f�r mitt spel
                        if (player_is_falling && player_jump_and_fall_speed <= 0)
                            player_character_position.Y -= 10 * player_jump_and_fall_speed / 285;
                        if (player_jump_and_fall_speed <= 0)
                            {
                                player_is_falling = true;
                                player_is_jumping = false;
                            }

                        // G�r s� att min karakt�r kan hoppa
                        if (keyboard.IsKeyDown(Keys.Space)) player_is_jumping = true;
                        // Om du �r p� plattformen och trycker p� space s� hoppar du i jump_duration sekunder 
                        if (player_is_jumping && player_jump_and_fall_speed > 0)
                            player_character_position.Y -= 10 * player_jump_and_fall_speed / 285;
                        if (player_character_object.Intersects(ground_object))
                        {
                            player_jump_and_fall_speed = 750f;
                            player_is_falling = false;
                        }

                        if (player_is_jumping || player_is_falling)
                            player_jump_and_fall_speed -= gameTime.ElapsedGameTime.Milliseconds * 1.5f;
                        // Detta anv�nds f�r att kameran f�r spelet ska flytta p� sig om spelaren �r i mitten av sk�rmen
                        //if (player_character_position.X >= max_x_player)

                        #endregion



                    break;

                #endregion

                #region Level Two

                case GameStates.level_two:
                    if (keyboard.IsKeyDown(Keys.Escape) && menu_delay >= 250)
                    {
                        gameState = GameStates.choose_a_level;
                        menu_delay = 0;
                    }

                    break;

                #endregion

                #region Level Three

                case GameStates.level_three:
                    if (keyboard.IsKeyDown(Keys.Escape) && menu_delay >= 250)
                    {
                        gameState = GameStates.choose_a_level;
                        menu_delay = 0;
                    }

                    break;

                #endregion
            }

            #endregion


            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        #endregion

        #region Information

        /// <summary>
        ///     This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>

        #endregion
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.LightGray);

            // TODO: Add your drawing code here
            spriteBatch.Begin();

            #region Bild utritning

            #region Main menu

            if (gameState == GameStates.main_menu)
                // Allting i main menu
                // Om menu scroll �r ett nummer, s� ska det numret bli vitt ist�llet f�r svart
            {
                spriteBatch.DrawString(main_menu_game_name_font, main_menu_game_name_text,
                    main_menu_game_name_position - main_menu_game_name_position / 2, Color.Black);
                if (menu_scrolling == 0)
                    spriteBatch.DrawString(main_menu_font, play_game,
                        main_menu_text_play_game_position - main_menu_text_play_game_position / 2, Color.White);
                else
                    spriteBatch.DrawString(main_menu_font, play_game,
                        main_menu_text_play_game_position - main_menu_text_play_game_position / 2, Color.Black);

                if (menu_scrolling == 1)
                    spriteBatch.DrawString(main_menu_font, credits,
                        main_menu_text_credits_position - main_menu_text_credits_position / 2, Color.White);
                else
                    spriteBatch.DrawString(main_menu_font, credits,
                        main_menu_text_credits_position - main_menu_text_credits_position / 2, Color.Black);

                if (menu_scrolling == 2)
                    spriteBatch.DrawString(main_menu_font, exit_game,
                        main_menu_text_exit_game_position - main_menu_text_exit_game_position / 2, Color.White);
                else
                    spriteBatch.DrawString(main_menu_font, exit_game,
                        main_menu_text_exit_game_position - main_menu_text_exit_game_position / 2, Color.Black);
            }

            #endregion

            #region Choose a level

            if (gameState == GameStates.choose_a_level)
            {
                GraphicsDevice.Clear(Color.Red);
                spriteBatch.DrawString(main_menu_game_name_font, choose_a_level_text,
                    main_menu_game_name_position / 1.5f, Color.Black);
                if (choose_a_level_scrolling == 0)
                    spriteBatch.DrawString(main_menu_font, choose_a_level_level_one_text,
                        choose_a_level_text_level_one_position, Color.White);
                else
                    spriteBatch.DrawString(main_menu_font, choose_a_level_level_one_text,
                        choose_a_level_text_level_one_position, Color.Black);

                if (choose_a_level_scrolling == 1)
                    spriteBatch.DrawString(main_menu_font, choose_a_level_level_two_text,
                        choose_a_level_text_level_two_position, Color.White);
                else
                    spriteBatch.DrawString(main_menu_font, choose_a_level_level_two_text,
                        choose_a_level_text_level_two_position, Color.Black);

                if (choose_a_level_scrolling == 2)
                    spriteBatch.DrawString(main_menu_font, choose_a_level_level_three_text,
                        choose_a_level_text_level_three_position, Color.White);
                else
                    spriteBatch.DrawString(main_menu_font, choose_a_level_level_three_text,
                        choose_a_level_text_level_three_position, Color.Black);
            }

            #endregion

            #region Credits

            if (gameState == GameStates.credits) GraphicsDevice.Clear(Color.Green);

            #endregion

            #region Level One

            if (gameState == GameStates.level_one)
            {
                GraphicsDevice.Clear(Color.LightGray);
                spriteBatch.Draw(brick, brick_position, Color.White);
                spriteBatch.Draw(player_character, player_character_position, Color.White);
                spriteBatch.Draw(ground, ground_position, Color.White);
            }

            #endregion

            #region Level Two

            if (gameState == GameStates.level_two) GraphicsDevice.Clear(Color.Blue);

            #endregion

            #region Level Three

            if (gameState == GameStates.level_three) GraphicsDevice.Clear(Color.Purple);

            #endregion

            #region spriteBatch.End();

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}

#endregion

#endregion