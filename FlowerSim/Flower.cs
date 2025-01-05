using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Effects;

namespace FlowerSim
{
    internal abstract class Flower
    {
        public abstract string Name { get; }
        public abstract VectorIllust Illustrate();
    }

    class FlowerRose : Flower
    {
        public override string Name { get; } = "Rose";
        public double Size { get; set; } = 100.0;
        public int PetalCount { get; set; } = 7;
        public int LayerCount { get; set; } = 4;

        private double center;

        public override VectorIllust Illustrate()
        {
            center = Size + 10.0;

            var illust = new VectorIllust();

            var petal = CreatePetal();

            var petalCount = PetalCount;
            var angle = 360.0 / PetalCount;
            var delta = angle / 2.0;
            for (int i = 0; i < LayerCount; i++)
            {
                var overlaidPetal = CreateOverlaidPetal(petal, angle);

                for (int j = 0; j < PetalCount; j++)
                {
                    var rotate = VectorTransformRotate.Create(illust, angle * j + delta * i, center, center);
                    VectorGeometry.Create(rotate, overlaidPetal);
                }

                petal = CreateInnerPetal(petal, 0.7);
            }

            return illust;
        }

        private Point GetPoint(double x, double y) => new Point(center + x * Size, center + y * Size);
        private BezierSegment GetBezierSegment((double X, double Y) p1, (double X, double Y) p2, (double X, double Y) p3) => new BezierSegment(GetPoint(p1.X, p1.Y), GetPoint(p2.X, p2.Y), GetPoint(p3.X, p3.Y), true);

        private PathGeometry CreatePetal()
        {
            var geometry = new PathGeometry(new[]
            {
                new PathFigure(new Point(center, center), new PathSegmentCollection(new[]
                {
                    GetBezierSegment((-0.10, -0.05), (-0.35, -0.30), (-0.40, -0.40)),
                    GetBezierSegment((-0.55, -0.70), (-0.20, -1.00), (-0.10, -1.00)),
                    GetBezierSegment((-0.05, -1.00), (-0.05, -1.00), (0.00, -0.95)),
                    GetBezierSegment((+0.05, -1.00), (+0.05, -1.00), (+0.10, -1.00)),
                    GetBezierSegment((+0.20, -1.00), (+0.55, -0.70), (+0.40, -0.40)),
                    GetBezierSegment((+0.35, -0.30), (+0.10, -0.05), (+0.00, +0.00)),
                }), true)
            });
            geometry.Freeze();

            return geometry;
        }

        private Geometry CreateOverlaidPetal(Geometry petal, double angle)
        {
            var nextPetal = petal.Clone();
            nextPetal.Transform = new RotateTransform(angle, center, center);

            var geometry = new CombinedGeometry(GeometryCombineMode.Exclude, petal, nextPetal);
            geometry.Freeze();

            return geometry;
        }

        private PathGeometry CreateInnerPetal(PathGeometry petal, double scale)
        {
            var scaleX = (1.0 + 2.0 * scale) / 3.0;
            var scaleY = scale;

            var geometry = new PathGeometry();

            var start = new Point(center, center);
            foreach(var figure in petal.Figures)
            {
                var innerFigure = figure.Clone();
                foreach(BezierSegment segment in innerFigure.Segments)
                {
                    segment.Point1 = Interpolate(start, segment.Point1, scaleX, scaleY);
                    segment.Point2 = Interpolate(start, segment.Point2, scaleX, scaleY);
                    segment.Point3 = Interpolate(start, segment.Point3, scaleX, scaleY);
                }
                geometry.Figures.Add(innerFigure);
            }
            geometry.Freeze();

            return geometry;

            /*
            var geometry = new PathGeometry();

            var start = new Point(CENTER, CENTER);
            foreach(var figure in petal.Figures)
            {

            }

            return geometry;
            */
        }

        private Point Interpolate(Point p1, Point p2, double scaleX, double scaleY)
        {
            return new Point(p1.X + (p2.X - p1.X) * scaleX, p1.Y + (p2.Y - p1.Y) * scaleY);
        }
    }

    class FlowerHydrangia : Flower
    {
        public override string Name { get; } = "Hydrangia";

        public override VectorIllust Illustrate()
        {
            var illust = new VectorIllust();
            return illust;
        }
    }
}
