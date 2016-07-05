using Catan.Constants;
using Catan.Menu;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Catan
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class GameClass : Game
    {
        private GameState gameState;
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private KeyboardState oldKBState;
        private KeyboardState newKBState;
        private SpriteFont gameFont;
        private SpriteFont menuFont;
        private IList<MenuItem> menuOptions;
        private int menuIndex;
        private Texture2D menuBackground;
        private Texture2D aboutBackground;
        private Rectangle backgroundRect;


        public GameClass()
            : base()
        {
            this.graphics = new GraphicsDeviceManager(this);
            this.Content.RootDirectory = "Content"; //directory for images
            this.graphics.PreferredBackBufferWidth = UIConstants.windowWidth;
            this.graphics.PreferredBackBufferHeight = UIConstants.windowHeight;
            this.graphics.ApplyChanges();

            this.IsMouseVisible = true;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {

            this.Services.AddService(typeof(GraphicsDeviceManager), this.graphics);
            this.spriteBatch = new SpriteBatch(this.GraphicsDevice);
            this.Services.AddService(typeof(SpriteBatch), this.spriteBatch);

            this.gameState = 0;

            this.oldKBState = Keyboard.GetState();
            this.newKBState = Keyboard.GetState();

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

            //fonts
            this.gameFont = this.Content.Load<SpriteFont>("Arial");
            this.menuFont = this.Content.Load<SpriteFont>("ArialMenu");

            //textures
            this.menuBackground = this.Content.Load<Texture2D>("menubackground");
            this.aboutBackground = this.Content.Load<Texture2D>("aboutbackground");

            //rectangles
            this.backgroundRect = new Rectangle(0, 0, UIConstants.windowWidth, UIConstants.windowHeight);

            this.menuOptions = new List<MenuItem>
          {
              new MenuItem("New game", new Vector2(UIConstants.windowWidth / 2, 270), Color.Crimson, this.Content),
              new MenuItem("About", new Vector2(UIConstants.windowWidth / 2, 320), Color.Crimson, this.Content),
              new MenuItem("Exit", new Vector2(UIConstants.windowWidth / 2, 370), Color.Crimson, this.Content)
          };

            this.menuIndex = 1;


            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }

            this.oldKBState = this.newKBState;
            this.newKBState = Keyboard.GetState();

            switch (gameState)
            {
                case GameState.Menu:
                    if (this.newKBState.IsKeyDown(Keys.Enter) && this.oldKBState.IsKeyUp(Keys.Enter))
                    {
                        this.gameState = (GameState)menuIndex;
                    }
                    if (this.newKBState.IsKeyDown(Keys.Down) && this.oldKBState.IsKeyUp(Keys.Down) && menuIndex <= 2)
                    {
                        menuIndex += 1;
                    }
                    if (this.newKBState.IsKeyDown(Keys.Up) && this.oldKBState.IsKeyUp(Keys.Up) && menuIndex >= 2)
                    {
                        menuIndex -= 1;
                    }
                    break;
                case GameState.NewGame:

                    if (this.newKBState.IsKeyDown(Keys.M) && this.oldKBState.IsKeyUp(Keys.M))
                    {
                        this.gameState = GameState.Menu;
                    }

                    //main game logic here

                    break;
                case GameState.About:

                    if (this.newKBState.IsKeyDown(Keys.M) && this.oldKBState.IsKeyUp(Keys.M))
                    {
                        this.gameState = GameState.Menu;
                    }

                    //for now an empty menu

                    break;
                case GameState.Exit:
                    Exit();
                    break;
            }

            // TODO: Add your update logic here

            base.Update(gameTime);
        }


        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            this.spriteBatch.Begin();
            // TODO: Add your drawing code here

            switch (this.gameState)
            {
                case GameState.Menu:

                    this.spriteBatch.Draw(this.menuBackground, this.backgroundRect, Color.White);
                    for (int i = 0; i < menuOptions.Count; i += 1)
                    {
                        if (i == menuIndex - 1)
                        {
                            this.menuOptions[i].UpdateColor(Color.Black);
                        }
                        else
                        {
                            this.menuOptions[i].UpdateColor(Color.Crimson);
                        }
                        this.menuOptions[i].Draw(this.spriteBatch);
                    }
                    break;
                case GameState.NewGame:
                    this.spriteBatch.DrawString(this.menuFont, "In Game", new Vector2(50), Color.White);
                    break;
                case GameState.About:
                    this.spriteBatch.Draw(this.aboutBackground, this.backgroundRect, Color.White);
                    this.spriteBatch.DrawString(this.menuFont, "About", new Vector2(UIConstants.aboutXOffset, UIConstants.aboutYOffset), Color.White);
                    this.spriteBatch.DrawString(this.gameFont, UIConstants.aboutMessage, new Vector2(UIConstants.aboutXOffset, (UIConstants.aboutXOffset + UIConstants.aboutTextYOffset * 2)), Color.White);
                    this.spriteBatch.DrawString(this.gameFont, UIConstants.aboutMessageNote, new Vector2(UIConstants.aboutXOffset, (UIConstants.aboutXOffset + UIConstants.aboutTextYOffset/2 * 7)), Color.White);
                    this.spriteBatch.DrawString(this.gameFont, UIConstants.aboutMessageUI, new Vector2(UIConstants.aboutXOffset, (UIConstants.aboutXOffset + UIConstants.aboutTextYOffset/2 * 9)), Color.White);
                    break;
                case GameState.Exit:
                    this.spriteBatch.Draw(this.menuBackground, this.backgroundRect, Color.White);
                    break;
                default:
                    break;
            }


            this.spriteBatch.End();


            base.Draw(gameTime);
        }
    }
}
