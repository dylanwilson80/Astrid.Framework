using System;
using OpenTK.Graphics.OpenGL;

namespace Astrid.Windows.Graphics
{
    public static class GLError
    {
        public static void ThrowOnError(int programId)
        {
            if (GL.GetError() != ErrorCode.NoError)
            {
                var log = GL.GetProgramInfoLog(programId);
                throw new InvalidOperationException(log);
            }
        }
    }
}
