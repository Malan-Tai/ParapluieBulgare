using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ParapluieBulgare.Code
{
    class NPC : Character
    {

        public NPC(Texture2D t)
        {
            x = 500;
            y = 50;
            texture = t;
        }

        public void Update()
        {
            x -= 1;
        }

        public void Draw(SpriteBatch spriteBatch, int cameraX)
        {
            spriteBatch.Draw(texture, new Rectangle(x - cameraX, y, width, width), Color.Black);
            DrawDialog(spriteBatch, cameraX);
        }
    }
}
