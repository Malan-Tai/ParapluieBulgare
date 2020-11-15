using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
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
            "MC_Walk_SpriteSheet",
            "costard_fem_idle",
            "costard_idle",
            "costard_idle_2",
            "cuistot_idle",
            "cuistot_idle_2",
            "Directeur_idle",
            "Journaliste_IDLE",
            "rando_fem_idle",
            "Background",
            "vigile_walk_3",
            "Ascenseur"
        };
        Dictionary<string, Texture2D> textureDict;

        List<string> allfaces = new List<string>
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

        SoundEffect soundBulgared;

        public static int ThreatLevel = 0;
        public static bool Win = false;
        public static bool Lose = false;

        public Game1()
        {
            textureDict = new Dictionary<string, Texture2D>();
            facebook = new Dictionary<string, Texture2D>();

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

            player = new Player(GetAnimation("MC_Walk_SpriteSheet"), GetAnimation("MC_Walk_SpriteSheet"), facebook["face01"], white);
            floors = new Floor[]
            {
                new Floor(player, -1, textureDict["Background"], GetFloorNPCs(-1), GetFloorFurnitures(-1)),
                new Floor(player, 0, textureDict["Background"], GetFloorNPCs(0), GetFloorFurnitures(0)),
                new Floor(player, 1, textureDict["Background"], GetFloorNPCs(1), GetFloorFurnitures(1)),
                new Floor(player, 2, textureDict["Background"], GetFloorNPCs(2), GetFloorFurnitures(2)),
                new Floor(player, 3, textureDict["Background"], GetFloorNPCs(3), GetFloorFurnitures(3)),
                new Floor(player, 4, textureDict["Background"], GetFloorNPCs(4), GetFloorFurnitures(4))
            };

            guards = new List<Guard>
            {
                new Guard(GetAnimation("vigile_walk_3"), GetAnimation("vigile_walk_3"), facebook["faceVigile"], 0, 700)
            };
            Guard guard = guards[0];
            guard.SetUnlockingConditions(new List<HintsEnum> { HintsEnum.BadgeLabo });
            DialogBox b1 = new DialogBox("Halte la malheureux, acces au labo interdit!", guard);
            DialogTree t = new DialogTree(new List<DialogBox> { b1 });
            guard.SetDialogTree(t);

            currentFloor = floors[1];

            timer = new Timer(600);

            NPC.soundBulgared = soundBulgared;
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
            foreach (string text in allfaces)
            {
                facebook.Add(text, Content.Load<Texture2D>(text));
            }

            white = textureDict["white"];

            soundBulgared = Content.Load<SoundEffect>("Bulgared");
        }

        private Animation GetAnimation(string spritesheet)
        {
            switch (spritesheet)
            {
                case "MC_Walk_SpriteSheet":
                    return new Animation(textureDict["MC_Walk_SpriteSheet"], 32, 32, 6, 10);
                case "vigile_walk_3":
                    return new Animation(textureDict["vigile_walk_3"], 32, 32, 6, 10);
                case "costard_fem_idle":
                    return new Animation(textureDict["costard_fem_idle"], 32, 32, 2, 60);
                case "costard_idle":
                    return new Animation(textureDict["costard_idle"], 32, 32, 2, 60);
                case "costard_idle_2":
                    return new Animation(textureDict["costard_idle_2"], 32, 32, 2, 60);
                case "cuistot_idle":
                    return new Animation(textureDict["cuistot_idle"], 32, 32, 2, 60);
                case "cuistot_idle_2":
                    return new Animation(textureDict["cuistot_idle_2"], 32, 32, 2, 60);
                case "Directeur_idle":
                    return new Animation(textureDict["Directeur_idle"], 32, 32, 7, 60);
                case "Journaliste_IDLE":
                    return new Animation(textureDict["Journaliste_IDLE"], 32, 32, 2, 60);
                case "rando_fem_idle":
                    return new Animation(textureDict["rando_fem_idle"], 32, 32, 2, 60);
                default:
                    return null;
            }
        }

        private List<NPC> GetFloorNPCs(int floor)
        {
            List<NPC> npcs = new List<NPC>();

            switch (floor)
            {
                case 0:
                    npcs = new List<NPC>
                    {
                        new NPC(GetAnimation("Journaliste_IDLE"), GetAnimation("Journaliste_IDLE"), facebook["face02"], 80),
                        new NPC(GetAnimation("Journaliste_IDLE"), GetAnimation("Journaliste_IDLE"), facebook["face02"], 200),
                        new NPC(GetAnimation("costard_idle"), GetAnimation("costard_idle"), facebook["face02"], 500)
                    };

                    npcs[0].Target = true;

                    NPC npc = npcs[2];
                    DialogBox b1 = new DialogBox("coucou", npc);
                    DialogBox b2 = new DialogBox("wesh frr", player);
                    DialogBox b3 = new DialogBox("vazy kass toa", npc, false, HintsEnum.BadgeLabo);

                    DialogBox b4 = new DialogBox("Alors poto le labo ?", npc);

                    DialogTree tree = new DialogTree(new List<DialogBox> { b1, b2, b3 }, new List<HintsEnum> { HintsEnum.BadgeLabo }, new List<DialogBox> { b4 });
                    npc.SetDialogTree(tree);
                    break;
                case 1:
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
            if (sniper == null && ThreatLevel == 1)
            {
                sniper = new Sniper(white);
            }

            timer.update(gameTime.ElapsedGameTime.TotalSeconds);

            if (timer.isOver())
                Exit();

            KeyboardState state = Keyboard.GetState();
            
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            int oldFloor = currentFloor.Number;
            if (!elevator)
            {
                string switchFloor = currentFloor.Update(state, prevKeyState, guards);
                if (sniper != null) sniper.Update(player);

                if (switchFloor == "stairs up")
                {
                    int n = currentFloor.Number;
                    if (n < floors[floors.Length-1].Number - 1)
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
            spriteBatch.DrawString(font, time, new Vector2(400, 10), Color.Black);

            if (Win) spriteBatch.DrawString(font, "YOU WIN", new Vector2(300, 150), Color.Red);
            if (Lose) spriteBatch.DrawString(font, "YOU LOSE", new Vector2(300, 150), Color.Red);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
