using System.Collections.Generic;
using Astrid.Components.Components;
using Astrid.Framework;

namespace Astrid.Components.Systems
{
    public class DrawableSystem : ComponentSystem<Drawable>
    {
        public DrawableSystem(GraphicsDevice graphicsDevice, Camera camera)
        {
            _camera = camera;
            _spriteBatch = new SpriteBatch(graphicsDevice);
            _drawables = new List<Drawable>();

            ParallaxFactor = 1.0f;
        }

        public float ParallaxFactor { get; set; }

        private readonly Camera _camera;
        private readonly SpriteBatch _spriteBatch;
        private readonly List<Drawable> _drawables;

        protected override void OnAttached(Drawable drawable)
        {
            _drawables.Add(drawable);
        }

        protected override void OnDetached(Drawable drawable)
        {
            _drawables.Remove(drawable);
        }

        public void Draw(float deltaTime)
        {
            _spriteBatch.Begin(_camera.GetViewMatrix(ParallaxFactor));

            foreach (var drawable in _drawables)
                drawable.Draw(_spriteBatch);

            _spriteBatch.End();
        }

        public override void Update(float deltaTime)
        {   
        }
    }
}
