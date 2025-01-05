using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;

namespace FlowerSim
{
    internal class VectorGroup : VectorCollection, IVectorElement
    {
        public void Draw(DrawingContext drawingContext)
        {
            foreach (var element in this)
            {
                element.Draw(drawingContext);
            }
        }

        public string ToSVG()
        {
            throw new NotImplementedException();
        }
    }

    class VectorTransformRotate : VectorCollection, IVectorElement
    {
        private readonly Transform transform;

        private VectorTransformRotate(double angle, double centerX, double centerY)
        {
            transform = new RotateTransform(angle, centerX, centerY);
        }

        public static VectorCollection Create(VectorCollection collection, double angle, double centerX, double centerY)
        {
            var element = new VectorTransformRotate(angle, centerX, centerY);
            collection.Add(element);
            return element;
        }

        public void Draw(DrawingContext drawingContext)
        {
            drawingContext.PushTransform(transform);

            foreach (var element in this)
            {
                element.Draw(drawingContext);
            }

            drawingContext.Pop();
        }

        public string ToSVG()
        {
            throw new NotImplementedException();
        }
    }
}