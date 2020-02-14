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
    class Ship
    {
        public BoundingSphere boundingSphere;
      

        private float displayLimitFront;
        public float DisplayLimitFront
        {
            get { return displayLimitFront; }
            set { displayLimitFront = value; }
        }

        private float displayLimitBack;
        public float DisplayLimitBack
        {
            get { return displayLimitBack; }
            set { displayLimitBack = value; }
        }

        private bool alive;
        public bool ShipStatus
        {
            get { return alive; }
            set { alive = value; }
        }
               
        private float speed;
        public float Speed
        {
            get { return speed; }
            set { speed = value; }
        }

        private ShipModel model;

        public ShipModel Model
        {
            get { return model; }
            set { model = value; }
        }

        private Matrix world;



        public Model World
        {
            get { return World; }
            set { World = value; }
        }

        private Vector3 position;

        public Vector3 Position
        {
            get { return position; }
            set { position = value; }
        }
        private Quaternion rot;
        public Quaternion Rotation
        {
            get { return rot; }
            set { rot = value; }
        }

        public void BoundingSphereSetDim()
        {
            Random random = new Random();
       //     DebugShapeRenderer.AddBoundingSphere(boundingSphere, Color.DarkRed, 2.0f);
           // Console.WriteLine("CENTER = " + boundingSphere.Center);

            foreach (ModelMesh mesh in this.model.Model.Meshes)
            {
                this.boundingSphere = BoundingSphere.CreateMerged(this.boundingSphere, mesh.BoundingSphere);
            }
            //   this.boundingSphere.Center = new Vector3(random.Next(-1, 1), random.Next(-1, 1), random.Next(-400, 1));
            //this.boundingSphere.Radius = random.Next(20, 20000);
            

        }




        public Ship(Vector3 position, Random random, bool startingStatus)
        {
            this.position = position;
            this.world = Matrix.CreateTranslation(position);
            this.speed = (float)random.NextDouble();
            this.ShipStatus = startingStatus;
            this.displayLimitFront = 0f;
            this.displayLimitBack = displayLimitFront;


        }

        /*

        public Ship(Vector3 position, Random random, float displayLimitFront, float displayLimitBack)
        {
            this.position = position;
            this.world = Matrix.CreateTranslation(position);
            // rotação
            this.rot = Quaternion.CreateFromAxisAngle(world.Up, MathHelper.TwoPi);
            this.speed =  (float)random.Next(1, 3) / 10;
            this.ShipStatus = true;
            this.displayLimitFront = displayLimitFront;
            this.displayLimitBack = displayLimitBack;
        }


/**/

        public Ship(Vector3 position, Random random, float displayLimitFront, float displayLimitBack)
        {
            this.position = position;
            this.world = Matrix.CreateTranslation(position);
            // rotação
            this.rot = Quaternion.CreateFromAxisAngle(world.Up, MathHelper.TwoPi);
            this.speed = (float)(Math.Pow(-1, (random.Next(-1, 1)))*(random.Next(1, 10))/10); // para gerar velocidades positivas e negativas excluíndo o zero
            //this.speed = (float) random.Next(1, 3)/10;
            this.ShipStatus = true;
            this.displayLimitFront = displayLimitFront;
            this.displayLimitBack = displayLimitBack;  

        }

    




        // Iniciação alternativa recorendo a polimorfismo com o objectivo de poder declarar naves mortas no início para a pool
        public Ship(Vector3 position, Random random, float displayLimitFront, bool startingStatus)
        {
            this.position = position;
            this.world = Matrix.CreateTranslation(position);
            this.speed = (float)random.NextDouble();
            this.ShipStatus = startingStatus;
            this.displayLimitFront = displayLimitFront;
            this.displayLimitBack = displayLimitFront;
        }

       public void LoadContent(ContentManager content)
        {
            model = new ShipModel(content);
            BoundingSphereSetDim();
        }

        


        public void Update(GameTime gameTime)
        {
            if (Speed > 0 && displayLimitFront < position.Z)
            {
                position.Z -= 2f * speed * gameTime.ElapsedGameTime.Milliseconds;
                world = Matrix.CreateTranslation(position);



            }
            else if (Speed < 0 && displayLimitBack > position.Z)
            {
                position.Z -= 2f * speed * gameTime.ElapsedGameTime.Milliseconds;
                world = Matrix.CreateTranslation(position);


            }

            else if (this.ShipStatus == true) { this.ShipStatus = false; }

            boundingSphere.Center = position;
            // boundingSphere.Radius = 200;

        }

        public void Draw(Camera camera)
        {
            foreach (ModelMesh mesh in model.Model.Meshes)
            {

                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.EnableDefaultLighting();
                    effect.World =  Matrix.CreateTranslation(position) * Matrix.CreateFromQuaternion(Rotation);
                    effect.View = camera.View;
                    effect.Projection = camera.Projection;
     
                }          
                mesh.Draw();
            }

            DebugShapeRenderer.AddBoundingSphere(boundingSphere, Color.Red);
        }

    }
}
