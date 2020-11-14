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
        //public static System.Drawing.Rectangle res = Screen.PrimaryScreen.Bounds;

        //public static int WIDTH = res.Width;
        //public static int HEIGHT = res.Height;

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
            "background"
        };
        Dictionary<string, Texture2D> textureDict;
        Texture2D white;

        SpriteFont font;

        Player player;

        Floor[] floors;
        Floor currentFloor;

        Timer timer;

        bool elevator = false;
        ElevatorGUI elevatorGUI = null;

        SoundEffect soundBulgared;

        public Game1()
        {
            textureDict = new Dictionary<string, Texture2D>();

            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            //graphics.PreferredBackBufferWidth = WIDTH;
            //graphics.PreferredBackBufferHeight = HEIGHT;
            //graphics.IsFullScreen = true;
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
            DialogBox.boxRect = new Rectangle(0, 0, 400, (int)(0.261 * 400));

            player = new Player(GetAnimation("MC_Walk_SpriteSheet"), GetAnimation("MC_Walk_SpriteSheet"), white);
            floors = new Floor[]
            {
                new Floor(player, -1, textureDict["background"], GetFloorNPCs(-1)),
                new Floor(player, 0, textureDict["background"], GetFloorNPCs(0)),
                new Floor(player, 1, textureDict["background"], GetFloorNPCs(1)),
                new Floor(player, 2, textureDict["background"], GetFloorNPCs(2)),
                new Floor(player, 3, textureDict["background"], GetFloorNPCs(3)),
                new Floor(player, 4, textureDict["background"], GetFloorNPCs(4))
            };
            currentFloor = floors[1];

            timer = new Timer(600);
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
            white = textureDict["white"];

            soundBulgared = Content.Load<SoundEffect>("Bulgared");
        }

        private Animation GetAnimation(string spritesheet)
        {
            switch (spritesheet)
            {
                case "MC_Walk_SpriteSheet":
                    return new Animation(textureDict["MC_Walk_SpriteSheet"], 32, 32, 6, 10);
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
                        new NPC(GetAnimation("Journaliste_IDLE"), GetAnimation("Journaliste_IDLE"), soundBulgared, 80),
                        new NPC(GetAnimation("Journaliste_IDLE"), GetAnimation("Journaliste_IDLE"), soundBulgared, 200),
                        new NPC(GetAnimation("costard_idle"), GetAnimation("costard_idle"), soundBulgared, 500)
                    };

                    NPC npc = npcs[2];
                    DialogBox b1 = new DialogBox("coucou", npc);
                    DialogBox b2 = new DialogBox("wesh frr", player);
                    DialogBox b3 = new DialogBox("vazy kass toa", npc);
                    DialogTree tree = new DialogTree(new List<DialogBox> { b1, b2, b3 });
                    npc.SetDialogTree(tree);
                    break;
                case 1:
                    break;
                default:
                    break;
            }

            return npcs;
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
            timer.update(gameTime.ElapsedGameTime.TotalSeconds);

            if (timer.isOver())
                Exit();

            KeyboardState state = Keyboard.GetState();
            
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (!elevator)
            {
                string switchFloor = currentFloor.Update(state, prevKeyState);

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
                    Console.Out.WriteLine("elevator : " + switchFloor);
                }
            }

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

            currentFloor.Draw(spriteBatch, graphics.PreferredBackBufferWidth);
            if (elevator) elevatorGUI.Draw(spriteBatch, graphics.PreferredBackBufferWidth);

            string time = timer.getTime();
            Vector2 position = new Vector2(400, 10);
            spriteBatch.DrawString(font, time, position, Color.Black);

            spriteBatch.End();

            base.Draw(gameTime);

            
        }
    }
}
