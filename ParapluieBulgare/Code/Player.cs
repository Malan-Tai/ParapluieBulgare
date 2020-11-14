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
        JournalGUI journal;

        public Point Coords
        {
            get
            {
                return new Point(x, 50);
            }
        }

        public Player(Animation anim, Texture2D t)
        {
            x = 0;
            y = 100;
            animation = anim;

            journal = new JournalGUI(t);
            journal.AddHint(new Hint("La cible est un chercheur."));
        }


        public int CameraX(int windowWidth)
        {
            return x - (windowWidth - width) / 2;
        }

        public string Update(KeyboardState keyState, KeyboardState prevKeyState)
        {
            if (keyState.IsKeyDown(Keys.Right))
            {
                x += 10;
            }
            if (keyState.IsKeyDown(Keys.Left))
            {
                x -= 10;
            }

            if (keyState.IsKeyDown(Keys.J) && !prevKeyState.IsKeyDown(Keys.J))
            {
                journal.Toggle();
            }

            animation.Update();

            if (keyState.IsKeyDown(Keys.Down) && !prevKeyState.IsKeyDown(Keys.Down))
            {
                return "down";
            }
            if (keyState.IsKeyDown(Keys.Up) && !prevKeyState.IsKeyDown(Keys.Up))
            {
                return "up";
            }
            return "";
        }

        public void Draw(SpriteBatch spriteBatch, int cameraX)
        {
            //spriteBatch.Draw(texture, new Rectangle(x - cameraX, y, width, width), Color.White);
            animation.Draw(spriteBatch, new Rectangle(x - cameraX, y, width, width), false);
            DrawDialog(spriteBatch, cameraX);
            journal.Draw(spriteBatch, font);
        }
    }
}
