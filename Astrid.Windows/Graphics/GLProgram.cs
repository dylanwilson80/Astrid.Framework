#if ANDROID
using OpenTK.Graphics.ES20;
#else
using OpenTK.Graphics.OpenGL;
#endif

namespace Astrid.Windows.Graphics
{
    public class GLProgram
    {
        public GLProgram()
        {
        }

        public int Id { get; private set; }

        private void CheckErrors()
        {
            GLError.ThrowOnError(() => GL.GetProgramInfoLog(Id));
        }

        public void Create()
        {
            Id = GL.CreateProgram();
            CheckErrors();
        }

        public void Link()
        {
            GL.LinkProgram(Id);
            CheckErrors();
        }

        public void AttachShader(GLShader shader)
        {
            GL.AttachShader(Id, shader.Id);
            CheckErrors();
        }

        public void Use()
        {
            GL.UseProgram(Id);
            CheckErrors();
        }

        public int GetAttribLocation(string name)
        {
            var location = GL.GetAttribLocation(Id, name);
            CheckErrors();
            return location;
        }

        public int GetUniformLocation(string name)
        {
            var location = GL.GetUniformLocation(Id, name);
            CheckErrors();
            return location;
        }
    }
}
