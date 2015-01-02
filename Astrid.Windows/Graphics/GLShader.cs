using System;
using OpenTK.Graphics.OpenGL;

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

            if (GL.GetError() != ErrorCode.NoError)
            {
                var log = GL.GetShaderInfoLog(shaderId);
                throw new InvalidOperationException(log);
            }

            Id = shaderId;
        }
    }
}
