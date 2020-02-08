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
   public class ShipModel
    {
        private Model model;
        public ModelMesh mesh;
        public Model Model
        {
            get { return model; }
            set { model = value; }
        }
        public ShipModel(ContentManager content)
        {
            model = content.Load<Model>("modelo\\TESTE_COM_TEXTURAS");
        }
        
    }
}
