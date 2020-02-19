using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aula7
{
    class Ship
    {
        private float speed;

        public float Speed
        {
            get { return speed; }
            set { speed = value; }
        }

        private Model model;

        public Model Model
        {
            get { return model; }
            set { model = value; }
        }

        private Matrix world;

        private Vector3 position;

        public Vector3 Position
        {
            get { return position; }
            set { position = value; }
        }


        public Ship(Vector3 position, Random random)
        {
            this.position = position;
            this.world = Matrix.CreateTranslation(position);
            this.speed = (float) random.NextDouble();   
        }

        public void LoadContent(ContentManager content)
        {
            model = content.Load<Model>("modelo\\p1_saucer");
        }

        public void Update(GameTime gameTime)
        {
            position.Z -= 2f * speed * gameTime.ElapsedGameTime.Milliseconds;
            world = Matrix.CreateTranslation(position);
        }

        public void Draw(Camera camera)
        {
            foreach (ModelMesh mesh in model.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.LightingEnabled = false;
                    effect.World = world * Matrix.CreateScale(0.01f);
                    effect.View = camera.View;
                    effect.Projection = camera.Projection;
                }
                mesh.Draw();
            }
        }

    }
}
