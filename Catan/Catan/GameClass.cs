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
using System.Linq;
using Catan.GameObjects;
using Catan.DevelopmentCards;

using Microsoft.Xna.Framework.Content;
using Catan.Interfaces;
using Catan.Utilities;

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
        private SpriteFont gameMessageFont;
        private SpriteFont winMessageFont;

        //menu related
        private IList<MenuItem> menuOptions;
        private Texture2D menuBackground;
        private int menuIndex;

        //game related
        private Texture2D gameBackground;
        private Texture2D winBackground;
        private Dice dices;
        private ScoreBoard scoreBoard;
        public IList<Player> players;
        public Player playerOnTurn;
        public IPlayer winner;
        private Texture2D develpomentCardButton;
        private Texture2D villageButton;
        private Texture2D townButton;
        private Texture2D roadButton;
        private Texture2D boatButton;
        private Rectangle develpomentCardRect;
        private Rectangle villageRect;
        private Rectangle townRect;
        private Rectangle roadRect;
        private Rectangle boatRect;
        private bool isStartRound;
        private string errorMessage = string.Empty;
        private string statusMessage = string.Empty;

        private Settlement[,] settlements;
        private LineObject[,] roadsAndBoats;

        private Queue<IDevelopmentCard> developmentCards;


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
            this.gameMessageFont = this.Content.Load<SpriteFont>("ArialMedium");
            this.winMessageFont = this.Content.Load<SpriteFont>("ArialWin");

            this.isStartRound = true;

            //textures
            this.menuBackground = this.Content.Load<Texture2D>("menubackground");
            this.aboutBackground = this.Content.Load<Texture2D>("aboutbackground");
            this.gameBackground = this.Content.Load<Texture2D>("gamebackground");
            this.winBackground = this.Content.Load<Texture2D>("winbackground");
            this.develpomentCardButton = this.Content.Load<Texture2D>("developmentcard");
            this.villageButton = this.Content.Load<Texture2D>("village");
            this.townButton = this.Content.Load<Texture2D>("town");
            this.roadButton = this.Content.Load<Texture2D>("road");
            this.boatButton = this.Content.Load<Texture2D>("boat");

            //rectangles
            this.backgroundRect = new Rectangle(0, 0, UIConstants.windowWidth, UIConstants.windowHeight);
            this.develpomentCardRect = new Rectangle(10, 250, 40, 40);
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
            this.developmentCards = new Queue<IDevelopmentCard>(CommonConstants.DevelopmentCardsNumber);
            this.LoadDevelopmentCardRepository();

            //subscribe to Player wins event
            foreach (var player in this.players)
            {
                player.WinPointsReached += this.PlayerWins;
            }
            //objects
            this.dices = new Dice(this.Content, "dice1", 670, 530, 110, 50);
            this.dices.Roll();
            this.scoreBoard = new ScoreBoard(this.players, this.Content, "scoreboard", 0, 0, 105, 103);

            //
            this.settlements = new Settlement[20, 9];
            this.roadsAndBoats = new LineObject[20, 17];

            //Load Content
            ContentLoader.LoadSettlements(this.settlements, this.Content);
            ContentLoader.LoadroadsAndBoats(this.roadsAndBoats, this.Content);

            this.playerOnTurn = this.players[0];
            this.statusMessage = "Build 2 villages";
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

                    if (this.isStartRound && this.newMouseState.LeftButton == ButtonState.Pressed && this.oldMouseState.LeftButton == ButtonState.Released)
                    {
                        var player = this.Players.FirstOrDefault(pl => pl.Villages.Count < 2 || pl.LineObjects.Count < 2);
                        if (player == null)
                        {
                            this.isStartRound = false;
                        }
                        else
                        {
                            this.PlayerOnTurn = player;
                            if (player.Villages.Count < 2)
                            {
                                this.BuildStartVillage(player, true);
                            }
                            else
                            {
                                this.BuildStartRoad(player, true);
                            }
                        }
                        //check which is the next move and which player to do it
                        if (player.LineObjects.Count >= 2)
                        {
                            int currentPLayerIndex = this.Players.IndexOf(player);
                            int nextPlayerIndex = (currentPLayerIndex + 1) % this.Players.Count;
                            this.playerOnTurn = this.Players[nextPlayerIndex];
                            if (currentPLayerIndex == this.Players.Count - 1)
                            {
                                this.statusMessage = "Roll dices";
                                this.isStartRound = false;
                            }
                            else this.statusMessage = "Build 2 villages";
                        }
                        else if (player.Villages.Count >= 2) this.statusMessage = "Build 2 roads";
                    }
                    else
                    {

                        //Buttons for actions
                        if (this.develpomentCardRect.Contains(Mouse.GetState().X, Mouse.GetState().Y) && this.newMouseState.LeftButton == ButtonState.Pressed && this.oldMouseState.LeftButton == ButtonState.Released)
                        {
                            //TODO: conditions to draw Development card
                            this.statusMessage = "Buy cards";
                            var developmentCardDrawed = this.GetDevelopmentCard();
                            this.PlayerOnTurn.DevCardsPossedsed.Add(developmentCardDrawed);

                        }
                        if (this.villageRect.Contains(Mouse.GetState().X, Mouse.GetState().Y) && this.newMouseState.LeftButton == ButtonState.Pressed && this.oldMouseState.LeftButton == ButtonState.Released)
                        {

                            this.BuildStartVillage(this.playerOnTurn, false);
                            this.statusMessage = "Build village";
                        }
                        if (this.townRect.Contains(Mouse.GetState().X, Mouse.GetState().Y) && this.newMouseState.LeftButton == ButtonState.Pressed && this.oldMouseState.LeftButton == ButtonState.Released)
                        {

                            //Add functionality
                            this.statusMessage = "Build town";
                        }
                        if (this.roadRect.Contains(Mouse.GetState().X, Mouse.GetState().Y) && this.newMouseState.LeftButton == ButtonState.Pressed && this.oldMouseState.LeftButton == ButtonState.Released)
                        {

                            this.BuildStartRoad(this.playerOnTurn, false);
                            this.statusMessage = "Build road";
                        }
                        if (this.boatRect.Contains(Mouse.GetState().X, Mouse.GetState().Y) && this.newMouseState.LeftButton == ButtonState.Pressed && this.oldMouseState.LeftButton == ButtonState.Released)
                        {

                            //Add functionality
                            this.statusMessage = "Build boat";
                        }


                        if (this.dices.Rectangle.Contains(Mouse.GetState().X, Mouse.GetState().Y) && this.newMouseState.LeftButton == ButtonState.Pressed && this.oldMouseState.LeftButton == ButtonState.Released)
                        {

                            this.dices.Roll();
                            //TODO: Loop over and collect resources

                            //this.playerOnTurn


                            this.playerOnTurn = this.players[this.playerOnTurn.Id % 4];
                            this.playerOnTurn.AddPoints(2);//TEST REMOVE LATER
                            this.scoreBoard.Update(this.players);
                        }
                    }
                    break;
                case GameState.About:

                    if (this.newKBState.IsKeyDown(Keys.M) && this.oldKBState.IsKeyUp(Keys.M))
                    {
                        this.gameState = GameState.Menu;
                    }

                    break;
                case GameState.Exit:
                    Exit();
                    break;
            }

            base.Update(gameTime);
        }

        private bool BuildStartVillage(IPlayer player, bool buildWithDevCard)
        {
            int mouseCoorX = 0;
            int mouseCoorY = 0;
            if (this.newMouseState.LeftButton == ButtonState.Pressed
                       && this.oldMouseState.LeftButton == ButtonState.Released
                && Mouse.GetState().X > 0 && Mouse.GetState().Y > 0)
            {
                mouseCoorX = Mouse.GetState().X;
                mouseCoorY = Mouse.GetState().Y;


                for (uint x = 0; x < 20; x++)
                {
                    for (uint y = 0; y < 9; y++)
                    {
                        if (settlements[x, y] == null)
                        {
                            continue;
                        }
                        if (settlements[x, y].Rectangle.Contains(mouseCoorX, mouseCoorY))
                        {
                            try
                            {
                                settlements[x, y].Build(player, buildWithDevCard);
                                var tempX = settlements[x, y].ScreenX;
                                var tempY = settlements[x, y].ScreenY;
                                var imageString = string.Format("villageplayer" + player.Id);
                                Village tempVillage = new Village(x, y, player.Id, Content, imageString, tempX, tempY, 20, 20);
                                settlements[x, y] = tempVillage;
                                player.Villages.Add(tempVillage);
                                return true;
                            }
                            catch (Exceptions.IllegalBuildPositionException ib)
                            {
                                this.errorMessage = ib.Message;
                            }
                            catch (Exception e)
                            {
                                this.errorMessage = e.Message;
                            }
                        }
                    }
                }
            }

            return true;
        }

        private bool BuildStartRoad(IPlayer player, bool buildWithDevCard)
        {
            int mouseCoorX = 0;
            int mouseCoorY = 0;
            if (this.newMouseState.LeftButton == ButtonState.Pressed
                       && this.oldMouseState.LeftButton == ButtonState.Released
                && Mouse.GetState().X > 0 && Mouse.GetState().Y > 0)
            {
                mouseCoorX = Mouse.GetState().X;
                mouseCoorY = Mouse.GetState().Y;

            }
            for (uint x = 0; x < 20; x++)
            {
                for (uint y = 0; y < 17; y++)
                {
                    if (roadsAndBoats[x, y] == null)
                    {
                        continue;
                    }

                    if (roadsAndBoats[x, y].Rectangle.Contains(mouseCoorX, mouseCoorY))
                    {
                        try
                        {
                            roadsAndBoats[x, y].Build(player, buildWithDevCard);
                            var startTempX = roadsAndBoats[x, y].StartPointX;
                            var startTempY = roadsAndBoats[x, y].StartPointY;
                            var endTempX = roadsAndBoats[x, y].EndPointX;
                            var endTempY = roadsAndBoats[x, y].EndPointY;
                            var tempX = roadsAndBoats[x, y].ScreenX;
                            var tempY = roadsAndBoats[x, y].ScreenY;

                            var imageString = DataGenerator.GenerateRoadName(x, y) + player.Id;

                            Road tempRoad = new Road(startTempX, startTempY, endTempX, endTempY, player.Id,
                                                        Content, imageString, tempX, tempY, 20, 30); //TODO //TODO: Set proper image
                            roadsAndBoats[x, y] = tempRoad;

                            player.LineObjects.Add(tempRoad);
                            return true;
                        }
                        catch (Exceptions.IllegalBuildPositionException ib)
                        {
                            this.errorMessage = ib.Message;
                        }
                        catch (Exception e)
                        {
                            this.errorMessage = e.Message;
                        }
                    }
                }
            }

            return true;
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
                    this.spriteBatch.Draw(this.develpomentCardButton, this.develpomentCardRect, Color.White);
                    this.spriteBatch.Draw(this.villageButton, this.villageRect, Color.White);
                    this.spriteBatch.Draw(this.townButton, this.townRect, Color.White);
                    this.spriteBatch.Draw(this.roadButton, this.roadRect, Color.White);
                    this.spriteBatch.Draw(this.boatButton, this.boatRect, Color.White);
                    //rest
                    this.playerOnTurn.Draw(this.spriteBatch);
                    this.spriteBatch.DrawString(this.menuFont, this.playerOnTurn.UserName + "'s Turn ", new Vector2(115, 5), Color.White);
                    this.spriteBatch.DrawString(this.gameMessageFont, this.statusMessage, new Vector2(115, 50), Color.White);
                    this.spriteBatch.DrawString(this.gameMessageFont, this.errorMessage, new Vector2(115, 80), Color.Red);

                    //Draw Roads
                    for (int i = 0; i < 20; i++)
                    {
                        for (int j = 0; j < 17; j++)
                        {
                            if (roadsAndBoats[i, j] != null)
                            {
                                roadsAndBoats[i, j].Draw(this.spriteBatch);
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
                    this.spriteBatch.Draw(this.winBackground, this.backgroundRect, Color.White);
                    this.spriteBatch.DrawString(this.winMessageFont, this.winner.UserName + " Wins", new Vector2(UIConstants.windowWidth / 2, 100) - (this.winMessageFont.MeasureString(this.winner.UserName + " WINS") / 2), Color.Crimson);
                    break;
                default:
                    break;
            }


            this.spriteBatch.End();


            base.Draw(gameTime);
        }


        //customMethods






        public IDevelopmentCard GetDevelopmentCard()
        {
            return this.developmentCards.Dequeue();
        }

        public void PushDevelopmentCard(IDevelopmentCard card)
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
            foreach (var developmentCard in ContentLoader.GenerateDevelopmentCards())
            {
                this.PushDevelopmentCard(developmentCard);
            }
        }
    }
}
