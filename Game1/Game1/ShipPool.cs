using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1
{
    class ShipPool
    {

        private Random random;
        private List<Ship> shipsP;
        private Ship ship;
        GraphicsDeviceManager graphicsP;
      
        public int teste;
       


        private static int ShipSeedArea = 1500*2;
        private static int ShipLimitArea = 1000*2;
        private static int ShipCount = 11000;


        protected void Initialize()
        {
            shipsP = new List<Ship>();
           
            random = new Random();
        }


        // construtor para riar a pool hardcoded
        public ShipPool(ContentManager Content, GraphicsDeviceManager graphics)
        {
            Initialize();
            graphicsP = graphics;
            Content.RootDirectory = "Content";
            for (int i = 0; i <= ShipCount; i++)
            {
                ship = new Ship(new Vector3(random.Next(-ShipSeedArea, ShipSeedArea), random.Next(-ShipSeedArea, ShipSeedArea), random.Next(-ShipLimitArea, ShipLimitArea)), random, -ShipLimitArea, ShipLimitArea);
                ship.ShipStatus = false;

                //if (shipsP[i].boundingSphere.Intersects(shipsP[i + 1].boundingSphere)){ ship.ShipStatus = false; }
                ship.LoadContent(Content);
               
                shipsP.Add(ship);
               
            }

        }





        // Retira da Pool Objecto do tipo Ship
        public Ship GetObject()
        {
            if (shipsP.Count > 0)
            {
                Ship obj = shipsP[0];
                obj.ShipStatus = true;
                shipsP.RemoveAt(0);
                return obj;
                
            }
            return null;
        }


        // Insere na Pool Objecto já não necessário do tipo Ship
        public void ReturnObjectToPool(Ship obj)
        {
            shipsP.Add(obj);
            obj.ShipStatus = false;
            //Console.WriteLine(obj);
           // if (obj.boundingSphere.Intersects(obj.boundingSphere)) { obj.ShipStatus = false; Console.WriteLine("Boom"); }
        }



        public void Update(GameTime gameTime)
        {
            

            //sHIPpOOL SÓ a devolver ships e torna-las vivas. Provavelmente não teremos de atualizar as ships na pool


            /*
            Console.Write("No Loop Update de ShipPool");
            foreach (Ship ship in shipsP)
            {
                Camera.Update(gameTime, graphicsP.GraphicsDevice, ship);

                if (ship.ShipStatus == true)
                {
                    ship.Update(gameTime);
                }




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

             //  base.Update(gameTime);

    */
        }


        public void Draw()
        {

        }






    }
}
