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
            y = 100;

            journal = new JournalGUI(t);
            journal.AddHint(new Hint("La cible est un chercheur."));
        }

        public int CameraX(int windowWidth)
        {
            return x - (windowWidth - width) / 2;
        }

        public string Update(KeyboardState keyState, KeyboardState prevKeyState, List<NPC> npcs)
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

            base.Update();

            if (keyState.IsKeyDown(Keys.Down) && !prevKeyState.IsKeyDown(Keys.Down))
            {
                return "down";
            }
            if (keyState.IsKeyDown(Keys.Up) && !prevKeyState.IsKeyDown(Keys.Up))
            {
                return "up";
            }
            return "";
        }

        public void Interaction(List<NPC> npcs)
        {
            foreach(NPC npc in npcs)
            {
                if (BoxCollider.Intersects(npc.BoxCollider))
                {
                    interactingWith = npc;
                    npc.StartInteraction(this);

                    break;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch, int cameraX)
        {
            currentAnimation.Draw(spriteBatch, new Rectangle(x - cameraX, y, width, width), flip);
            DrawDialog(spriteBatch, cameraX);
            journal.Draw(spriteBatch, font);
        }
    }
}
