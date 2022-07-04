using FlashCard1.Messages;
using FlashCard1.Model;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using Microsoft.Toolkit.Mvvm.Messaging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Uwp.HCore.Service.Database;
using Uwp.Model.Model;
using Windows.UI.Xaml.Controls;

namespace FlashCard1.Pages.TopicPage.Dialog
{
    public class EditTopicDialogViewModel : ObservableObject
    {
        private readonly IDataService _dataservice;

        public EditTopicDialogViewModel(IDataService dataservice)
        {
            _dataservice = dataservice;

            ATopic = WeakReferenceMessenger.Default.Send<SendObjectMessage>().Response as Topic;

            // Lấy màu của đối tượng để binding
            foreach(var i in Colors.ListColor)
            {
                if (i.Code.Equals(ATopic.Color))
                {
                    SelectedColor = i;
                }
            }
        }

        public Colors Colors { get; set; } = new Colors();

        private Topic _aTopic;
        public Topic ATopic
        {
            get => _aTopic;
            set => SetProperty(ref _aTopic, value);
        }

        private Color _selectedColor;
        public Color SelectedColor 
        {
            get => _selectedColor; 
            set => SetProperty(ref _selectedColor, value); 
        }


        private RelayCommand<ContentDialog> _acceptSaveData;
        public RelayCommand<ContentDialog> AcceptSaveData => _acceptSaveData ?? (_acceptSaveData = new RelayCommand<ContentDialog>((dialog) =>
        {
            ATopic.Color = SelectedColor.Code;
            _dataservice.UpdateTopicData(ATopic);
            dialog.Hide();
        }));



        private RelayCommand<ContentDialog> _cancelCommand;
        public RelayCommand<ContentDialog> CancelCommand => _cancelCommand ?? (_cancelCommand = new RelayCommand<ContentDialog>((dialog) =>
        {
            dialog.Hide();
        }));
    }
}
