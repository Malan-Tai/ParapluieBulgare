//using System;
//using System.Collections.Generic;
//using System.Text;
//using Microsoft.Xna.Framework;
//using Microsoft.Xna.Framework.Graphics;

//namespace ParapluieBulgare.Code
//{
//    class DialogBox
//    {
//        public static SpriteFont font;
//        public static Texture2D dialogBoxTexture;

//        private Rectangle boxRect;
//        private string text;

//        public DialogBox(int x, int y, int w, int h, string t)
//        {
//            boxRect = new Rectangle(x, y, w, h);
//            text = t;
//        }

//        public void Increment(int h)
//        {
//            boxRect = new Rectangle(boxRect.X, boxRect.Y - h, boxRect.Width, boxRect.Height);
//        }

//        public void Draw(SpriteBatch spriteBatch)
//        {
//            Rectangle textRect = new Rectangle();
//            textRect.X = boxRect.X + (int)(boxRect.Width * 0.05);
//            textRect.Y = boxRect.Y + (int)(boxRect.Height * 0.05);
//            textRect.Width = (int)(boxRect.Width * 0.9);
//            textRect.Height = (int)(boxRect.Height * 0.9);


//            int lineHeight = (int)font.MeasureString(text).Y;
//            List<string> lines = TextWrap.Wrap(text, textRect.Width, font);

//            spriteBatch.Draw(dialogBoxTexture, boxRect, Color.White);
//            for (int i = 0; i < lines.Count; i++)
//            {
//                spriteBatch.DrawString(font, lines[i],
//                    new Vector2(textRect.X, textRect.Y + i * lineHeight),
//                    Color.Black, 0, new Vector2(0, 0), 1, SpriteEffects.None, 0);
//            }
//        }
//    }
//}
