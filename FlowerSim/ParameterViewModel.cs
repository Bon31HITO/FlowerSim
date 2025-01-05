using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;

namespace FlowerSim
{
    internal class ParameterViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private readonly object source;
        private readonly PropertyInfo propertyInfo;

        public string Name { get; init; }
        public Type Type { get; init; }
        
        private object? value;
        public object? Value
        {
            get => value;
            set
            {
                if (this.value == value) return;

                switch (Type)
                {
                    case Type @_ when @_ == typeof(int):
                        if (int.TryParse(value?.ToString(), out var intValue))
                        {
                            propertyInfo.SetValue(source, intValue);
                            this.value = intValue;
                        }
                        break;

                    case Type @_ when @_ == typeof(double):
                        if (double.TryParse(value?.ToString(), out var doubleValue))
                        {
                            propertyInfo.SetValue(source, doubleValue);
                            this.value = doubleValue;
                        }
                        break;

                    case Type @_ when @_ == typeof(string):
                        propertyInfo.SetValue(source, value?.ToString());
                        this.value = value;
                        break;
                }

                PropertyChanged?.Invoke(this, ValuePropertyChangedEventArgs);
            }
        }
        private readonly static PropertyChangedEventArgs ValuePropertyChangedEventArgs = new (nameof(Value));

        private ParameterViewModel(object source, string name, Type type, object? value)
        {
            this.source = source;
            Name = name;
            Type = type;
            this.value = value;

            propertyInfo = source.GetType().GetProperty(name) ?? throw new Exception();
        }

        public static ParameterViewModel[] Generate(object source)
        {
            var properties = source.GetType().GetProperties();
            var parameters = new List<ParameterViewModel>();

            foreach (var property in properties)
            {
                if (property.CanRead && property.CanWrite)
                {
                    var value = property.GetValue(source);
                    parameters.Add(new ParameterViewModel(source, property.Name, property.PropertyType, value));
                }
            }
            return parameters.ToArray();
        }
    }
}
