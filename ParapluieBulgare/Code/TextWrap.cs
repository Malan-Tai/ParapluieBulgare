using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace ParapluieBulgare.Code
{
    static class TextWrap
    {
        public static List<string> Wrap(string text, int width, SpriteFont font)
        {
            List<string> list = new List<string>();
            if (text == null) return list;

            string[] words = text.Split(new char[] { ' ' });

            int curWidth = 0;
            string curText = "";

            foreach (string word in words)
            {
                curWidth += (int)font.MeasureString(word + " ").X;
                if (curWidth > width)
                {
                    list.Add(curText);
                    curText = word;
                    curWidth = (int)font.MeasureString(word + " ").X;
                }
                else
                {
                    curText += word + " ";
                }
            }
            list.Add(curText);

            return list;
        }
    }
}
