using System;
using Astrid.Core;

namespace Astrid.Framework.Graphics
{
    public class Camera
    {
        public Camera()
        {
            Position = Vector2.Zero;
            Rotation = 0;
            Zoom = 1;
            Origin = Vector2.Zero;
        }

        public Vector2 Position { get; set; }
        public float Rotation { get; set; }
        public float Zoom { get; set; }
        public Vector2 Origin { get; set; }

        public Vector2 ToWorldSpace(Vector2 position)
        {
            return Vector2.Transform(position, Matrix.Invert(GetViewMatrix()));
        }

        public Vector2 ToScreenSpace(Vector2 position)
        {
            return Vector2.Transform(position, GetViewMatrix());
        }

        public Rectangle GetVisibileRectangle(int screenWidth, int screenHeight)
        {
            // NOT TESTED - Source: http://gamedev.stackexchange.com/questions/59301/xna-2d-camera-scrolling-why-use-matrix-transform
            var viewMatrix = GetViewMatrix();
            var inverseViewMatrix = Matrix.Invert(viewMatrix);
            var topLeft = Vector2.Transform(Vector2.Zero, inverseViewMatrix);
            var topRight = Vector2.Transform(new Vector2(screenWidth, 0), inverseViewMatrix);
            var bottomLeft = Vector2.Transform(new Vector2(0, screenHeight), inverseViewMatrix);
            var bottomRight = Vector2.Transform(new Vector2(screenWidth, screenHeight), inverseViewMatrix);
            var min = new Vector2(
                MathHelper.Min(topLeft.X, MathHelper.Min(topRight.X, MathHelper.Min(bottomLeft.X, bottomRight.X))),
                MathHelper.Min(topLeft.Y, MathHelper.Min(topRight.Y, MathHelper.Min(bottomLeft.Y, bottomRight.Y))));
            var max = new Vector2(
                MathHelper.Max(topLeft.X, MathHelper.Max(topRight.X, MathHelper.Max(bottomLeft.X, bottomRight.X))),
                MathHelper.Max(topLeft.Y, MathHelper.Max(topRight.Y, MathHelper.Max(bottomLeft.Y, bottomRight.Y))));

            return new Rectangle((int)min.X, (int)min.Y, (int)(max.X - min.X), (int)(max.Y - min.Y));
        }

        public Matrix GetViewMatrix(float parallaxFactor)
        {
            var cx = (int)(Position.X * parallaxFactor);
            var cy = (int)(Position.Y * parallaxFactor) ;
            return
                Matrix.CreateTranslation(new Vector3(-cx + 0.5f, -cy + 0.5f, 0)) *
                Matrix.CreateRotationZ(Rotation) *
                Matrix.CreateScale(Zoom) *
                Matrix.CreateTranslation(Origin.X, Origin.Y, 0);
        }

        public Matrix GetViewMatrix()
        {
            return GetViewMatrix(1.0f);
        }
    }
}
