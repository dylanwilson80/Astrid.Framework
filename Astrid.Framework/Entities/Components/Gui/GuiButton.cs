namespace Astrid.Framework.Entities.Components.Gui
{
    public class GuiButton : GuiControl
    {
        public GuiButton()
        {
        }

        public override string ToString()
        {
            return string.Format("{0} {1}", GetCurrentSprite(), IsPressed);
        }
    }
}