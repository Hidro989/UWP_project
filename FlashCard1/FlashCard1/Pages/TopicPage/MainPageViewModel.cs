using FlashCard1.Messages;
using FlashCard1.Model;
using FlashCard1.Pages.CardPage;
using FlashCard1.Pages.TopicPage.Dialog;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using Microsoft.Toolkit.Mvvm.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Uwp.HCore.Service.Database;
using Uwp.HCore.Service.Navigation;
using Uwp.Model.Model;
using Windows.UI.Xaml.Controls;

namespace FlashCard1.Pages.TopicPage
{
    public class MainPageViewModel : ObservableObject
    {
        private readonly INavigationService _navigationService;
        private readonly IDataService _dataService;

        public MainPageViewModel(INavigationService navigationService, IDataService dataService)
        {
            _navigationService = navigationService;
            _dataService = dataService;

            AllTopicData = _dataService.GetTopicData();

            Topics = AllTopicData;
            CheckDelete = false;
            WeakReferenceMessenger.Default.Register<ChangeMessage>(this, (r, m) =>
            {
                CheckDelete = m.check;
            });

            WeakReferenceMessenger.Default.Register<SendObjectMessage>(this, (r, m) =>
            {
                m.Reply(SelectedTopicItem);
            });
        }

        private bool _doesShow;
        public bool DoesShow 
        { 
            get => _doesShow;
            set => SetProperty(ref _doesShow, value);
        }


        private bool _checkDelete;
        public bool CheckDelete
        {
            get => _checkDelete;
            set => SetProperty(ref _checkDelete, value);
        }

        public Topic SelectedTopicItem { get; set; }


        private ObservableCollection<Topic> _allTopicData;
        public ObservableCollection<Topic> AllTopicData
        {
            get => _allTopicData;
            set => SetProperty(ref _allTopicData, value);
        }

        private ObservableCollection<Topic> _topics;
        public ObservableCollection<Topic> Topics
        {
            get => _topics;
            set => SetProperty(ref _topics, value);
        }


        #region Xử lý hiển thị MainPage Content
        private RelayCommand _mainPageLoadedCommand;
        public RelayCommand MainPageLoadedCommand => _mainPageLoadedCommand ?? (_mainPageLoadedCommand = new RelayCommand(() =>
        {
            ShowEmptyScreen();
        }));

        private void ShowEmptyScreen()
        {
            DoesShow = (AllTopicData.Count > 0) ? true : false;
        }
        #endregion

        #region Mở AddTopicDialog
        private RelayCommand _openAddTopicDialogCommand;
        public RelayCommand OpenAddTopicDialogCommand => _openAddTopicDialogCommand ?? (_openAddTopicDialogCommand = new RelayCommand(async () =>
        {
            ContentDialog dialog = new AddTopicDialog();
            await dialog.ShowAsync();
            AllTopicData = _dataService.GetTopicData();
            Topics = AllTopicData;
            ShowEmptyScreen();
        }));
        #endregion

        #region Mở DeleteTopicDialo + Xử lý xóa Topic
        private RelayCommand<Topic> _openDeleteTopicDialogCommand;
        public RelayCommand<Topic> OpenDeleteTopicDialogCommand => _openDeleteTopicDialogCommand ?? (_openDeleteTopicDialogCommand = new RelayCommand<Topic>( async (selectedItem) =>
        {
            ContentDialog dialog = new DeleteTopicDialog();
            await dialog.ShowAsync();

            if(CheckDelete == true)
            {
                Topics.Remove(selectedItem);
                _dataService.DeleteTopicData(selectedItem);
            }
            ShowEmptyScreen();

        }));
        #endregion

        #region Mở EditTopicDialog
        private RelayCommand<Topic> _openEditTopicDialogCommand;
        public RelayCommand<Topic> OpenEditTopicDialogCommand => _openEditTopicDialogCommand ?? (_openEditTopicDialogCommand = new RelayCommand<Topic>(async (selectedItem) =>
        {
            SelectedTopicItem = selectedItem;
            ContentDialog dialog = new EditTopicDialog();
            await dialog.ShowAsync();
        }));
        #endregion

        #region Mở PremiumDialog
        private RelayCommand _openPremiumDialogCommand;
        public RelayCommand OpenPremiumDialogCommand => _openPremiumDialogCommand ?? (_openPremiumDialogCommand = new RelayCommand(async () =>
        {
            ContentDialog dialog = new PremiumDialog();
            await dialog.ShowAsync();
        }));
        #endregion

        #region Xử lý SelectionChanged + Navigate
        private RelayCommand _selectionChangedCommand;
        public RelayCommand SelectionChangedCommand => _selectionChangedCommand ?? (_selectionChangedCommand = new RelayCommand(() =>
        {   
            _navigationService.Navigate(typeof(MainCardPage));
        }));
        #endregion


        #region Xử lý tìm kiếm
        private RelayCommand<string> _autoSuggestTextChangedCommand;
        public RelayCommand<string> AutoSuggestTextChangedCommand => _autoSuggestTextChangedCommand ?? (_autoSuggestTextChangedCommand = new RelayCommand<string>((str) =>
        {
            str = str.Trim();

            if (String.IsNullOrEmpty(str))
            {
                Topics = AllTopicData;
            }
            else
            {
                Topics = HandleSearch(str);
            }
        }));


        // Kiểm tra ký tự nhập vào có tồn tại trong tên của Topic
        private ObservableCollection<Topic> HandleSearch(string str)
        {
            ObservableCollection<Topic> SearchData = new ObservableCollection<Topic>();
            
            foreach (Topic t in AllTopicData)
            {
                if(t.Name.IndexOf(str) > -1)
                {
                    SearchData.Add(t);
                }
            }

            return SearchData;
        }

        #endregion
    }
}
