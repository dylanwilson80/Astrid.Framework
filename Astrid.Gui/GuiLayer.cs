using System.Collections.Generic;
using Astrid.Framework;

namespace Astrid.Gui
{
    public class GuiLayer : ScreenLayer
    {
        public GuiLayer(GraphicsDevice graphicsDevice)
        {
            Controls = new List<GuiControl>();
            _spriteBatch = new SpriteBatch(graphicsDevice);
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
            _spriteBatch.Begin();

            foreach (var control in Controls)
                control.Draw(_spriteBatch);

            _spriteBatch.End();
        }
    }
}