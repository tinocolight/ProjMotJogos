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
        Random randomS = new Random();

        public BoundingSphere boundingSphere;

        Message posicionMessage;
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
        public void GetPosicionMessage()
        {
            posicionMessage = new Message();
            posicionMessage.Type = MessageType.ShipPosition;

        }
        public void BoundingSphereSetDim()
        {
            Random random = new Random();
      

            foreach (ModelMesh mesh in this.model.Model.Meshes)
            {
                this.boundingSphere = BoundingSphere.CreateMerged(this.boundingSphere, mesh.BoundingSphere);
            }
            
        }




        public Ship(Vector3 position, Random random, float displayLimitFront, float displayLimitBack)
        {
            this.position = position;
            this.world = Matrix.CreateTranslation(position);
            // rotação
            this.speed = (float)(Math.Pow(-1, (random.Next(-1, 1))) * (random.Next(1, 10)) / 100); // para gerar velocidades positivas e negativas excluíndo o zero
            if (speed > 0) {this.rot = Quaternion.CreateFromAxisAngle(world.Up, MathHelper.Pi);}
            if (speed < 0) { this.rot = Quaternion.CreateFromAxisAngle(world.Up, MathHelper.TwoPi); }
            this.ShipStatus = true;
            this.displayLimitFront = displayLimitFront;
            this.displayLimitBack = displayLimitBack;
            GetPosicionMessage();

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

            posicionMessage.V3Value = position;
           // Console.WriteLine(position);
            MessageBus.Instance.SendMessage(posicionMessage);
        }

        public void Draw()
        {
            foreach (ModelMesh mesh in model.Model.Meshes)
            {

                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.EnableDefaultLighting();
                    effect.World = Matrix.CreateFromQuaternion(Rotation) * Matrix.CreateTranslation(position) ;
                    effect.View = Camera.View;
                    effect.Projection = Camera.Projection;
                   

                }
                mesh.Draw();
            }

            DebugShapeRenderer.AddBoundingSphere(boundingSphere, Color.Red);
        }

    }
}
