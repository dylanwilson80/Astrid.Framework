using Astrid.Core;

namespace Astrid.Android
{
    public class AndroidGraphicsDevice : GraphicsDevice
    {
        public AndroidGraphicsDevice(int width, int height) 
            : base(width, height)
        {
        }

        protected override void OnResize(int width, int height)
        {
            throw new System.NotImplementedException();
        }

        public override void EnableDepthMask()
        {
            throw new System.NotImplementedException();
        }

        public override void DisableDepthMask()
        {
            throw new System.NotImplementedException();
        }

        public override void BindTexture(Texture texture)
        {
            throw new System.NotImplementedException();
        }

        public override void RenderBatch(float[] vertexData, int vertexCount)
        {
            throw new System.NotImplementedException();
        }

        public override void Clear(Color color)
        {
            throw new System.NotImplementedException();
        }

        public override void SetViewMatrix(Matrix viewMatrix)
        {
            throw new System.NotImplementedException();
        }

        public override void UsePrimitiveBatchProgram()
        {
            throw new System.NotImplementedException();
        }

        public override void UseSpriteBatchProgram()
        {
            throw new System.NotImplementedException();
        }
    }
}