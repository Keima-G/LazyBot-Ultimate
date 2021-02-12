namespace LazyLib.Helpers
{
    using System;

    public class Vector
    {
        private float _x;
        private float _y;
        private float _z;

        public Vector()
        {
            this._x = 0f;
            this._y = 0f;
            this._z = 0f;
        }

        public Vector(Vector v)
        {
            this._x = v._x;
            this._y = v._y;
            this._z = v._z;
        }

        public Vector(float x, float y, float z)
        {
            this._x = x;
            this._y = y;
            this._z = z;
        }

        public static Vector operator +(Vector v1, Vector v2) => 
            new Vector(v1._x + v2._x, v1._y + v2._y, v1._z + v2._z);

        public static float operator *(Vector v1, Vector v2) => 
            ((v1._x * v2._x) + (v1._y * v2._y)) + (v1._z * v2._z);

        public static Vector operator -(Vector v1, Vector v2) => 
            new Vector(v1._x - v2._x, v1._y - v2._y, v1._z - v2._z);

        public void SetVec(Vector v)
        {
            this._x = v._x;
            this._y = v._y;
            this._z = v._z;
        }

        public void SetVec(float x, float y, float z)
        {
            this._x = x;
            this._y = y;
            this._z = z;
        }

        public float X
        {
            get => 
                this._x;
            set => 
                this._x = value;
        }

        public float Y
        {
            get => 
                this._y;
            set => 
                this._y = value;
        }

        public float Z
        {
            get => 
                this._z;
            set => 
                this._z = value;
        }
    }
}

