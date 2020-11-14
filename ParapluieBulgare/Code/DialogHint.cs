using System;
using System.Collections.Generic;
using System.Text;

namespace ParapluieBulgare.Code
{
    class DialogHint : Hint
    {
        public string Sentence { get; set; }

        public DialogHint(string text, string sentence) : base(text)
        {
            Sentence = sentence;
        }
    }
}
