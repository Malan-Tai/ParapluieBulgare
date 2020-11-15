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
        public static int width;
        public static int height;


        private Texture2D texture;
        public int Number { get; set; }

        private List<NPC> npcs;
        private List<Furniture> furnitures;
        private Player player;

        private List<Rectangle> elevators;
        private List<Rectangle> stairs;

        public Floor(Player p, int n, Texture2D t, List<NPC> npcList, List<Furniture> furn)
        {
            player = p;
            texture = t;
            Number = n;
          
            npcs = npcList;
            furnitures = furn;

            int ratio = 2 * Game1.HEIGHT / (3 * 47);

            if (n == -1 || n == 4)
            {
                stairs = new List<Rectangle>();
            }
            else
            {
                stairs = new List<Rectangle>
                {
                    new Rectangle(0, 0, 33 * ratio, 47 * ratio),
                    new Rectangle((467 - 54) * ratio, 0, 54 * ratio, 47 * ratio)
                };
            }
            elevators = new List<Rectangle> { new Rectangle(182 * ratio, (47 - 32) * ratio, 32 * ratio, 32 * ratio) };
        }

        public string Update(KeyboardState keyState, KeyboardState prevKeyState, List<Guard> guards)
        {
            foreach (Furniture furniture in furnitures)
            {
                furniture.Update(keyState, prevKeyState);
            }
            foreach (NPC npc in npcs)
            {
                npc.Update(keyState, prevKeyState);
            }
            foreach (Guard guard in guards)
            {
                guard.Update(keyState, prevKeyState, player, Number);
            }
            string key = player.Update(keyState, prevKeyState, npcs, guards, Number);

            if (key != "")
            {
                foreach (Rectangle rect in stairs)
                {
                    if (rect.Intersects(player.BoxCollider))
                    {
                        if (key == "up") return "stairs up";
                        if (key == "down") return "stairs down";
                    }
                }
                foreach (Rectangle rect in elevators)
                {
                    if (rect.Intersects(player.BoxCollider)) return "elevator";
                }
            }
            return "";
        }

        public void Draw(SpriteBatch spriteBatch, int windowWidth, List<Guard> guards)
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
            width = w;
            height = h;
            spriteBatch.Draw(texture, new Rectangle(-cameraX, 0, w, h), sourceRectangle, Color.White);

            //foreach (Rectangle rect in elevators)
            //{
            //    spriteBatch.Draw(Game1.white, new Rectangle(rect.X - cameraX, rect.Y, rect.Width, rect.Height), new Color(255, 0, 0, 20));
            //}
            //foreach (Rectangle rect in stairs)
            //{
            //    spriteBatch.Draw(Game1.white, new Rectangle(rect.X - cameraX, rect.Y, rect.Width, rect.Height), new Color(255, 0, 0, 20));
            //}

            foreach (Furniture furniture in furnitures)
            {
                furniture.Draw(spriteBatch, cameraX);
            }
            foreach (NPC npc in npcs)
            {
                npc.Draw(spriteBatch, cameraX);
            }
            foreach (Guard guard in guards)
            {
                guard.Draw(spriteBatch, cameraX, Number);
            }
            player.Draw(spriteBatch, cameraX);
        }

        public void DrawFractionTowardsTop(SpriteBatch spriteBatch,  int frame, bool leaving)
        {
            int cameraX = player.CameraX(Game1.WIDTH);
            float frameRatio = frame / 120.0f;
            frameRatio = 1 - frameRatio;

            Rectangle sourceRectangle = new Rectangle();
            sourceRectangle.Width = texture.Width;
            sourceRectangle.X = 0;

            sourceRectangle.Height = texture.Height / nbFloors;
            sourceRectangle.Y = (nbFloors - Number - 2) * sourceRectangle.Height;

            int y = 0;
            if (leaving)
            {
                int hiddenHeight = (int)(sourceRectangle.Height * frameRatio);
                y = hiddenHeight;
            }
            else
            {
                int hiddenHeight = (int)(sourceRectangle.Height * (1 - frameRatio));
                y = -hiddenHeight;
            }

            int ratio = 2 * Game1.HEIGHT / (3 * sourceRectangle.Height);
            int h = sourceRectangle.Height * ratio;
            int w = sourceRectangle.Width * ratio;
            width = w;
            height = h;
            spriteBatch.Draw(texture, new Rectangle(-cameraX, y * ratio, w, h), sourceRectangle, Color.White);
        }

        public void DrawFractionTowardsBottom(SpriteBatch spriteBatch, int frame, bool leaving)
        {
            int cameraX = player.CameraX(Game1.WIDTH);
            float frameRatio = frame / 120.0f;

            Rectangle sourceRectangle = new Rectangle();
            sourceRectangle.Width = texture.Width;
            sourceRectangle.X = 0;

            sourceRectangle.Height = texture.Height / nbFloors;
            sourceRectangle.Y = (nbFloors - Number - 2) * sourceRectangle.Height;

            int y = 0;
            if (leaving)
            {
                int hiddenHeight = (int)(sourceRectangle.Height * (1 - frameRatio));
                y = -hiddenHeight;
            }
            else
            {
                int hiddenHeight = (int)(sourceRectangle.Height * frameRatio);
                y = hiddenHeight;
            }

            int ratio = 2 * Game1.HEIGHT / (3 * sourceRectangle.Height);
            int h = sourceRectangle.Height * ratio;
            int w = sourceRectangle.Width * ratio;
            width = w;
            height = h;
            spriteBatch.Draw(texture, new Rectangle(-cameraX, y * ratio, w, h), sourceRectangle, Color.White);
        }

    }
}
