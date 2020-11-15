using System;
using System.Collections.Generic;
using System.Text;

namespace ParapluieBulgare.Code
{
    enum HintsEnum
    {
        CibleChercheur,
        BadgeLabo,
        VaccinJM0T4,
        PrenomCamille,
        DoloresDemandee,
        DoloresSecretaire,
        BureauPatron,
        RespoProjet,
        ChatBerlioz,
        HappinessManager,
        AtelierCuisine,
        FichierPhysique,
        HintsCount
    }

    class Hint
    {
        public string Text { get; set; }
        public bool AddedToJournal { get; set; } = false;
        public HintsEnum HintType { get; set; }

        public Hint(HintsEnum hint)
        {
            HintType = hint;
            switch (hint)
            {
                case HintsEnum.CibleChercheur:
                    Text = "La cible est un ou une chercheuse";
                    break;
                case HintsEnum.BadgeLabo:
                    Text = "J'ai acces au laboratoire";
                    break;
                default:
                    break;
            }
        }
    }
}
