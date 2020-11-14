using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace ParapluieBulgare.Code
{
    class JournalGUI
    {
        private List<Hint> hints;
        private bool open = false;

        public JournalGUI()
        {
            hints = new List<Hint>();
        }

        public void AddHint(Hint hint)
        {
            hints.Add(hint);
        }

        public void Toggle()
        {
            open = !open;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (open)
            {

            }
        }
    }
}
