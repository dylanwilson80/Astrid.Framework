using Microsoft.Xna.Framework.Graphics;

namespace Astrid.MonoGame
{
    public class MonoGameTexture : Astrid.Framework.Assets.Texture
    {
        public MonoGameTexture(Texture2D texture2d, int id, string filePath) 
            : base(id, filePath, texture2d.Name, texture2d.Width, texture2d.Height)
        {
            Texture = texture2d;
        }

        public Texture2D Texture { get; private set; }
    }
}