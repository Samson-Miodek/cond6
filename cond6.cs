using System;

namespace ConsoleApp2
{
    class Program
    {
        //Cond6. * Заданы координаты трех точек на плоскости. Являются ли они вершинами квадрата? Если да, то найти координаты четвертой вершины.
        static void Main(string[] args)
        {

            var p1 = new Point();

            p1.x = 2;
            p1.y = 2;

            var p2 = new Point();
            p2.x = 4;
            p2.y = 2;

            var p3 = new Point();
            p3.x = 2;
            p3.y = 4;


            var p = FindСoordinates(p1, p2, p3);//если нельзя построить квадрат - вернет 0 0
            Console.WriteLine(p.x);//4
            Console.WriteLine(p.y);//4
        }

        public struct Point
        {
            public double x;
            public double y;
        }

        public static double GetLength(double dx, double dy)
        {
            return Math.Sqrt(dx * dx + dy * dy);
        }

        public static Point FindСoordinates(Point p1, Point p2, Point p3)
        {
            // вектора a,b,c
            var ax = p2.x - p1.x;
            var ay = p2.y - p1.y;

            var bx = p3.x - p2.x;
            var by = p3.y - p2.y;

            var cx = p1.x - p3.x;
            var cy = p1.y - p3.y;

            //их длины
            var la = GetLength(ax, ay);
            var lb = GetLength(bx, by);
            var lc = GetLength(cx, cy);

            var lengths = new double[] { la, lb, lc };
            var diagonal = -1.0;

            //1 у квадрата все стороны равны
            //2 Диагональ квадрата равна стороне квадрата, умноженной на корень из двух
            //проверяем есть ли две равные длины
            //и проверяем есть ли длина равная другой длине умноженной на корень 2 (ищем диагональ)
            var sqrt2 = Math.Sqrt(2);
            if (la == lb || la == lc || lb == lc)
                for (var i = 0; i < lengths.Length; i++)
                    for (var j = 0; j < lengths.Length; j++)
                        if (lengths[i] == lengths[j] * sqrt2)
                            diagonal = lengths[i];

            if (diagonal < 0)
                return new Point();

            //находим центр масс 3 вершин (массы равны 1)
            //центр масс будем ближе всего к углу между двумя сторонами
            var px = (p1.x + p2.x + p3.x) / 3;
            var py = (p1.y + p2.y + p3.y) / 3;

            var points = new Point[] { p1, p2, p3 };
            var minPointInd = 0;
            var min = Double.MaxValue;

            //находим ближайшую точку
            for (var i = 0; i < points.Length; i++)
            {
                var d = GetLength(points[i].x - px, points[i].y - py);
                if (d < min)
                {
                    min = d;
                    minPointInd = i;
                }
            }
            //вектор от угла между двумя сторонами к центру масс
            var nx = px - points[minPointInd].x;
            var ny = py - points[minPointInd].y;
            //его длина
            var nl = GetLength(nx, ny);

            var p = new Point();
            //(nx / nl) и (ny / nl) значит, что мы нормализуем вектор.
            //умножем вектор на длину диагонали и прибавлем его к координате угла между двумя сторонами
            p.x = diagonal * (nx / nl) + points[minPointInd].x;
            p.y = diagonal * (ny / nl) + points[minPointInd].y;
            return p;
        }
    }
}
