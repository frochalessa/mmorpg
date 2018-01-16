using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    public class GameScreenManager
    {
        public Stack<GameScreen> GameScreens { get; private set; }

        public GameScreenManager()
        {
            GameScreens = new Stack<GameScreen>();
        }



        public void Push(GameScreen gameScreen)
        {
            GameScreens.Push(gameScreen);
        }

        public void Update(GameTime gameTime)
        {
            bool gameScreenPopped = false;
            do
            {
                gameScreenPopped = false;

                if (GameScreens.Count == 0)
                {
                    Game1.Instance.Exit();
                    return;
                }

                var gs = GameScreens.Peek();

                if (gs.Initialized == false)
                {
                    gs.Initialize();
                    gs.LoadContent(Game1.Instance.GraphicsDevice);
                }

                gs.Update(gameTime);

                if (gs.Quit)
                {
                    GameScreens.Pop();
                    gameScreenPopped = true;

                }
            } while (gameScreenPopped);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            GameScreens.Peek().Draw(spriteBatch);
        }
    }
}
