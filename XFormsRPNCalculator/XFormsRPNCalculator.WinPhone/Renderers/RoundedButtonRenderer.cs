using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Xamarin.Forms;
using Xamarin.Forms.Platform.WinPhone;
using XFormsRPNCalculator;
using XFormsRPNCalculator.WinPhone.Renderers;

[assembly: ExportRenderer(typeof(RoundedButton), typeof(RoundedButtonRenderer))]

namespace XFormsRPNCalculator.WinPhone.Renderers
{
    public class RoundedButtonRenderer : ButtonRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Button> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                var button = (RoundedButton)e.NewElement;

                button.SizeChanged += (s, args) =>
                {
                    Control.ApplyTemplate();
                    var borders = Control.GetVisuals<Border>();
                    var radius = Math.Min(button.Width, button.Height) / 2.0;

                    foreach (var border in borders)
                    {
                        border.CornerRadius = new CornerRadius(radius);
                    }
                };
            }
        }
    }

    public static class DependencyObjectExtensions
    {
        public static IEnumerable<T> GetVisuals<T>(this DependencyObject root)
            where T : DependencyObject
        {
            int count = VisualTreeHelper.GetChildrenCount(root);

            for (int i = 0; i < count; i++)
            {
                var child = VisualTreeHelper.GetChild(root, i);

                if (child is T)
                {
                    yield return child as T;
                }

                foreach (var descendants in child.GetVisuals<T>())
                {
                    yield return descendants;
                }
            }
        }
    }
}