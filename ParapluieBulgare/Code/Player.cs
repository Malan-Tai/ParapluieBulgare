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
        public Player(Texture2D t)
        {
            x = 0;
            y = 400;
            texture = t;
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
            spriteBatch.Draw(texture, new Rectangle(x - cameraX, y, width, width), Color.White);
            DrawDialog(spriteBatch, cameraX);
        }
    }
}
