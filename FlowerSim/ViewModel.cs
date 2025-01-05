using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FlowerSim
{
    internal class ViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private Flower[] flowers;
        private IEnumerable<ParameterViewModel>[] flowerParameters;

        public IEnumerable<string> Flowers
        {
            get
            {
                foreach(var flower in flowers)
                {
                    yield return flower.Name;
                }
            }
        }

        private int selectedFlowerIndex;
        public int SelectedFlowerIndex
        {
            get => selectedFlowerIndex;
            set
            {
                if (selectedFlowerIndex == value) return;
                selectedFlowerIndex = value;
                PropertyChanged?.Invoke(this, SelectedFlowerIndexChangedEventArgs);
                PropertyChanged?.Invoke(this, SelectedFlowerParametersChangedEventArgs);
                PropertyChanged?.Invoke(this, SelectedFlowerIllustChangedEventArgs);
            }
        }
        private readonly static PropertyChangedEventArgs SelectedFlowerIndexChangedEventArgs = new PropertyChangedEventArgs(nameof(SelectedFlowerIndex));

        public IEnumerable<ParameterViewModel> SelectedFlowerParameters
        {
            get => flowerParameters[selectedFlowerIndex];
        }
        private readonly static PropertyChangedEventArgs SelectedFlowerParametersChangedEventArgs = new PropertyChangedEventArgs(nameof(SelectedFlowerParameters));

        public VectorIllust SelectedFlowerIllust
        {
            get => flowers[selectedFlowerIndex].Illustrate();
        }
        private readonly static PropertyChangedEventArgs SelectedFlowerIllustChangedEventArgs = new PropertyChangedEventArgs(nameof(SelectedFlowerIllust));

        public ViewModel()
        {
            flowers = new Flower[]
            {
                new FlowerRose(),
                new FlowerHydrangia()
            };

            var count = flowers.Length;
            flowerParameters = new IEnumerable<ParameterViewModel>[count];
            for (int i = 0; i < count; i++)
            {
                var parameters = ParameterViewModel.Generate(flowers[i]);

                foreach(var parameter in parameters)
                {
                    parameter.PropertyChanged += OnParameterChanged;
                }

                flowerParameters[i] = parameters;
            }

            selectedFlowerIndex = 0;
        }

        private void OnParameterChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName != "Value") return;
            
            PropertyChanged?.Invoke(this, SelectedFlowerIllustChangedEventArgs);
        }
    }
}
