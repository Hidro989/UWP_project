using FlashCard1.Messages;
using Microsoft.Toolkit.Mvvm.Messaging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Uwp.Model.Model;
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
    public sealed partial class EditCardDialog : ContentDialog
    {   

        public Card Item { get; set; }
        public EditCardDialog()
        {
            this.InitializeComponent();
            Item = WeakReferenceMessenger.Default.Send<SendCardMessage>().Response;
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

        private void EditCardDialog1_Loaded(object sender, RoutedEventArgs e)
        {
            string back = Item.DesBack;
            string front = Item.DesFront;
            RichBack.Document.SetText(Windows.UI.Text.TextSetOptions.FormatRtf, back);
            RichFront.Document.SetText(Windows.UI.Text.TextSetOptions.FormatRtf, front);
        }
    }
}
