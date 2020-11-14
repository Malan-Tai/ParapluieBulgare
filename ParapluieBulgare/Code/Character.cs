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
    class Character
    {
        //position
        protected int x;
        protected int y;

        //size
        protected int width = 100;

        //texture
        protected Texture2D texture;

        //dialog
        protected SpriteFont m_font;
        protected Texture2D m_dialogBoxTexture;
        
        public void InitDialogContent(SpriteFont font, Texture2D dialogBoxTex)
        {
            m_dialogBoxTexture = dialogBoxTex;
            m_font = font;
        }
        public void DrawDialog(SpriteBatch spriteBatch , int cameraX)
        {

            Rectangle boxRect = new Rectangle();
            boxRect.Width = 4 * width;
            boxRect.Height = (int)(0.261 * boxRect.Width);
            boxRect.X = x - cameraX;
            boxRect.Y = y - boxRect.Height;
           

            Rectangle textRect = new Rectangle();
            textRect.X = boxRect.X + (int)(boxRect.Width * 0.05);
            textRect.Y = boxRect.Y + (int)(boxRect.Height * 0.05);
            textRect.Width = (int)(boxRect.Width * 0.9);
            boxRect.Height = (int)(boxRect.Height * 0.9);


            string text = "Bonjour, je m appelle Hugo ! Et j aime raconter ma life dans des dialog box ! Je peux faire encore des lignes tu sais ^^";
            
            int lineHeight = (int) m_font.MeasureString(text).Y;
            List<string> lines = TextWrap.Wrap(text, textRect.Width, m_font);

            
            spriteBatch.Draw(m_dialogBoxTexture, boxRect, Color.White);
            for (int i = 0; i < lines.Count(); i++)
            {
                Console.WriteLine(lines[i]);
                spriteBatch.DrawString(m_font, lines[i],
                    new Vector2(textRect.X, textRect.Y + i*lineHeight),
                    Color.Black, 0, new Vector2(0, 0), 1, SpriteEffects.None, 0);
            }
        }
    }
}
