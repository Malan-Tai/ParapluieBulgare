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

    public static class DialogManager
    {
        static SpriteFont font;
        static Texture2D dialogBoxTexture;
        public static void InitContent(SpriteFont font, Texture2D dialogBoxTex)
        {
            //DialogBox.dialogBoxTexture = dialogBoxTex;
            //DialogBox.font = font;
            DialogManager.dialogBoxTexture = dialogBoxTex;
            DialogManager.font = font;
        }

        public static void DrawDialog(SpriteBatch spriteBatch, Rectangle boxRect, string text)
        {
            //Rectangle textRect = new Rectangle();
            //textRect.X = boxRect.X + (int)(boxRect.Width * 0.05);
            //textRect.Y = boxRect.Y + (int)(boxRect.Height * 0.05);
            //textRect.Width = (int)(boxRect.Width * 0.9);
            //boxRect.Height = (int)(boxRect.Height * 0.9);


            //int lineHeight = (int)(font.MeasureString(text).Y);
            //List<string> lines = TextWrap.Wrap(text, textRect.Width, font);

            //spriteBatch.Draw(dialogBoxTexture, boxRect, Color.White);
            //for (int i = 0; i < lines.Count(); i++)
            //{
            //    spriteBatch.DrawString(DialogManager.font, lines[i],
            //        new Vector2(textRect.X, textRect.Y + i * lineHeight),
            //        Color.Black, 0, new Vector2(0, 0), 1, SpriteEffects.None, 0);
            //}
        }
    }
}
