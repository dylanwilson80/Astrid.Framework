using System;
using System.Collections.Generic;
using Astrid.Core;

namespace Astrid
{
    public class PrimitiveBatch
    {
        public PrimitiveBatch(GraphicsDevice graphicsDevice)
        {
            _graphicsDevice = graphicsDevice;
        }

        private readonly GraphicsDevice _graphicsDevice;
        private readonly List<float> _vertexData = new List<float>();
        private bool _hasBegun;
        private int _vertexCount;
        private Matrix _viewMatrix;

        public void Begin(Matrix viewMatrix)
        {
            _hasBegun = true;
            _graphicsDevice.DisableDepthMask();
            _viewMatrix = viewMatrix;
        }

        public void Begin()
        {
            if (_hasBegun) throw new InvalidOperationException("A call to Begin must be followed by call to End");

            Begin(Matrix.Identity);
        }

        public void End()
        {
            if (!_hasBegun) throw new InvalidOperationException("A call to End must be preceeded by a call to Begin");

            Flush();
            _graphicsDevice.EnableDepthMask();
            _hasBegun = false;
        }

        private void Flush()
        {
            if (_vertexCount > 0)
            {
                _graphicsDevice.UsePrimitiveBatchProgram();
                _graphicsDevice.SetViewMatrix(_viewMatrix);
                _graphicsDevice.RenderBatch(_vertexData.ToArray(), _vertexCount);
                _vertexData.Clear();
                _vertexCount = 0;
            }
        }

        public void DrawRectangle(Color color, Rectangle rectangle)
        {
            DrawRectangle(color, rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height);
        }

        public void DrawRectangle(Color color, int x, int y, int width, int height)
        {
            // top
            AddVertex(new Vector2(x, y), color);
            AddVertex(new Vector2(x + width, y), color);

            // right
            AddVertex(new Vector2(x + width, y), color);
            AddVertex(new Vector2(x + width, y + height), color);

            // bottom
            AddVertex(new Vector2(x + width, y + height), color);
            AddVertex(new Vector2(x, y + height), color);

            // left
            AddVertex(new Vector2(x, y + height), color);
            AddVertex(new Vector2(x, y), color);
        }

        private void AddVertex(Vector2 position, Color color)
        {
            _vertexData.Add(position.X);
            _vertexData.Add(position.Y);
            _vertexData.Add(color.R / 255f);
            _vertexData.Add(color.G / 255f);
            _vertexData.Add(color.B / 255f);
            _vertexData.Add(color.A / 255f);
            _vertexCount++;
        }
    }
}