using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EOTACD
{
    public class Camera
    {
        public Matrix transform;
        private Vector2 position;

        public Camera()
        {
        }

        public void Update(Vector2 position)
        {
            this.position = position;
            transform = Matrix.CreateScale(new Vector3(1, 1, 0)) * Matrix.CreateTranslation(new Vector3(-position.X, -position.Y, 0));
        }
    }
}
