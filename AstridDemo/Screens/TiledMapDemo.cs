using Astrid.Gui;
using Astrid.Maps;

namespace AstridDemo.Screens
{
    public class TiledMapDemo : Screen
    {
        public TiledMapDemo(IScreenContext game) 
            : base(game)
        {
        }

        public override void Show()
        {
            var tiledMap = AssetManager.Load<TiledMap>("tiled-map.json");


            base.Show();
        }
    }
}