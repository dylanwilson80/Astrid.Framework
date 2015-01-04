using Astrid.Framework.Assets;

namespace Astrid.MonoGame
{
    public class MonoGameSoundEffect : SoundEffect
    {
        public MonoGameSoundEffect(Microsoft.Xna.Framework.Audio.SoundEffect soundEffect, int id) 
            : base(id, soundEffect.Name)
        {
        }
    }
}