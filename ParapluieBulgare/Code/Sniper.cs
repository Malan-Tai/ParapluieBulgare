using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ParapluieBulgare.Code
{
    class Sniper
    {
        private int x;
        private int y;
        private int width;
        private Texture2D texture;

        private float speed = 3.0f;

        private Rectangle BoxCollider
        {
            get
            {
                return new Rectangle(x - width / 2, y - width / 2, width, width);
            }
        }

        public Sniper(Texture2D t)
        {
            texture = t;
            x = 0;
            y = 0;
            width = 30;
        }

        public void SwitchFloor(int oldFloor, int newFloor)
        {
            y += (newFloor - oldFloor) * 2 * Game1.HEIGHT / 3;
        }

        public void Update(Player player)
        {
            int ratio = 2 * Game1.HEIGHT / (3 * 47);
            int headOffset = 15 * ratio;
            int dx = player.Coords.X + headOffset - x;
            int dy = player.Coords.Y + 10 - y;
            Vector2 velocity = new Vector2(dx, dy) * speed / (float)Math.Sqrt(dx * dx + dy * dy);
            x += (int)Math.Ceiling(velocity.X);
            y += (int)Math.Ceiling(velocity.Y);

            if (BoxCollider.Contains(player.Coords.X + headOffset, player.Coords.Y + 10)) //if (BoxCollider.Intersects(player.BoxCollider))
            {
                Game1.Lose = true;
            }
        }

        public void Draw(SpriteBatch spriteBatch, int cameraX)
        {
            spriteBatch.Draw(texture, new Rectangle(x - cameraX - width / 2, y - width / 2, width, width), Color.Red);
        }
    }
}
