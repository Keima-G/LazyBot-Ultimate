namespace LazyLib.Helpers
{
    using System;

    public class Matrix
    {
        private float _x1;
        private float _x2;
        private float _x3;
        private float _y1;
        private float _y2;
        private float _y3;
        private float _z1;
        private float _z2;
        private float _z3;

        public Matrix()
        {
        }

        public Matrix(float x1, float x2, float x3, float y1, float y2, float y3, float z1, float z2, float z3)
        {
            this._x1 = x1;
            this._x2 = x2;
            this._x3 = x3;
            this._y1 = y1;
            this._y2 = y2;
            this._y3 = y3;
            this._z1 = z1;
            this._z2 = z2;
            this._z3 = z3;
        }

        public float Det() => 
            ((((((this._x1 * this._y2) * this._z3) + ((this._x2 * this._y3) * this._z1)) + ((this._x3 * this._y1) * this._z2)) - ((this._x3 * this._y2) * this._z1)) - ((this._x2 * this._y1) * this._z3)) - ((this._x1 * this._y3) * this._z2);

        public Matrix Inverse()
        {
            float num = 1f / this.Det();
            return new Matrix(num * ((this._y2 * this._z3) - (this._y3 * this._z2)), num * ((this._x3 * this._z2) - (this._x2 * this._z3)), num * ((this._x2 * this._y3) - (this._x3 * this._y2)), num * ((this._y3 * this._z1) - (this._y1 * this._z3)), num * ((this._x1 * this._z3) - (this._x3 * this._z1)), num * ((this._x3 * this._y1) - (this._x1 * this._y3)), num * ((this._y1 * this._z2) - (this._y2 * this._z1)), num * ((this._x2 * this._z1) - (this._x1 * this._z2)), num * ((this._x1 * this._y2) - (this._x2 * this._y1)));
        }

        public static Vector operator *(Vector v, Matrix m) => 
            new Vector(((m._x1 * v.X) + (m._y1 * v.Y)) + (m._z1 * v.Z), ((m._x2 * v.X) + (m._y2 * v.Y)) + (m._z2 * v.Z), ((m._x3 * v.X) + (m._y3 * v.Y)) + (m._z3 * v.Z));

        public Vector GetFirstColumn =>
            new Vector(this._x1, this._y1, this._z1);
    }
}

