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
    class Player : Character
    {
        JournalGUI journal;

        public Player(Animation idle, Animation walk, Texture2D face, Texture2D texture) : base(idle, walk, face)
        {
            x = 0;
            y = 2 * Game1.HEIGHT / 3 - width - 20;

            journal = new JournalGUI(texture);
            journal.AddHint(new Hint(HintsEnum.CibleChercheur));
        }

        public void AddHint(Hint hint)
        {
            journal.AddHint(hint);
        }

        public bool CheckHints(List<HintsEnum> hints)
        {
            return journal.CheckHints(hints);
        }

        public int CameraX(int windowWidth)
        {
            return x - (windowWidth - width) / 2;
        }

        public string Update(KeyboardState keyState, KeyboardState prevKeyState, List<NPC> npcs, List<Guard> guards)
        {
            string returned = "";
            if (interactingWith == null && !leftConversation)
            {
                int prevX = x;
                if (keyState.IsKeyDown(Keys.Right))
                {
                    x += 10;
                }
                if (keyState.IsKeyDown(Keys.Left))
                {
                    x -= 10;
                }
                if (keyState.IsKeyDown(Keys.E) && !prevKeyState.IsKeyDown(Keys.E))
                {
                    Interaction(npcs);
                }
                if (keyState.IsKeyDown(Keys.J) && !prevKeyState.IsKeyDown(Keys.J))
                {
                    journal.Toggle();
                }
                if (keyState.IsKeyDown(Keys.A) && !prevKeyState.IsKeyDown(Keys.A))
                {
                    Kill(npcs);
                }

                if (keyState.IsKeyDown(Keys.Down) && !prevKeyState.IsKeyDown(Keys.Down))
                {
                    returned = "down";
                }
                if (keyState.IsKeyDown(Keys.Up) && !prevKeyState.IsKeyDown(Keys.Up))
                {
                    returned = "up";
                }

                foreach (NPC npc in npcs)
                {
                    if (BoxCollider.Intersects(npc.BoxCollider) && npc.Blocks(this))
                    {
                        x = prevX;
                        interactingWith = npc;
                        npc.StartInteraction(this);
                        break;
                    }
                }
                foreach (Guard guard in guards)
                {
                    if (BoxCollider.Intersects(guard.BoxCollider) && guard.Blocks(this))
                    {
                        x = prevX;
                        interactingWith = guard;
                        guard.StartInteraction(this);
                        break;
                    }
                }
            }

            base.Update(keyState, prevKeyState);
            return returned;
        }

        public void Interaction(List<NPC> npcs)
        {
            Console.Out.WriteLine("trying interaction");
            foreach(NPC npc in npcs)
            {
                if (BoxCollider.Intersects(npc.BoxCollider))
                {
                    Console.Out.WriteLine("found");
                    interactingWith = npc;
                    npc.StartInteraction(this);
                    break;
                }
            }
        }


        public void Kill(List<NPC> npcs)
        {
            foreach (NPC npc in npcs)
            {
                if (BoxCollider.Intersects(npc.BoxCollider))
                {
                    npc.Die(npcs);
                    break;
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch, int cameraX)
        {
            currentAnimation.Draw(spriteBatch, new Rectangle(x - cameraX, y, width, width), Flip);
            base.Draw(spriteBatch, cameraX);
            journal.Draw(spriteBatch, font);
        }
    }
}
