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
        public Model Model
        {
            get { return model; }
            set { model = value; }
        }
        public Ovulo(ContentManager content)
        {
            model = content.Load<Model>("modelo\\nucleo");
        }

        public void Draw()
        {
            foreach (ModelMesh mesh in model.Meshes)
            {

                foreach (BasicEffect effect in mesh.Effects)
                {
                    //effect.EnableDefaultLighting();
                    effect.World = Matrix.CreateTranslation(0, -300, -8000);
                    effect.View = Camera.View;
                    effect.Projection = Camera.Projection;
                }
                mesh.Draw();
            }
        }
    }
}
