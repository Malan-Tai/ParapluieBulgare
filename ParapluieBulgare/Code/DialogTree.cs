using System;
using System.Collections.Generic;
using System.Text;

namespace ParapluieBulgare.Code
{
    class DialogTree
    {
        private List<DialogBox> baseConversation;
        private List<HintsEnum> ConditionsNeeded;
        private List<DialogBox> unlockedConversation;
        private int curLine = 0;

        private List<DialogBox> curConversation;

        public DialogTree(List<DialogBox> conv, List<HintsEnum> cond = null, List<DialogBox> unlockConv = null)
        {
            baseConversation = conv;
            baseConversation.Add(new DialogBox("...", null, true));

            ConditionsNeeded = cond;
            unlockedConversation = unlockConv;
            if (unlockConv != null) unlockedConversation.Add(new DialogBox("...", null, true));

            curConversation = baseConversation;
        }

        public void StartConversation(Player player)
        {
            curConversation = baseConversation;
            if (unlockedConversation != null && player.CheckHints(ConditionsNeeded))
            {
                curConversation = unlockedConversation;
            }
        }

        public DialogBox Next()
        {
            DialogBox box = curConversation[curLine];
            curLine++;

            if (curLine >= curConversation.Count)
            {
                curLine = 0;
            }

            return box;
        }

    }
}
