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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Simple_To_do_List.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class EditExtendedItem : Page
    {   
        public Models.Item_Extened alist = new Models.Item_Extened();
        public int priority_of_Item = -1;
        
        public EditExtendedItem()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var parameter = e.Parameter as Models.Item_Extened;
            alist = parameter;
            base.OnNavigatedTo(e);
        }
        private void Back_To_Previous_Page(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }

        private void Handle_Accept_Button(object sender, RoutedEventArgs e)
        {
            string subject = txtSubject.Text.Trim();
            rbDes.Document.GetText(Windows.UI.Text.TextGetOptions.AdjustCrlf, out string description);
            description = description.Trim();
            string date = DatePicker_Item.Date.DateTime.ToString();
            string time = TimePicker_Item.Time.ToString();
            if (!Validator(subject, description))
            {
                return;
            }
            DataAccessLibrary.DataAccess.Update_Item_Extened(alist.Id_Item, subject, description, priority_of_Item, date, time);
            Frame.GoBack();
        }

        private bool Validator(string subject, string description)
        {
            if (string.IsNullOrEmpty(subject) || string.IsNullOrEmpty(description))
            {
                txtAlert.Text = "Please complete all fields!";
                txtAlert.Visibility = Visibility.Visible;
                return false;
            }
            return true;
        }
        private void EditExtened_PageLoaded(object sender, RoutedEventArgs e)
        {
            List<string> priority = new List<string>();
            priority.Add("Low");
            priority.Add("Medium");
            priority.Add("Hight");
            priority.Add("Very Hight");
            ComboBox_Priority.ItemsSource = priority;
            ComboBox_Priority.SelectedItem = alist.Priority_Item;
            rbDes.Document.SetText(Windows.UI.Text.TextSetOptions.None, alist.Title_Item);
            DatePicker_Item.Date = DateTime.Parse(alist.Date_Time_Item);
            TimePicker_Item.Time = TimeSpan.Parse(alist.Time_Item);

        }

        private void ComboBoxPriority_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string priority = e.AddedItems[0].ToString();

            switch (priority)
            {
                case "Low":
                    priority_of_Item = 0;
                    break;

                case "Medium":
                    priority_of_Item = 1;
                    break;

                case "Hight":
                    priority_of_Item = 2;
                    break;

                case "Very Hight":
                    priority_of_Item = 3;
                    break;
            }
        }

        private void txtSubject_TextChanged(object sender, TextChangedEventArgs e)
        {
            txtAlert.Visibility = Visibility.Collapsed;
        }

        private void rbDes_TextChanged(object sender, RoutedEventArgs e)
        {
            txtAlert.Visibility = Visibility.Collapsed;
        }
    }


}
