using System.Collections.Generic;
using Astrid.Core;
using Astrid.Framework.Assets;

namespace Astrid.Framework.Graphics
{
    public abstract class GraphicsDevice
    {
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