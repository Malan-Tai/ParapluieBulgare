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
        private int prevX;
        protected int x;
        protected int y;

        //size
        protected int width = 100;

        //texture
        protected Animation idleAnimation;
        protected Animation walkAnimation;
        protected Animation currentAnimation;
        protected bool flip;

        //dialog
        protected SpriteFont font;
        protected Texture2D dialogBoxTexture;
        protected string currentText;
        protected bool dialogOpened = false;
        
        public Character(Animation idle, Animation walk)
        {
            idleAnimation = idle;
            walkAnimation = walk;
            currentAnimation = idle;
        }

        public void InitDialogContent(SpriteFont font, Texture2D dialogBoxTex)
        {
            this.dialogBoxTexture = dialogBoxTex;
            this.font = font;
            this.currentText = "Bonjour, je m appelle Hugo ! Et j aime raconter ma life dans des dialog box ! Je peux faire encore des lignes tu sais ^^";
        }

        public virtual void Update()
        {
            if (x != prevX)
            {
                currentAnimation = walkAnimation;
                if (x < prevX) flip = false;
                else flip = true;
            }
            else
            {
                currentAnimation = idleAnimation;
            }

            currentAnimation.Update();
            prevX = x;
        }

        public void say(string text)
        {
            dialogOpened = true;
            currentText = text;
        }

        public void closeDialog()
        {
            dialogOpened = false;
            currentText = "...";
        }

        public void DrawDialog(SpriteBatch spriteBatch , int cameraX)
        {
            if (!dialogOpened)
                return;

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

            
            int lineHeight = (int) font.MeasureString(currentText).Y;
            List<string> lines = TextWrap.Wrap(currentText, textRect.Width, font);
            
            spriteBatch.Draw(dialogBoxTexture, boxRect, Color.White);
            for (int i = 0; i < lines.Count(); i++)
            {
                Console.WriteLine(lines[i]);
                spriteBatch.DrawString(font, lines[i],
                    new Vector2(textRect.X, textRect.Y + i*lineHeight),
                    Color.Black, 0, new Vector2(0, 0), 1, SpriteEffects.None, 0);
            }
        }
    }
}
