#region License

/*
MIT License
Copyright © 2006 The Mono.Xna Team

All rights reserved.

Authors:
Olivier Dufour (Duff)

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/

#endregion License

using System;
using System.Text;

namespace Astrid.Core
{
    public class BoundingFrustum : IEquatable<BoundingFrustum>
    {
        #region Private Fields

        private const int PlaneCount = 6;
        private readonly Vector3[] corners = new Vector3[CornerCount];
        private readonly Plane[] planes = new Plane[PlaneCount];
        private Matrix matrix;

        #endregion Private Fields

        #region Public Fields

        public const int CornerCount = 8;

        #endregion

        #region Public Constructors

        public BoundingFrustum(Matrix value)
        {
            matrix = value;
            CreatePlanes();
            CreateCorners();
        }

        #endregion Public Constructors

        #region Public Properties

        public Matrix Matrix
        {
            get { return matrix; }
            set
            {
                matrix = value;
                CreatePlanes(); // FIXME: The odds are the planes will be used a lot more often than the matrix
                CreateCorners(); // is updated, so this should help performance. I hope ;)
            }
        }

        public Plane Near
        {
            get { return planes[0]; }
        }

        public Plane Far
        {
            get { return planes[1]; }
        }

        public Plane Left
        {
            get { return planes[2]; }
        }

        public Plane Right
        {
            get { return planes[3]; }
        }

        public Plane Top
        {
            get { return planes[4]; }
        }

        public Plane Bottom
        {
            get { return planes[5]; }
        }

        #endregion Public Properties

        #region Public Methods

        public bool Equals(BoundingFrustum other)
        {
            return (this == other);
        }

        public static bool operator ==(BoundingFrustum a, BoundingFrustum b)
        {
            if (Equals(a, null))
                return (Equals(b, null));

            if (Equals(b, null))
                return (Equals(a, null));

            return a.matrix == (b.matrix);
        }

        public static bool operator !=(BoundingFrustum a, BoundingFrustum b)
        {
            return !(a == b);
        }

        public ContainmentType Contains(BoundingBox box)
        {
            ContainmentType result = default(ContainmentType);
            Contains(ref box, out result);
            return result;
        }

        public void Contains(ref BoundingBox box, out ContainmentType result)
        {
            bool intersects = false;
            for (int i = 0; i < PlaneCount; ++i)
            {
                PlaneIntersectionType planeIntersectionType = default(PlaneIntersectionType);
                box.Intersects(ref planes[i], out planeIntersectionType);
                switch (planeIntersectionType)
                {
                    case PlaneIntersectionType.Front:
                        result = ContainmentType.Disjoint;
                        return;
                    case PlaneIntersectionType.Intersecting:
                        intersects = true;
                        break;
                }
            }
            result = intersects ? ContainmentType.Intersects : ContainmentType.Contains;
        }

        /*
        public ContainmentType Contains(BoundingFrustum frustum)
        {
            if (this == frustum)                // We check to see if the two frustums are equal
                return ContainmentType.Contains;// If they are, there's no need to go any further.

            throw new NotImplementedException();
        }
        */

        public ContainmentType Contains(BoundingSphere sphere)
        {
            ContainmentType result = default(ContainmentType);
            Contains(ref sphere, out result);
            return result;
        }

        public void Contains(ref BoundingSphere sphere, out ContainmentType result)
        {
            bool intersects = false;
            for (int i = 0; i < PlaneCount; ++i)
            {
                PlaneIntersectionType planeIntersectionType = default(PlaneIntersectionType);

                // TODO: we might want to inline this for performance reasons
                sphere.Intersects(ref planes[i], out planeIntersectionType);
                switch (planeIntersectionType)
                {
                    case PlaneIntersectionType.Front:
                        result = ContainmentType.Disjoint;
                        return;
                    case PlaneIntersectionType.Intersecting:
                        intersects = true;
                        break;
                }
            }
            result = intersects ? ContainmentType.Intersects : ContainmentType.Contains;
        }

        public ContainmentType Contains(Vector3 point)
        {
            ContainmentType result = default(ContainmentType);
            Contains(ref point, out result);
            return result;
        }

        public void Contains(ref Vector3 point, out ContainmentType result)
        {
            for (int i = 0; i < PlaneCount; ++i)
            {
                // TODO: we might want to inline this for performance reasons
                if (ClassifyPoint(ref point, ref planes[i]) > 0)
                {
                    result = ContainmentType.Disjoint;
                    return;
                }
            }
            result = ContainmentType.Contains;
        }

        private static float ClassifyPoint(ref Vector3 point, ref Plane plane)
        {
            return point.X * plane.Normal.X + point.Y * plane.Normal.Y + point.Z * plane.Normal.Z + plane.D;
        }

        public override bool Equals(object obj)
        {
            var f = obj as BoundingFrustum;
            return (Equals(f, null)) ? false : (this == f);
        }

        public Vector3[] GetCorners()
        {
            return (Vector3[]) corners.Clone();
        }

        public void GetCorners(Vector3[] corners)
        {
            if (corners == null) throw new ArgumentNullException("corners");
            if (corners.Length < CornerCount) throw new ArgumentOutOfRangeException("corners");

            this.corners.CopyTo(corners, 0);
        }

        public override int GetHashCode()
        {
            return matrix.GetHashCode();
        }

        public bool Intersects(BoundingBox box)
        {
            bool result = false;
            Intersects(ref box, out result);
            return result;
        }

        public void Intersects(ref BoundingBox box, out bool result)
        {
            ContainmentType containment = default(ContainmentType);
            Contains(ref box, out containment);
            result = containment != ContainmentType.Disjoint;
        }

        /*
        public bool Intersects(BoundingFrustum frustum)
        {
            throw new NotImplementedException();
        }
        */

        public bool Intersects(BoundingSphere sphere)
        {
            bool result = default(bool);
            Intersects(ref sphere, out result);
            return result;
        }

        public void Intersects(ref BoundingSphere sphere, out bool result)
        {
            ContainmentType containment = default(ContainmentType);
            Contains(ref sphere, out containment);
            result = containment != ContainmentType.Disjoint;
        }

        /*
        public PlaneIntersectionType Intersects(Plane plane)
        {
            throw new NotImplementedException();
        }

        public void Intersects(ref Plane plane, out PlaneIntersectionType result)
        {
            throw new NotImplementedException();
        }

        public Nullable<float> Intersects(Ray ray)
        {
            throw new NotImplementedException();
        }

        public void Intersects(ref Ray ray, out Nullable<float> result)
        {
            throw new NotImplementedException();
        }
        */

        public override string ToString()
        {
            var sb = new StringBuilder(256);
            sb.Append("{Near:");
            sb.Append(planes[0]);
            sb.Append(" Far:");
            sb.Append(planes[1]);
            sb.Append(" Left:");
            sb.Append(planes[2]);
            sb.Append(" Right:");
            sb.Append(planes[3]);
            sb.Append(" Top:");
            sb.Append(planes[4]);
            sb.Append(" Bottom:");
            sb.Append(planes[5]);
            sb.Append("}");
            return sb.ToString();
        }

        #endregion Public Methods

        #region Private Methods

        private void CreateCorners()
        {
            IntersectionPoint(ref planes[0], ref planes[2], ref planes[4], out corners[0]);
            IntersectionPoint(ref planes[0], ref planes[3], ref planes[4], out corners[1]);
            IntersectionPoint(ref planes[0], ref planes[3], ref planes[5], out corners[2]);
            IntersectionPoint(ref planes[0], ref planes[2], ref planes[5], out corners[3]);
            IntersectionPoint(ref planes[1], ref planes[2], ref planes[4], out corners[4]);
            IntersectionPoint(ref planes[1], ref planes[3], ref planes[4], out corners[5]);
            IntersectionPoint(ref planes[1], ref planes[3], ref planes[5], out corners[6]);
            IntersectionPoint(ref planes[1], ref planes[2], ref planes[5], out corners[7]);
        }

        private void CreatePlanes()
        {
            planes[0] = new Plane(-matrix.M13, -matrix.M23, -matrix.M33, -matrix.M43);
            planes[1] = new Plane(matrix.M13 - matrix.M14, matrix.M23 - matrix.M24, matrix.M33 - matrix.M34,
                matrix.M43 - matrix.M44);
            planes[2] = new Plane(-matrix.M14 - matrix.M11, -matrix.M24 - matrix.M21, -matrix.M34 - matrix.M31,
                -matrix.M44 - matrix.M41);
            planes[3] = new Plane(matrix.M11 - matrix.M14, matrix.M21 - matrix.M24, matrix.M31 - matrix.M34,
                matrix.M41 - matrix.M44);
            planes[4] = new Plane(matrix.M12 - matrix.M14, matrix.M22 - matrix.M24, matrix.M32 - matrix.M34,
                matrix.M42 - matrix.M44);
            planes[5] = new Plane(-matrix.M14 - matrix.M12, -matrix.M24 - matrix.M22, -matrix.M34 - matrix.M32,
                -matrix.M44 - matrix.M42);

            NormalizePlane(ref planes[0]);
            NormalizePlane(ref planes[1]);
            NormalizePlane(ref planes[2]);
            NormalizePlane(ref planes[3]);
            NormalizePlane(ref planes[4]);
            NormalizePlane(ref planes[5]);
        }

        private static void IntersectionPoint(ref Plane a, ref Plane b, ref Plane c, out Vector3 result)
        {
            // Formula used
            //                d1 ( N2 * N3 ) + d2 ( N3 * N1 ) + d3 ( N1 * N2 )
            //P =   -------------------------------------------------------------------------
            //                             N1 . ( N2 * N3 )
            //
            // Note: N refers to the normal, d refers to the displacement. '.' means dot product. '*' means cross product

            Vector3 v1, v2, v3;
            Vector3 cross;

            Vector3.Cross(ref b.Normal, ref c.Normal, out cross);

            float f;
            Vector3.Dot(ref a.Normal, ref cross, out f);
            f *= -1.0f;

            Vector3.Cross(ref b.Normal, ref c.Normal, out cross);
            Vector3.Multiply(ref cross, a.D, out v1);
            //v1 = (a.D * (Vector3.Cross(b.Normal, c.Normal)));


            Vector3.Cross(ref c.Normal, ref a.Normal, out cross);
            Vector3.Multiply(ref cross, b.D, out v2);
            //v2 = (b.D * (Vector3.Cross(c.Normal, a.Normal)));


            Vector3.Cross(ref a.Normal, ref b.Normal, out cross);
            Vector3.Multiply(ref cross, c.D, out v3);
            //v3 = (c.D * (Vector3.Cross(a.Normal, b.Normal)));

            result.X = (v1.X + v2.X + v3.X) / f;
            result.Y = (v1.Y + v2.Y + v3.Y) / f;
            result.Z = (v1.Z + v2.Z + v3.Z) / f;
        }

        private void NormalizePlane(ref Plane p)
        {
            float factor = 1f / p.Normal.Length();
            p.Normal.X *= factor;
            p.Normal.Y *= factor;
            p.Normal.Z *= factor;
            p.D *= factor;
        }

        #endregion
    }
}