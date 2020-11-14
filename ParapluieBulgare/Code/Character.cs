﻿using System;
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
        public static Texture2D faceSprite;

        private DialogTree tree = null;
        private DialogBox box = null;
        protected Character interactingWith = null;

        protected bool leftConversation = false;

        //position
        private int prevX;
        protected int x;
        protected int y;

        public Point Coords
        {
            get
            {
                return new Point(x, y);
            }
        }
        public Rectangle BoxCollider
        {
            get
            {
                return new Rectangle(x, y, width, width);
            }
        }

        //size
        protected int width;

        //texture
        protected Animation idleAnimation;
        protected Animation walkAnimation;
        protected Animation currentAnimation;
        public bool Flip { get; set; }
        
        public Character(Animation idle, Animation walk, Texture2D face)
        {
            faceSprite = face;
            idleAnimation = idle;
            walkAnimation = walk;
            currentAnimation = idle;

            int ratio = 4 * Game1.HEIGHT / (9 * 32);
            width = ratio * 32 - 20;
        }

        public void SetDialogTree(DialogTree dialog)
        {
            tree = dialog;
        }

        public void StartInteraction(Character other)
        {
            interactingWith = other;
            if (tree != null) box = tree.Next();
            else StopInteraction();
        }

        public void StopInteraction(bool stopOther = true)
        {
            if (stopOther) interactingWith.StopInteraction(false);
            interactingWith = null;
            leftConversation = true;
        }

        public virtual void Update(KeyboardState keyState, KeyboardState prevKeyState)
        {
            if (leftConversation) leftConversation = false;

            if (x != prevX)
            {
                currentAnimation = walkAnimation;
                if (x < prevX) Flip = false;
                else Flip = true;
            }
            else
            {
                currentAnimation = idleAnimation;
            }

            currentAnimation.Update();
            prevX = x;

            if (interactingWith != null && tree != null)
            {
                if (keyState.IsKeyDown(Keys.E) && !prevKeyState.IsKeyDown(Keys.E))
                {
                    box = tree.Next();
                }
                if (box.End)
                {
                    StopInteraction();
                }
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch, int cameraX)
        {
            if (interactingWith != null && tree != null)
            {
                box.Draw(spriteBatch, cameraX);
            }
        }
    }
}
