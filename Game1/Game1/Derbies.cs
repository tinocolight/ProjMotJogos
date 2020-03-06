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
    public class Derbies
    {
        private Model model;
        public ModelMesh mesh;
        public Model Model
        {
            get { return model; }
            set { model = value; }
        }
        public Derbies(ContentManager content)
        {
            model = content.Load<Model>("modelo\\sky_sphere");
        }

        public void Draw()
        {
            foreach (ModelMesh mesh in model.Meshes)
            {

                foreach (BasicEffect effect in mesh.Effects)
                {
                    //effect.EnableDefaultLighting();
                    effect.World = Matrix.CreateTranslation(0, -150, -4000);
                    effect.View = Camera.View;
                    effect.Projection = Camera.Projection;
                }
                mesh.Draw();
            }
        }
    }
}
