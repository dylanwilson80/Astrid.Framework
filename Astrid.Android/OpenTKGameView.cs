using System;
using Android.Content;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.ES20;
using OpenTK.Platform.Android;

namespace Astrid.Android
{
    public class OpenTKGameView : AndroidGameView
    {
        public OpenTKGameView(Context context)
            : base(context)
        {
            ContextRenderingApi = GLVersion.ES2;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            var vertexShaderId = CompileShader(ShaderType.VertexShader, _vertexShader);
            var fragmentShaderId = CompileShader(ShaderType.FragmentShader, _fragmentShader);

            _programId = GL.CreateProgram();
            GL.AttachShader(_programId, vertexShaderId);
            GL.AttachShader(_programId, fragmentShaderId);
            GL.LinkProgram(_programId);

            if (GL.GetErrorCode() != ErrorCode.NoError)
            {
                var log = GL.GetProgramInfoLog(_programId);
                throw new InvalidOperationException(log);
            }

            GL.UseProgram(_programId);

            _colorUniformLocation = GL.GetUniformLocation(_programId, "u_Color");
            _positionAttributeLocation = GL.GetAttribLocation(_programId, "a_Position");

            if (GL.GetErrorCode() != ErrorCode.NoError)
            {
                var log = GL.GetProgramInfoLog(_programId);
                throw new InvalidOperationException(log);
            }
        }

        private static int CompileShader(ShaderType shaderType, string code)
        {
            var shaderId = GL.CreateShader(shaderType);
            GL.ShaderSource(shaderId, code);
            GL.CompileShader(shaderId);

            if (GL.GetErrorCode() != ErrorCode.NoError)
            {
                var log = GL.GetShaderInfoLog(shaderId);
                throw new InvalidOperationException(log);
            }

            return shaderId;
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            GL.ClearColor(1.0f, 0.0f, 0.0f, 1.0f);
            GL.Clear(ClearBufferMask.ColorBufferBit);

            //GL.Viewport(0, 0, Width, Height);
            //GL.UseProgram(_programId);

            // pin the data, so that GC doesn't move them, while used
            // by native code
            unsafe
            {
                fixed (float* pinnedVertices = _vertices)
                {
                    GL.VertexAttribPointer(_positionAttributeLocation, 2, VertexAttribPointerType.Float, false, 0, new IntPtr(pinnedVertices));
                    GL.EnableVertexAttribArray(_positionAttributeLocation);
                    GL.Uniform4(_colorUniformLocation, 1.0f, 0.0f, 1.0f, 1.0f);
                    GL.DrawArrays(BeginMode.Triangles, 0, 6);
                    GL.Finish();
                }
            }

            SwapBuffers();
        }

        private readonly float[] _vertices = 
        {
            -0.5f, -0.5f,
             0.5f,  0.5f,
            -0.5f,  0.5f,
            
            -0.5f, -0.5f,
             0.5f, -0.5f,
             0.5f,  0.5f,
        };

        private int _colorUniformLocation;
        private int _positionAttributeLocation;
        private int _programId;

        private const string _vertexShader =
            "attribute vec4 a_Position; \n" +
            "void main() \n" +
            "{ \n" +
            "    gl_Position = a_Position; \n" +
            "} \n";

        private const string _fragmentShader =
            "precision mediump float; \n" +
            "uniform vec4 u_Color; \n" +
            "void main() \n" +
            "{ \n" +
            "    gl_FragColor = u_Color; \n" +
            "} \n";
    }
}