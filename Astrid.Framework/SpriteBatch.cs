using System;
using System.Collections.Generic;
using Astrid.Core;
using Astrid.Framework.Assets;

namespace Astrid.Framework
{
    public class SpriteBatch
    {
        public SpriteBatch(GraphicsDevice graphicsDevice)
        {
            _graphicsDevice = graphicsDevice;
            _hasBegun = false;
        }

        private readonly GraphicsDevice _graphicsDevice;
        private readonly List<float> _vertexData = new List<float>();
        private bool _hasBegun;
        private int _vertexCount;
        private Texture _currentTexture;
        private Matrix _viewMatrix;

        public void Begin(Matrix viewMatrix)
        {
            _hasBegun = true;
            _graphicsDevice.DisableDepthMask();
            _viewMatrix = viewMatrix;
        }

        public void Begin()
        {
            Begin(Matrix.Identity);
        }

        public void End()
        {
            Flush();
            _currentTexture = null;
            _graphicsDevice.EnableDepthMask();
            _hasBegun = false;
        }

        private void Flush()
        {
            if (_vertexCount > 0 && _currentTexture != null)
            {
                _graphicsDevice.UseSpriteBatchProgram();
                _graphicsDevice.BindTexture(_currentTexture);
                _graphicsDevice.SetViewMatrix(_viewMatrix);
                _graphicsDevice.RenderBatch(_vertexData.ToArray(), _vertexCount);
                _vertexData.Clear();
                _vertexCount = 0;
            }
        }

        private static Vector2 RotatePoint(Vector2 point ,float cos, float sin)
        {
            var rx = cos * point.X - sin * point.Y;
            var ry = sin * point.X + cos * point.Y;
            return new Vector2(rx, ry);
        }

        private Vector2[] CreatePoints(Vector2 position, float width, float height, Vector2 origin, float rotation, Vector2 scale)
        {
            var worldOriginX = position.X;
            var worldOriginY = position.Y;

            var localOriginX = origin.X * width;
            var localOriginY = origin.Y * height;
            var tx0 = -localOriginX;
            var ty0 = -localOriginY;
            var tx1 = width - localOriginX;
            var ty1 = height - localOriginY;

            if (scale != Vector2.One)
            {
                tx0 *= scale.X;
                ty0 *= scale.Y;
                tx1 *= scale.X;
                ty1 *= scale.Y;
            }

            var points = new Vector2[4];
            points[0] = new Vector2(tx0, ty0);
            points[1] = new Vector2(tx1, ty0);
            points[2] = new Vector2(tx1, ty1);
            points[3] = new Vector2(tx0, ty1);

            // ReSharper disable once CompareOfFloatsByEqualityOperator
            if (rotation != 0)
            {
                var cos = (float)Math.Cos(rotation);
                var sin = (float)Math.Sin(rotation);

                points[0] = RotatePoint(points[0], cos, sin);
                points[1] = RotatePoint(points[1], cos, sin);
                points[2] = RotatePoint(points[2], cos, sin);
                points[3] = RotatePoint(points[3], cos, sin);
            }

            points[0].X += worldOriginX;
            points[0].Y += worldOriginY;
            points[1].X += worldOriginX;
            points[1].Y += worldOriginY;
            points[2].X += worldOriginX;
            points[2].Y += worldOriginY;
            points[3].X += worldOriginX;
            points[3].Y += worldOriginY;

            return points;
        }

        private void AddQuad(Vector2[] points, Color color, float u0, float v0, float u1, float v1)
        {
            AddVertex(points[0], color, u0, v0);  // TL
            AddVertex(points[1], color, u1, v0);  // TR
            AddVertex(points[2], color, u1, v1);  // BR

            AddVertex(points[0], color, u0, v0);  // TL
            AddVertex(points[2], color, u1, v1);  // BR
            AddVertex(points[3], color, u0, v1);  // BL
        }

        private void AddVertex(Vector2 position, Color color, float u, float v)
        {
            _vertexData.Add(position.X);
            _vertexData.Add(position.Y);
            _vertexData.Add(u);
            _vertexData.Add(v);
            _vertexData.Add(color.R / 255f);
            _vertexData.Add(color.G / 255f);
            _vertexData.Add(color.B / 255f);
            _vertexData.Add(color.A / 255f);
            _vertexCount++;
        }

        private void PrepareTexture(Texture texture)
        {
            if (!_hasBegun) throw new InvalidOperationException("Begin must be called before Update");

            if (texture != _currentTexture)
            {
                Flush();
                _currentTexture = texture;
            }
        }

        public void Draw(Texture texture, Vector2 position, Color color, Vector2 origin, float rotation, Vector2 scale)
        {
            PrepareTexture(texture);
            var points = CreatePoints(position, texture.Width, texture.Height, origin, rotation, scale);
            AddQuad(points, color, 0.0f, 0.0f, 1.0f, 1.0f);
        }

        public void Draw(Texture texture, Vector2 position)
        {
            Draw(texture, position, Color.White, Vector2.Zero, 0, Vector2.One);
        }

        public void Draw(Texture texture, int x, int y, int width, int height)
        {
            PrepareTexture(texture);
            var position = new Vector2(x, y);
            var points = CreatePoints(position, width, height, Vector2.Zero, 0, Vector2.One);
            AddQuad(points, Color.White, 0.0f, 0.0f, 1.0f, 1.0f);
        }

        public void Draw(TextureRegion textureRegion, Vector2 position)
        {
            Draw(textureRegion, position, Color.White, Vector2.Zero, 0, Vector2.One);
        }

        public void Draw(TextureRegion textureRegion, Vector2 position, Color color, Vector2 origin, float rotation, Vector2 scale)
        {
            PrepareTexture(textureRegion.Texture);
            var points = CreatePoints(position, textureRegion.Width, textureRegion.Height, origin, rotation, scale);
            var uv = textureRegion.GetUV();
            AddQuad(points, color, uv[0], uv[1], uv[2], uv[3]);
        }

        public void Draw(Sprite sprite, Vector2 position, float rotation, Vector2 scale)
        {
            if (sprite.TextureRegion == null || !sprite.IsVisible)
                return;

            Draw(sprite.TextureRegion, position, sprite.Color, sprite.Origin, rotation, scale);
        }
    }
}
