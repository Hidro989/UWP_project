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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Simple_To_do_List.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class EditNormalItem : Page
    {   
        Models.Item alist = new Models.Item();
        public EditNormalItem()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var parameter = e.Parameter as Models.Item;
            alist = parameter;
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
            rbDes.Document.GetText(Windows.UI.Text.TextGetOptions.None, out description);
            if (!(Validator(subject, description.Trim())))
            {
                return;
            }
            DataAccessLibrary.DataAccess.Update_Item_Normal(alist.Id_Item, subject, description);
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

        private void txtSubject_TextChanged(object sender, TextChangedEventArgs e)
        {
            txtAlert.Visibility = Visibility.Collapsed;
        }

        private void rbDes_TextChanged(object sender, RoutedEventArgs e)
        {
            txtAlert.Visibility = Visibility.Collapsed;
        }

        private void EditNormalItem_PageLoaded(object sender, RoutedEventArgs e)
        {
            rbDes.Document.SetText(Windows.UI.Text.TextSetOptions.None, alist.Description_Item);
        }
    }
}
