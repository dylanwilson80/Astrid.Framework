using System;
using OpenTK.Graphics.ES20;

namespace Astrid.Windows
{
    public abstract class GLBatchProgram
    {
        protected GLBatchProgram()
        {
        }

        public int Id { get; protected set; }

        public virtual void Build()
        {
            var vertexShader = GetVertexShader();
            var fragmentShader = GetFragmentShader();

            vertexShader.Compile();
            fragmentShader.Compile();

            Id = GL.CreateProgram();
            GL.AttachShader(Id, vertexShader.Id);
            GL.AttachShader(Id, fragmentShader.Id);
            GL.LinkProgram(Id);
            
            int isLinked;
            GL.GetProgram(Id, ProgramParameter.LinkStatus, out isLinked);

            if(isLinked == 0)
            {
	            int maxLength;
                string log;
                GL.GetProgram(Id, ProgramParameter.InfoLogLength, out maxLength);

                GL.GetProgramInfoLog(Id, out log);
                
                GL.DeleteProgram(Id);
                GL.DeleteShader(vertexShader.Id);
                GL.DeleteShader(fragmentShader.Id);
                throw new InvalidOperationException(log);
            }
 
            //Always detach shaders after a successful link.
            GL.DetachShader(Id, vertexShader.Id);
            GL.DetachShader(Id, fragmentShader.Id);

            GL.UseProgram(Id);

            AttribPosition = GL.GetAttribLocation(Id, "a_Position");
            AttribColor = GL.GetAttribLocation(Id, "a_Color");
            Matrix = GL.GetUniformLocation(Id, "u_Matrix");
            UniformViewMatrix = GL.GetUniformLocation(Id, "u_ViewMatrix");

            Stride = CalculateStride();
        }

        protected const int BytesPerFloat = 4;
        protected const int PositionComponentCount = 2;
        protected const int ColorCount = 4;
        
        protected int Stride { get; set; }
        protected int AttribPosition { get; set; }
        protected int Matrix { get; set; }
        protected int UniformViewMatrix { get; set; }
        protected int AttribColor { get; set; }

        public int MatrixLocation { get { return Matrix; } }
        public int ViewMatrixLocation { get { return UniformViewMatrix; } }

        protected abstract GLShader GetVertexShader();
        protected abstract GLShader GetFragmentShader();
        protected abstract int CalculateStride();

        public abstract void Render(float[] vertexData, int vertexCount);
    }
}