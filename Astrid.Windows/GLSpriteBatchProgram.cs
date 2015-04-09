using System;
using OpenTK.Graphics.ES20;

namespace Astrid.Windows
{
    public class GLSpriteBatchProgram : GLBatchProgram
    {
        public GLSpriteBatchProgram()
        {
        }

        protected const int TextureCoordCount = 2;

        protected int AttribTextureCoordinates;

        public override void Build()
        {
            base.Build();

            AttribTextureCoordinates = GL.GetAttribLocation(Id, "a_TextureCoordinates");
        }

        protected override sealed GLShader GetVertexShader()
        {
            const string code =
                "uniform mat4 u_Matrix; \n" +
                "uniform mat4 u_ViewMatrix; \n" +
                "attribute vec4 a_Position; \n" +
                "attribute vec4 a_Color; \n" +
                "attribute vec2 a_TextureCoordinates; \n" +
                "varying vec4 v_Color; \n" +
                "varying vec2 v_TextureCoordinates; \n" +
                "void main() \n" +
                "{ \n" +
                "    v_Color = a_Color; \n" +
                "    v_TextureCoordinates = a_TextureCoordinates; \n" +
                "    gl_Position = u_Matrix * u_ViewMatrix * a_Position; \n" +
                "} \n";
            return new GLShader(ShaderType.VertexShader, code);
        }

        protected override sealed GLShader GetFragmentShader()
        {
            const string code =
                "precision mediump float; \n" +
                "uniform sampler2D u_TextureUnit; \n" +
                "varying vec4 v_Color; \n" +
                "varying vec2 v_TextureCoordinates; \n" +
                "void main() \n" +
                "{ \n" +
                "    gl_FragColor = v_Color * texture2D(u_TextureUnit, v_TextureCoordinates); \n" +
                "} \n";
            return new GLShader(ShaderType.FragmentShader, code);
        }

        protected override int CalculateStride()
        {
            return (PositionComponentCount + TextureCoordCount + ColorCount) * BytesPerFloat;
        }

        public override void Render(float[] vertexData, int vertexCount)
        {
            // pin the data, so that GC doesn't move them, while used
            // by native code
            unsafe
            {
                fixed (float* pinnedVertices = vertexData)
                {
                    var pinnedVerticesPtr = new IntPtr(pinnedVertices);

                    // positions
                    GL.VertexAttribPointer(AttribPosition, 2, VertexAttribPointerType.Float, false, Stride, pinnedVerticesPtr);
                    GL.EnableVertexAttribArray(AttribPosition);

                    // texture coords
                    GL.VertexAttribPointer(AttribTextureCoordinates, 2, VertexAttribPointerType.Float, false, Stride, pinnedVerticesPtr + 8);
                    GL.EnableVertexAttribArray(AttribTextureCoordinates);

                    // color
                    GL.VertexAttribPointer(AttribColor, 4, VertexAttribPointerType.Float, false, Stride, pinnedVerticesPtr + 16);
                    GL.EnableVertexAttribArray(AttribColor);
                    
                    GL.DrawArrays(BeginMode.Triangles, 0, vertexCount);
                    GL.Finish();
                }
            }
        }
    }
}