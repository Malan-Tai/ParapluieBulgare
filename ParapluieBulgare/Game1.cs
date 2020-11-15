using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using ParapluieBulgare.Code;

namespace ParapluieBulgare
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        public static int WIDTH = 1280;
        public static int HEIGHT = 720;

        Song music;

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        KeyboardState prevKeyState;

        List<string> allTextures = new List<string> 
        {
            "white",
            "bulleDeTexte",
            "background",
            "ascenseur",
            "carnet",
            "transparent",
            "TitleScreen",
            "Viseur",

            "persos/Cadres/Cadre1",
            "persos/Cadres/Cadre2",
            "persos/Cadres/Cadre3",
            "persos/Cadres/Cadre4",
            "persos/Cadres/Secretaire",
            "persos/Cadres/Secretaire_Walk",

            "persos/Chercheuses/Chercheur1",
            "persos/Chercheuses/Chercheur2",
            "persos/Chercheuses/Chercheuse1",
            "persos/Chercheuses/Chercheuse2",

            "persos/Cuisinier/Cuistot1",
            "persos/Cuisinier/Cuistot2",
            "persos/Cuisinier/Cuistot3",

            "persos/Direction/Directeur1",
            "persos/Direction/Directeur2",
            "persos/Direction/Directeur3",
            "persos/Direction/Directeur4",

            "persos/Employees/Employe1",
            "persos/Employees/Employe2",
            "persos/Employees/Employe3",
            "persos/Employees/Employe4",
            "persos/Employees/Employe5",
            "persos/Employees/Employe6",
            "persos/Employees/Employe7",
            "persos/Employees/Employe8",

            "persos/Joueur/JoueurIdle",
            "persos/Joueur/JoueurWalk",

            "persos/Journalistes/Journaliste_IDLE",

            "persos/Techniciens/Techos1",
            "persos/Techniciens/Techos2",
            "persos/Techniciens/Techos3",
            "persos/Techniciens/Techos4",

            "persos/Vigiles/VigileIdle",
            "persos/Vigiles/VigileWalk",

            "Monitor"

        };
        List<string> allSounds = new List<string>
        {
            "Bulgared",
            "Intro"
        };
        Dictionary<string, Texture2D> textureDict;

        List<string> allFaces = new List<string>
        {
            "persos/Cadres/Cadre1Tronche",
            "persos/Cadres/Cadre2Tronche",
            "persos/Cadres/Cadre3Tronche",
            "persos/Cadres/Cadre4Tronche",
            "persos/Cadres/SecretaireTronche",

            "persos/Chercheuses/Chercheur1Tronche",
            "persos/Chercheuses/Chercheur2Tronche",
            "persos/Chercheuses/Chercheuse1Tronche",
            "persos/Chercheuses/Chercheuse2Tronche",

            "persos/Cuisinier/Cuistot1Tronche",
            "persos/Cuisinier/Cuistot2Tronche",
            "persos/Cuisinier/Cuistot3Tronche",

            "persos/Direction/Directeur1Tronche",
            "persos/Direction/Directeur2Tronche",
            "persos/Direction/Directeur4Tronche",

            "persos/Employees/Employe1Tronche",
            "persos/Employees/Employe2Tronche",
            "persos/Employees/Employe3Tronche",
            "persos/Employees/Employe4Tronche",
            "persos/Employees/Employe5Tronche",
            "persos/Employees/Employe6Tronche",
            "persos/Employees/Employe7Tronche",
            "persos/Employees/Employe8Tronche",

            "persos/Joueur/JoueurTronche",

            "persos/Journalistes/JournalisteTronche",

            "persos/Techniciens/Techos1Tronche",
            "persos/Techniciens/Techos2Tronche",
            "persos/Techniciens/Techos3Tronche",
            "persos/Techniciens/Techos4Tronche",

            "persos/Vigiles/VigileTronche",

            "Badge",
            "Docu_Vaccin",
            "Serveur",
            "PC_Boss"
        };
        Dictionary<string, Texture2D> facebook;

        Texture2D white;

        SpriteFont font;

        Player player;
        Sniper sniper;

        Floor[] floors;
        Floor currentFloor;
        List<Guard> guards;

        Timer timer;

        bool elevator = false;
        bool switchFloorAnimation = false;
        int curFloorAnimation = 0;
        int leavingFloor = 0;
        int nextFloor = 0;
        ElevatorGUI elevatorGUI = null;

        Dictionary<string, SoundEffect> soundDict;



        public static int ThreatLevel = 0;
        public static bool Win = false;
        public static bool Lose = false;

        private bool isPlayingIntro = true;

        SoundEffectInstance audioIntroInstance;

        public Game1()
        {
            textureDict = new Dictionary<string, Texture2D>();
            facebook = new Dictionary<string, Texture2D>();
            soundDict = new Dictionary<string, SoundEffect>();


            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferWidth = WIDTH;
            graphics.PreferredBackBufferHeight = HEIGHT;
            graphics.ApplyChanges();
            graphics.PreferredBackBufferWidth = WIDTH;
            graphics.PreferredBackBufferHeight = HEIGHT;
            graphics.ApplyChanges();
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            base.Initialize();

            Character.font = font;
            DialogBox.font = font;
            DialogBox.dialogBoxTexture = textureDict["bulleDeTexte"];
            DialogBox.boxRect = new Rectangle(0, 0, WIDTH - (HEIGHT / 3), HEIGHT / 3 + 30); //(int)(0.261 * (WIDTH - (HEIGHT / 3))));

            player = new Player(GetAnimation("joueur_idle"), GetAnimation("joueur_walk"), GetTronche("joueur"), textureDict["carnet"]);

            guards = new List<Guard>
            {
                new Guard(GetAnimation("vigile_idle"), GetAnimation("vigile_walk"), GetTronche("vigile"), 0, 1500),
                new Guard(GetAnimation("vigile_idle"), GetAnimation("vigile_walk"), GetTronche("vigile"), 2, 300),
                new Guard(GetAnimation("vigile_idle"), GetAnimation("vigile_walk"), GetTronche("vigile"), 2, 1450)
            };
            Guard guard = guards[1];
            guard.SetUnlockingConditions(new List<HintsEnum> { HintsEnum.BadgeLabo });
            DialogBox b1 = new DialogBox("Desole monsieur, vous n'avez pas acces au laboratoire.", guard);
            DialogTree t = new DialogTree(new List<DialogBox> { b1 });
            guard.SetDialogTree(t);
            guard = guards[2];
            guard.SetUnlockingConditions(new List<HintsEnum> { HintsEnum.BadgeLabo });
            b1 = new DialogBox("Desole monsieur, vous n'avez pas acces au laboratoire.", guard);
            t = new DialogTree(new List<DialogBox> { b1 });
            guard.SetDialogTree(t);

            guards[2].Flip = true;

            floors = new Floor[]
            {
                new Floor(player, -1, textureDict["background"], GetFloorNPCs(-1), GetFloorFurnitures(-1)),
                new Floor(player, 0, textureDict["background"], GetFloorNPCs(0), GetFloorFurnitures(0)),
                new Floor(player, 1, textureDict["background"], GetFloorNPCs(1), GetFloorFurnitures(1)),
                new Floor(player, 2, textureDict["background"], GetFloorNPCs(2), GetFloorFurnitures(2)),
                new Floor(player, 3, textureDict["background"], GetFloorNPCs(3), GetFloorFurnitures(3)),
                new Floor(player, 4, textureDict["background"], GetFloorNPCs(4), GetFloorFurnitures(4))
            };
            currentFloor = floors[1];

            timer = new Timer(600);

            NPC.soundBulgared = soundDict["Bulgared"];

            //jouer l'intro
            audioIntroInstance = soundDict["Intro"].CreateInstance();
            audioIntroInstance.Play();
            isPlayingIntro = true;
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            font = Content.Load<SpriteFont>("font");

            foreach (string text in allTextures)
            {
                textureDict.Add(text, Content.Load<Texture2D>(text));
            }
            foreach (string text in allFaces)
            {
                facebook.Add(text, Content.Load<Texture2D>(text));
            }
            foreach (string text in allSounds)
            {
                Console.WriteLine(text);
                soundDict.Add(text, Content.Load<SoundEffect>(text));
            }
            white = textureDict["white"];

            music = Content.Load<Song>("Title");
        }

        public Texture2D GetTronche(string spritesheet)
        {
            switch (spritesheet)
            {
                case "cadre1":
                    return facebook["persos/Cadres/Cadre1Tronche"];
                case "cadre2":
                    return facebook["persos/Cadres/Cadre2Tronche"];
                case "cadre3":
                    return facebook["persos/Cadres/Cadre3Tronche"];
                case "cadre4":
                    return facebook["persos/Cadres/Cadre4Tronche"];
                case "secretaire":
                    return facebook["persos/Cadres/SecretaireTronche"];

                case "chercheur1":
                    return facebook["persos/Chercheuses/Chercheur1Tronche"];
                case "chercheuse1":
                    return facebook["persos/Chercheuses/Chercheuse1Tronche"];
                case "chercheur2":
                    return facebook["persos/Chercheuses/Chercheur2Tronche"];
                case "chercheuse2":
                    return facebook["persos/Chercheuses/Chercheuse2Tronche"];

                case "cuistot1":
                    return facebook["persos/Cuisinier/Cuistot1Tronche"];
                case "cuistot2":
                    return facebook["persos/Cuisinier/Cuistot2Tronche"];
                case "cuistot3":
                    return facebook["persos/Cuisinier/Cuistot2Tronche"];

                case "directeur1":
                    return facebook["persos/Direction/Directeur1Tronche"];
                case "directeur2":
                    return facebook["persos/Direction/Directeur2Tronche"];
                case "directeur4":
                    return facebook["persos/Direction/Directeur4Tronche"];

                case "employe1":
                    return facebook["persos/Employees/Employe1Tronche"];
                case "employe2":
                    return facebook["persos/Employees/Employe2Tronche"];
                case "employe3":
                    return facebook["persos/Employees/Employe3Tronche"];
                case "employe4":
                    return facebook["persos/Employees/Employe4Tronche"];
                case "employe5":
                    return facebook["persos/Employees/Employe5Tronche"];
                case "employe6":
                    return facebook["persos/Employees/Employe6Tronche"];
                case "employe7":
                    return facebook["persos/Employees/Employe7Tronche"];
                case "employe8":
                    return facebook["persos/Employees/Employe8Tronche"];

                case "joueur":
                    return facebook["persos/Joueur/JoueurTronche"];

                case "journaliste":
                    return facebook["persos/Journalistes/JournalisteTronche"];

                case "techos1":
                    return facebook["persos/Techniciens/Techos1Tronche"];
                case "techos2":
                    return facebook["persos/Techniciens/Techos2Tronche"];
                case "techos3":
                    return facebook["persos/Techniciens/Techos3Tronche"];
                case "techos4":
                    return facebook["persos/Techniciens/Techos4Tronche"];

                case "vigile":
                    return facebook["persos/Vigiles/VigileTronche"];

                default:
                    return null;
            }
        }
        private Animation GetAnimation(string spritesheet)
        {
            switch (spritesheet)
            {
                case "transparent":
                    return new Animation(textureDict["transparent"], 32, 32, 1, 60);
              
                case "cadre1":
                    return new Animation(textureDict["persos/Cadres/Cadre1"], 32, 32, 2, 60);
                case "cadre2":
                    return new Animation(textureDict["persos/Cadres/Cadre2"], 32, 32, 2, 60);
                case "cadre3":
                    return new Animation(textureDict["persos/Cadres/Cadre3"], 32, 32, 2, 60);
                case "cadre4":
                    return new Animation(textureDict["persos/Cadres/Cadre4"], 32, 32, 2, 60);
                case "secretaire":
                    return new Animation(textureDict["persos/Cadres/Secretaire"], 32, 32, 2, 60);
                case "secretaire_walk":
                    return new Animation(textureDict["persos/Cadres/Secretaire_Walk"], 32, 32, 7, 10);


                case "chercheur1":
                    return new Animation(textureDict["persos/Chercheuses/Chercheur1"], 32, 32, 2, 60);
                case "chercheur2":
                    return new Animation(textureDict["persos/Chercheuses/Chercheur2"], 32, 32, 2, 60);
                case "chercheuse1":
                    return new Animation(textureDict["persos/Chercheuses/Chercheuse1"], 32, 32, 2, 60);
                case "chercheuse2":
                    return new Animation(textureDict["persos/Chercheuses/Chercheuse2"], 32, 32, 2, 60);


                case "cuistot1":
                    return new Animation(textureDict["persos/Cuisinier/Cuistot1"], 32, 32, 2, 60);
                case "cuistot2":
                    return new Animation(textureDict["persos/Cuisinier/Cuistot2"], 32, 32, 2, 60);
                case "cuistot3":
                    return new Animation(textureDict["persos/Cuisinier/Cuistot1"], 32, 32, 2, 60);


                case "directeur1":
                    return new Animation(textureDict["persos/Direction/Directeur1"], 32, 32, 2, 60);
                case "directeur2":
                    return new Animation(textureDict["persos/Direction/Directeur2"], 32, 32, 7, 10);
                case "directeur3":
                    return new Animation(textureDict["persos/Direction/Directeur3"], 32, 32, 2, 60);
                case "directeur4":
                    return new Animation(textureDict["persos/Direction/Directeur4"], 32, 32, 2, 60);


                case "employe1":
                    return new Animation(textureDict["persos/Employees/Employe1"], 32, 32, 2, 60);
                case "employe2":
                    return new Animation(textureDict["persos/Employees/Employe2"], 32, 32, 2, 60);
                case "employe3":
                    return new Animation(textureDict["persos/Employees/Employe3"], 32, 32, 2, 60);
                case "employe4":
                    return new Animation(textureDict["persos/Employees/Employe4"], 32, 32, 2, 60);
                case "employe5":
                    return new Animation(textureDict["persos/Employees/Employe5"], 32, 32, 2, 60);
                case "employe6":
                    return new Animation(textureDict["persos/Employees/Employe6"], 32, 32, 2, 60);
                case "employe7":
                    return new Animation(textureDict["persos/Employees/Employe7"], 32, 32, 2, 60);
                case "employe8":
                    return new Animation(textureDict["persos/Employees/Employe8"], 32, 32, 2, 60);


                case "joueur_idle":
                    return new Animation(textureDict["persos/Joueur/JoueurIdle"], 32, 32, 2, 60);
                case "joueur_walk":
                    return new Animation(textureDict["persos/Joueur/JoueurWalk"], 32, 32, 7, 10);


                case "journaliste":
                    return new Animation(textureDict["persos/Journalistes/Journaliste_IDLE"], 32, 32, 2, 60);


                case "techos1":
                    return new Animation(textureDict["persos/Techniciens/Techos1"], 32, 32, 2, 60);
                case "techos2":
                    return new Animation(textureDict["persos/Techniciens/Techos2"], 32, 32, 2, 60);
                case "techos3":
                    return new Animation(textureDict["persos/Techniciens/Techos3"], 32, 32, 2, 60);
                case "techos4":
                    return new Animation(textureDict["persos/Techniciens/Techos4"], 32, 32, 2, 60);


                case "vigile_idle":
                    return new Animation(textureDict["persos/Vigiles/VigileIdle"], 32, 32, 2, 60);
                case "vigile_walk":
                    return new Animation(textureDict["persos/Vigiles/VigileWalk"], 32, 32, 8, 10);

                case "moniteur":
                    return new Animation(textureDict["Monitor"], 32, 32, 8, 10);

                default:
                    return null;
            }
        }

        private List<NPC> GetFloorNPCs(int floor)
        {
            List<NPC> npcs = new List<NPC>();

            switch (floor)
            {
                case -1:
                    npcs = new List<NPC>
                    {
                        new NPC(GetAnimation("techos4"), GetAnimation("techos4"), GetTronche("techos4"), 700),
                        new NPC(GetAnimation("techos2"), GetAnimation("techos2"), GetTronche("techos2"), 1000),
                    };

                    npcs[0].Flip = true;

                    DialogBox b1 = new DialogBox("... devrait pas avoir le droit de nous faire travailler autant !", npcs[0]);
                    DialogBox b2 = new DialogBox("C'est vrai, mais le syndicat a reussi a obtenir plus de budget pour organiser des activites bien-etre. C'est un premier pas.", npcs[1]);
                    DialogBox b3 = new DialogBox("De la poudre aux yeux moi je te dis !", npcs[0]);
                    DialogBox b4 = new DialogBox("Peut-etre mais c'est un premier pas. Tu devrais assister a l'atelier cuisine, c'est assez cool comme ambiance. Meme les chefs de projet semblent plus '.sympathiques' !", npcs[1], false, HintsEnum.AtelierCuisine);
                    DialogBox b5 = new DialogBox("Mouais... J'irai y jeter un oeil... Mais le combat n'est pas fini !", npcs[0]);
                    DialogTree tree = new DialogTree(new List<DialogBox> { b1, b2, b3, b4, b5 });
                    npcs[1].SetDialogTree(tree);

                    break;
                case 0:
                    npcs = new List<NPC>
                    {
                        new NPC(GetAnimation("employe1"), GetAnimation("employe1"), GetTronche("employe1"), 600),
                        new NPC(GetAnimation("journaliste"), GetAnimation("journaliste"), GetTronche("journaliste"), 1300),

                        new NPC(GetAnimation("cadre1"), GetAnimation("cadre1"), GetTronche("cadre1"), 2400),
                        new NPC(GetAnimation("employe2"), GetAnimation("employe2"), GetTronche("employe2"), 3200),
                        new NPC(GetAnimation("employe3"), GetAnimation("employe3"), GetTronche("employe3"), 3550)
                    };

                    npcs[0].Flip = true;
                    npcs[1].Flip = true;
                    npcs[3].Flip = true;

                    b1 = new DialogBox("Non Monsieur, vous ne passerez pas ! Vous ne disposez d'aucune autorisation !", guards[0]);
                    b2 = new DialogBox("Vous ne pouvez pas faire ca ! C'est contraire a la liberte de la presse ! Des choses se passent ici et nous le saurons, soyez-en sur !", npcs[1]);
                    b3 = new DialogBox("Oui oui c'est ca, c'est ca. Partez ou je serai contraint d'appeler la police.", guards[0]);
                    b4 = new DialogBox("Ouais c'est ca allez-y, appelez les flics ! Histoire qu'ils decouvrent vos petites manigances. On sait ce que vous cachez sur vos serveurs !", npcs[1]);
                    b5 = new DialogBox("Bien sur, et moi je suis reptilien...", guards[0]);
                    DialogBox b6 = new DialogBox("Moquez-vous !", npcs[1]);
                    tree = new DialogTree(new List<DialogBox> { b1, b2, b3, b4, b5, b6 });
                    npcs[1].SetDialogTree(tree);

                    b1 = new DialogBox("...  et  la  je  suis  tombee  sur  ce  compte,  regarde.", npcs[4]);
                    b2 = new DialogBox("Oh  nooooon  trop  mignon  !  Mais  il  est  a  qui  ce  petit  bebe  chat  ?", npcs[3]);
                    b3 = new DialogBox("Au  patron  figure-toi  !", npcs[4]);
                    b4 = new DialogBox("Serieux  ??  Je  ne  le  voyais  pas  aussi... 'connecte'.", npcs[3]);
                    b5 = new DialogBox("Ouais  enfin  mate  le  nom  du  chat  :  Berlioz...  C'est  pas  le  blaze  le  plus  tendance  non  plus...  ", npcs[4], false, HintsEnum.ChatBerlioz);
                    b6 = new DialogBox("Eh  !  Ca  reste  un  vieux  schnock  multimillionnaire  apres  tout...", npcs[3]);
                    DialogBox b7 = new DialogBox("Chuuuuut  parle  moins  fort,  y  a  un  type  chelou  qui  nous  observe  !", npcs[4]);
                    DialogBox b8 = new DialogBox("  ...", npcs[3]);
                    tree = new DialogTree(new List<DialogBox> { b1, b2, b3, b4, b5, b6, b7, b8 });
                    npcs[4].SetDialogTree(tree);

                    break;
                case 1:
                    npcs = new List<NPC>
                    {
                        new NPC(GetAnimation("chercheuse1"), GetAnimation("chercheuse1"), GetTronche("chercheuse1"), 400),
                        new NPC(GetAnimation("employe4"), GetAnimation("employe4"), GetTronche("employe4"), 600),
                        new NPC(GetAnimation("cadre2"), GetAnimation("cadre2"), GetTronche("cadre2"), 1100),
                        new NPC(GetAnimation("employe5"), GetAnimation("employe5"), GetTronche("employe2"), 1300),
                        new NPC(GetAnimation("directeur2"), GetAnimation("directeur2"), GetTronche("directeur2"), 2380),

                        new NPC(GetAnimation("techos1"), GetAnimation("techos1"), GetTronche("techos1"), 2800),
                        new NPC(GetAnimation("cuistot1"), GetAnimation("cuistot1"), GetTronche("cuistot1"), 3280),
                        new NPC(GetAnimation("cuistot2"), GetAnimation("cuistot2"), GetTronche("cuistot2"), 3550),
                        new NPC(GetAnimation("cuistot3"), GetAnimation("cuistot3"), GetTronche("cuistot3"), 3850)
                    };

                    npcs[0].Flip = true;
                    npcs[2].Flip = true;
                    npcs[5].Flip = true;
                    npcs[6].Flip = true;

                    npcs[7].Target = true;

                    b1 = new DialogBox("Eh  j't'ai  pas  raconte  mon  rendez-vous  la  semaine  derniere  avec  la  DRH.", npcs[3]);
                    b2 = new DialogBox("Siii  Patrick,  ca  fait  trois  fois  que  tu  nous  la  racontes...  Eh  tu  devrais  pas  t'en  vanter  merde  !  Te  toucher  au  boulot,  sans  rire  ??", npcs[2]);
                    b3 = new DialogBox("Rooh  c'est  bon,  j'explose  tous  les  commerciaux,  j'ai  l'droit  a  mon  moment  detente.", npcs[3]);
                    b4 = new DialogBox("...", npcs[2]);
                    b5 = new DialogBox("Donc  je  disais...  Tu  vois  la  secretaire  du  boss.", npcs[3]);
                    b6 = new DialogBox("Dolores  ?", npcs[2], false, HintsEnum.DoloresSecretaire);
                    b7 = new DialogBox("Ouais  c'est  ca  !  Elle  a  deboule  super  furax  dans  le  bureau.  Soit  disant  que  Sophie  aurait  vu  le  patron  sans  passer  par  elle,  qu'elle  trouvait  ca  intolerable.  Elle  m'a  fait  peur  cette  con  mais  en  vrai  ca  m'a  un  peu  excite...", npcs[3]);
                    b8 = new DialogBox("T'es  vraiment  irrecuperable...  Raah  j'ai  plus  faim  maintenant,  t'es  chiant...  ", npcs[2]);
                    tree = new DialogTree(new List<DialogBox> { b1, b2, b3, b4, b5, b6, b7, b8 });
                    npcs[3].SetDialogTree(tree);

                    break;
                case 2:
                    npcs = new List<NPC>
                    {
                        new NPC(GetAnimation("chercheur1"), GetAnimation("chercheur1"), GetTronche("chercheuse1"), 600),
                        new NPC(GetAnimation("directeur1"), GetAnimation("directeur1"), GetTronche("directeur1"), 1100),
                        new NPC(GetAnimation("chercheuse2"), GetAnimation("chercheuse2"), GetTronche("chercheuse2"), 1300),

                        new NPC(GetAnimation("chercheur2"), GetAnimation("chercheur2"), GetTronche("chercheur2"), 2850),
                        new NPC(GetAnimation("techos3"), GetAnimation("techos3"), GetTronche("techos3"), 3700)
                    };

                    npcs[1].Flip = true;
                    npcs[3].Flip = true;

                    break;
                case 3:
                    npcs = new List<NPC>
                    {
                        new NPC(GetAnimation("employe6"), GetAnimation("employe6"), GetTronche("employe6"), 580),
                        new NPC(GetAnimation("employe7"), GetAnimation("employe7"), GetTronche("employe7"), 900),
                        new NPC(GetAnimation("employe8"), GetAnimation("employe8"), GetTronche("employe8"), 1020),
                        new NPC(GetAnimation("cadre3"), GetAnimation("cadre3"), GetTronche("cadre3"), 2200),
                        new NPC(GetAnimation("cadre4"), GetAnimation("cadre4"), GetTronche("cadre4"), 2400)
                    };

                    npcs[0].Flip = true;
                    npcs[3].Flip = true;

                    b1 = new DialogBox("Eh  Christine,  t'as  vu  Dolores  aujourd'hui  ?", npcs[2], false, HintsEnum.DoloresDemandee);
                    b2 = new DialogBox("Euh...  Nan  je  crois  pas,  elle  est  peut-etre  en  conge.  C'est  important  ?", npcs[1]);
                    b3 = new DialogBox("J'aimerais  bien  prendre  rendez-vous  avec  le  big  boss  et  discuter  tranquillement  promotion  t'vois.", npcs[2]);
                    b4 = new DialogBox("Ah  ouais  au  culot  en  fait.", npcs[1]);
                    b5 = new DialogBox("Faut  bien  s'imposer  ma  bonne  dame.  Bon,  j'appellerai  directement  a  son  bureau  en  fin  de  journee.", npcs[2]);

                    b6 = new DialogBox("Tu  n'as  toujours  pas  teste  les  nouvelles  activites  proposees  par  le  CE  pour  'avoir  le  smile'  ?", npcs[2]);
                    b7 = new DialogBox("Nan  j'ai  pas  vraiment  regarde.", npcs[1]);
                    b8 = new DialogBox("Ca  devrait  t'interesser  pourtant,  c'est  Alicia  qui  s'en  charge.", npcs[2]);
                    DialogBox b9 = new DialogBox("A...  Arrete  !  Tu  sais  tres  bien  que  je  suis  trop  genee  quand  elle  est  la...", npcs[1]);
                    DialogBox b10 = new DialogBox("Fais  gaffe,  tu  vas  te  faire  devancer  par  Camille  hahaha", npcs[2]);
                    DialogBox b11 = new DialogBox("Par...  Camille,  celle  dont  tout  le  monde  parle  !?", npcs[1]);
                    DialogBox b12 = new DialogBox("Oui,  qui  travaille  sur  le  projet  de  super-vaccin,  celle-la  meme.", npcs[2]);
                    DialogBox b13 = new DialogBox("Eh  qu'est-ce  qui  te  fait  croire  qu'elle  est  aussi...  ", npcs[1]);
                    DialogBox b14 = new DialogBox("J'ai  mes  sources  eheheh.  Et  puis,  elle  reserve  toujours  les  creneaux  entiers  ou  Alicia  participe  egalement...  C'est  tellement  crame  !  Encore  aujourd'hui,  elle  a  bloque  sa  journee  pour  faire  l'atelier  cuisine  !", npcs[2], false, HintsEnum.AtelierCuisine);

                    tree = new DialogTree(new List<DialogBox> { b1, b2, b3, b4, b5 }, new List<HintsEnum> { HintsEnum.HappinessManager }, new List<DialogBox> { b6, b7, b8, b8, b9, b10, b11, b12, b13, b14 });
                    npcs[2].SetDialogTree(tree);

                    break;
                case 4:
                    npcs = new List<NPC>
                    {
                        new NPC(GetAnimation("directeur3"), GetAnimation("directeur3"), GetTronche("directeur3"), 350),
                        new NPC(GetAnimation("directeur4"), GetAnimation("directeur4"), GetTronche("directeur4"), 700),
                        new NPC(GetAnimation("secretaire"), GetAnimation("secretaire_walk"), GetTronche("secretaire"), 2450)
                    };

                    npcs[0].Flip = true;

                    NPC npc = npcs[2];
                    npc.SetUnlockingConditions(new List<HintsEnum> { HintsEnum.DoloresPartie });
                    b1 = new DialogBox("Que faites-vous la ? C'est prive, vous n'avez pas a etre ici.", npc);
                    b2 = new DialogBox("J'ai croise Camille a l'instant a la cafeteria. Elle souhaiterait vous voir concernant le rendez-v...", player);
                    b3 = new DialogBox("Quel toupet.", npcs[2], false, HintsEnum.DoloresPartie);
                    tree = new DialogTree(new List<DialogBox> { b1 }, new List<HintsEnum> { HintsEnum.DoloresSecretaire, HintsEnum.DoloresDemandee}, new List<DialogBox> { b2, b3 });
                    npc.SetDialogTree(tree);

                    b1 = new DialogBox("...  nan  j'avais  pas  mon  mot  a  dire  !  Ca  venait  carrement  de  la-haut.", npcs[0]);
                    b2 = new DialogBox("Mais  c'est  n'importe  quoi...  Une  'happiness  manager'.  Ce  qu'on  n'invente  pas  aujourd'hui  franchement...  ", npcs[1]);
                    b3 = new DialogBox("Et  comme  par  hasard,  c'est  la  p'tite  Alicia  qui  est  'promue'.  Moi,  je  te  dis  que  ça  passe  sous  le  bu...", npcs[0]);
                    b4 = new DialogBox("Eh  oh  t'y  vas  un  peu  fort  la,  en  plus  d'être  carrement  sexiste  !  Va  falloir  changer  de  mentalite  Arnaud  !  Eh  puis  tout  compte  fait,  c'est  peut-être  pas  si  mal,  ça  reduira  peut-etre  notre  turnover.", npcs[1]);
                    b5 = new DialogBox("Mouais  pas  faux...  Enfin  moi,  j'vois  deja  Gabrielle  faire  des  yeux  comme  ca  lorsqu'elle  verra  le  budget  de  ces  activites  '100%  happy  avec  Marpha  Biotech'", npcs[0], false, HintsEnum.HappinessManager);
                    tree = new DialogTree(new List<DialogBox> { b1, b2, b3, b4, b5 });
                    npcs[1].SetDialogTree(tree);

                    break;
                default:
                    break;
            }

            return npcs;
        }

        private List<Furniture> GetFloorFurnitures(int floor)
        {
            List<Furniture> furnitures = new List<Furniture>();

            switch (floor)
            {
                case 2:
                    furnitures = new List<Furniture>
                    {
                        new Furniture(GetAnimation("transparent"), GetTronche("Docu_Vaccin"), 900),
                        new Furniture(GetAnimation("moniteur"), GetTronche("Serveur"), 3420)
                    };
                    break;
                case 3:
                    furnitures = new List<Furniture>
                    {
                        new Furniture(GetAnimation("transparent"), GetTronche("Badge"), 2800),
                        new Furniture(GetAnimation("transparent"), GetTronche("journaliste"), 3500)
                    };
                    break;
                case 4:
                    furnitures = new List<Furniture>
                    {
                        new Furniture(GetAnimation("transparent"), GetTronche("PC_Boss"), 3350)
                    };
                    break;
                default:
                    break;
            }

            return furnitures;
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            KeyboardState state = Keyboard.GetState();

            if (isPlayingIntro)
            {
                if (audioIntroInstance.State == SoundState.Stopped || state.GetPressedKeys().Length > 0)
                {
                    isPlayingIntro = false;
                    audioIntroInstance.Stop();
                    audioIntroInstance.Dispose();
                    MediaPlayer.Play(music);
                    MediaPlayer.IsRepeating = true;
                }
                else
                {
                    return;
                }
            }

            timer.update(gameTime.ElapsedGameTime.TotalSeconds);

            if (timer.isOver())
                Exit();

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (!Win && !Lose)
            {
                if (sniper == null && ThreatLevel == 2)
                {
                    sniper = new Sniper(textureDict["Viseur"]);
                }

                timer.update(gameTime.ElapsedGameTime.TotalSeconds);

                if (timer.isOver())
                    Exit();

                int oldFloor = currentFloor.Number;
                if (!elevator && !switchFloorAnimation)
                {
                    string switchFloor = currentFloor.Update(state, prevKeyState, guards);
                    if (sniper != null) sniper.Update(player);

                    if (switchFloor == "stairs up")
                    {
                        int n = currentFloor.Number;
                        if (n < floors[floors.Length - 1].Number - 1)
                        {
                            currentFloor = floors[n + 2];
                            switchFloorAnimation = true;
                            curFloorAnimation = 120;
                            leavingFloor = oldFloor;
                            nextFloor = currentFloor.Number;
                        }
                        Console.Out.WriteLine("floor up : " + (n + 1));
                    }
                    else if (switchFloor == "stairs down")
                    {
                        int n = currentFloor.Number;
                        if (n > 0)
                        {
                            currentFloor = floors[n];
                            switchFloorAnimation = true;
                            curFloorAnimation = 120;
                            leavingFloor = oldFloor;
                            nextFloor = currentFloor.Number;
                        }
                        Console.Out.WriteLine("floor down : " + (n - 1));
                    }
                    else if (switchFloor == "elevator")
                    {
                        elevator = true;
                        elevatorGUI = new ElevatorGUI(floors.Length, currentFloor.Number, white);
                    }
                }
                else if (switchFloorAnimation)
                {
                    curFloorAnimation--;
                    if (curFloorAnimation % 120 == 0)
                    {
                        if (leavingFloor > nextFloor)
                        {
                            nextFloor--;
                            leavingFloor--;
                        }
                        else
                        {
                            nextFloor++;
                            leavingFloor++;
                        }
                    }
                    if (curFloorAnimation <= 0)
                    {
                        switchFloorAnimation = false;
                    }
                }
                else
                {
                    int switchFloor = elevatorGUI.Update(state, prevKeyState);
                    if (switchFloor != -1)
                    {
                        currentFloor = floors[switchFloor];
                        elevator = false;
                        elevatorGUI = null;
                        Console.Out.WriteLine("elevator : " + (switchFloor - 1));

                        if (oldFloor != currentFloor.Number)
                        {
                            switchFloorAnimation = true;
                            curFloorAnimation = Math.Abs(currentFloor.Number - oldFloor) * 120;
                            leavingFloor = oldFloor;
                            if (currentFloor.Number > oldFloor) nextFloor = oldFloor + 1;
                            else nextFloor = oldFloor - 1;
                        }
                    }
                }
                if (sniper != null) sniper.SwitchFloor(oldFloor, currentFloor.Number);

                prevKeyState = state;
            }
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, null, null);

            if (!switchFloorAnimation)
            {
                currentFloor.Draw(spriteBatch, graphics.PreferredBackBufferWidth, guards);
                if (sniper != null) sniper.Draw(spriteBatch, player.CameraX(WIDTH));
            }
            else
            {
                if (leavingFloor > nextFloor)
                {
                    floors[leavingFloor + 1].DrawFractionTowardsBottom(spriteBatch, curFloorAnimation % 120, true);
                    floors[nextFloor + 1].DrawFractionTowardsBottom(spriteBatch, curFloorAnimation % 120, false);
                }
                else
                {
                    floors[leavingFloor + 1].DrawFractionTowardsTop(spriteBatch, curFloorAnimation % 120, true);
                    floors[nextFloor + 1].DrawFractionTowardsTop(spriteBatch, curFloorAnimation % 120, false);
                }

                spriteBatch.Draw(white, new Rectangle(0, 2 * HEIGHT / 3, WIDTH, 2 * HEIGHT / 3), Color.Black);
            }


            if (elevator) elevatorGUI.Draw(spriteBatch);

            string time = timer.getTime();
            int len = (int)font.MeasureString(time).X;
            int h = (int)font.MeasureString(time).Y;
            spriteBatch.Draw(white, new Rectangle((WIDTH - len) / 2 - 20, 0, len + 40, h + 20), Color.Black);
            spriteBatch.DrawString(font, time, new Vector2((WIDTH - len) / 2, 10), Color.Red);

            if (Win) spriteBatch.DrawString(font, "YOU WIN", new Vector2(300, 150), Color.Red);
            if (Lose) spriteBatch.DrawString(font, "YOU LOSE", new Vector2(300, 150), Color.Red);

            if (isPlayingIntro)
            {
                spriteBatch.Draw(textureDict["TitleScreen"], new Rectangle(0,0,WIDTH,HEIGHT), Color.White);
            }


            spriteBatch.End();

            base.Draw(gameTime);       
        }
    }
}
