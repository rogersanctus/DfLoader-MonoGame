using Microsoft.Xna.Framework;

namespace DfLoader
{
    /// <summary>
    /// Matrixes for scaling, Z rotations and positioning transformations.
    /// You must call ApplyMatrix to update the inner matrix with the current transformations.
    /// </summary>
    public class Transformation
    {
        /// <summary>
        /// Transformation origin.
        /// </summary>
        public Vector2 Origin = Vector2.Zero;

        /// <summary>
        /// Transformation position.
        /// </summary>
        public Vector2 Position = Vector2.Zero;

        /// <summary>
        /// Transformation scale.
        /// </summary>
        public Vector2 Scale = Vector2.One;

        /// <summary>
        /// Transformation rotation.
        /// </summary>
        public float Rotation = 0;

        /// <summary>
        /// Get the inner matrix transformations.
        /// </summary>
        /// <returns>The transformation matrix.</returns>
        public Matrix GetMatrix() { return matrix; }

        /// <summary>
        /// Get the identity transformations.
        /// </summary>
        public static Transformation Identity { get { return new Transformation(); } }


        private Matrix matrix = Matrix.Identity;        


        public Transformation()
        {
        }

        /// <summary>
        /// Apply the current transformations to the transformations matrix
        /// </summary>
        public void ApplyMatrix()
        {
            matrix =
                Matrix.CreateTranslation(new Vector3(-Origin, 0.0f)) *
                Matrix.CreateScale(new Vector3(Scale, 1.0f)) *
                Matrix.CreateRotationZ(Rotation) *
                Matrix.CreateTranslation(new Vector3(Position, 0.0f));
        }


        /// <summary>
        /// Make a transformation composition of 'a' in function of 'b'.
        /// </summary>
        /// <param name="a">First transformation.</param>
        /// <param name="b">Second transformation.</param>
        /// <returns>The final transformation.</returns>
        public static Transformation Compose(Transformation a, Transformation b)
        {
            Transformation result = new Transformation();
            Vector2 transformedPosition = a.TransformVector(b.Position);            
            result.Position = transformedPosition;
            result.Rotation = a.Rotation + b.Rotation;
            result.Scale = a.Scale * b.Scale;
            return result;
        }

        /// <summary>
        /// Linear Interpolation between two transformation states.
        /// </summary>
        /// <param name="t1">Transformations from.</param>
        /// <param name="t2">Transformations to.</param>
        /// <param name="amount">How much to lerp.</param>
        /// <param name="result">The final linear interpolation transformation.</param>
        public static void Lerp(ref Transformation t1, ref Transformation t2, float amount, ref Transformation result)
        {
            result.Position = Vector2.Lerp(t1.Position, t2.Position, amount);
            result.Scale = Vector2.Lerp(t1.Scale, t2.Scale, amount);
            result.Rotation = MathHelper.Lerp(t1.Rotation, t2.Rotation, amount);
        }

        /// <summary>
        /// Transform a vector input in function of the transformation matrix.
        /// </summary>
        /// <param name="input">Vector to transform.</param>
        /// <returns>Transformed vector.</returns>
        public Vector2 TransformVector(Vector2 input)
        {
            Vector2 result = Vector2.Transform(input, matrix);
            return result;
        }
    }
}
