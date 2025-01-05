using System.Collections.Generic;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace FlowerSim
{
    internal class VectorView : Canvas
    {
        static VectorView()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(VectorView), new FrameworkPropertyMetadata(typeof(VectorView)));
        }

        public VectorIllust Content
        {
            get { return (VectorIllust)GetValue(ContentProperty); }
            set { SetValue(ContentProperty, value); }
        }

        public static readonly DependencyProperty ContentProperty =
            DependencyProperty.Register(
                nameof(Content),
                typeof(VectorIllust),
                typeof(VectorView),
                new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender));

        protected override void OnRender(DrawingContext dc)
        {
            Content.Draw(dc);
        }
    }
}
