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

        private Character talker;

        public DialogBox(string t, Character sayer, bool end = false)
        {
            text = t;
            talker = sayer;
            End = end;
        }

        public void Draw(SpriteBatch spriteBatch, int cameraX)
        {
            Rectangle textRect = new Rectangle();
            Point coords = talker.Coords;

            textRect.X = coords.X - cameraX + (int)(boxRect.Width * 0.1);
            textRect.Y = coords.Y + (int)(boxRect.Height * 0.1);
            textRect.Width = (int)(boxRect.Width * 0.8);
            textRect.Height = (int)(boxRect.Height * 0.8);

            int lineHeight = (int)font.MeasureString("AJGZTYFRuidfuyzaegfuy78941!.").Y;
            List<string> lines = TextWrap.Wrap(text, textRect.Width, font);

            SpriteEffects effects = SpriteEffects.FlipHorizontally;
            if (talker.Flip) effects = SpriteEffects.None;
            spriteBatch.Draw(dialogBoxTexture, new Rectangle(coords.X - cameraX, coords.Y, boxRect.Width, boxRect.Height), null, Color.White, 0, new Vector2(), effects, 0);
            for (int i = 0; i < lines.Count; i++)
            {
                spriteBatch.DrawString(font, lines[i],
                    new Vector2(textRect.X, textRect.Y + i * lineHeight),
                    Color.Black, 0, new Vector2(0, 0), 1, SpriteEffects.None, 0);
            }
        }
    }
}
