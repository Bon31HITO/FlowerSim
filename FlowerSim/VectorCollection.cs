using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowerSim
{
    internal abstract class VectorCollection : IEnumerable<IVectorElement>
    {
        private readonly List<IVectorElement> elements;

        public VectorCollection()
        {
            elements = new List<IVectorElement>();
        }

        public VectorCollection(params IVectorElement[] elements)
        {
            this.elements = elements.ToList();
        }

        public VectorCollection(IEnumerable<IVectorElement> elements)
        {
            this.elements = elements.ToList();
        }

        public IEnumerator<IVectorElement> GetEnumerator()
        {
            foreach (var element in elements)
            {
                yield return element;
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public void Add(IVectorElement element)
        {
            elements.Add(element);
        }
    }
}
