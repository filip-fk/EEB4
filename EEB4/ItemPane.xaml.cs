using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Composition;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Hosting;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace EEB4
{
    public sealed partial class ItemPane : UserControl
    {
        public ItemPane(int height, int width, string title, HorizontalAlignment title_ha, Grid content, string footer_l, string footer_r)
        {
            this.InitializeComponent();
            InitializePane(height, width, title, title_ha, content, footer_l, footer_r);
        }

        private void InitializePane(int height, int width, string title, HorizontalAlignment title_ha, Grid content, string footer_l, string footer_r)
        {
            main0.Width = width;
            main0.Height = height;

            Type type = content.GetType();
            title_.Text = title.ToUpper();
            title_.HorizontalAlignment = title_ha;
            con_.Children.Add(content);
            f_l.Text = footer_l;
            f_r.Text = footer_r;

            shadowHost.Width = width;
            shadowHost.Height = height;
            InitializeDropShadow(shadowHost, main01);
        }

        private void InitializeDropShadow(UIElement ShadowHost, Shape shadowTarget)
        {
            Visual hostVisual = ElementCompositionPreview.GetElementVisual(ShadowHost);
            Compositor compositor = hostVisual.Compositor;

            // Create a drop shadow
            var dropShadow = compositor.CreateDropShadow();
            dropShadow.Color = Color.FromArgb(255, 75, 75, 80);
            dropShadow.BlurRadius = 18.0f;
            dropShadow.Offset = new Vector3(3f, 3f, 0.0f);
            // Associate the shape of the shadow with the shape of the target element
            dropShadow.Mask = shadowTarget.GetAlphaMask();

            // Create a Visual to hold the shadow
            var shadowVisual = compositor.CreateSpriteVisual();
            shadowVisual.Shadow = dropShadow;

            // Add the shadow as a child of the host in the visual tree
            ElementCompositionPreview.SetElementChildVisual(ShadowHost, shadowVisual);

            // Make sure size of shadow host and shadow visual always stay in sync
            var bindSizeAnimation = compositor.CreateExpressionAnimation("hostVisual.Size");
            bindSizeAnimation.SetReferenceParameter("hostVisual", hostVisual);

            shadowVisual.StartAnimation("Size", bindSizeAnimation);
        }
    }
}
