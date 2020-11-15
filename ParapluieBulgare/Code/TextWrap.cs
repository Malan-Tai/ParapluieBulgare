using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace ParapluieBulgare.Code
{
    static class TextWrap
    {
        //public static List<string> Wrap(string text, int width, SpriteFont font)
        //{
        //    int charWidth = (int)font.MeasureString("M").X;

        //    int MaxCharsPerLine = width / charWidth;
        //    int MaxLines = 3;
        //    var pages = new List<string>();

        //    var capacity = MaxCharsPerLine * MaxLines > text.Length ? text.Length : MaxCharsPerLine * MaxLines;

        //    var result = new StringBuilder(capacity);
        //    var resultLines = 0;

        //    var currentWord = new StringBuilder();
        //    var currentLine = new StringBuilder();

        //    for (var i = 0; i < text.Length; i++)
        //    {
        //        var currentChar = text[i];
        //        var isNewLine = text[i] == '\n';
        //        var isLastChar = i == text.Length - 1;

        //        currentWord.Append(currentChar);

        //        if (char.IsWhiteSpace(currentChar) || isLastChar)
        //        {
        //            var potentialLength = currentLine.Length + currentWord.Length;

        //            if (potentialLength > MaxCharsPerLine)
        //            {
        //                result.AppendLine(currentLine.ToString());

        //                currentLine.Clear();

        //                resultLines++;
        //            }

        //            currentLine.Append(currentWord);

        //            currentWord.Clear();

        //            if (isLastChar || isNewLine)
        //            {
        //                result.AppendLine(currentLine.ToString());
        //            }

        //            if (resultLines > MaxLines || isLastChar || isNewLine)
        //            {
        //                pages.Add(result.ToString());

        //                result.Clear();

        //                resultLines = 0;

        //                if (isNewLine)
        //                {
        //                    currentLine.Clear();
        //                }
        //            }
        //        }
        //    }

        //    return pages;
        //}

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
