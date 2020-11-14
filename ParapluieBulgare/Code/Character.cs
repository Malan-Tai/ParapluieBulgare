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
        private int prevX;
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
        protected Animation idleAnimation;
        protected Animation walkAnimation;
        protected Animation currentAnimation;
        protected bool flip;

        //dialog
        protected string currentText;
        protected bool dialogOpened = false;
        
        public Character(Animation idle, Animation walk)
        {
            idleAnimation = idle;
            walkAnimation = walk;
            currentAnimation = idle;
        }

        public void StartInteraction(Character other)
        {
            interactingWith = other;
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
