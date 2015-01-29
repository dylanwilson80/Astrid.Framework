using System;
using System.Text;
using Common.Logging;
using OpenTK.Graphics.ES20;

namespace Astrid.Windows.Graphics
{
    public class GLShader
    {
        private static readonly ILog _logger = LogManager.GetCurrentClassLogger();

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

            if (shaderId == 0)
                throw new InvalidOperationException("Unable to create shader");
            
            GL.ShaderSource(shaderId, _code);
            GL.CompileShader(shaderId);

            int isCompiled;
            GL.GetShader(shaderId, ShaderParameter.CompileStatus, out isCompiled);

            if (isCompiled == 0)
            {
                int length;
                GL.GetShader(shaderId, ShaderParameter.InfoLogLength, out length);

                if (length > 0)
                {
                    var log = new StringBuilder(length);
                    GL.GetShaderInfoLog(shaderId, length, out length, log);
                    _logger.Error(log);
                }

                GL.DeleteShader(shaderId);
                throw new InvalidOperationException(string.Format("Unable to compile shader of type {0}" + ShaderType));
            }

            Id = shaderId;
        }
    }
}
