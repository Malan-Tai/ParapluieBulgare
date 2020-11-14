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
    class Player : Character
    {
        JournalGUI journal;

        public Player(Animation idle, Animation walk, Texture2D t) : base(idle, walk)
        {
            x = 0;
            y = 200;

            journal = new JournalGUI(t);
            journal.AddHint(new Hint("La cible est un chercheur."));
        }

        public int CameraX(int windowWidth)
        {
            return x - (windowWidth - width) / 2;
        }

        public string Update(KeyboardState keyState, KeyboardState prevKeyState, List<NPC> npcs)
        {
            string returned = "";
            if (interactingWith == null && !leftConversation)
            {
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

                if (keyState.IsKeyDown(Keys.Down) && !prevKeyState.IsKeyDown(Keys.Down))
                {
                    returned = "down";
                }
                if (keyState.IsKeyDown(Keys.Up) && !prevKeyState.IsKeyDown(Keys.Up))
                {
                    returned = "up";
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

        public override void Draw(SpriteBatch spriteBatch, int cameraX)
        {
            currentAnimation.Draw(spriteBatch, new Rectangle(x - cameraX, y, width, width), Flip);
            base.Draw(spriteBatch, cameraX);
            journal.Draw(spriteBatch, font);
        }
    }
}
