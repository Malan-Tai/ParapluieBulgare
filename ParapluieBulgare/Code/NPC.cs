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
    class NPC
    {
        private int x;
        private Texture2D texture;

        public NPC(Texture2D t)
        {
            x = 500;
            texture = t;
        }

        public void Update()
        {
            x -= 1;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, new Rectangle(x, 50, 100, 100), Color.Black);
        }
    }
}
