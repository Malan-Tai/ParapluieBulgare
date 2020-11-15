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


        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        KeyboardState prevKeyState;

        List<string> allTextures = new List<string> 
        {
            "white",
            "bulleDeTexte",
            "background",
            "ascenseur",

            "player_walk",
            "Employe1",
            "costard_idle",
            "costard_idle2",
            "costard_fem_idle",
            "costard_fem_idle2",
            "cuistot_idle",
            "cuistot_idle2",
            "directeur_idle",
            "journaliste_idle",
            "vigile_walk",

            "TitleScreen"
        };
        List<string> allSounds = new List<string>
        {
            "Bulgared",
            "Intro"
        };
        Dictionary<string, Texture2D> textureDict;

        List<string> allFaces = new List<string>
        {
            "face01",
            "face02",
            "faceVigile"
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

            player = new Player(GetAnimation("player_walk"), GetAnimation("player_walk"), facebook["face01"], white);
            floors = new Floor[]
            {
                new Floor(player, -1, textureDict["background"], GetFloorNPCs(-1), GetFloorFurnitures(-1)),
                new Floor(player, 0, textureDict["background"], GetFloorNPCs(0), GetFloorFurnitures(0)),
                new Floor(player, 1, textureDict["background"], GetFloorNPCs(1), GetFloorFurnitures(1)),
                new Floor(player, 2, textureDict["background"], GetFloorNPCs(2), GetFloorFurnitures(2)),
                new Floor(player, 3, textureDict["background"], GetFloorNPCs(3), GetFloorFurnitures(3)),
                new Floor(player, 4, textureDict["background"], GetFloorNPCs(4), GetFloorFurnitures(4))
            };

            guards = new List<Guard>
            {
                new Guard(GetAnimation("vigile_walk"), GetAnimation("vigile_walk"), facebook["faceVigile"], 0, 1500),
                new Guard(GetAnimation("vigile_walk"), GetAnimation("vigile_walk"), facebook["faceVigile"], 2, 300),
                new Guard(GetAnimation("vigile_walk"), GetAnimation("vigile_walk"), facebook["faceVigile"], 2, 1450)
            };
            //Guard guard = guards[0];
            //guard.SetUnlockingConditions(new List<HintsEnum> { HintsEnum.BadgeLabo });
            //DialogBox b1 = new DialogBox("Halte la malheureux, acces au labo interdit!", guard);
            //DialogTree t = new DialogTree(new List<DialogBox> { b1 });
            //guard.SetDialogTree(t);

            guards[2].Flip = true;

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
        }

        private Animation GetAnimation(string spritesheet)
        {
            switch (spritesheet)
            {
                case "player_walk":
                    return new Animation(textureDict["player_walk"], 32, 32, 6, 10);
                case "vigile_walk":
                    return new Animation(textureDict["vigile_walk"], 32, 32, 6, 10);
                case "Employe1":
                    return new Animation(textureDict["Employe1"], 32, 32, 2, 60);
                case "costard_fem_idle":
                    return new Animation(textureDict["costard_fem_idle"], 32, 32, 2, 60);
                case "costard_fem_idle2":
                    return new Animation(textureDict["costard_fem_idle2"], 32, 32, 2, 60);
                case "costard_idle":
                    return new Animation(textureDict["costard_idle"], 32, 32, 2, 60);
                case "costard_idle2":
                    return new Animation(textureDict["costard_idle2"], 32, 32, 2, 60);
                case "cuistot_idle":
                    return new Animation(textureDict["cuistot_idle"], 32, 32, 2, 60);
                case "cuistot_idle2":
                    return new Animation(textureDict["cuistot_idle2"], 32, 32, 2, 60);
                case "directeur_idle":
                    return new Animation(textureDict["directeur_idle"], 32, 32, 7, 60);
                case "journaliste_idle":
                    return new Animation(textureDict["journaliste_idle"], 32, 32, 2, 60);
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
                        new NPC(GetAnimation("journaliste_idle"), GetAnimation("journaliste_idle"), facebook["face02"], 700),
                        new NPC(GetAnimation("journaliste_idle"), GetAnimation("journaliste_idle"), facebook["face02"], 1000),
                    };

                    npcs[0].Target = true;
                    npcs[0].Flip = true;

                    NPC npc = npcs[0];
                    DialogBox b1 = new DialogBox("coucou", npc);
                    DialogBox b2 = new DialogBox("wesh frr", player);
                    DialogBox b3 = new DialogBox("vazy kass toa", npc, false, HintsEnum.BadgeLabo);

                    DialogBox b4 = new DialogBox("Alors poto le labo ?", npc);

                    DialogTree tree = new DialogTree(new List<DialogBox> { b1, b2, b3 }, new List<HintsEnum> { HintsEnum.BadgeLabo }, new List<DialogBox> { b4 });
                    npc.SetDialogTree(tree);
                    break;
                case 0:
                    npcs = new List<NPC>
                    {
                        new NPC(GetAnimation("Employe1"), GetAnimation("Employe1"), facebook["face02"], 600),
                        new NPC(GetAnimation("journaliste_idle"), GetAnimation("journaliste_idle"), facebook["face02"], 1300),

                        new NPC(GetAnimation("costard_fem_idle"), GetAnimation("costard_fem_idle"), facebook["face02"], 2400),
                        new NPC(GetAnimation("journaliste_idle"), GetAnimation("journaliste_idle"), facebook["face02"], 3200),
                        new NPC(GetAnimation("journaliste_idle"), GetAnimation("journaliste_idle"), facebook["face02"], 3550)
                    };

                    npcs[0].Flip = true;
                    npcs[1].Flip = true;
                    npcs[3].Flip = true;

                    break;
                case 1:
                    npcs = new List<NPC>
                    {
                        new NPC(GetAnimation("journaliste_idle"), GetAnimation("journaliste_idle"), facebook["face02"], 400),
                        new NPC(GetAnimation("journaliste_idle"), GetAnimation("journaliste_idle"), facebook["face02"], 600),
                        new NPC(GetAnimation("costard_idle"), GetAnimation("costard_idle"), facebook["face02"], 1100),
                        new NPC(GetAnimation("journaliste_idle"), GetAnimation("journaliste_idle"), facebook["face02"], 1300),
                        new NPC(GetAnimation("directeur_idle"), GetAnimation("directeur_idle"), facebook["face02"], 2380),

                        new NPC(GetAnimation("cuistot_idle"), GetAnimation("cuistot_idle"), facebook["face02"], 3280),
                        new NPC(GetAnimation("cuistot_idle"), GetAnimation("cuistot_idle"), facebook["face02"], 3550),
                        new NPC(GetAnimation("cuistot_idle"), GetAnimation("cuistot_idle"), facebook["face02"], 3850)
                    };

                    npcs[0].Flip = true;
                    npcs[2].Flip = true;
                    npcs[5].Flip = true;

                    break;
                case 2:
                    npcs = new List<NPC>
                    {
                        new NPC(GetAnimation("journaliste_idle"), GetAnimation("journaliste_idle"), facebook["face02"], 600),
                        new NPC(GetAnimation("directeur_idle"), GetAnimation("directeur_idle"), facebook["face02"], 1100),
                        new NPC(GetAnimation("costard_idle"), GetAnimation("costard_idle"), facebook["face02"], 1300),

                        new NPC(GetAnimation("journaliste_idle"), GetAnimation("journaliste_idle"), facebook["face02"], 2850),
                        new NPC(GetAnimation("costard_idle"), GetAnimation("costard_idle"), facebook["face02"], 3700)
                    };

                    npcs[1].Flip = true;
                    npcs[3].Flip = true;

                    break;
                case 3:
                    npcs = new List<NPC>
                    {
                        new NPC(GetAnimation("journaliste_idle"), GetAnimation("journaliste_idle"), facebook["face02"], 580),
                        new NPC(GetAnimation("journaliste_idle"), GetAnimation("journaliste_idle"), facebook["face02"], 900),
                        new NPC(GetAnimation("journaliste_idle"), GetAnimation("journaliste_idle"), facebook["face02"], 1020),
                        new NPC(GetAnimation("costard_fem_idle"), GetAnimation("costard_fem_idle"), facebook["face02"], 2200),
                        new NPC(GetAnimation("costard_idle"), GetAnimation("costard_idle"), facebook["face02"], 2400)
                    };

                    npcs[0].Flip = true;
                    npcs[3].Flip = true;

                    break;
                case 4:
                    npcs = new List<NPC>
                    {
                        new NPC(GetAnimation("directeur_idle"), GetAnimation("directeur_idle"), facebook["face02"], 350),
                        new NPC(GetAnimation("directeur_idle"), GetAnimation("directeur_idle"), facebook["face02"], 700),
                        new NPC(GetAnimation("costard_fem_idle2"), GetAnimation("costard_fem_idle2"), facebook["face02"], 2450)
                    };

                    npcs[0].Flip = true;

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
                case 0:
                    break;
                case 1:
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
                if(audioIntroInstance.State == SoundState.Stopped || state.GetPressedKeys().Length > 0)
                {
                    isPlayingIntro = false;
                    audioIntroInstance.Stop();
                    audioIntroInstance.Dispose();
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
                if (sniper == null && ThreatLevel == 1)
                {
                    sniper = new Sniper(white);
                }

                timer.update(gameTime.ElapsedGameTime.TotalSeconds);

                if (timer.isOver())
                    Exit();

                int oldFloor = currentFloor.Number;
                if (!elevator)
                {
                    string switchFloor = currentFloor.Update(state, prevKeyState, guards);
                    if (sniper != null) sniper.Update(player);

                    if (switchFloor == "stairs up")
                    {
                        int n = currentFloor.Number;
                        if (n < floors[floors.Length - 1].Number - 1)
                        {
                            currentFloor = floors[n + 2];
                        }
                        Console.Out.WriteLine("floor up : " + (n + 1));
                    }
                    else if (switchFloor == "stairs down")
                    {
                        int n = currentFloor.Number;
                        if (n > 0)
                        {
                            currentFloor = floors[n];
                        }
                        Console.Out.WriteLine("floor down : " + (n - 1));
                    }
                    else if (switchFloor == "elevator")
                    {
                        elevator = true;
                        elevatorGUI = new ElevatorGUI(floors.Length, currentFloor.Number, white);
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

            currentFloor.Draw(spriteBatch, graphics.PreferredBackBufferWidth, guards);
            if (sniper != null) sniper.Draw(spriteBatch, player.CameraX(WIDTH));

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
