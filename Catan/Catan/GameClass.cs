namespace Catan
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Input;
    using Microsoft.Xna.Framework.Media;

    using Catan.Constants;
    using Catan.Menu;
    using Catan.Common;
    using Catan.GameObjects;
    using Catan.DevelopmentCards;
    using Catan.Interfaces;
    using Catan.Utilities;

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
        private Dice dices;
        private ScoreBoard scoreBoard;
        public IList<Player> players;
        public Player playerOnTurn;
        public IPlayer winner;
        private Texture2D gameBackground;
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
        private HexField[,] hexFields;

        private Queue<IDevelopmentCard> developmentCards;

        //other
        private Texture2D aboutBackground;
        private Texture2D winBackground;
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

        public Player PlayerOnTurn
        {
            get { return this.playerOnTurn; }
            set { this.playerOnTurn = value; }
        }

        public Settlement[,] Settlements
        {
            get { return this.settlements; }
        }
        public LineObject[,] RoadsAndBoats
        {
            get { return this.roadsAndBoats; }
        }
        public HexField[,] HexFields
        {
            get { return this.hexFields; }
        }

        // methods
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

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            this.isStartRound = true;

            // Background music
            backgroundMusic = Content.Load<Song>("Sounds/FloatingCities");
            MediaPlayer.Play(backgroundMusic);

            //fonts
            this.gameFont = this.Content.Load<SpriteFont>("Arial");
            this.menuFont = this.Content.Load<SpriteFont>("ArialMenu");
            this.gameMessageFont = this.Content.Load<SpriteFont>("ArialMedium");
            this.winMessageFont = this.Content.Load<SpriteFont>("ArialWin");

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

            //Content to be Loaded
            this.settlements = new Settlement[20, 9];
            this.roadsAndBoats = new LineObject[20, 17];
            this.hexFields = new HexField[10, 7];

            //Load Content
            ContentLoader.LoadSettlements(this.settlements, this.Content);
            ContentLoader.LoadroadsAndBoats(this.roadsAndBoats, this.Content);
            ContentLoader.LoadHexFields(this.hexFields, this.roadsAndBoats, this.settlements, this.Content);

            this.playerOnTurn = this.players[0];
            this.statusMessage = "Build village 1";
            this.menuOptions = new List<MenuItem>
            {
              new MenuItem("New game", new Vector2(UIConstants.windowWidth / 2, 270), Color.Crimson, this.Content),
              new MenuItem("About", new Vector2(UIConstants.windowWidth / 2, 320), Color.Crimson, this.Content),
              new MenuItem("Exit", new Vector2(UIConstants.windowWidth / 2, 370), Color.Crimson, this.Content)
             };

            this.menuIndex = 1;
        }

        protected override void UnloadContent()
        {

        }

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
                        var playerVillages = this.PlayerOnTurn.Villages.Count;
                        var playerRoads = this.PlayerOnTurn.LineObjects.Count;
                        if (playerVillages == playerRoads)
                        {
                            //player must build a village
                            this.BuildStartVillage(this.PlayerOnTurn, true);
                        }
                        else if (playerVillages > playerRoads)
                        {
                            //player must build a road
                            this.BuildStartRoad(this.PlayerOnTurn, true);
                        }
                        //check which is the next move and which player to do it
                        playerVillages = this.PlayerOnTurn.Villages.Count;
                        playerRoads = this.PlayerOnTurn.LineObjects.Count;
                        if (playerRoads >= 2 && playerVillages >= 2)
                        {
                            int currentPLayerIndex = this.Players.IndexOf(this.PlayerOnTurn);
                            int nextPlayerIndex = (currentPLayerIndex + 1) % this.Players.Count;
                            this.playerOnTurn = this.Players[nextPlayerIndex];
                            if (currentPLayerIndex == this.Players.Count - 1)
                            {
                                this.statusMessage = "Roll dices";
                                this.isStartRound = false;
                                this.gameState = GameState.PlayerOnTurn;
                            }
                            else
                            {
                                this.statusMessage = "Build village 1";
                            }
                        }
                        else if (playerVillages == 1 && playerRoads == 0) this.statusMessage = "Build road 1";
                        else if (playerVillages == 1 && playerRoads == 1) this.statusMessage = "Build village 2";
                        else if (playerVillages == 2 && playerRoads == 1) this.statusMessage = "Build road 2";
                    }
                    break;
                case GameState.PlayerOnTurn:
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
                        this.gameState = GameState.BuildVillage;
                        this.statusMessage = "Build village";
                    }
                    if (this.townRect.Contains(Mouse.GetState().X, Mouse.GetState().Y) && this.newMouseState.LeftButton == ButtonState.Pressed && this.oldMouseState.LeftButton == ButtonState.Released)
                    {
                        //TODO: Add functionality
                        this.statusMessage = "Build town";
                    }
                    if (this.roadRect.Contains(Mouse.GetState().X, Mouse.GetState().Y) && this.newMouseState.LeftButton == ButtonState.Pressed && this.oldMouseState.LeftButton == ButtonState.Released)
                    {
                        this.gameState = GameState.BuildRoad;
                        this.statusMessage = "Build road";
                    }
                    if (this.boatRect.Contains(Mouse.GetState().X, Mouse.GetState().Y) && this.newMouseState.LeftButton == ButtonState.Pressed && this.oldMouseState.LeftButton == ButtonState.Released)
                    {
                        //TODO: Add functionality
                        this.statusMessage = "Build boat";
                    }
                    if (this.dices.Rectangle.Contains(Mouse.GetState().X, Mouse.GetState().Y) && this.newMouseState.LeftButton == ButtonState.Pressed && this.oldMouseState.LeftButton == ButtonState.Released)
                    {
                        this.dices.Roll();
                        //TODO: Loop over and collect resources
                        this.playerOnTurn = this.players[this.playerOnTurn.Id % 4];
                        //this.playerOnTurn.AddPoints(2);//For testing purposes
                        this.scoreBoard.Update(this.players);
                    }
                    break;
                case GameState.BuildRoad:
                    if (this.newMouseState.LeftButton == ButtonState.Pressed &&
                        this.oldMouseState.LeftButton == ButtonState.Released &&
                        this.BuildStartRoad(this.playerOnTurn, false))
                    {
                        this.statusMessage = "Roll dices";
                        this.gameState = GameState.PlayerOnTurn;
                    }
                    break;
                case GameState.BuildBoat:
                    this.gameState = GameState.PlayerOnTurn;
                    //TODO: Add functionality
                    /*if (this.BuildStartRoad(this.playerOnTurn, false);
                    {
                        this.statusMessage = "Roll dices";
                    }      */
                    break;
                case GameState.BuildVillage:

                    if (this.BuildStartVillage(this.playerOnTurn, false))
                    {
                        this.statusMessage = "Roll dices";
                        this.gameState = GameState.PlayerOnTurn;
                    }
                    break;
                case GameState.BuildTown:
                    this.gameState = GameState.PlayerOnTurn;
                    //TODO: Add functionality
                    /*if (this.newKBState.IsKeyDown(Keys.M) && this.oldKBState.IsKeyUp(Keys.M))
                    {
                       this.statusMessage = "Roll dices";
                    }*/
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

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            this.spriteBatch.Begin();

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
                    this.spriteBatch.Draw(this.gameBackground, this.backgroundRect, Color.White);
                    this.dices.Draw(this.spriteBatch);
                    this.scoreBoard.Draw(this.spriteBatch);
                    //Buttons
                    this.spriteBatch.Draw(this.develpomentCardButton, this.develpomentCardRect, Color.White);
                    this.spriteBatch.Draw(this.villageButton, this.villageRect, Color.White);
                    this.spriteBatch.Draw(this.townButton, this.townRect, Color.White);
                    this.spriteBatch.Draw(this.roadButton, this.roadRect, Color.White);
                    this.spriteBatch.Draw(this.boatButton, this.boatRect, Color.White);
                    //Rest
                    this.playerOnTurn.Draw(this.spriteBatch);
                    this.spriteBatch.DrawString(this.menuFont, this.playerOnTurn.UserName + "'s Turn ", new Vector2(115, 5), Color.White);
                    this.spriteBatch.DrawString(this.gameMessageFont, this.statusMessage, new Vector2(115, 50), Color.White);
                    this.spriteBatch.DrawString(this.gameMessageFont, this.errorMessage, new Vector2(115, 80), Color.Red);
                    //Draw Settlements
                    for (int i = 0; i < hexFields.GetLength(0); i++)
                    {
                        for (int j = 0; j < hexFields.GetLength(1); j++)
                        {
                            if (hexFields[i, j] != null)
                            {
                                hexFields[i, j].Draw(this.spriteBatch);
                            }
                        }
                    }
                    //Draw Roads
                    for (int i = 0; i < roadsAndBoats.GetLength(0); i++)
                    {
                        for (int j = 0; j < roadsAndBoats.GetLength(1); j++)
                        {
                            if (roadsAndBoats[i, j] != null)
                            {
                                roadsAndBoats[i, j].Draw(this.spriteBatch);
                            }
                        }
                    }
                    //Draw Settlements
                    for (int i = 0; i < settlements.GetLength(0); i++)
                    {
                        for (int j = 0; j < settlements.GetLength(1); j++)
                        {
                            if (settlements[i, j] != null)
                            {
                                settlements[i, j].Draw(this.spriteBatch);
                            }
                        }
                    }
                    break;
            }

            this.spriteBatch.End();

            base.Draw(gameTime);
        }

        //customMethods
        private bool BuildStartVillage(IPlayer player, bool buildWithDevCard)
        {
            int mouseCoorX = 0;
            int mouseCoorY = 0;
            if (this.newMouseState.LeftButton == ButtonState.Pressed
                && this.oldMouseState.LeftButton == ButtonState.Released
                && Mouse.GetState().X > 0 && Mouse.GetState().Y > 0)
            {
                this.errorMessage = string.Empty;
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
                                var tempX = settlements[x, y].ScreenX;
                                var tempY = settlements[x, y].ScreenY;
                                var imageString = string.Format("villageplayer" + player.Id);
                                Village tempVillage = new Village(x, y, 0, Content, imageString, tempX, tempY, 20, 20);
                                tempVillage.Build(player, buildWithDevCard);
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
                            var startTempX = roadsAndBoats[x, y].StartPointX;
                            var startTempY = roadsAndBoats[x, y].StartPointY;
                            var endTempX = roadsAndBoats[x, y].EndPointX;
                            var endTempY = roadsAndBoats[x, y].EndPointY;
                            var tempX = roadsAndBoats[x, y].ScreenX;
                            var tempY = roadsAndBoats[x, y].ScreenY;

                            var imageString = DataGenerator.GenerateRoadName(x, y) + player.Id;

                            Road tempRoad = new Road(startTempX, startTempY, endTempX, endTempY, 0,
                                                        Content, imageString, tempX, tempY, 20, 30);
                            tempRoad.Build(player, buildWithDevCard);
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
