using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace FlowerSim
{
    internal class VectorIllust : VectorCollection
    {
        public void Draw(DrawingContext drawingContext)
        {
            foreach (var element in this)
            {
                element.Draw(drawingContext);
            }
        }
    }
}
