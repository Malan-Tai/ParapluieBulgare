using System;
using System.Collections.Generic;
using System.Text;

namespace ParapluieBulgare.Code
{
    class DialogTree
    {
        private List<DialogBox> conversation;
        private int curLine = 0;

        public DialogTree(List<DialogBox> conv)
        {
            conversation = conv;
            conversation.Add(new DialogBox("...", null, true));
        }

        public DialogBox Next()
        {
            DialogBox box = conversation[curLine];
            curLine++;

            if (curLine >= conversation.Count)
            {
                curLine = 0;
            }

            return box;
        }

    }
}
