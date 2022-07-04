using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using DataAccessLibrary;
using System.Diagnostics;
using Windows.Devices.Input;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Simple_To_do_List
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public ObservableCollection<Models.List> lists { get; set; }
        public ObservableCollection<Models.Item_Extened> items { get; set; }
        public ObservableCollection<Models.Item> item_normals { get; set; }

        public int selector = -1;

        public bool type_of_selector;

        public MainPage()
        {
            this.InitializeComponent();
            if (lists == null)
            {
                lists = new ObservableCollection<Models.List>();
            }

            if (items == null)
            {
                items = new ObservableCollection<Models.Item_Extened>();
            }

            if (item_normals == null)
            {
                item_normals = new ObservableCollection<Models.Item>();
            }

        }

        private void Move_To_Add_Frame(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Views.AddListContent));
        }

        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {   
            Innit_List();
        }

        private void Innit_List()
        {
            List<string> listData = DataAccess.GetData_List();
            foreach (string itemData in listData)
            {
                string[] arrListstring = itemData.Split(',');

                Models.List list = new Models.List();
                list.Code_List = arrListstring[0];
                list.Title_List = arrListstring[1];
                list.Type_List = (arrListstring[2] == "1") ? "Extended" : "Normal";
                list.Amount_ItemOfList = arrListstring[3];
                lists.Add(list);
            }
            lvList.ItemsSource = lists;
        }

        private void Handle_Delete_Item(object sender, RoutedEventArgs e)
        {   
            MenuFlyoutItem item = sender as MenuFlyoutItem;
            Models.List list1 = item.Tag as Models.List;
            DataAccess.Delete_List(list1.Code_List);
            if(list1.Type_List == "Normal")
            {
                DataAccess.Delete_All_Item_Normal(list1.Code_List);
            }
            else if(list1.Type_List == "Extended")
            {
                DataAccess.Delete_All_Item_Extended(list1.Code_List);
            }
            lists.Remove(list1);
            BtnAddItem.IsEnabled = false;
            BtnSort.IsEnabled = false;
            BtnDeleteAll.IsEnabled = false;
            titleOfItem.Text = "";
        }

        private  void Handle_Rename_Item(object sender, RoutedEventArgs e)
        {
            MenuFlyoutItem item = sender as MenuFlyoutItem;
            Models.List list1 = item.Tag as Models.List;
            Frame.Navigate(typeof(Views.RenameList), list1);
        }

        private void Handle_ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            lvItem_Extended.Visibility = Visibility.Collapsed;
            lvItem.Visibility = Visibility.Collapsed;
            
            selector = lvList.SelectedIndex;

            if(selector != -1)
            {
                if (!string.IsNullOrEmpty(lists[selector].Title_List))
                {
                    titleOfItem.Text = lists[selector].Title_List;
                }
                BtnAddItem.IsEnabled = true;
                BtnSort.IsEnabled = true;

                type_of_selector = (lists[selector].Type_List == "Normal") ? false : true;

                if (type_of_selector)
                {
                    lvItem_Extended.Visibility = Visibility.Visible;
                    if(!(DataAccess.Count_ItemExtended(lists[selector].Code_List) == 0))
                    {
                        BtnDeleteAll.IsEnabled = true;
                    }
                    Innit_ItemExtended();
                }
                else
                {
                    lvItem.Visibility = Visibility.Visible;
                    if (!(DataAccess.Count_ItemNormal(lists[selector].Code_List) == 0))
                    {
                        BtnDeleteAll.IsEnabled = true;
                    }
                    Innit_ItemNormal();
                }
            }

            
        }

        private void Innit_ItemExtended()
        {
            items.Clear();
            List<string> listData = DataAccess.GetData_By_CodeList1(lists[selector].Code_List);
            foreach (string itemData in listData)
            {
                string[] arrListstring = itemData.Split(',');
                
                Models.Item_Extened item = new Models.Item_Extened();
                item.Id_Item = int.Parse(arrListstring[0]);
                item.Code_List = arrListstring[1];
                item.Title_Item = arrListstring[2];
                item.Description_Item = arrListstring[3];

                if (arrListstring[4] == "0")
                {
                    item.Priority_Item = "Low";
                }
                else if (arrListstring[4] == "1")
                {
                    item.Priority_Item = "Medium";
                }
                else if (arrListstring[4] == "2")
                {
                    item.Priority_Item = "Hight";
                }
                else if (arrListstring[4] == "3")
                {
                    item.Priority_Item = "Very Hight";
                }

                item.Date_Time_Item = arrListstring[5];
                item.Time_Item = arrListstring[6];


                if (arrListstring[7] == "0")
                {
                    item.Is_Finished = false;
                }
                else
                {
                    item.Is_Finished = true;
                }
                Debug.WriteLine(arrListstring[7]);
                items.Add(item);
            }
            lvItem_Extended.ItemsSource = items;
        }

        private void Innit_ItemNormal()
        {
            item_normals.Clear();
            List<string> listData = DataAccess.GetData_By_CodeList(lists[selector].Code_List);

            foreach (string itemData in listData)
            {
                string[] arrListstring = itemData.Split(',');

                Models.Item item = new Models.Item();

                item.Id_Item = int.Parse(arrListstring[0]);
                item.Code_List = arrListstring[1];
                item.Title_Item = arrListstring[2];
                item.Description_Item = arrListstring[3];
                if (arrListstring[4] == "0")
                {
                    item.Is_Finished = false;
                }
                else
                {
                    item.Is_Finished = true;
                }

                item_normals.Add(item);
            }
            lvItem.ItemsSource = item_normals;
        }
        private void Btn_Add_Item_Child(object sender, RoutedEventArgs e)
        {
            if (selector != -1)
            {
                if (type_of_selector)
                {
                    Frame.Navigate(typeof(Views.AddExtenedItemContent), lists[selector].Code_List);
                }
                else
                {
                    Frame.Navigate(typeof(Views.AddItemNormalContent), lists[selector].Code_List);
                }
            }
        }

        private void Handle_Delete_All_Item(object sender, RoutedEventArgs e)
        {
            if (type_of_selector == true)
            {
                DataAccess.Delete_All_Item_Extended(lists[selector].Code_List);
                int number =  DataAccess.Count_ItemExtended(lists[selector].Code_List);
                DataAccess.Update_EntieyOfList(lists[selector].Code_List, number);
                Innit_ItemExtended();
                lists.Clear();
                Innit_List();
            }
            else if (type_of_selector == false)
            {
                DataAccess.Delete_All_Item_Normal(lists[selector].Code_List);
                int number = DataAccess.Count_ItemNormal(lists[selector].Code_List);
                DataAccess.Update_EntieyOfList(lists[selector].Code_List, number);
                Innit_ItemNormal();
                lists.Clear();
                Innit_List();
            }
            BtnDeleteAll.IsEnabled = false;
        }

        private void Delete_Item_Normal(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            Models.Item item = (Models.Item)button.DataContext;
            int id = item.Id_Item;
            DataAccess.Delete_Item_Normal_Id(id);
            item_normals.Clear();
            Innit_ItemNormal();
            int number = DataAccess.Count_ItemExtended(lists[selector].Code_List);
            DataAccess.Update_EntieyOfList(lists[selector].Code_List, number);
            if(number == 0)
            {
                BtnDeleteAll.IsEnabled = false;
            }
            lists.Clear();
            Innit_List();
        }

        private void Delete_Item_Extended(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            Models.Item_Extened item = (Models.Item_Extened)button.DataContext;
            int id = item.Id_Item;
            DataAccess.Delete_Item_Extended_Id(id);
            items.Clear();
            Innit_ItemExtended();
            int number = DataAccess.Count_ItemExtended(lists[selector].Code_List);
            if (number == 0)
            {
                BtnDeleteAll.IsEnabled = false;
            }
            DataAccess.Update_EntieyOfList(lists[selector].Code_List, number);
            lists.Clear();
            Innit_List();
        }

        private void Move_To_Edit_ItemExtended(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            Models.Item_Extened item = (Models.Item_Extened)button.DataContext;
            Frame.Navigate(typeof(Views.EditExtendedItem), item);
        }

        private void Move_To_Edit_ItemNormal(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            Models.Item item = (Models.Item)button.DataContext;
            Frame.Navigate(typeof(Views.EditNormalItem), item);
        }

        private void Handle_Checked_NormalItem(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = sender as CheckBox;
            bool check = checkBox.IsChecked.Value;
            Models.Item item = (Models.Item)(checkBox.DataContext);
            DataAccess.Update_Item_Normal_Finish(item.Id_Item, check);
        }

        private void Handle_Checked_ExtendedItem(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = sender as CheckBox;
            bool check = checkBox.IsChecked.Value;
            Models.Item_Extened itemZ = (Models.Item_Extened)(checkBox.DataContext);
            DataAccess.Update_Item_Extended_Finish(itemZ.Id_Item, check);
        }

    }
}
