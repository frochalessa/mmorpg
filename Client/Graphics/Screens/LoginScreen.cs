using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeonBit.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Client
{
    class LoginScreen : GameScreen
    {
        Gui gui = new Gui();

        public LoginScreen()
        {
        }

        public override void Initialize()
        {
            gui.Start();
            MenuManager.ChangeMenu(MenuManager.Menu.Login);
            base.Initialize();
        }

        public override void LoadContent(GraphicsDevice graphicsDevice)
        {
            base.LoadContent(graphicsDevice);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            UserInterface.Active.Draw(spriteBatch);

            base.Draw(spriteBatch);
        }
    }

}
