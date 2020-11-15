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
        //PrenomSophie,
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
                    Text = "La cible est la personne dirigeant un projet de vaccin";
                    break;
                case HintsEnum.BadgeLabo:
                    Text = "J'ai acces au laboratoire";
                    break;
                case HintsEnum.VaccinJM0T4:
                    Text = "Le projet de vaccin est code JM0-T4";
                    break;
                //case HintsEnum.PrenomSophie:
                //    Text = "";
                //    break;
                case HintsEnum.DoloresDemandee:
                    Text = "Une certaine Dolores est demandee a la cafet";
                    break;
                case HintsEnum.DoloresSecretaire:
                    Text = "La secretaire du patron s'appelle Dolores";
                    break;
                case HintsEnum.BureauPatron:
                    Text = "J'ai acces au bureau du patron";
                    break;
                case HintsEnum.RespoProjet:
                    Text = "L'ordinateur du patron contient quatre codes et directeurs de projets";
                    break;
                case HintsEnum.ChatBerlioz:
                    Text = "Le patron aime enormement son chat Berlioz";
                    break;
                case HintsEnum.HappinessManager:
                    Text = "Il y a un nouveau happiness manager";
                    break;
                case HintsEnum.AtelierCuisine:
                    Text = "Le happiness manager organise des ateliers cuisine";
                    break;
                case HintsEnum.FichierPhysique:
                    Text = "Le serveur contient des donnees physiques sur les employes";
                    break;
                default:
                    break;
            }
        }
    }
}
