using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ParapluieBulgare.Code
{
    class Furniture : Character
    {
        public bool Target { get; set; } = false;

        public Furniture(Animation idle, Animation walk, int x = 500) : base(idle, walk)
        {
            this.x = x;
            y = 2 * Game1.HEIGHT / 3 - width - 20;
        }

        public override void Update(KeyboardState keyState, KeyboardState prevKeyState)
        {
            base.Update(keyState, prevKeyState);
        }

        public override void Draw(SpriteBatch spriteBatch, int cameraX)
        {
            currentAnimation.Draw(spriteBatch, new Rectangle(x - cameraX, y, width, width), Flip);
            base.Draw(spriteBatch, cameraX);
        }
    }
}
