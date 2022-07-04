using FlashCard1.Messages;
using Microsoft.Toolkit.Mvvm.Messaging;
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
    public sealed partial class ResetAllDialog : ContentDialog
    {
        public ResetAllDialog()
        {
            this.InitializeComponent();
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ResetAllDialog1.Hide();
        }

        private void Accepte_Reset(object sender, RoutedEventArgs e)
        {
            WeakReferenceMessenger.Default.Send(new SubChangeMessage(true));
            ResetAllDialog1.Hide();
        }
    }
}
