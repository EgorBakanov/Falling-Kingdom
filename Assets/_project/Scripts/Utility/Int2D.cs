namespace Nara.MFGJS2020.Utility
{
    [System.Serializable]
    public class Int2D
    {
        public Int2D(int x, int y)
        {
            this.x = x;
            this.y = y;
            length = y * x;
            m = new int[y * x];
        }

        public int length;
        public int x, y;
        public int[] m;

        public int this[int index]
        {
            get { return m[index]; }
            set { m[index] = value; }
        }

        public int this[int x, int y]
        {
            get { return m[x * this.y + y]; }
            set { m[x * this.y + y] = value; }
        }

        public static implicit operator int[](Int2D arr) => arr.m;
    }
}