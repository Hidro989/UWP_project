using FlashCard1.Messages;
using FlashCard1.Pages.CardPage.Dialog;
using FlashCard1.Pages.LearningPage.Dialog;
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

namespace FlashCard1.Pages.LearningPage
{
    public class MainLearnPageViewModel : ObservableObject
    {
        public readonly IDataService _dataService;
        public readonly INavigationService _navigationService;

        public MainLearnPageViewModel(IDataService dataService, INavigationService navigationService)
        {
            _dataService = dataService;
            _navigationService = navigationService;

            Choices = WeakReferenceMessenger.Default.Send<SendKeyValueMessage>().Response;
            ATopic = WeakReferenceMessenger.Default.Send<SendObjectMessage>().Response as Topic;

            WeakReferenceMessenger.Default.Register<SendObjectMessage>(this, (r, m) =>
            {
                m.Reply(ATopic);
            });


            Cards = _dataService.GetCardData(ATopic.Id);

            switch (Choices.ElementAt(1).Value)
            {
                case 0:
                    Cards = getAllCard();
                    break;
                case 1:
                    Cards = getNotLearntCards();
                    break;
                case 2:
                    Cards = getRandomNotLearnCards(20);
                    break;
                case 3: 
                    Cards = getRandomNotLearnCards(10);
                    break;
                case 4:
                    Cards = getRandomNotLearnCards(5);
                    break;
            }

            SelectedCard = Cards[0];
            Cards[0].Views += 1;
            _dataService.UpdateCardData(Cards[0]);

            DoesShow = Choices.ElementAt(0).Value == 0 ? true : false;


            // Thiết lập màu chế độ light
            CheckBackground = true;
            ColorBackground = "White";
            ColorLetter = "Black";
            ColorMode = "White";

            WeakReferenceMessenger.Default.Register<SendCardMessage>(this, (r, m) =>
            {
                m.Reply(SelectedCard);
            });
        }

        private ObservableCollection<Card> getAllCard()
        {   
            foreach(var card in Cards)
            {
                if(card.Progress == 100)
                {
                    card.Progress = 0;
                    _dataService.UpdateCardData(card);
                }
            }

            return getNotLearntCards();
        }

        private ObservableCollection<Card> getRandomNotLearnCards(int v)
        {
            ObservableCollection<Card> Randomedcars = new ObservableCollection<Card>();
            ObservableCollection<Card> cars = new ObservableCollection<Card>();
            cars = getNotLearntCards();

            v = (v >= cars.Count) ? cars.Count() : v;

            if (cars.Count > 2)
            {
                List<int> listRandom = new List<int>();
                int upperBound = cars.Count;

                Random t = new Random();

                for (int i = 0; i < v; ++i)
                {
                    int number = t.Next(upperBound);
                    if (listRandom.Contains(number) == false)
                    {
                        listRandom.Add(number);
                        Randomedcars.Add(cars[number]);
                    }
                    else
                    {
                        i--;
                    }

                }
            }
            else
            {
                Randomedcars = cars;
            }
            return Randomedcars;
        }

        private ObservableCollection<Card> getNotLearntCards()
        {
            ObservableCollection<Card> cars = new ObservableCollection<Card>();
            
            foreach(var card in Cards)
            {
                if(card.Progress < 100)
                {
                    cars.Add(card);
                }
            }

            return cars;
        }

        public IDictionary<string, int> Choices { get; set; }

        private ObservableCollection<Card> _cards;
        public ObservableCollection<Card> Cards
        {
            get => _cards;
            set => SetProperty(ref _cards, value);
        }


        private bool _doesShow;
        public bool DoesShow
        {
            get => _doesShow;
            set => SetProperty(ref _doesShow, value);
        }


        private Topic _aTopic;
        public Topic ATopic
        {
            get => _aTopic;
            set => SetProperty(ref _aTopic, value);
        }

        private Card _selectedCard;
        public Card SelectedCard
        {
            get => _selectedCard;
            set => SetProperty(ref _selectedCard, value);
        }


        private RelayCommand _learnedCommand;
        public RelayCommand LeanedCommand => _learnedCommand ?? (_learnedCommand = new RelayCommand(async () =>
        {
           
            SelectedCard.Progress += 20;
            
            if(SelectedCard.Progress == 100)
            {
                Cards.Remove(SelectedCard);
            }
            _dataService.UpdateCardData(SelectedCard);

            if(Cards.Count > 0)
            {
                //Lấy thẻ mới
                Random t = new Random();
                int number = t.Next(Cards.Count);
                SelectedCard = Cards[number];

                // Tăng lượt xem cho thẻ mới
                SelectedCard.Views += 1;
                _dataService.UpdateCardData(SelectedCard);

                DoesShow = Choices.ElementAt(0).Value == 0 ? true : false;
            }
            else
            {
                ContentDialog dialog = new CongratulationDialog();
                await dialog.ShowAsync();
                _navigationService.GoBack();
            }
        }));



        private RelayCommand _needPracticeCommand;
        public RelayCommand NeedPracticeCommad => _needPracticeCommand ?? (_needPracticeCommand = new RelayCommand(() =>
        {
            if(SelectedCard.Progress > 0)
            {
                SelectedCard.Progress -= 20;
                _dataService.UpdateCardData(SelectedCard);

            }

            if(Cards.Count > 0)
            {   
                // Lấy thẻ mới
                Random t = new Random();
                int number = t.Next(Cards.Count);
                SelectedCard = Cards[number];

                // Tăng lượt xem cho thẻ mới
                SelectedCard.Views += 1;
                _dataService.UpdateCardData(SelectedCard);

                DoesShow = Choices.ElementAt(0).Value == 0 ? true : false;
            }
        }));



        private RelayCommand _flipCommand;
        public RelayCommand FlipCommad => _flipCommand ?? (_flipCommand = new RelayCommand(() =>
        {
            DoesShow = !DoesShow;
        }));

        #region Xử lý Dark/Light Mode

        private bool _checkBackground;
        public bool CheckBackground
        {
            get => _checkBackground;
            set => SetProperty(ref _checkBackground, value);
        }

        private string _colorBackground;
        public string ColorBackground
        {
            get => _colorBackground;
            set => SetProperty(ref _colorBackground, value);
        }

        private string _colorLetter;
        public string ColorLetter
        {
            get => _colorLetter;
            set => SetProperty(ref _colorLetter, value);
        }

        private string _colorMode;
        public string ColorMode
        {
            get => _colorMode;
            set => SetProperty(ref _colorMode, value);
        }

        private RelayCommand _handleBackgroud;
        public RelayCommand HandleBackground => _handleBackgroud ?? (_handleBackgroud = new RelayCommand(() =>
        {
            CheckBackground = !CheckBackground;
            
            if(CheckBackground == false)
            {
                ColorBackground = "Black";
                ColorLetter = "White";
                ColorMode = "Orange";
            }
            else
            {
                ColorBackground = "White";
                ColorLetter = "Black";
                ColorMode = "White";
            }
        }));

        #endregion

        private RelayCommand _openEditCardDialog;
        public RelayCommand OpenEditCardDialog => _openEditCardDialog ?? (_openEditCardDialog = new RelayCommand(async () => {
            ContentDialog dialog = new EditCardDialog();
            await dialog.ShowAsync();
        }));

        private RelayCommand _goBack;
        public RelayCommand GoBack => _goBack ?? (_goBack = new RelayCommand(() =>
        {
            _navigationService.GoBack();
        }));
    }
}
