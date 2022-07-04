using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Content Dialog item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace FlashCard1.Pages.CardPage.Dialog
{
    public sealed partial class AddCardDialog : ContentDialog
    {
        public AddCardDialog()
        {
            this.InitializeComponent();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            RectangleAnimation1.Begin();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            RectangleAnimation.Begin();
        }

        private void RichEditBox_LostFocus(object sender, RoutedEventArgs e)
        {
            RichEditBox editBox = sender as RichEditBox;
            editBox.Document.GetText(Windows.UI.Text.TextGetOptions.AdjustCrlf, out string text);
            editBox.Tag = text;
        }

        private void RichEditBox_LostFocus_1(object sender, RoutedEventArgs e)
        {
            RichEditBox editBox = sender as RichEditBox;
            editBox.Document.GetText(Windows.UI.Text.TextGetOptions.AdjustCrlf, out string text);
            editBox.Tag = text;
        }
    }
}
