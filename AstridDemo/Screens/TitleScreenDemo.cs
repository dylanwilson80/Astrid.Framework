using System;
using Astrid;
using Astrid.Animations;
using Astrid.Core;
using Astrid.Gui;
using Astrid.Gui.Fonts;

namespace AstridDemo.Screens
{
    public class TitleScreenDemo : Screen
    {
        public TitleScreenDemo(GameBase game) 
            : base(game)
        {
            Viewport = new StretchViewport(GraphicsDevice, 800, 480);
        }

        private Music _music;
        private SoundEffect _soundEffect;

        public override void Show()
        {
            _music = AssetManager.Load<Music>("song.mp3");
            _soundEffect = AssetManager.Load<SoundEffect>("click.wav");

            var backgroundLayer = new SpriteLayer(Viewport);
            Layers.Add(backgroundLayer);

            var backgroundTexture = AssetManager.Load<Texture>("hills_800x480.png");
            var backgroundSprite = Sprite.Create(backgroundTexture, 0, 0, 800, 480);
            backgroundLayer.Sprites.Add(backgroundSprite);

            var guiLayer = new GuiLayer(Viewport);

            var font = AssetManager.Load("courier-new-32.fnt", new BitmapFontLoader());
            var label = new GuiLabel(font)
            {
                Text = "Welcome to", 
                TextColor = new Color(81, 32, 0),
                Position = new Vector2(400, 45)
            };
            guiLayer.Controls.Add(label);

            var logoTexture = AssetManager.Load<Texture>("astrid-logo.png");
            var image = new GuiImage(logoTexture)
            {
                Position = new Vector2(400, -150),
                Origin = new Vector2(0.5f, 1.0f)
            };
            guiLayer.Controls.Add(image);
            
            var parameters = new TransitionParameters(1.0f, EasingFunctions.QuadraticEaseIn);
            Animations.CreateSequence(image)
                .MoveTo(new Vector2(400, 150), parameters)
                .ScaleTo(new Vector2(1.2f, 0.6f), new TransitionParameters(0.2f, EasingFunctions.CubicEaseOut))
                .ScaleTo(new Vector2(1.0f, 1.0f), new TransitionParameters(0.2f, EasingFunctions.CubicEaseIn))
                .ScaleTo(new Vector2(0.8f, 1.2f), new TransitionParameters(0.2f, EasingFunctions.CubicEaseOut))
                .ScaleTo(new Vector2(1.0f, 1.0f), new TransitionParameters(0.2f, EasingFunctions.CubicEaseIn))
                .Play();

            var buttonTexture = AssetManager.Load<Texture>("play-button.png");
            var playButton = new GuiButton(buttonTexture.ToTextureRegion())
            {
                Position = new Vector2(400, -260),
                Rotation = MathHelper.TwoPi
            };
            playButton.Click += PlayButtonOnClick;
            guiLayer.Controls.Add(playButton);
            Layers.Add(guiLayer);

            var transitionParameters = new TransitionParameters(1.0f, EasingFunctions.CubicEaseInOut);
            Animations
                .CreateSequence(playButton)
                .MoveTo(new Vector2(50, 50), transitionParameters)
                .MoveTo(new Vector2(700, 400), transitionParameters)
                .MoveTo(new Vector2(700, 230), transitionParameters)
                .MoveTo(new Vector2(400, 250), transitionParameters)
                .Delay(1.0f)
                .RotateTo(0, transitionParameters)
                .Delay(1.0f)
                .RotateTo(MathHelper.TwoPi, transitionParameters)
                .FadeOut(transitionParameters)
                .FadeIn(transitionParameters)
                .Play();
            
            base.Show();
        }

        public override void Hide()
        {
            _music.Stop();
            _music.Dispose();
            _soundEffect.Dispose();
            base.Hide();
        }
        
        private void PlayButtonOnClick(object sender, EventArgs eventArgs)
        {
            _soundEffect.Play();

            if (_music.PlaybackState == PlaybackState.Playing)
                _music.Pause();
            else
                _music.Play();
        }
    }
}