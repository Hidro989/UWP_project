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
    public sealed partial class RenameList : Page
    {
        public Models.List alist = new Models.List();
        public RenameList()
        {
            this.InitializeComponent();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var pramater = e.Parameter as Models.List;
            alist = pramater;
            base.OnNavigatedTo(e);
        }

        private void Back_To_Previous_Page(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }
        private void Handle_Accept_Button(object sender, RoutedEventArgs e)
        {
            if (!Validator_AddListPage())
            {
                txtAlert.Text = "Please complete all fields!";
                txtAlert.Visibility = Visibility.Visible;
                return;
            }
            DataAccessLibrary.DataAccess.Update_NameOfList(alist.Code_List, txtSubject.Text.Trim());
            Frame.GoBack();
        }

        private bool Validator_AddListPage()
        {
            if (!(txtSubject.Text == ""))
            {
                return true;
            }
            return false;
        }
        private void TxtSubject_TextChanged(object sender, TextChangedEventArgs e)
        {
            txtAlert.Visibility = Visibility.Collapsed;
        }

        private void Edit_PageLoaded(object sender, RoutedEventArgs e)
        {
            txtSubject.Text = alist.Title_List.ToString();
        }
    }
}
