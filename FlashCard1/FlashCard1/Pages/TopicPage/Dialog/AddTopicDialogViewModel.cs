using FlashCard1.Model;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Uwp.HCore.Service.Database;
using Uwp.Model.Model;
using Windows.UI.Xaml.Controls;

namespace FlashCard1.Pages.TopicPage.Dialog
{
    public class AddTopicDialogViewModel : ObservableObject
    {
        private IDataService _dataService;
        public AddTopicDialogViewModel(IDataService dataService)
        {
            _dataService = dataService;

            ATopic = new Topic
            {
                Name = ""
            };
        }

        private Topic _aTopic;
        public Topic ATopic
        {
            get => _aTopic;
            set => SetProperty(ref _aTopic, value);
        }

        public Colors Colors { get; set; } = new Colors();

        private int _selectedColorIndex;
        public int SelectedColorIndex
        {
            get => _selectedColorIndex;
            set => SetProperty(ref _selectedColorIndex, value);
        }


        private RelayCommand<ContentDialog> _acceptSaveData;
        public RelayCommand<ContentDialog> AcceptSaveData => _acceptSaveData ?? (_acceptSaveData = new RelayCommand<ContentDialog>((dialog) =>
        {   
            ATopic.Color = Colors.ListColor[SelectedColorIndex].Code;
            _dataService.InsertTopicData(ATopic);
            dialog.Hide();
        }));


        private RelayCommand<ContentDialog> _cancelCommand;
        public RelayCommand<ContentDialog> CancelCommand => _cancelCommand ?? (_cancelCommand = new RelayCommand<ContentDialog>((dialog) =>
        {
            dialog.Hide();
        }));
    }
}
