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
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private KeyboardState oldKBState;
        private SpriteFont gameFont; //to be used later
        private Texture2D frameTexture;


        private MenuChecker menus;
        private UserInterfaceElement mainMenu;
        private List<UserInterfaceElement> UIElements;

        public GameClass() : base()
        {
            this.graphics = new GraphicsDeviceManager(this);
            this.Content.RootDirectory = "Content"; //directory for images
            this.graphics.PreferredBackBufferWidth = UserInterfaceConstants.windowWidth;
            this.graphics.PreferredBackBufferWidth = UserInterfaceConstants.windowHeight;
            this.IsMouseVisible = true;

            this.UIElements = new List<UserInterfaceElement>();
            this.menus.openCount = 0;
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

          this.oldKBState = Keyboard.GetState();

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

         
          
          this.gameFont = this.Content.Load<SpriteFont>("Arial");
          this.frameTexture = this.Content.Load<Texture2D>("Frame"); //TODO: Check

            FrontMenu startMenu = new FrontMenu(this, "Catan Game", menus.OnOpen, menus.OnClose);
            startMenu.Initialize();
            startMenu.LoadContent();
            startMenu.Items.Add(new SelectableItem<string>("New Game", this.OnPlay));
            startMenu.Items.Add(new SelectableItem<string>("Exit", this.OnExit));
            this.mainMenu = new Frame(startMenu, this.frameTexture, Color.Red);
            this.UIElements.Add(this.mainMenu);

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
                Exit();
            this.mainMenu.Show();

           for (int index = 0; index < this.UIElements.Count; index++)
           {
               this.UIElements[index].Update(gameTime);
           }

           if (this.menus.AllClosed)
           {

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

            for (int index = 0; index < this.UIElements.Count; index++)
            {
                this.UIElements[index].Draw(this.spriteBatch);
            }

            this.spriteBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }

        protected void OnExit(object menuItem, EventArgs e = null)
        {
            this.Exit();
        }

        /// <summary>
        /// Continue the game
        /// </summary>
        /// <param name="menuItem">Menu item</param>
        /// <param name="e">Not used</param>
        protected void OnPlay(object menuItem, EventArgs e = null)
        { 
        
        }
    }
}
