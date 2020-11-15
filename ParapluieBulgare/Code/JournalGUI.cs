using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ParapluieBulgare.Code
{
    class JournalGUI
    {
        private List<Hint> hints;
        private bool open = false;

        private Texture2D texture;

        public JournalGUI(Texture2D texture)
        {
            this.texture = texture;
            hints = new List<Hint>();
        }

        public bool CheckHints(List<HintsEnum> condition)
        {
            int found = 0;
            foreach (Hint hint in hints)
            {
                if (condition.Contains(hint.HintType)) found++;
            }

            return found == condition.Count;
        }

        public void AddHint(Hint hint)
        {
            hints.Add(hint);
        }

        public void Toggle()
        {
            open = !open;
        }

        public void Draw(SpriteBatch spriteBatch, SpriteFont font)
        {
            if (open)
            {
                spriteBatch.Draw(texture, new Rectangle(20, 20, 700, 300), Color.BlanchedAlmond);

                int h = (int)font.MeasureString("A").Y;
                int y = h;

                foreach (Hint hint in hints)
                {
                    spriteBatch.DrawString(font, hint.Text, new Vector2(40, y), Color.Black);
                    y += h;
                }
            }
        }
    }
}
