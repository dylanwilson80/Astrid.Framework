
using Microsoft.Xna.Framework.Graphics;

namespace Astrid.MonoGame
{
    public class MonoGameGraphicsDevice : Astrid.Framework.Graphics.GraphicsDevice
    {
        private readonly Microsoft.Xna.Framework.Graphics.GraphicsDevice _graphicsDevice;
        private readonly Microsoft.Xna.Framework.Graphics.SpriteBatch _spriteBatch;

        public MonoGameGraphicsDevice(Microsoft.Xna.Framework.Graphics.GraphicsDevice graphicsDevice, Astrid.Framework.Graphics.Camera camera)
        {
            _graphicsDevice = graphicsDevice;
            _spriteBatch = new Microsoft.Xna.Framework.Graphics.SpriteBatch(_graphicsDevice);
        }

        public override void EnableDepthMask()
        {
        }

        public override void DisableDepthMask()
        {
        }

        private Microsoft.Xna.Framework.Graphics.Texture2D _currentTexture;

        public override void BindTexture(Astrid.Framework.Assets.Texture texture)
        {
            var monoGameTexture = (MonoGameTexture) texture;
            _currentTexture = monoGameTexture.Texture;
        }

        public override void RenderBatch(float[] vertexData, int vertexCount)
        {
            var j = 0;
            _spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null);

            //for (var i = 0; i < vertexCount; i++)
            //{
                var position = new Microsoft.Xna.Framework.Vector2(vertexData[j + 0], vertexData[j + 1]);
                var color = new Microsoft.Xna.Framework.Color(vertexData[j + 4], vertexData[j + 5], vertexData[j + 6], vertexData[j + 7]);

                _spriteBatch.Draw(_currentTexture, position, color);
                j += 8;
            //

            _spriteBatch.End();
        }

        public override void UsePrimitiveBatchProgram()
        {
        }

        public override void UseSpriteBatchProgram()
        {
        }

        public override void SetViewMatrix(Astrid.Core.Matrix viewMatrix)
        {
        }

        public override void Clear(Astrid.Core.Color c)
        {
            var color = new Microsoft.Xna.Framework.Color(c.R, c.G, c.B, c.A);
            _graphicsDevice.Clear(color);
        }
    }
}