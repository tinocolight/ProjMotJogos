using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;
namespace Game1
{
    class Sperm
    {    //Posição inicial da camâra
        static Vector3 position = new Vector3(30, 10, 30);

        //Vector de direção inicial
      // static Vector3  direction = new Vector3(0, 0, -1f);

        Random randomS = new Random();
   //     static KeyboardState keyStateAnterior;
        public BoundingSphere boundingSphere;

        //Velocidade do movimento (translação)
        static private float moveSpeed = 2f;

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
        public bool SpermStatus
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

        private Model model;
        //public ModelMesh mesh;
        public Model Model
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

       // private Vector3 cosition;

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


            foreach (ModelMesh mesh in this.model.Meshes)
            {
                this.boundingSphere = BoundingSphere.CreateMerged(this.boundingSphere, mesh.BoundingSphere);
            }

        }



        public Sperm(Vector3 position, float displayLimitFront, float displayLimitBack)
        {
           // this.position = position;
            this.world = Matrix.CreateTranslation(position);
            // rotação
          //  this.speed = (float)(Math.Pow(-1, (random.Next(-1, 1))) * (random.Next(1, 10)) / 100); // para gerar velocidades positivas e negativas excluíndo o zero
           // if (speed > 0) { this.rot = Quaternion.CreateFromAxisAngle(world.Up, MathHelper.Pi); }
           // if (speed < 0) { this.rot = Quaternion.CreateFromAxisAngle(world.Up, MathHelper.TwoPi); }
            this.SpermStatus = true;
            this.displayLimitFront = displayLimitFront;
            this.displayLimitBack = displayLimitBack;
            GetPosicionMessage();

        }


        static private void Foward()
        {
            position.Z = position.Z+moveSpeed;
           // Console.WriteLine(position.Z);
        }

        static private void Backward()
        {
            position.Z = position.Z -moveSpeed;
           // Console.WriteLine(position.Z);
        }

        static private void Up()
        {
            position.Y = position.Y+moveSpeed;
             //Console.WriteLine(position.Y);
        }

        static private void Down()
        {
            position.Y = position.Y - moveSpeed;
            //Console.WriteLine(position.Y);
        }
        static private void Right()
        {
            position.X = position.X+moveSpeed;
            //Console.WriteLine(position.X);
        }
        static private void Left()
        {
            position.X = position.X - moveSpeed;
            //Console.WriteLine(position.X);
        }


   


        public void LoadContent(ContentManager content)
        {
            model = content.Load<Model>("modelo\\TESTE_COM_TEXTURAS");
            BoundingSphereSetDim();
        }



        public void Update(GameTime gameTime)
        {
            KeyboardState kb = Keyboard.GetState();
    
            if (kb.IsKeyDown(Keys.W))
            {
                 

                Foward();
            }
            if (kb.IsKeyDown(Keys.S))
            {
                Backward();

            }
            if (kb.IsKeyDown(Keys.A))
            {
                Left();

            }
            if (kb.IsKeyDown(Keys.D))
            {
                Right();            }
            if (kb.IsKeyDown(Keys.Q))
            {
                Up();
            }
            if (kb.IsKeyDown(Keys.E))
            {
                Down();
            }

            else if (this.SpermStatus == true) { this.SpermStatus = false; }

            boundingSphere.Center = position;

            posicionMessage.V3Value = position;
            MessageBus.Instance.SendMessage(posicionMessage);
        }

        public void Draw()
        {
            
                foreach (ModelMesh mesh in model.Meshes)
                {

                    foreach (BasicEffect effect in mesh.Effects)
                    {
                        effect.EnableDefaultLighting();
                        effect.World = Matrix.CreateFromQuaternion(Rotation) * Matrix.CreateTranslation(position);
                        effect.View = Camera.View;
                        effect.Projection = Camera.Projection;


                    }
                    mesh.Draw();
                }

                DebugShapeRenderer.AddBoundingSphere(boundingSphere, Color.Red);
            }
        
    }
}
