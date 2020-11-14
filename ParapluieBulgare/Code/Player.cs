using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ParapluieBulgare.Code
{
    class Player : Character
    {
      
        public Player(Texture2D t)
        {
            x = 0;
            y = 100;
            texture = t;
        }


        public int CameraX(int windowWidth)
        {
            return x - (windowWidth - width) / 2;
        }

        public void Update(KeyboardState keyState, KeyboardState prevKeyState)
        {
            if (keyState.IsKeyDown(Keys.Right))
            {
                x += 10;
            }
            if (keyState.IsKeyDown(Keys.Left))
            {
                x -= 10;
            }
        }

        public void Draw(SpriteBatch spriteBatch, int cameraX)
        {
            spriteBatch.Draw(texture, new Rectangle(x - cameraX, y, width, width), Color.White);
            DrawDialog(spriteBatch, cameraX);
        }
    }
}
