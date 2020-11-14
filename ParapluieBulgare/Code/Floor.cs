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
    class Floor
    {
        static readonly int nbFloors = 6;  

        private Texture2D texture;
        public int Number { get; set; }

        private List<NPC> npcs;
        private Player player;

        private List<Rectangle> elevators;
        private List<Rectangle> stairs;

        public Floor(Player p, int n, Texture2D t, List<NPC> npcList)
        {
            player = p;
            texture = t;
            Number = n;
          
            npcs = npcList;

            stairs = new List<Rectangle>
            {
                new Rectangle(0, 20, 150, 150),
                new Rectangle(4850, 20, 150, 150)
            };
            elevators = new List<Rectangle> { new Rectangle(2425, 20, 150, 150) };
        }

        public string Update(KeyboardState keyState, KeyboardState prevKeyState)
        {
            foreach (NPC npc in npcs)
            {
                npc.Update(keyState, prevKeyState);
            }
            string key = player.Update(keyState, prevKeyState, npcs);

            if (key != "")
            {
                foreach (Rectangle rect in stairs)
                {
                    if (rect.Contains(player.Coords))
                    {
                        if (key == "up") return "stairs up";
                        if (key == "down") return "stairs down";
                    }
                }
                foreach (Rectangle rect in elevators)
                {
                    if (rect.Contains(player.Coords)) return "elevator";
                }
            }
            return "";
        }

        public int Draw(SpriteBatch spriteBatch, int windowWidth)
        {
            int cameraX = player.CameraX(windowWidth);

            Rectangle sourceRectangle = new Rectangle();
            sourceRectangle.Width = texture.Width;
            sourceRectangle.Height = texture.Height / nbFloors;
            sourceRectangle.X = 0;
            sourceRectangle.Y = (nbFloors - Number - 2) * sourceRectangle.Height;

            int ratio = 2 * Game1.HEIGHT / (3 * sourceRectangle.Height);
            int h = sourceRectangle.Height * ratio;
            int w = sourceRectangle.Width * ratio;
            spriteBatch.Draw(texture, new Rectangle(-cameraX, 0, w, h), sourceRectangle, Color.White);

            //foreach (Rectangle rect in elevators)
            //{
            //    spriteBatch.Draw(texture, new Rectangle(rect.X - cameraX, rect.Y, rect.Width, rect.Height), Color.Yellow);
            //}
            //foreach (Rectangle rect in stairs)
            //{
            //    spriteBatch.Draw(texture, new Rectangle(rect.X - cameraX, rect.Y, rect.Width, rect.Height), Color.DarkBlue);
            //}

            foreach (NPC npc in npcs)
            {
                npc.Draw(spriteBatch, cameraX);
            }
            player.Draw(spriteBatch, cameraX);

            return cameraX;
        }
        
    }
}
