using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace FlowerSim
{
    internal class ParametersView : Control
    {
        private ListView? container;

        static ParametersView()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ParametersView), new FrameworkPropertyMetadata(typeof(ParametersView)));
        }

        public IEnumerable<ParameterViewModel> Content
        {
            get { return (IEnumerable<ParameterViewModel>)GetValue(ContentProperty); }
            set { SetValue(ContentProperty, value); }
        }

        public static readonly DependencyProperty ContentProperty =
            DependencyProperty.Register(
                nameof(Content),
                typeof(IEnumerable<ParameterViewModel>),
                typeof(ParametersView),
                new PropertyMetadata(Array.Empty<ParameterViewModel>(), OnContentChanged));

        private static void OnContentChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is not ParametersView view) return;
            if (e.NewValue is not IEnumerable<ParameterViewModel> parameters) throw new InvalidCastException("Content must be IEnumerable<ParameterViewModel>");
            view.UpdateUI();
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            container = Template.FindName("PART_Container", this) as ListView;
            UpdateUI();
        }

        private void UpdateUI()
        {
            if (container == null) return;
            container.ItemsSource = Content;
        }
    }
}
