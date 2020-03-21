using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Game1
{
    public class Ovulo
    {
        private Model model;
        public ModelMesh mesh;
        public BoundingSphere boundingSphere;

        public Model Model
        {
            get { return model; }
            set { model = value; }
        }
        public Ovulo(ContentManager content)
        {
            model = content.Load<Model>("modelo\\nucleo");
            BoundingSphereSetDim();

        }
        public void BoundingSphereSetDim()
        {
            Random random = new Random();


            foreach (ModelMesh mesh in this.model.Meshes)
            {
                this.boundingSphere = BoundingSphere.CreateMerged(this.boundingSphere, mesh.BoundingSphere);
            }

        }

        public void Draw()
        {
            foreach (ModelMesh mesh in model.Meshes)
            {

                foreach (BasicEffect effect in mesh.Effects)
                {
                    //effect.EnableDefaultLighting();
                    effect.World = Matrix.CreateTranslation(0, 400, 5000);
                    effect.View = Camera.View;
                    effect.Projection = Camera.Projection;
                }
                DebugShapeRenderer.AddBoundingSphere(boundingSphere, Color.Red);

                mesh.Draw();
            }
        }
    }
}
