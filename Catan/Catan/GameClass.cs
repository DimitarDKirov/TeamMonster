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
using Catan.GameObjects;
using Catan.DevelopmentCards;

using Microsoft.Xna.Framework.Content;
using Catan.Interfaces;

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
        public IPlayer winner;
        private Texture2D villageButton;
        private Texture2D townButton;
        private Texture2D roadButton;
        private Texture2D boatButton;
        private Rectangle villageRect;
        private Rectangle townRect;
        private Rectangle roadRect;
        private Rectangle boatRect;

        private Settlement[,] settlements;
        private LineObject[,] roadsAndboats;

        private Queue<DevelopmentCard> developmentCards;


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

        public IList<Player> Players
        {
            get { return this.players; }
            set { this.players = value; }
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

            //Load Developemnt cards
            this.developmentCards = new Queue<DevelopmentCard>(CommonConstants.DevelopmentCardsNumber);
            this.LoadDevelopmentCardRepository();

            //subscribe to Player wins event
            foreach (var player in this.players)
            {
                player.WinPointsReached += this.PlayerWins;
            }
            //objects
            this.dices = new Dice(this.Content, "dice1", 670, 530, 110, 50);
            this.dices.Roll();
            this.scoreBoard = new ScoreBoard(this.players, this.Content, "scoreboard", 0, 0, 105, 100);

            //
            this.settlements = new Settlement[20, 9];
            this.roadsAndboats = new LineObject[20, 17];

            //Load Content
            GameClass.LoadSettlements(this.settlements, this.Content);
            GameClass.LoadRoadsAndBoats(this.roadsAndboats, this.Content);

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

                    //Draw Roads
                    for (int i = 0; i < 20; i++)
                    {
                        for (int j = 0; j < 17; j++)
                        {
                            if (roadsAndboats[i, j] != null)
                            {
                                roadsAndboats[i, j].Draw(this.spriteBatch);
                            }
                        }
                    }

                    //Draw Settlements
                    for (int i = 0; i < 20; i++)
                    {
                        for (int j = 0; j < 9; j++)
                        {
                            if (settlements[i, j] != null)
                            {
                                settlements[i, j].Draw(this.spriteBatch);
                            }
                        }
                    }

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
                case GameState.Win:
                    this.spriteBatch.DrawString(this.menuFont, this.winner.UserName + " WINS!!!", new Vector2(UIConstants.aboutXOffset, UIConstants.aboutYOffset), Color.White);
                    break;
                default:
                    break;
            }


            this.spriteBatch.End();


            base.Draw(gameTime);
        }


        //customMethods
        private static void LoadRoadsAndBoats(LineObject[,] lineObject, ContentManager content)
        {
            lineObject[7, 3] = new LineObject(7, 1, 8, 1, 0, content, "transperent", 290, 110, 20, 30);
            lineObject[8, 3] = new LineObject(8, 1, 9, 1, 0, content, "transperent", 330, 110, 20, 30);
            lineObject[9, 3] = new LineObject(9, 1, 10, 1, 0, content, "transperent", 370, 110, 20, 30);
            lineObject[10, 3] = new LineObject(10, 1, 11, 1, 0, content, "transperent", 410, 110, 20, 30);
            lineObject[11, 3] = new LineObject(11, 1, 12, 1, 0, content, "transperent", 450, 110, 20, 30);
            lineObject[12, 3] = new LineObject(12, 1, 13, 1, 0, content, "transperent", 490, 110, 20, 30);

            lineObject[7, 4] = new LineObject(7, 1, 7, 2, 0, content, "transperent", 270, 145, 20, 30);
            lineObject[9, 4] = new LineObject(9, 1, 9, 2, 0, content, "transperent", 350, 145, 20, 30);
            lineObject[11, 4] = new LineObject(11, 1, 11, 2, 0, content, "transperent", 430, 145, 20, 30);
            lineObject[13, 4] = new LineObject(13, 1, 13, 2, 0, content, "transperent", 510, 145, 20, 30);

            lineObject[6, 5] = new LineObject(6, 2, 7, 2, 0, content, "transperent", 250, 180, 20, 30);
            lineObject[7, 5] = new LineObject(7, 2, 8, 2, 0, content, "transperent", 290, 180, 20, 30);
            lineObject[8, 5] = new LineObject(8, 2, 9, 2, 0, content, "transperent", 330, 180, 20, 30);
            lineObject[9, 5] = new LineObject(9, 2, 10, 2, 0, content, "transperent", 370, 180, 20, 30);
            lineObject[10, 5] = new LineObject(10, 2, 11, 2, 0, content, "transperent", 410, 180, 20, 30);
            lineObject[11, 5] = new LineObject(11, 2, 12, 2, 0, content, "transperent", 450, 180, 20, 30);
            lineObject[12, 5] = new LineObject(12, 2, 13, 2, 0, content, "transperent", 490, 180, 20, 30);
            lineObject[13, 5] = new LineObject(13, 2, 14, 2, 0, content, "transperent", 530, 180, 20, 30);

            lineObject[6, 6] = new LineObject(6, 2, 6, 3, 0, content, "transperent", 230, 215, 20, 30);
            lineObject[8, 6] = new LineObject(8, 2, 8, 3, 0, content, "transperent", 310, 215, 20, 30);
            lineObject[10, 6] = new LineObject(10, 2, 10, 3, 0, content, "transperent", 390, 215, 20, 30);
            lineObject[12, 6] = new LineObject(12, 2, 12, 3, 0, content, "transperent", 470, 215, 20, 30);
            lineObject[14, 6] = new LineObject(14, 2, 14, 3, 0, content, "transperent", 550, 215, 20, 30);

            lineObject[5, 7] = new LineObject(5, 3, 6, 3, 0, content, "transperent", 210, 250, 20, 30);
            lineObject[6, 7] = new LineObject(6, 3, 7, 3, 0, content, "transperent", 250, 250, 20, 30);
            lineObject[7, 7] = new LineObject(7, 3, 8, 3, 0, content, "transperent", 290, 250, 20, 30);
            lineObject[8, 7] = new LineObject(8, 3, 9, 3, 0, content, "transperent", 330, 250, 20, 30);
            lineObject[9, 7] = new LineObject(9, 3, 10, 3, 0, content, "transperent", 370, 250, 20, 30);
            lineObject[10, 7] = new LineObject(10, 3, 11, 3, 0, content, "transperent", 410, 250, 20, 30);
            lineObject[11, 7] = new LineObject(11, 3, 12, 3, 0, content, "transperent", 450, 250, 20, 30);
            lineObject[12, 7] = new LineObject(12, 3, 13, 3, 0, content, "transperent", 490, 250, 20, 30);
            lineObject[13, 7] = new LineObject(13, 3, 14, 3, 0, content, "transperent", 530, 250, 20, 30);
            lineObject[14, 7] = new LineObject(14, 3, 15, 3, 0, content, "transperent", 570, 250, 20, 30);

            lineObject[5, 8] = new LineObject(5, 3, 5, 4, 0, content, "transperent", 190, 285, 20, 30);
            lineObject[7, 8] = new LineObject(7, 3, 7, 4, 0, content, "transperent", 270, 285, 20, 30);
            lineObject[9, 8] = new LineObject(9, 3, 9, 4, 0, content, "transperent", 350, 285, 20, 30);
            lineObject[11, 8] = new LineObject(11, 3, 11, 4, 0, content, "transperent", 430, 285, 20, 30);
            lineObject[13, 8] = new LineObject(13, 3, 13, 4, 0, content, "transperent", 510, 285, 20, 30);
            lineObject[15, 8] = new LineObject(15, 3, 15, 4, 0, content, "transperent", 590, 285, 20, 30);

            lineObject[5, 9] = new LineObject(5, 4, 6, 4, 0, content, "transperent", 210, 320, 20, 30);
            lineObject[6, 9] = new LineObject(6, 4, 7, 4, 0, content, "transperent", 250, 320, 20, 30);
            lineObject[7, 9] = new LineObject(7, 4, 8, 4, 0, content, "transperent", 290, 320, 20, 30);
            lineObject[8, 9] = new LineObject(8, 4, 9, 4, 0, content, "transperent", 330, 320, 20, 30);
            lineObject[9, 9] = new LineObject(9, 4, 10, 4, 0, content, "transperent", 370, 320, 20, 30);
            lineObject[10, 9] = new LineObject(10, 4, 11, 4, 0, content, "transperent", 410, 320, 20, 30);
            lineObject[11, 9] = new LineObject(11, 4, 12, 4, 0, content, "transperent", 450, 320, 20, 30);
            lineObject[12, 9] = new LineObject(12, 4, 13, 4, 0, content, "transperent", 490, 320, 20, 30);
            lineObject[13, 9] = new LineObject(13, 4, 14, 4, 0, content, "transperent", 530, 320, 20, 30);
            lineObject[14, 9] = new LineObject(14, 4, 15, 4, 0, content, "transperent", 570, 320, 20, 30);

            lineObject[6, 10] = new LineObject(6, 4, 6, 5, 0, content, "transperent", 230, 355, 20, 30);
            lineObject[8, 10] = new LineObject(8, 4, 8, 5, 0, content, "transperent", 310, 355, 20, 30);
            lineObject[10, 10] = new LineObject(10, 4, 10, 5, 0, content, "transperent", 390, 355, 20, 30);
            lineObject[12, 10] = new LineObject(12, 4, 12, 5, 0, content, "transperent", 470, 355, 20, 30);
            lineObject[14, 10] = new LineObject(14, 4, 14, 5, 0, content, "transperent", 550, 355, 20, 30);

            lineObject[6, 11] = new LineObject(6, 5, 7, 5, 0, content, "transperent", 250, 390, 20, 30);
            lineObject[7, 11] = new LineObject(7, 5, 8, 5, 0, content, "transperent", 290, 390, 20, 30);
            lineObject[8, 11] = new LineObject(8, 5, 9, 5, 0, content, "transperent", 330, 390, 20, 30);
            lineObject[9, 11] = new LineObject(9, 5, 10, 5, 0, content, "transperent", 370, 390, 20, 30);
            lineObject[10, 11] = new LineObject(10, 5, 11, 5, 0, content, "transperent", 410, 390, 20, 30);
            lineObject[11, 11] = new LineObject(11, 5, 12, 5, 0, content, "transperent", 450, 390, 20, 30);
            lineObject[12, 11] = new LineObject(12, 5, 13, 5, 0, content, "transperent", 490, 390, 20, 30);
            lineObject[13, 11] = new LineObject(13, 5, 14, 5, 0, content, "transperent", 530, 390, 20, 30);

            lineObject[7, 12] = new LineObject(7, 5, 9, 6, 0, content, "transperent", 270, 425, 20, 30);
            lineObject[9, 12] = new LineObject(9, 5, 7, 6, 0, content, "transperent", 350, 425, 20, 30);
            lineObject[11, 12] = new LineObject(11, 5, 11, 6, 0, content, "transperent", 430, 425, 20, 30);
            lineObject[13, 12] = new LineObject(13, 5, 13, 6, 0, content, "transperent", 510, 425, 20, 30);

            lineObject[7, 11] = new LineObject(7, 5, 8, 5, 0, content, "transperent", 290, 460, 20, 30);
            lineObject[8, 11] = new LineObject(8, 5, 9, 5, 0, content, "transperent", 330, 460, 20, 30);
            lineObject[9, 11] = new LineObject(9, 5, 10, 5, 0, content, "transperent", 370, 460, 20, 30);
            lineObject[10, 11] = new LineObject(10, 5, 11, 5, 0, content, "transperent", 410, 460, 20, 30);
            lineObject[11, 11] = new LineObject(11, 5, 12, 5, 0, content, "transperent", 450, 460, 20, 30);
            lineObject[12, 11] = new LineObject(12, 5, 13, 5, 0, content, "transperent", 490, 460, 20, 30);

        }

        private static void LoadSettlements(Settlement[,] settlement, ContentManager content)
        {
            settlement[7, 1] = new Settlement(7, 1, 0, content, "transperent", 270, 125, 20, 20);
            settlement[8, 1] = new Settlement(8, 1, 0, content, "transperent", 310, 125, 20, 20);
            settlement[9, 1] = new Settlement(9, 1, 0, content, "transperent", 350, 105, 20, 20);
            settlement[10, 1] = new Settlement(10, 1, 0, content, "transperent", 390, 125, 20, 20);
            settlement[11, 1] = new Settlement(11, 1, 0, content, "transperent", 430, 105, 20, 20);
            settlement[12, 1] = new Settlement(12, 1, 0, content, "transperent", 470, 125, 20, 20);
            settlement[13, 1] = new Settlement(13, 1, 0, content, "transperent", 510, 105, 20, 20);

            settlement[6, 2] = new Settlement(6, 2, 0, content, "transperent", 230, 195, 20, 20);
            settlement[7, 2] = new Settlement(7, 2, 0, content, "transperent", 270, 175, 20, 20);
            settlement[8, 2] = new Settlement(8, 2, 0, content, "transperent", 310, 195, 20, 20);
            settlement[9, 2] = new Settlement(9, 2, 0, content, "transperent", 350, 175, 20, 20);
            settlement[10, 2] = new Settlement(10, 2, 0, content, "transperent", 390, 195, 20, 20);
            settlement[11, 2] = new Settlement(11, 2, 0, content, "transperent", 430, 175, 20, 20);
            settlement[12, 2] = new Settlement(12, 2, 0, content, "transperent", 470, 195, 20, 20);
            settlement[13, 2] = new Settlement(13, 2, 0, content, "transperent", 510, 175, 20, 20);
            settlement[14, 2] = new Settlement(12, 2, 0, content, "transperent", 550, 195, 20, 20);

            settlement[5, 3] = new Settlement(5, 3, 0, content, "transperent", 190, 265, 20, 20);
            settlement[6, 3] = new Settlement(6, 3, 0, content, "transperent", 230, 245, 20, 20);
            settlement[7, 3] = new Settlement(7, 3, 0, content, "transperent", 270, 265, 20, 20);
            settlement[8, 3] = new Settlement(8, 3, 0, content, "transperent", 310, 245, 20, 20);
            settlement[9, 3] = new Settlement(9, 3, 0, content, "transperent", 350, 265, 20, 20);
            settlement[10, 3] = new Settlement(10, 3, 0, content, "transperent", 390, 245, 20, 20);
            settlement[11, 3] = new Settlement(11, 3, 0, content, "transperent", 430, 265, 20, 20);
            settlement[12, 3] = new Settlement(12, 3, 0, content, "transperent", 470, 245, 20, 20);
            settlement[13, 3] = new Settlement(13, 3, 0, content, "transperent", 510, 245, 20, 20);
            settlement[14, 3] = new Settlement(14, 3, 0, content, "transperent", 550, 265, 20, 20);
            settlement[15, 3] = new Settlement(15, 3, 0, content, "transperent", 590, 245, 20, 20);

            settlement[5, 4] = new Settlement(5, 4, 0, content, "transperent", 190, 315, 20, 20);
            settlement[6, 4] = new Settlement(6, 4, 0, content, "transperent", 230, 335, 20, 20);
            settlement[7, 4] = new Settlement(7, 4, 0, content, "transperent", 270, 315, 20, 20);
            settlement[8, 4] = new Settlement(8, 4, 0, content, "transperent", 310, 335, 20, 20);
            settlement[9, 4] = new Settlement(9, 4, 0, content, "transperent", 350, 315, 20, 20);
            settlement[10, 4] = new Settlement(10, 4, 0, content, "transperent", 390, 335, 20, 20);
            settlement[11, 4] = new Settlement(11, 4, 0, content, "transperent", 430, 315, 20, 20);
            settlement[12, 4] = new Settlement(12, 4, 0, content, "transperent", 370, 335, 20, 20);
            settlement[13, 4] = new Settlement(13, 4, 0, content, "transperent", 510, 335, 20, 20);
            settlement[14, 4] = new Settlement(14, 4, 0, content, "transperent", 540, 335, 20, 20);
            settlement[15, 4] = new Settlement(15, 4, 0, content, "transperent", 590, 335, 20, 20);

            settlement[6, 5] = new Settlement(6, 5, 0, content, "transperent", 230, 385, 20, 20);
            settlement[7, 5] = new Settlement(7, 5, 0, content, "transperent", 270, 405, 20, 20);
            settlement[8, 5] = new Settlement(8, 5, 0, content, "transperent", 310, 385, 20, 20);
            settlement[9, 5] = new Settlement(9, 5, 0, content, "transperent", 350, 405, 20, 20);
            settlement[10, 5] = new Settlement(10, 5, 0, content, "transperent", 390, 385, 20, 20);
            settlement[11, 5] = new Settlement(11, 5, 0, content, "transperent", 430, 405, 20, 20);
            settlement[12, 5] = new Settlement(12, 5, 0, content, "transperent", 470, 385, 20, 20);
            settlement[13, 5] = new Settlement(13, 5, 0, content, "transperent", 510, 405, 20, 20);
            settlement[14, 5] = new Settlement(14, 5, 0, content, "transperent", 550, 385, 20, 20);

            settlement[7, 6] = new Settlement(7, 6, 0, content, "transperent", 270, 455, 20, 20);
            settlement[8, 6] = new Settlement(8, 6, 0, content, "transperent", 310, 475, 20, 20);
            settlement[9, 6] = new Settlement(9, 6, 0, content, "transperent", 350, 455, 20, 20);
            settlement[10, 6] = new Settlement(10, 6, 0, content, "transperent", 390, 475, 20, 20);
            settlement[11, 6] = new Settlement(11, 6, 0, content, "transperent", 430, 455, 20, 20);
            settlement[12, 6] = new Settlement(12, 6, 0, content, "transperent", 470, 475, 20, 20);
            settlement[13, 6] = new Settlement(13, 6, 0, content, "transperent", 510, 475, 20, 20);

        }



        public DevelopmentCard GetDevelopmentCard()
        {
            return this.developmentCards.Dequeue();
        }

        public void PushDevelopmentCard(DevelopmentCard card)
        {
            this.developmentCards.Enqueue(card);
        }

        private void PlayerWins(object sender, EventArgs args)
        {
            IPlayer winner = sender as IPlayer;
            if (winner != null) this.winner = winner;
            this.gameState = GameState.Win;
        }

        private void LoadDevelopmentCardRepository()
        {
            int[] developmentCardsTypesCount = new int[] { 14, 5, 3, 3 };
            Random rand = new Random();
            for (int i = 0; i < CommonConstants.DevelopmentCardsNumber; i++)
            {
                int cardType = rand.Next(developmentCardsTypesCount.Length);
                while (developmentCardsTypesCount[cardType] <= 0)
                {
                    cardType = (cardType + 1) % developmentCardsTypesCount.Length;
                }
                developmentCardsTypesCount[cardType]--;
                DevelopmentCard card = null;
                switch (cardType)
                {
                    case 0: card = new KnightCard(); break;
                    case 1: card = new VictoryPointCard(); break;
                    case 2: card = new RoadBuildCard(); break;
                    case 3: card = new ResourceGetCard(); break;
                    default: throw new ArgumentOutOfRangeException("Such development card type does not exist");
                }
                this.developmentCards.Enqueue(card);
            }
        }

    }
}
