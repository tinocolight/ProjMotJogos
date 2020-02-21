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



        private static int ShipSeedArea  = 500;
        private static int ShipLimitArea = 600;
        private static int ShipCount = 1000;



        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Skybox skybox;
        List<Ship> ships;
        //Camera camera;
        Random random;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);


            //  graphics.SynchronizeWithVerticalRetrace = true;
            graphics.PreferMultiSampling = true;
            graphics.GraphicsProfile = GraphicsProfile.Reach;
            graphics.GraphicsProfile = GraphicsProfile.HiDef;
            graphics.PreferredBackBufferWidth = 1200;
            graphics.PreferredBackBufferHeight = 800;
            graphics.IsFullScreen = false;
            Content.RootDirectory = "Content";

        }


        protected override void Initialize()
        {
            //camera = new Camera();
            Camera.Initialize(GraphicsDevice);
            DebugShapeRenderer.Initialize(GraphicsDevice);
            random = new Random();


            ships = new List<Ship>();
            //base está a chamar o construtor de uma classe acima de  Game1
            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            skybox = new Skybox(Content);
            for (int i = 0; i <= ShipCount; i++)
            {

               Ship ship = new Ship(new Vector3(random.Next(-ShipSeedArea, ShipSeedArea), random.Next(-ShipSeedArea, ShipSeedArea), random.Next(-ShipLimitArea, ShipLimitArea)), random, -ShipLimitArea, ShipLimitArea);
               ship.LoadContent(Content);



                //Adiciona o elemento acabado de criar à lista
                ships.Add(ship);

            }
         

        }

      
        protected override void UnloadContent()
        {
           
        }

        
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            foreach (Ship ship in ships)
            {
                Camera.Update(gameTime, GraphicsDevice, ship);
                if (ship.ShipStatus == true)
                {

                    ship.Update(gameTime); }

       


                

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

                    



             

            }
            
            base.Update(gameTime);
        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            foreach (Ship ship in ships)
            {
                if (Camera.frustum.Intersects(ship.boundingSphere)) {
                    if (ship.ShipStatus)
                        ship.Draw();
                   
                }
            }
            skybox.Draw(Camera.View,Camera.Projection,Camera.getPosition());
            DebugShapeRenderer.Draw(gameTime, Camera.View, Camera.Projection);
            base.Draw(gameTime);
        }
    }
}
