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
        public static SpriteFont font;
        public static Texture2D dialogBoxTexture;

        protected Character interactingWith = null;
        


        //position
        protected int x;
        protected int y;
        public Point Coords
        {
            get
            {
                return new Point(x, 50);
            }
        }
        public Rectangle BoxCollider
        {
            get
            {
                return new Rectangle(Coords.X, Coords.Y, width, width);
            }
        }
     

        //size
        protected int width = 100;

        //texture
        protected Texture2D texture;

        //dialog
        protected string currentText;
        protected bool dialogOpened = false;

        public void StartInteraction(Character other)
        {
            interactingWith = other;
        }

        public void Say(string text)
        {
            dialogOpened = true;
            currentText = text;
        }

        public void CloseDialog()
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

            DialogManager.DrawDialog(spriteBatch, boxRect, currentText);
        }
    }
}
