using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace ParapluieBulgare.Code
{
    class ElevatorGUI
    {
        private int numberFloors;
        private int currentFloor;

        private Texture2D texture;

        public ElevatorGUI(int N, int f, Texture2D t)
        {
            numberFloors = N; //6
            currentFloor = f; // -1 to 4

            texture = t;
        }

        public int Update(KeyboardState keyState, KeyboardState prevKeyState)
        {
            if (keyState.IsKeyDown(Keys.Up) && !prevKeyState.IsKeyDown(Keys.Up))
            {
                currentFloor++;
                if (currentFloor >= numberFloors - 1) currentFloor = -1;
            }
            if (keyState.IsKeyDown(Keys.Down) && !prevKeyState.IsKeyDown(Keys.Down))
            {
                currentFloor--;
                if (currentFloor < -1) currentFloor = numberFloors - 2;
            }

            if ((keyState.IsKeyDown(Keys.Enter) && !prevKeyState.IsKeyDown(Keys.Enter)) || (keyState.IsKeyDown(Keys.Space) && !prevKeyState.IsKeyDown(Keys.Space)))
            {
                return currentFloor + 1;
            }
            return -1;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            int width = 150;
            int height = 2 * Game1.HEIGHT / 3 - 50;
            int x = Game1.WIDTH / 2 + 128;
            spriteBatch.Draw(texture, new Rectangle(x, 25, width, height), new Color(64, 64, 64));

            int totHeight = height / numberFloors;
            int h = 2 * totHeight / 3;
            int off = totHeight / 3;
            for (int i = 0; i < numberFloors; i++)
            {
                int floor = i - 1;
                int y = -25 + height - off / 2 - i * totHeight;
                int add = 0;
                Color buttonColor = Color.DarkRed;
                string txt = floor.ToString();
                if (floor == currentFloor)
                {
                    buttonColor = Color.Red;
                    add += 10;
                }

                Rectangle rect = new Rectangle(x + off - add / 2, y - add / 2, h + add, h + add);

                spriteBatch.Draw(texture, rect, buttonColor);
                spriteBatch.DrawString(Character.font, txt, new Vector2(rect.X + h + off + add / 2, rect.Y + add / 2), Color.White);
            }
        }
    }
}
