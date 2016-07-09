using Catan.Constants;
using Catan.Dices;
using Catan.Menu;
using Catan.Common;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;

namespace Catan
{

    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class GameClass : Game
    {
        private static GameClass game;

        public static GameClass Game
        {
            get
            {
                if (game == null)
                {
                    game = new GameClass();
                }
                return game;
            }
        }
        //general
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private Song backgroundMusic;

        private GameState gameState;

        //controllers states
        private KeyboardState oldKBState;
        private KeyboardState newKBState;
        private MouseState newMouseState;
        private MouseState oldMouseState;

        //fonts
        private SpriteFont gameFont;
        private SpriteFont menuFont;

        //menu related
        private IList<MenuItem> menuOptions;
        private Texture2D menuBackground;
        private int menuIndex;

        //game related
        private Texture2D gameBackground;
        private Dice dices;
        private ScoreBoard scoreBoard;
        public IList<Player> players;
        public Player playerOnTurn;
        private Texture2D villageButton;
        private Texture2D townButton;
        private Texture2D roadButton;
        private Texture2D boatButton;
        private Rectangle villageRect;
        private Rectangle townRect;
        private Rectangle roadRect;
        private Rectangle boatRect;

        //other
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

        // properties

        public int Players
        {
            get { return Players; }
            set { Players = value; }
        }
        public Player PlayerOnTurn { get; set; }


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
            this.oldMouseState = Mouse.GetState();
            this.newMouseState = Mouse.GetState();


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

            // Background music
            backgroundMusic = Content.Load<Song>("Sounds/FloatingCities");
            MediaPlayer.Play(backgroundMusic);

            //fonts
            this.gameFont = this.Content.Load<SpriteFont>("Arial");
            this.menuFont = this.Content.Load<SpriteFont>("ArialMenu");

            //textures
            this.menuBackground = this.Content.Load<Texture2D>("menubackground");
            this.aboutBackground = this.Content.Load<Texture2D>("aboutbackground");
            this.gameBackground = this.Content.Load<Texture2D>("gamebackground");
            this.villageButton = this.Content.Load<Texture2D>("village");
            this.townButton = this.Content.Load<Texture2D>("town");
            this.roadButton = this.Content.Load<Texture2D>("road");
            this.boatButton = this.Content.Load<Texture2D>("boat");

            //rectangles
            this.backgroundRect = new Rectangle(0, 0, UIConstants.windowWidth, UIConstants.windowHeight);
            this.villageRect = new Rectangle(10, 300, 40, 40);
            this.townRect = new Rectangle(10, 350, 40, 40);
            this.roadRect = new Rectangle(10, 400, 40, 40);
            this.boatRect = new Rectangle(10, 450, 40, 40);

            //Load players
            this.players = new List<Player>{
                new Player("Player 1", Color.Red, this.Content, 0, 500, 430, 100),
                new Player("Player 2", Color.Blue, this.Content, 0, 500, 430, 100),
                new Player("Player 3", Color.Green, this.Content, 0, 500, 430, 100),
                new Player("Player 4", Color.Yellow, this.Content, 0, 500, 430, 100)
            };

            //objects
            this.dices = new Dice(this.Content, "dice1", 670, 530, 110, 50);
            this.dices.Roll();
            this.scoreBoard = new ScoreBoard(this.players, this.Content, "scoreboard", 0, 0, 105, 100);

            //FOR TEST ONLY: REMOVE LATER
            this.players[0].SetResourceValue(ResourceType.Grain, 4);
            this.players[1].SetResourceValue(ResourceType.Iron, 3);
            this.players[2].SetResourceValue(ResourceType.Lumber, 2);
            this.players[3].SetResourceValue(ResourceType.Brick, 8);

            this.playerOnTurn = this.players[0];

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
            this.oldMouseState = this.newMouseState;
            this.newMouseState = Mouse.GetState();

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



                    if (this.dices.Rectangle.Contains(Mouse.GetState().X, Mouse.GetState().Y) && this.newMouseState.LeftButton == ButtonState.Pressed && this.oldMouseState.LeftButton == ButtonState.Released)
                    {
                        this.dices.Roll();
                        this.playerOnTurn = this.players[this.playerOnTurn.Id % 4];
                        this.playerOnTurn.AddPoints(2);
                        this.scoreBoard.Update(this.players);
                    }

                    //main game logic here
                    //TODO: make functionality on buttons click

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
                    this.spriteBatch.Draw(this.gameBackground, this.backgroundRect, Color.White);
                    this.dices.Draw(this.spriteBatch);
                    this.scoreBoard.Draw(this.spriteBatch);
                    //buttons
                    this.spriteBatch.Draw(this.villageButton, this.villageRect, Color.White);
                    this.spriteBatch.Draw(this.townButton, this.townRect, Color.White);
                    this.spriteBatch.Draw(this.roadButton, this.roadRect, Color.White);
                    this.spriteBatch.Draw(this.boatButton, this.boatRect, Color.White);
                    //rest
                    this.playerOnTurn.Draw(this.spriteBatch);
                    this.spriteBatch.DrawString(this.menuFont, this.playerOnTurn.UserName + "'s Turn", new Vector2(270, 10), Color.White);

                    break;
                case GameState.About:
                    this.spriteBatch.Draw(this.aboutBackground, this.backgroundRect, Color.White);
                    this.spriteBatch.DrawString(this.menuFont, "About", new Vector2(UIConstants.aboutXOffset, UIConstants.aboutYOffset), Color.White);
                    this.spriteBatch.DrawString(this.gameFont, UIConstants.aboutMessage, new Vector2(UIConstants.aboutXOffset, (UIConstants.aboutXOffset + UIConstants.aboutTextYOffset * 2)), Color.White);
                    this.spriteBatch.DrawString(this.gameFont, UIConstants.aboutMessageNote, new Vector2(UIConstants.aboutXOffset, (UIConstants.aboutXOffset + UIConstants.aboutTextYOffset / 2 * 7)), Color.White);
                    this.spriteBatch.DrawString(this.gameFont, UIConstants.aboutMessageUI, new Vector2(UIConstants.aboutXOffset, (UIConstants.aboutXOffset + UIConstants.aboutTextYOffset / 2 * 9)), Color.White);
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
