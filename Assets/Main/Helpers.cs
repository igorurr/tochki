using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MainInGame
{
    [System.Serializable]
	public class Point
	{
		public int x { get; set; }
		public int y { get; set; }
		public int z { get { return -x-y; } set { y = -x-value; } }

		public Point(int x, int y)
		{
			this.x = x;
			this.y = y;
		}

		public static bool operator == (Point a, Point b)
		{
			return (a.x == b.x) && (a.y == b.y);
		}
		public static bool operator != (Point a, Point b)
		{
			return a != b;
		}

        // Для определения расстояния между точками. Стабильно работает для точек одной линии, для рандомных точек - неизвестно. short - т.к. не используется для точек расстояние которых больше 9
        public static short operator - (Point a, Point b)
        {
            return (short)Mathf.Max( Mathf.Abs(a.x-b.x), Mathf.Abs(a.y-b.y), Mathf.Abs(a.z-b.z) );
        }

        public override string ToString()
        {
            return x.ToString() + "*" + y.ToString();
        }
        public Vector2 GetCoord2D()
        {
            float gcx = 1.2f * (-y/2f + x);
            float gcy = 1.03923f * y;
            return new Vector2(gcx, gcy);
        }

        public int GetSystCoordOrientation()
        {
            int ret = (x + y) % 3;
            return ret < 0 ? 3+ret : ret;
        }

        public Point GetAround(short pos)
        {
            if( GetSystCoordOrientation() == 0 )
                switch (pos)
                {
                    case 1:
                        return new Point(x + 1, y);
                    case 2:
                        return new Point(x + 1, y+1);
                    case 3:
                        return new Point(x+2, y + 2);
                    case 4:
                        return new Point(x + 1, y + 2);
                    case 5:
                        return new Point(x, y+1);
                    case 6:
                        return new Point(x-1, y + 1);
                    case 7:
                        return new Point(x - 2, y);
                    case 8:
                        return new Point(x -2, y-1);
                    case 9:
                        return new Point(x-1, y -1);
                    case 10:
                        return new Point(x - 1, y - 2);
                    case 11:
                        return new Point(x, y-2);
                    case 12:
                        return new Point(x+1, y - 1);
                }
            else
                switch (pos)
                {
                    case 1:
                        return new Point(x + 1, y+1);
                    case 2:
                        return new Point(x + 1, y + 2);
                    case 3:
                        return new Point(x, y + 2);
                    case 4:
                        return new Point(x - 1, y + 1);
                    case 5:
                        return new Point(x-1, y);
                    case 6:
                        return new Point(x - 2, y - 1);
                    case 7:
                        return new Point(x - 2, y-2);
                    case 8:
                        return new Point(x - 1, y - 2);
                    case 9:
                        return new Point(x, y - 1);
                    case 10:
                        return new Point(x + 1, y - 1);
                    case 11:
                        return new Point(x+2, y);
                    case 12:
                        return new Point(x + 2, y + 1);
                }
            return null;
        }
    }

    [System.Serializable]
    public class Typple2<T1,T2>
	{
		public T1 x1 { get; set; }
		public T2 x2 { get; set; }
		internal Typple2(T1 x1, T2 x2)
		{
			this.x1 = x1;
			this.x2 = x2;
		}
	}
};