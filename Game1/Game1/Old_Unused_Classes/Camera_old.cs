﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1
{
    class Camera_old
    {
        private Matrix projection;

        public Matrix Projection
        {
            get { return projection; }
            set { projection = value; }
        }

        private Matrix view;

        public Matrix View
        {
            get { return view; }
            set { view = value; }
        }

        private Vector3 position;

        public Vector3 Position
        {
            get { return position; }
            set { position = value; }
        }

        public Camera_old(Vector3 position, GraphicsDeviceManager graphics)
        { 
            view = Matrix.CreateLookAt(Vector3.Zero, Vector3.Forward, Vector3.Up);
            projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(50f), graphics.PreferredBackBufferWidth / graphics.PreferredBackBufferHeight, 1, 100000);
        }

    }
}
