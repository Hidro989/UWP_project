using FlashCard1.Messages;
using FlashCard1.Pages.CardPage.Dialog;
using FlashCard1.Pages.LearningPage;
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

namespace FlashCard1.Pages.CardPage
{
    public class MainCardPageViewModel : ObservableObject
    {
        private readonly INavigationService _navigationService;
        private readonly IDataService _dataService;
        public MainCardPageViewModel(INavigationService navigationService, IDataService dataService)
        {
            _navigationService = navigationService;
            _dataService = dataService;

            IsDeleted = false;
            IsReseted = false;

            ATopic = WeakReferenceMessenger.Default.Send<SendObjectMessage>().Response as Topic;
            
            WeakReferenceMessenger.Default.Register<SendObjectMessage>(this, (r, m) =>
            {
                m.Reply(ATopic);
            });

            WeakReferenceMessenger.Default.Register<ChangeMessage>(this, (r, m) =>
            {
                IsDeleted = m.check;
            });

            WeakReferenceMessenger.Default.Register<SubChangeMessage>(this, (r, m) =>
            {
                IsReseted = m.check;
            });

            WeakReferenceMessenger.Default.Register<SendCardMessage>(this, (r, m) =>
            {
                m.Reply(SelectedCardItem);
            });

            WeakReferenceMessenger.Default.Register<KeyValueMessage>(this, (r, m) =>
            {
                Choices = m.Choice;
            });

            WeakReferenceMessenger.Default.Register<SendKeyValueMessage>(this, (r, m) =>
            {
                m.Reply(Choices);
            });

            Cards = _dataService.GetCardData(ATopic.Id);
            Learndcard = getLearnCard();
            ShowEmptyScreen();
        }


        public IDictionary<string, int> Choices { get; set; }

        private ObservableCollection<Card> _cards;
        public ObservableCollection<Card> Cards
        {
            get => _cards;
            set
            {
                SetProperty(ref _cards, value);
            }
        }


        private Topic _aTopic;
        public Topic ATopic
        {
            get => _aTopic;
            set => SetProperty(ref _aTopic, value);
        }

        #region Xử lý EmptyScreen
        private void ShowEmptyScreen()
        {
            DoesShowEmptyScreen = (Cards.Count > 0) ? false : true;
        }

        private bool _doesShowEmptyScreen;
        public bool DoesShowEmptyScreen
        {
            get => _doesShowEmptyScreen;
            set
            {
                SetProperty(ref _doesShowEmptyScreen, value);
                OpenAskLearnDialogCommand.NotifyCanExecuteChanged();
                OpenResetAllDialgCommand.NotifyCanExecuteChanged();
            }
        }

        #endregion

        #region Xử lý LearnedCard
        private int _learndcard;
        public int Learndcard
        {
            get => _learndcard;
            set => SetProperty(ref _learndcard, value);
        }

        private int getLearnCard()
        {
            int count = 0;
            foreach (Card c in Cards)
            {
                if (c.Progress == 100)
                {
                    count++;
                }
            }

            return count;
        }
        #endregion

        #region Mở EditItem
        private Card _selectedCardItem;
        public Card SelectedCardItem
        {
            get => _selectedCardItem;
            set => SetProperty(ref _selectedCardItem, value);
        }

        private RelayCommand<Card> _openEditCardDialgCommand;
        public RelayCommand<Card> OpenEditCardDialgCommand => _openEditCardDialgCommand ?? (_openEditCardDialgCommand = new RelayCommand<Card>(async (selectedItem) =>
        {
            SelectedCardItem = selectedItem;
            ContentDialog dialog = new EditCardDialog();
            await dialog.ShowAsync();
        }));
        #endregion

        #region Mở AddCardDialog
        private RelayCommand _openAddCardPageDialogCommand;
        public RelayCommand OpenAddCardPageDialogCommand => _openAddCardPageDialogCommand ?? (_openAddCardPageDialogCommand = new RelayCommand(async() =>
        {
            ContentDialog dialog = new AddCardDialog();
            await dialog.ShowAsync();
            Cards = _dataService.GetCardData(ATopic.Id);
            ShowEmptyScreen();
        }));
        #endregion

        #region Xử lý xóa Item
        private bool _isDeleted;
        public bool IsDeleted
        {
            get => _isDeleted;
            set => SetProperty(ref _isDeleted, value);
        }

        private RelayCommand<Card> _openDeleteCardDialog;
        public RelayCommand<Card> OpenDeleteCardDialog => _openDeleteCardDialog ?? (_openDeleteCardDialog = new RelayCommand<Card>(async (seletedItem) =>
        {
            ContentDialog dialog = new DeleteCardDialog();
            await dialog.ShowAsync();
            if (IsDeleted == true)
            {   
                _dataService.DeleteCardData(seletedItem);
                Cards.Remove(seletedItem);
                ShowEmptyScreen();
            }
        }));
        #endregion


        private RelayCommand _goBackMainPageCommand;
        public RelayCommand GoBackMainPageCommand => _goBackMainPageCommand ?? (_goBackMainPageCommand = new RelayCommand(() =>
        {
            _navigationService.GoBack();
        }));


        private RelayCommand<Button> _openAskLearnDialogCommand;
        public RelayCommand<Button> OpenAskLearnDialogCommand => _openAskLearnDialogCommand ?? (_openAskLearnDialogCommand = new RelayCommand<Button>(async (btn) =>
        {
            ContentDialog dialog = new AskLearnDialog();
            await dialog.ShowAsync();

            if(Choices != null)
            {
                int choice = Choices.ElementAt(1).Value;
                if(choice == 0)
                {
                    _navigationService.Navigate(typeof(MainLearningPage));
                }
                else if (choice == 1 || choice == 2 || choice == 3 || choice == 4)
                {
                    if(checkAllProgess() == true)
                    {
                        _navigationService.Navigate(typeof(MainLearningPage));
                    }
                    else
                    {
                        Flyout flyout = new Flyout();
                        TextBlock text = new TextBlock();
                        text.Text = "Please reset to continue learning !!";
                        flyout.Content = text;
                        flyout.ShowAt(btn);
                    }
                    
                }

            }
            
        },
         (btn) => (DoesShowEmptyScreen == false)));

        private bool checkAllProgess()
        {
            foreach(var c in Cards)
            {
                if(c.Progress < 100)
                {
                    return true;
                }
            }

            return false;
        }


        #region Xử lý Reset Progess
        private bool _isReseted;
        public bool IsReseted
        {
            get => _isReseted;
            set => SetProperty(ref _isReseted, value);
        }

        private RelayCommand _openResetAllDialogCommand;
        public RelayCommand OpenResetAllDialgCommand => _openResetAllDialogCommand ?? (_openResetAllDialogCommand = new RelayCommand(async() =>
        {
            ContentDialog dialog = new ResetAllDialog();
            await dialog.ShowAsync();
            if(IsReseted == true)
            {
                foreach(var c in Cards)
                {
                    c.Progress = 0;
                }
            }
        },
         () => (DoesShowEmptyScreen == false)));
        #endregion
    }
}
