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
        private Texture2D texture;
        private int number;

        private List<NPC> npcs;

        public Floor(int n, Texture2D t)
        {
            texture = t;
            number = n;

            npcs = new List<NPC>
            {
                new NPC(t)
            };
        }

        public void Update()
        {
            foreach (NPC npc in npcs)
            {
                npc.Update();
            }
        }

        public void Draw(SpriteBatch spriteBatch, int cameraX)
        {
            spriteBatch.Draw(texture, new Rectangle(-cameraX, 0, 1000, 500), Color.LightBlue);

            foreach (NPC npc in npcs)
            {
                npc.Draw(spriteBatch, cameraX);
            }
        }
        
    }
}
