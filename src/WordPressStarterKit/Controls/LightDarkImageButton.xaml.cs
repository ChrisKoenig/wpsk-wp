using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace WordPressStarterKit.Controls
{
    public partial class LightDarkImageButton : UserControl
    {
        public event RoutedEventHandler Click;

        public LightDarkImageButton()
        {
            InitializeComponent();
            this.MyButton.Click += (s, e) =>
                {
                    if (Click != null)
                        Click(s, e);
                };
            this.DataContext = this;
        }

        public string LightImagePath { get; set; }

        public string DarkImagePath { get; set; }

        public BitmapImage LightOrDarkImage
        {
            get
            {
                if (IsLightTheme())
                    return new BitmapImage(new Uri(LightImagePath, UriKind.Relative));
                else
                    return new BitmapImage(new Uri(DarkImagePath, UriKind.Relative));
            }
        }

        private bool IsLightTheme()
        {
            Visibility v = (Visibility)Resources["PhoneLightThemeVisibility"];
            return (v == System.Windows.Visibility.Visible);
        }
    }
}