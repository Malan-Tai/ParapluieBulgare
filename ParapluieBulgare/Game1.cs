using System;
//using System.Windows.Forms;
using Microsoft.Xna.Framework;
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

        Texture2D white;

        Player player;

        Floor[] floors;
        Floor currentFloor;

        bool elevator = false;
        ElevatorGUI elevatorGUI = null;

        public Game1()
        {
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

            player = new Player(white);

            floors = new Floor[]
            {
                new Floor(player, 0, white),
                new Floor(player, 1, white),
                new Floor(player, 2, white),
                new Floor(player, 3, white),
                new Floor(player, 4, white),
                new Floor(player, 5, white)
            };
            currentFloor = floors[0];
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            white = Content.Load<Texture2D>("white");
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

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (!elevator)
            {
                string switchFloor = currentFloor.Update(state, prevKeyState);

                if (switchFloor == "stairs up")
                {
                    int n = currentFloor.Number;
                    if (n <= floors.Length - 2)
                    {
                        currentFloor = floors[n + 1];
                    }
                    Console.Out.WriteLine("floor up : " + (n + 1));
                }
                else if (switchFloor == "stairs down")
                {
                    int n = currentFloor.Number;
                    if (n >= 1)
                    {
                        currentFloor = floors[n - 1];
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

            spriteBatch.Begin();

            currentFloor.Draw(spriteBatch, graphics.PreferredBackBufferWidth);
            if (elevator) elevatorGUI.Draw(spriteBatch, graphics.PreferredBackBufferWidth);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
