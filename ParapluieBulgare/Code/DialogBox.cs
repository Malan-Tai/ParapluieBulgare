using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ParapluieBulgare.Code
{
    class DialogBox
    {
        public static SpriteFont font;
        public static Texture2D dialogBoxTexture;
        public static Rectangle boxRect;

        private string text;
        public bool End { get; set; }
        public Hint Hint { get; set; } = null;

        private Character talker;

        public DialogBox(string t, Character sayer, bool end = false, HintsEnum hint = HintsEnum.HintsCount)
        {
            text = t;
            talker = sayer;
            End = end;
            if (hint != HintsEnum.HintsCount)
            {
                Hint = new Hint(hint);
            }
        }

        public void Draw(SpriteBatch spriteBatch, int cameraX)
        {
            int offset = 0;
            if (talker.Flip) offset = Game1.HEIGHT / 3;
            
            SpriteEffects effects = SpriteEffects.FlipHorizontally;
            if (talker.Flip) effects = SpriteEffects.None;
            spriteBatch.Draw(dialogBoxTexture, new Rectangle(offset, 2 * Game1.HEIGHT / 3 - 30, boxRect.Width, boxRect.Height), null, Color.White, 0, new Vector2(), effects, 0);

            if (talker.Flip) talker.DrawFace(spriteBatch, 10, Game1.HEIGHT * 2 / 3 - 10);
            else talker.DrawFace(spriteBatch, Game1.WIDTH - Game1.HEIGHT / 3 - 10, Game1.HEIGHT * 2 / 3 - 10);

            Rectangle textRect = new Rectangle(offset + 50, 2 * Game1.HEIGHT / 3 + 20, boxRect.Width - 100, boxRect.Height - 100);

            int lineHeight = (int)font.MeasureString(text).Y;
            List<string> lines = TextWrap.Wrap(text, textRect.Width, font);
            for (int i = 0; i < lines.Count; i++)
            {
                spriteBatch.DrawString(font, lines[i],
                    new Vector2(textRect.X, textRect.Y + i * lineHeight),
                    Color.Black, 0, new Vector2(0, 0), 1, SpriteEffects.None, 0);
            }
        }
    }
}
