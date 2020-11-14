﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ParapluieBulgare.Code
{
    class NPC : Character
    {
        public NPC(Animation idle, Animation walk, int x = 500) : base(idle, walk)
        {
            this.x = x;
            this.y = 100;
        }

        public override void Update()
        {
            base.Update();
        }

        public void Draw(SpriteBatch spriteBatch, int cameraX)
        {
            currentAnimation.Draw(spriteBatch, new Rectangle(x - cameraX, 50, width, width), flip);
            DrawDialog(spriteBatch, cameraX);
        }
    }
}
