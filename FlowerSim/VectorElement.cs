using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media;

namespace FlowerSim
{
    interface IVectorElement
    {
        void Draw(DrawingContext drawingContext);
        string ToSVG();
    }

    internal abstract class VectorElement : IVectorElement
    {
        protected readonly static Brush DEFAULT_BRUSH = Brushes.LightPink;
        protected readonly static Pen DEFAULT_PEN = new(Brushes.DarkRed, 1.5);

        public abstract void Draw(DrawingContext drawingContext);
        public abstract string ToSVG();

        protected VectorElement()
        {
        }

        /*
        public static void Generate<TVectorElement>(VectorCollection collection, TVectorElement element) where TVectorElement : VectorElement
        {
            collection.Add(element);
        }
        */
    }

    class VectorGeometry : VectorElement
    {
        private Geometry geometry;

        private VectorGeometry(Geometry geometry)
        {
            this.geometry = geometry;
        }

        public override void Draw(DrawingContext drawingContext)
        {
            drawingContext.DrawGeometry(DEFAULT_BRUSH, DEFAULT_PEN, geometry);
        }

        public override string ToSVG()
        {
            throw new NotImplementedException();
        }

        public static void Create(VectorCollection collection, Geometry geometry)
        {
            collection.Add(new VectorGeometry(geometry));
        }
    }
}
