﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ParapluieBulgare.Code
{
    class Player
    {
        private int x;
        private int width = 100;
        private Texture2D texture;
        
        public Player(Texture2D t)
        {
            x = 0;
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
            spriteBatch.Draw(texture, new Rectangle(x - cameraX, 50, width, width), Color.White);
        }
    }
}
