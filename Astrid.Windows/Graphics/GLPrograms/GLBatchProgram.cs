namespace Astrid.Windows.Graphics.GLPrograms
{
    public abstract class GLBatchProgram : GLProgram
    {
        protected GLBatchProgram()
        {
        }

        public virtual void Build()
        {
            var vertexShader = GetVertexShader();
            var fragmentShader = GetFragmentShader();

            vertexShader.Compile();
            fragmentShader.Compile();

            Create();
            AttachShader(vertexShader);
            AttachShader(fragmentShader);
            Link();
            Use();

            AttribPosition = GetAttribLocation("a_Position");
            AttribColor = GetAttribLocation("a_Color");
            Matrix = GetUniformLocation("u_Matrix");
            UniformViewMatrix = GetUniformLocation("u_ViewMatrix");

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