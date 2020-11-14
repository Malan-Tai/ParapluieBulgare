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

        public ElevatorGUI(int N, int i, Texture2D t)
        {
            numberFloors = N;
            currentFloor = i;

            texture = t;
        }

        public int Update(KeyboardState keyState, KeyboardState prevKeyState)
        {
            if (keyState.IsKeyDown(Keys.Up) && !prevKeyState.IsKeyDown(Keys.Up))
            {
                currentFloor++;
                if (currentFloor >= numberFloors) currentFloor = 0;
            }
            if (keyState.IsKeyDown(Keys.Down) && !prevKeyState.IsKeyDown(Keys.Down))
            {
                currentFloor--;
                if (currentFloor < 0) currentFloor = numberFloors - 1;
            }

            if ((keyState.IsKeyDown(Keys.Enter) && !prevKeyState.IsKeyDown(Keys.Enter)) || (keyState.IsKeyDown(Keys.Space) && !prevKeyState.IsKeyDown(Keys.Space)))
            {
                return currentFloor;
            }
            return -1;
        }

        public void Draw(SpriteBatch spriteBatch, int windowWidth)
        {
            spriteBatch.Draw(texture, new Rectangle(windowWidth - 300, 20, 200, 300), Color.Gray);

            int totHeight = 300 / numberFloors;
            int h = 2 * totHeight / 3;
            int off = totHeight / 3;
            for (int i = 0; i < numberFloors; i++)
            {
                int y = 300 - off / 2 - i * totHeight;
                int add = 0;
                if (i == currentFloor) add += 10;
                Rectangle rect = new Rectangle(windowWidth - 300 + off - add / 2, y - add / 2, h + add, h + add);

                spriteBatch.Draw(texture, rect, Color.Red);
            }
        }
    }
}
