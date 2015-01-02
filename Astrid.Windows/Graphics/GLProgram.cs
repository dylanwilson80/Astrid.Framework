using OpenTK.Graphics.OpenGL;

namespace Astrid.Windows.Graphics
{
    public class GLProgram
    {
        public GLProgram()
        {
        }

        public int Id { get; private set; }

        public void Create()
        {
            Id = GL.CreateProgram();
            GLError.ThrowOnError(Id);
        }

        public void Link()
        {
            GL.LinkProgram(Id);
            GLError.ThrowOnError(Id);
        }

        public void AttachShader(GLShader shader)
        {
            GL.AttachShader(Id, shader.Id);
            GLError.ThrowOnError(Id);
        }

        public void Use()
        {
            GL.UseProgram(Id);
            GLError.ThrowOnError(Id);
        }

        public int GetAttribLocation(string name)
        {
            var location = GL.GetAttribLocation(Id, name);
            GLError.ThrowOnError(Id);
            return location;
        }

        public int GetUniformLocation(string name)
        {
            var location = GL.GetUniformLocation(Id, name);
            GLError.ThrowOnError(Id);
            return location;
        }
    }
}
