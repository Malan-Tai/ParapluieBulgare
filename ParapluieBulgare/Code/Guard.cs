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
        private int speed = 2;
        private int target;
        private bool isGoingToChangeFloor = false;

        public Guard(Animation idle, Animation walk, int floor, int x = 500) : base(idle, walk, x)
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
            if (Game1.ThreatLevel >= 1)
            {
                if (playerFloor == currentFloor)
                {
                    if (!BoxCollider.Intersects(player.BoxCollider))
                    {
                        if (playerCoords.X > x) x += speed;
                        else x -= speed;
                    }
                }
                else if (isGoingToChangeFloor)
                {
                    //test target collision
                    int elevatorWidth = 100;
                    if (Math.Abs(x - target) <= elevatorWidth / 2)
                    {
                        currentFloor = playerFloor;
                        isGoingToChangeFloor = false;
                    }
                    else
                    {
                        if (target > x) x += speed;
                        else x -= speed;
                    }
                }
                else 
                { 
                    if (currentFloor == 0 || currentFloor == 5 || playerFloor == 0 || playerFloor == 5)
                    {
                        //ascenceur
                        target = Floor.width / 2;
                    }
                    else if (x <= Floor.width /4)
                    {
                        //escalier gauche
                        target = 0;
                    }
                    else if (x >= 3 * Floor.width / 4)
                    {
                        //escalier droit
                        target = Floor.width;
                    }
                    else
                    {
                        //ascenceur
                        target = Floor.width / 2;
                    }
                    isGoingToChangeFloor = true;
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
