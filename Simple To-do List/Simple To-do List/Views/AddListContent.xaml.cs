using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Popups;
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
    public sealed partial class AddListContent : Page
    {
        public bool type_of_list = false;
        public AddListContent()
        {
            this.InitializeComponent();
        }

        private void AddListContent_PageLoaded(object sender, RoutedEventArgs e)
        {
            initComboBox();
        }

        // Khoi tao du lieu cho ComboBox
        private void initComboBox()
        {
            List<string> types = new List<string>();
            types.Add("Normal");
            types.Add("Extended");
            ComboBox_Type.ItemsSource = types;
            ComboBox_Type.SelectedItem = ComboBox_Type.Items[0];
        }

        // Quay tro lai trang truoc
        private void Back_To_Previous_Page(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();  
        }

        private void ComboBox_Type_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            txtAlert.Visibility = Visibility.Collapsed;
            string typeName = e.AddedItems[0].ToString();
            switch (typeName)
            {
                case "Normal":
                    type_of_list = false;
                    break;
                case "Extended":
                    type_of_list = true;
                    break;
            }
            
        }

        private void Handle_Accept_Button(object sender, RoutedEventArgs e)
        {
            //txtSubject
            //type_of_list
            List<string> code_list= DataAccessLibrary.DataAccess.GetData_CodeList();
            if (!Validator_AddListPage())
            {
                txtAlert.Text = "Please complete all fields!";
                txtAlert.Visibility = Visibility.Visible;
                return;
            }
            string codeofList = Handle_Get_Code(type_of_list, code_list);
            DataAccessLibrary.DataAccess.AddData_To_List(codeofList, txtSubject.Text.Trim(), type_of_list, 0);
            Frame.GoBack();
        }

        //Handle Code of list
        private string Handle_Get_Code(bool type, List<string> listCode)
        {
            string code = type == true ? "E" : "N";
            int number;

            //Kiem tra 4 ki tu cuoi cua tung phan tu trong list
           while (true)
            {
                number = Random_Code();
                if(CheckRandom(number, listCode) == true)
                {
                    break;
                }

            }
            return code + number;
            
        }

        //Kiem tra ma moi co trung voi ma cu hay khong
        private bool CheckRandom(int number, List<string> listCode)
        {
            foreach(string key in listCode)
            {
                
                if (number.ToString() == key.Remove(0,1))
                {
                    return false;
                }
            }
            return true;
        }

        //Random ra ma moi
        private int Random_Code()
        {
            Random rd = new Random();
            int number = new Random().Next(1000, 10000);
            return number;

        }


        //Validate
        private bool Validator_AddListPage()
        {   
            if(!(txtSubject.Text == "") && ComboBox_Type.SelectedIndex > -1)
            {
                return true;
            }
            return false;
        }

        private void TxtSubject_TextChanged(object sender, TextChangedEventArgs e)
        {
            txtAlert.Visibility = Visibility.Collapsed;
        }
    }
}
