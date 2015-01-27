using System;
#if ANDROID
using OpenTK.Graphics.ES20;
#else
using OpenTK.Graphics.OpenGL;
#endif

namespace Astrid.Windows.Graphics
{
    public class GLShader
    {
        public GLShader(ShaderType shaderType, string code)
        {
            _code = code;
            ShaderType = shaderType;
        }

        public int Id { get; private set; }
        public ShaderType ShaderType { get; private set; }

        private readonly string _code;

        public void Compile()
        {
            var shaderId = GL.CreateShader(ShaderType);
            GL.ShaderSource(shaderId, _code);
            GL.CompileShader(shaderId);
            GLError.ThrowOnError(() => GL.GetShaderInfoLog(shaderId));

            Id = shaderId;
        }
    }
}
