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



        private static int ShipSeedArea  = 1500;
        private static int ShipLimitArea = 1000;
        private static int ShipCount = 1000;

        int point = 10;
        bool win = false;
        SpriteFont font;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Sperm sperm;
       // Skybox skybox;
        Derbies derbies;
        Ovulo ovulo;
        List<Ship> ships;
        ShipPool shipPool;

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
            sperm = new Sperm(new Vector3(-100,-100,-100), ShipSeedArea, ShipLimitArea);


            shipPool = new ShipPool(Content, graphics);
            
            ships = new List<Ship>();

            for(int i = 0; i < ShipCount - 1 ; i++)
            {
                // vamos passar à lista de ships a referência de um determinado numero de objectos do tipo ship já criados
                ships.Add (shipPool.GetObject());

            }

            //base está a chamar o construtor de uma classe acima de  Game1
            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.

            spriteBatch = new SpriteBatch(GraphicsDevice);
            font = Content.Load<SpriteFont>("Score");
            // skybox = new Skybox(Content);
            derbies = new Derbies(Content);
            ovulo = new Ovulo(Content);
            sperm.LoadContent(Content);
            /*
            for (int i = 0; i <= ShipCount; i++)
            {

               Ship ship = new Ship(new Vector3(random.Next(-ShipSeedArea, ShipSeedArea), random.Next(-ShipSeedArea, ShipSeedArea), random.Next(-ShipLimitArea, ShipLimitArea)), random, -ShipLimitArea, ShipLimitArea);
               ship.LoadContent(Content);




                //Adiciona o elemento acabado de criar à lista
                ships.Add(ship);

            }
            */


        }

      
        protected override void UnloadContent()
        {
           
        }

        
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            sperm.Update(gameTime);
            shipPool.Update(gameTime);

            for (int i = 0; i < ships.Count; i++)
            {
                //Devolve à pool ships mortas
               if (ships[i].ShipStatus == false) {
                    shipPool.ReturnObjectToPool(ships[i]);
                    ships.RemoveAt(i);
                   
                }

                //Vai buscar novos Objectos do tipo Ship à pool caso o numero caia abaixo do definido
                if (ships.Count < ShipCount) {
                    Ship obj = shipPool.GetObject();
                   
                    if (/*ship.ShipStatus == false && */ obj.Speed > 0f)
                    {

                        Vector3 pos = new Vector3(random.Next(-ShipSeedArea, ShipSeedArea), random.Next(-ShipSeedArea, ShipSeedArea), ShipLimitArea);
                        obj.Position = pos;
                        //ships[i].ShipStatus = true;
                        obj.DisplayLimitFront = -ShipLimitArea;

                    }
                    if (/*ship.ShipStatus == false &&*/ obj.Speed < 0f)
                    {

                        Vector3 pos = new Vector3(random.Next(-ShipSeedArea, ShipSeedArea), random.Next(-ShipSeedArea, ShipSeedArea), -ShipLimitArea);
                        obj.Position = pos;
                        //ships[i].ShipStatus = true;
                        obj.DisplayLimitBack = ShipLimitArea;

                    }

                   
                    ships.Add(obj);
                    if (ovulo.boundingSphere.Intersects(sperm.boundingSphere) && point == 10) { win = true; Console.WriteLine("WINNN"); }

                        if (derbies.boundingSphere.Intersects(sperm.boundingSphere))
                    {
                        sperm.SpermStatus = true;
                    }
                    else { sperm.Position= new Vector3(30, 10, 30); }
                }

                
            }


            foreach (Ship ship in ships)
            {
                if (ship.boundingSphere.Intersects(sperm.boundingSphere)) { ship.ShipStatus = false; Console.WriteLine(point); point++; }
                Camera.Update(gameTime, GraphicsDevice, sperm);
                /* if (ship.boundingSphere.Intersects(ship.boundingSphere))
                 {
                     ship.ShipStatus = false;
                 }*/
               

                if (ship.ShipStatus == true)
                {

                    ship.Update(gameTime); }

       
                /*
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
                                */
                    
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

            // skybox.Draw(Camera.View,Camera.Projection,Camera.getPosition());
            derbies.Draw();
            ovulo.Draw();
            sperm.Draw();
            DebugShapeRenderer.Draw(gameTime, Camera.View, Camera.Projection);
            base.Draw(gameTime);
/*
            spriteBatch.Begin();
            if (!win) { 
            spriteBatch.DrawString(font, "Score: " + point, new Vector2(100, 100), Color.White); }
            else{spriteBatch.DrawString(font, "Win: " , new Vector2(100, 100), Color.White);}
            
            spriteBatch.End();*/
        }
    }
}
