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
    public sealed partial class AddExtenedItemContent : Page
    {
        public int priority_of_Item = -1;
        public string codeList;
        public AddExtenedItemContent()
        {
            this.InitializeComponent();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var parameter = e.Parameter as string;
            codeList = parameter;
            base.OnNavigatedTo(e);
        }
        private void Back_To_Previous_Page(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }

        private void Handle_Accept_Button(object sender, RoutedEventArgs e)
        {   
            int id = getId();
            string subject = txtSubject.Text.Trim();
            rbDes.Document.GetText(Windows.UI.Text.TextGetOptions.AdjustCrlf, out string description);
            description = description.Trim();
            string date = DatePicker_Item.Date.DateTime.ToString();
            string time = TimePicker_Item.Time.ToString();

            Debug.WriteLine(date);
            if (!Validator(subject, description))
            {
                return;
            }
            DataAccessLibrary.DataAccess.AddData_to_ExtendItem(id, codeList, subject, description, priority_of_Item, date, time, false);
            int number = DataAccessLibrary.DataAccess.Count_ItemExtended(codeList);
            DataAccessLibrary.DataAccess.Update_EntieyOfList(codeList, number);
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
        private void AddExtened_PageLoaded(object sender, RoutedEventArgs e)
        {
            List<string> priority = new List<string>();
            priority.Add("Low");
            priority.Add("Medium");
            priority.Add("Hight");
            priority.Add("Very Hight");
            ComboBox_Priority.ItemsSource = priority;
            ComboBox_Priority.SelectedItem = priority[1];

            DatePicker_Item.Date = DateTime.Now;
            TimePicker_Item.Time = DateTime.Now.TimeOfDay;

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

        private int getId()
        {
            List<int> idlist = new List<int>();
            idlist = DataAccessLibrary.DataAccess.GetData_Id_ExTendedItem();

            int id;
            Random rd = new Random();
            id = rd.Next(1, 100000);

            while (true)
            {
                if (checkId(id, idlist))
                {
                    break;
                }
                id = rd.Next(1, 100000);
            }

            return id;
        }

        private bool checkId(int id, List<int> listId)
        {
            foreach (int item in listId)
            {
                if (item == id)
                {
                    return false;
                }
            }

            return true;
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
