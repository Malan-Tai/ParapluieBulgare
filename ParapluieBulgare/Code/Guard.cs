using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ParapluieBulgare.Code
{
    class Guard : NPC
    {
        private int currentFloor;

        public Guard(Animation idle, Animation walk, Texture2D face, int floor, int x = 500) : base(idle, walk, face, x)
        {
            currentFloor = floor;
        }

        public override void Die(List<NPC> npcs)
        {
            Console.WriteLine("Argh! Je suis mort ! En fait non.");
        }

        public override void Update(KeyboardState keyState, KeyboardState prevKeyState)
        {
        }

        public void Update(KeyboardState keyState, KeyboardState prevKeyState, Player player, int playerFloor)
        {
            Point playerCoords = player.Coords;
            if (Game1.ThreatLevel >= 2)
            {
                if (playerFloor == currentFloor)
                {
                    if (playerCoords.X > x) x += 2;
                    else x -= 2;
                }
                else
                {

                }
            }

            base.Update(keyState, prevKeyState);
        }

        public void Draw(SpriteBatch spriteBatch, int cameraX, int playerFloor)
        {
            if (currentFloor == playerFloor) base.Draw(spriteBatch, cameraX);
        }
    }
}
