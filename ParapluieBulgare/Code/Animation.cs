using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ParapluieBulgare.Code
{
    class Animation
    {
        private Texture2D spritesheet;
        private int spriteWidth;
        private int spriteHeight;
        private int framesNumber;
        private int framePerImage;

        private int curFrame = 0;
        private int frameCounter = 0;

        private Rectangle source;

        public Animation(Texture2D sheet, int w, int h, int frameN, int fpi)
        {
            spritesheet = sheet;
            spriteWidth = w;
            spriteHeight = h;
            framesNumber = frameN;
            framePerImage = fpi;

            source = new Rectangle(0, 0, w, h);
        }

        public void Update()
        {
            frameCounter++;
            if (frameCounter >= framePerImage)
            {
                frameCounter = 0;
                curFrame++;
                if (curFrame >= framesNumber) curFrame = 0;

                source = new Rectangle(spriteWidth * curFrame, 0, spriteWidth, spriteHeight);
            }
        }

        public void Draw(SpriteBatch spriteBatch, Rectangle destination, bool flip = false)
        {
            SpriteEffects effects = SpriteEffects.None;
            if (flip) effects = SpriteEffects.FlipHorizontally;
            spriteBatch.Draw(spritesheet, destination, source, Color.White, 0, new Vector2(), effects, 0);
        }
    }
}
