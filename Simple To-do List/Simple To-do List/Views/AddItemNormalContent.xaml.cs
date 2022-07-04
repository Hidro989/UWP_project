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
    public sealed partial class AddItemNormalContent : Page
    {
        public string codeList;
        public AddItemNormalContent()
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
            string description = "";
            string subject = txtSubject.Text.Trim();
            int id = getId();
            rbDes.Document.GetText(Windows.UI.Text.TextGetOptions.None, out description);
            if (!(Validator(subject, description.Trim())))
            {
                return;
            }
            DataAccessLibrary.DataAccess.AddData_to_NormalItem(id, codeList, subject, description, false);
            int number = DataAccessLibrary.DataAccess.Count_ItemNormal(codeList);
            DataAccessLibrary.DataAccess.Update_EntieyOfList(codeList, number);
            Frame.GoBack();
        }

        private bool Validator(string subject, string description)
        {
            if(string.IsNullOrEmpty(subject) || string.IsNullOrEmpty(description))
            {
                txtAlert.Text = "Please complete all fields!";
                txtAlert.Visibility = Visibility.Visible;
                return false;
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

        private int getId()
        {
            List<int> idlist = new List<int>();
            idlist = DataAccessLibrary.DataAccess.GetData_Id_NormalItem();

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
            foreach(int item in listId)
            {
                if(item == id)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
