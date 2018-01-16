using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    public abstract class GameScreen
    {

        public bool Initialized { get; protected set; }
        public bool Quit { get; protected set; }

        public ContentManager Content;


        public virtual void Initialize()
        {
            

            Initialized = true;
        }

        public virtual void LoadContent(GraphicsDevice graphicsDevice)
        {

        }

        public virtual void Update(GameTime gameTime)
        {

        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {

        }
    }
}
