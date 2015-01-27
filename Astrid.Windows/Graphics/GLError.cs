using System;
#if ANDROID
using OpenTK.Graphics.ES20;
#else
using OpenTK.Graphics.OpenGL;
#endif

namespace Astrid.Windows.Graphics
{
    public static class GLError
    {
        public static void ThrowOnError(Func<string> getErrorMessage)
        {
#if ANDROID
            if(GL.GetErrorCode() != ErrorCode.NoError)
#else
            if (GL.GetError() != ErrorCode.NoError)
#endif
            {
                throw new InvalidOperationException(getErrorMessage());
            }
        }
    }
}
