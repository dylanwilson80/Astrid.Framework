using System;
using Astrid.Core;
using Astrid.Framework.Assets;

namespace Astrid.Framework.Graphics
{
    public abstract class GraphicsDevice
    {
        protected GraphicsDevice(int width, int height)
        {
            Width = width;
            Height = height;
        }

        public int Width { get; private set; }
        public int Height { get; private set; }

        public void Resize(int width, int height)
        {
            Width = width;
            Height = height;
            OnResize(width, height);
            Resized.Raise(this, EventArgs.Empty);
        }

        public event EventHandler Resized;

        protected abstract void OnResize(int width, int height);
        public abstract void EnableDepthMask();
        public abstract void DisableDepthMask();
        public abstract void BindTexture(Texture texture);
        public abstract void RenderBatch(float[] vertexData, int vertexCount);
        public abstract void Clear(Color color);
        public abstract void SetViewMatrix(Matrix viewMatrix);
        public abstract void UsePrimitiveBatchProgram();
        public abstract void UseSpriteBatchProgram();
    }
}