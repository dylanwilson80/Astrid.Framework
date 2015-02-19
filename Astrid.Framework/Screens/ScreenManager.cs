namespace Astrid.Framework.Screens
{
    public class ScreenManager
    {
        public ScreenManager()
        {
        }

        private Screen _currentScreen;

        public void SetScreen(Screen newScreen)
        {
            if (_currentScreen != null)
                _currentScreen.Hide();

            _currentScreen = newScreen;

            if (_currentScreen != null)
                _currentScreen.Show();
        }

        public void Resize(int width, int height)
        {
            if(_currentScreen != null)
                _currentScreen.Resize(width, height);
        }

        public void Pause()
        {
            if (_currentScreen != null)
                _currentScreen.Pause();
        }

        public void Resume()
        {
            if (_currentScreen != null)
                _currentScreen.Resume();
        }

        public void Update(float deltaTime)
        {
            if (_currentScreen != null)
                _currentScreen.Update(deltaTime);
        }

        public void Render(float deltaTime)
        {
            if (_currentScreen != null)
                _currentScreen.Render(deltaTime);
        }
    }
}