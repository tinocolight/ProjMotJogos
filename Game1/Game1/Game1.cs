using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;


namespace Game1



{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {



        private static int ShipSeedArea  = 1000;
        private static int ShipLimitArea = 10000;
        private static int ShipCount = 1000;


        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        List<Ship> ships;
        Camera camera;
        Random random;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);


            //  graphics.SynchronizeWithVerticalRetrace = true;
            graphics.PreferMultiSampling = true;
            graphics.GraphicsProfile = GraphicsProfile.Reach;
         //   graphics.GraphicsProfile = GraphicsProfile.HiDef;
            graphics.PreferredBackBufferWidth = 1920;
            graphics.PreferredBackBufferHeight = 1080;
            Content.RootDirectory = "Content";

        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            camera = new Camera(new Vector3(0, 0, 100), graphics);
            DebugShapeRenderer.Initialize(GraphicsDevice);
            random = new Random();


            ships = new List<Ship>();
            //base está a chamar o construtor de uma classe acima de  Game1
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            for (int i = 0; i <= ShipCount; i++)
            {

               Ship ship = new Ship(new Vector3(random.Next(-ShipSeedArea, ShipSeedArea), random.Next(-ShipSeedArea, ShipSeedArea), random.Next(-ShipLimitArea, ShipLimitArea)), random, -ShipLimitArea, ShipLimitArea);
                ship.LoadContent(Content);



                //Adiciona o elemento acabado de criar à lista
               ships.Add(ship);

            }
            // TODO: use this.Content to load your game content here

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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            foreach (Ship ship in ships)
            {
                if (ship.ShipStatus == true)
                {
                 //   ship.Speed += .00005f*ship.Position.Z;  // somente para dar uma ideia de aceleração. Pode ser apagada a linha.
                   // ship.Speed = 5;
                    ship.Update(gameTime); }

                /*
                if (ship.ShipStatus == false)
                {

                    Vector3 pos = new Vector3(random.Next(-ShipSeedArea, ShipSeedArea), random.Next(-ShipSeedArea, ShipSeedArea), ShipLimitArea);
                    ship.Position = pos;
                    ship.ShipStatus = true;
                    ship.DisplayLimitFront = -ShipLimitArea;

                }
                */


                

                                else if (ship.ShipStatus == false && ship.Speed > 0f)
                                {

                                    Vector3 pos = new Vector3(random.Next(-ShipSeedArea, ShipSeedArea), random.Next(-ShipSeedArea, ShipSeedArea), ShipLimitArea);
                                    ship.Position = pos;
                                    ship.ShipStatus = true;
                                    ship.DisplayLimitFront = -ShipLimitArea;

                                }
                                else if (ship.ShipStatus == false && ship.Speed < 0f)
                                {

                                    Vector3 pos = new Vector3(random.Next(-ShipSeedArea, ShipSeedArea), random.Next(-ShipSeedArea, ShipSeedArea), -ShipLimitArea);
                                    ship.Position = pos;
                                    ship.ShipStatus = true;
                                    ship.DisplayLimitBack = ShipLimitArea;

                                }

                    



                //     ship.boundingSphere.Center = ship.Position;

            }

            base.Update(gameTime);
        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            foreach (Ship ship in ships)
            {
                if(ship.ShipStatus)
                ship.Draw(camera);
            }

            DebugShapeRenderer.Draw(gameTime, camera.View, camera.Projection);
            base.Draw(gameTime);
        }
    }
}
