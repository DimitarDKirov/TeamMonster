using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catan.Menu
{
       public abstract class UserInterfaceElement : DrawableGameComponent, Interfaces.IDrawableCustom
        {
        //constructor
        protected UserInterfaceElement(Game game, EventHandler showItemHandler = null, EventHandler hideItemHandler = null)
            : base(game)
        {
            this.ShowItemHandler = showItemHandler;
            this.HideItemHandler = hideItemHandler;
        }
        
        //events
        public event EventHandler ShowItemHandler;       // Event on menu being shown
        public event EventHandler HideItemHandler;      // Event on menu being hidden
        
        //properties
        public virtual Rectangle Rectangle { get; set; }
        public virtual Texture2D Texture { get; protected set; }
        protected GraphicsDeviceManager Graphics { get; set; }
        protected SpriteBatch SpriteBatch { get; set; }

        public override void Initialize()
        {
            this.Graphics = (GraphicsDeviceManager)Game.Services.GetService(typeof(GraphicsDeviceManager));
            this.SpriteBatch = (SpriteBatch)Game.Services.GetService(typeof(SpriteBatch));

            if (this.Graphics == null)
            {
                this.Graphics = new GraphicsDeviceManager(this.Game);
            }

            if (this.SpriteBatch == null)
            {
                this.SpriteBatch = new SpriteBatch(this.Game.GraphicsDevice);
            }

            base.Initialize();
        }
        
        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            this.SpriteBatch.Begin();
            Draw(this.SpriteBatch);
            this.SpriteBatch.End();
        }

        public virtual void LoadContent()
        {
            base.LoadContent();
        }
        
        //Draw from IDrawableCustom
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            
        }
        

        public virtual void Hide()
        {
            if (this.Visible == false)
            {
                return;
            }

            this.Enabled = false;
            this.Visible = false;

            if (HideItemHandler != null)
            {
                HideItemHandler(this, null);
            }
        }

        public virtual void Show()
        {
            if (this.Visible == true)
            {
                return;
            }

            this.Enabled = true;
            this.Visible = true;

            if (ShowItemHandler != null)
            {
                ShowItemHandler(this, null);
            }
        }
    }
}
