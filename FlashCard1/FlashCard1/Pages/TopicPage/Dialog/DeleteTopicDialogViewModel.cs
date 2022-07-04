using FlashCard1.Messages;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using Microsoft.Toolkit.Mvvm.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace FlashCard1.Pages.TopicPage.Dialog
{
    public class DeleteTopicDialogViewModel : ObservableObject
    {


        private RelayCommand<ContentDialog> _acceptDeleteCommand;
        public RelayCommand<ContentDialog> AccpetDeleteCommand => _acceptDeleteCommand ?? (_acceptDeleteCommand = new RelayCommand<ContentDialog>((dialog) =>
        {
            WeakReferenceMessenger.Default.Send(new ChangeMessage(true));
            dialog.Hide();
        }));

        private RelayCommand<ContentDialog> _cancelCommand;
        public RelayCommand<ContentDialog> CancelCommand => _cancelCommand ?? (_cancelCommand = new RelayCommand<ContentDialog>((dialog) =>
        {   
            dialog.Hide();
        }));
    }
}
