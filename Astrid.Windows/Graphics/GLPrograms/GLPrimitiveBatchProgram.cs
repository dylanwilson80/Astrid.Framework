using System;
using OpenTK.Graphics.ES20;

namespace Astrid.Windows.Graphics.GLPrograms
{
    public class GLPrimitiveBatchProgram : GLBatchProgram
    {
        protected override GLShader GetVertexShader()
        {
            const string code =
                "uniform mat4 u_Matrix; \n" +
                "uniform mat4 u_ViewMatrix; \n" +
                "attribute vec4 a_Position; \n" +
                "attribute vec4 a_Color; \n" +
                "varying vec4 v_Color; \n" +
                "void main() \n" +
                "{ \n" +
                "    v_Color = a_Color; \n" +
                "    gl_Position = u_Matrix * u_ViewMatrix * a_Position; \n" +
                "} \n";
            return new GLShader(ShaderType.VertexShader, code);
        }

        protected override GLShader GetFragmentShader()
        {
            const string code =
                "precision mediump float; \n" +
                "varying vec4 v_Color; \n" +
                "void main() \n" +
                "{ \n" +
                "    gl_FragColor = v_Color; \n" +
                "} \n";
            return new GLShader(ShaderType.FragmentShader, code);
        }

        protected override int CalculateStride()
        {
            return (PositionComponentCount + ColorCount) * BytesPerFloat;
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

                    // color
                    GL.VertexAttribPointer(AttribColor, 4, VertexAttribPointerType.Float, false, Stride, pinnedVerticesPtr + 8);
                    GL.EnableVertexAttribArray(AttribColor);

                    GL.DrawArrays(BeginMode.Lines, 0, vertexCount);
                    GL.Finish();
                }
            }
        }
    }
}