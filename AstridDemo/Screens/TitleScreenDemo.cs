using System;
using Astrid.Core;
using Astrid.Framework;
using Astrid.Framework.Animations;
using Astrid.Framework.Assets;
using Astrid.Framework.Assets.Fonts;
using Astrid.Framework.Entities.Components;
using Astrid.Framework.Graphics;
using Astrid.Framework.Gui;
using Astrid.Framework.Screens;

namespace AstridDemo.Screens
{
    public class ActionManager
    {
        private readonly AnimationSystem _animationSystem;
        private readonly ITransformable _target;

        public ActionManager(AnimationSystem animationSystem, ITransformable target)
        {
            _animationSystem = animationSystem;
            _target = target;
        }

        public ActionManager MoveTo(Vector2 position, float duration, EasingFunction easingFunction)
        {
            var animation = new Vector2Animation(_target.Position, position, v => _target.Position = v, duration)
            {
                EasingFunction = easingFunction
            };
            _animationSystem.Attach(animation);
            return this;
        }

        public ActionManager RotateTo(float rotation, float duration, EasingFunction easingFunction)
        {
            var animation = new FloatAnimation(_target.Rotation, rotation, r => _target.Rotation = r, duration)
            {
                EasingFunction = easingFunction
            };
            _animationSystem.Attach(animation);
            return this;
        }
    }

    public class TitleScreenDemo : Screen
    {
        public TitleScreenDemo(GameBase game) 
            : base(game)
        {
        }

        private AnimationSystem _animationSystem;

        public override void Show()
        {
            _animationSystem = new AnimationSystem();

            var guiLayer = new GuiLayer(GraphicsDevice);

            var font = AssetManager.Load("CourierNew_32.fnt", new BitmapFontLoader());
            var label = new GuiLabel(font, null)
            {
                Text = "Welcome to", 
                TextColor = new Color(81, 32, 0),
                Position = new Vector2(400, -45)
            };
            guiLayer.Controls.Add(label);

            var logoTexture = AssetManager.Load<Texture>("AstridLogo.png");
            var image = new GuiImage(logoTexture)
            {
                Position = new Vector2(400, -100),
                
            };
            guiLayer.Controls.Add(image);

            var buttonTexture = AssetManager.Load<Texture>("PlayButton.png");
            var normalSprite = new Sprite(buttonTexture);
            var pressedSprite = new Sprite(buttonTexture) {Color = Color.Gray};
            var playButton = new GuiButton(normalSprite, pressedSprite)
            {
                Position = new Vector2(400, -260),
                Rotation = MathHelper.TwoPi
            };
            playButton.Click += PlayButtonOnClick;
            guiLayer.Controls.Add(playButton);
            Layers.Add(guiLayer);

            var actionManager = new ActionManager(_animationSystem, playButton);
            actionManager
                .MoveTo(new Vector2(400, 240), 1.0f, EasingFunctions.CubicEaseInOut)
                .RotateTo(0, 1.0f, EasingFunctions.CubicEaseInOut);

            var actionManager2 = new ActionManager(_animationSystem, label);
            actionManager2
                .MoveTo(new Vector2(400, 45), 1.0f, EasingFunctions.CubicEaseInOut)
                .RotateTo(0, 1.0f, EasingFunctions.CubicEaseInOut);

            var actionManager3 = new ActionManager(_animationSystem, image);
            actionManager3
                .MoveTo(new Vector2(400, 100), 1.0f, EasingFunctions.CubicEaseInOut)
                .RotateTo(0, 1.0f, EasingFunctions.CubicEaseInOut);

            base.Show();
        }

        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);

            _animationSystem.Update(deltaTime);
        }

        private void PlayButtonOnClick(object sender, EventArgs eventArgs)
        {
            SetScreen(new AnimationDemo(Game));
        }
    }
}