using System.Collections.Generic;

namespace Astrid.Gui
{
    public class GuiLayer : ScreenLayer
    {
        public GuiLayer(Viewport viewport)
            : base(viewport)
        {
            Controls = new List<GuiControl>();
            _spriteBatch = new SpriteBatch(viewport.GraphicsDevice);
        }

        private readonly SpriteBatch _spriteBatch;

        public IList<GuiControl> Controls { get; private set; }

        public override void Update(float deltaTime, InputDevice inputDevice)
        {
            foreach (var control in Controls)
                control.Update(deltaTime, inputDevice);

            base.Update(deltaTime, inputDevice);
        }

        public override void Render(float deltaTime)
        {
            var viewMatrix = Viewport.Camera.GetViewMatrix();
            _spriteBatch.Begin(viewMatrix);

            foreach (var control in Controls)
                control.Draw(_spriteBatch);

            _spriteBatch.End();
        }
    }
}