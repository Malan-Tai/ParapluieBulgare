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
                //int ratio = Game1.HEIGHT / texture.Height;
                int height = Game1.HEIGHT - 20; //texture.Height * ratio;
                int width = Game1.WIDTH - 20; //texture.Width * ratio;
                int x = (Game1.WIDTH - width) / 2;
                int y = 10;

                spriteBatch.Draw(texture, new Rectangle(x, y, width, height), Color.White);

                Rectangle textRect1 = new Rectangle(50, 80, (width - 150) / 2, height - 110);
                Rectangle textRect2 = new Rectangle(width / 2 + 70, 80, (width - 150) / 2, height - 110);
                Rectangle curRect = textRect1;

                int h = (int)font.MeasureString("A").Y;
                y = curRect.Y;

                foreach (Hint hint in hints)
                {
                    List<string> lines = TextWrap.Wrap(hint.Text, curRect.Width, font);
                    foreach (string line in lines)
                    {
                        spriteBatch.DrawString(font, line, new Vector2(curRect.X, y), Color.Black);
                        y += 2 * h;
                        if (y > curRect.Y + curRect.Height)
                        {
                            curRect = textRect2;
                            y = curRect.Y;
                        }
                    }
                }
            }
        }
    }
}
