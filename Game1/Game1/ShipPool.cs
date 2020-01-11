using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1
{
    class ShipPool
    {
        /*
        Random random = new Random();
        List<Ship> ships;


        // Cria um determinado numero de naves alocadas à origem, desativadas
        public ShipPool(int shipNumber, ContentManager content)
        {
            for (int i = 0; i <= shipNumber; i++)
            {
                Ship ship = new Ship(new Vector3(0, 0, 0), random, false);
                ship.LoadContent(content);

                //Adiciona o elemento acabado de criar à lista
                ships.Add(ship);
            }

        }



        public ShipPool (int shipNumber, ContentManager content, int cubicGameSpace)
        {
            for (int i = 0; i <= shipNumber; i++)
            {
                Ship ship = new Ship(new Vector3(random.Next(-cubicGameSpace, cubicGameSpace), random.Next(-cubicGameSpace, cubicGameSpace), random.Next(-cubicGameSpace, cubicGameSpace)), random, -20000);
                ship.LoadContent(content);

                //Adiciona o elemento acabado de criar à lista
                ships.Add(ship);
            }

        }


        /*


        private float displayLimitZ;
        public float DisplayLimitZ
        {
            get { return displayLimitZ; }
            set { displayLimitZ = value; }
        }


        ships = new List<Ship>();


        spriteBatch = new SpriteBatch(GraphicsDevice);

            for (int i = 0; i <= 100; i++)
            {
                Ship ship = new Ship(new Vector3(random.Next(-5000, 5000), random.Next(-5000, 5000), random.Next(-5000, 5000)), random, -20000);
        ship.LoadContent(Content);

                //Adiciona o elemento acabado de criar à lista
                ships.Add(ship);
            }


    */



    }
}
