using FlashCard1.Messages;
using FlashCard1.Pages.LearningPage;
using Microsoft.Toolkit.Mvvm.Messaging;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    public sealed partial class AskLearnDialog : ContentDialog
    {

        public List<string> ComboboxItems { get; set; }

        public IDictionary<string, int> Choices { get; set; }

        public int SeletedComboboxIndex { get; set; }
        public int SeletedButtonRadiusIndex { get; set; }
        public AskLearnDialog()
        {
            this.InitializeComponent();
            Choices = new Dictionary<string, int>();

            ComboboxItems = new List<string>();

            ComboboxItems.Add("Normal (Front to Back)");
            ComboboxItems.Add("Fliped (Back to Front)");
            ComboboxMode.ItemsSource = ComboboxItems;
            ComboboxMode.SelectedIndex = 0;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            AskLearnDialog1.Hide();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Choices.Add("LH", SeletedComboboxIndex);
            Choices.Add("Option", SeletedButtonRadiusIndex);

            WeakReferenceMessenger.Default.Send(new KeyValueMessage(Choices));
            AskLearnDialog1.Hide();
        }

        private void ComboboxMode_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SeletedComboboxIndex = ComboboxMode.SelectedIndex;
        }

        private void RadioButtons_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RadioButtons radioButtons = sender as RadioButtons;
            SeletedButtonRadiusIndex = radioButtons.SelectedIndex; 
        }

    }
}
