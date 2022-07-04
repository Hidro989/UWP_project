using FlashCard1.Messages;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using Microsoft.Toolkit.Mvvm.Messaging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Uwp.HCore.Helper;
using Uwp.HCore.Service.Database;
using Uwp.Model.Model;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Xaml.Controls;

namespace FlashCard1.Pages.CardPage.Dialog
{
    public class EditCardDialogViewModel : ObservableObject
    {
        private readonly IDataService _dataService;

        public EditCardDialogViewModel(IDataService dataService)
        {
            _dataService = dataService;

            DoesShow = true;

            CardItem = WeakReferenceMessenger.Default.Send<SendCardMessage>().Response;

            ImageFrontItem = CardItem.ImageFront;
            ImageBackItem = CardItem.ImageBack;
            DoesShowImageFront = (ImageFrontItem != null) ? true : false;
            DoesShowImageBack = (ImageBackItem != null) ? true : false;
        }

        private Card _cardItem;
        public Card CardItem
        {
            get => _cardItem;
            set => SetProperty(ref _cardItem, value);
        }



        #region Xử lý hiển thị CardBack & CardFront
        private bool _doesShow;
        public bool DoesShow
        {
            get => _doesShow;
            set => SetProperty(ref _doesShow, value);
        }

        private RelayCommand _showFrontCard;
        public RelayCommand ShowFrontCard => _showFrontCard ?? (_showFrontCard = new RelayCommand(() =>
        {
            DoesShow = true;
        }));

        private RelayCommand _showBackCard;
        public RelayCommand ShowBackCard => _showBackCard ?? (_showBackCard = new RelayCommand(() => {
            DoesShow = false;
        }));
        #endregion

        #region Lấy ảnh từ Folder và hiển thị ImageFront
        private byte[] _imageFrontItem;
        public byte[] ImageFrontItem
        {
            get => _imageFrontItem;
            set => SetProperty(ref _imageFrontItem, value);
        }

        private RelayCommand _addPictureFront;
        public RelayCommand AddPictureFront => _addPictureFront ?? (_addPictureFront = new RelayCommand(async () =>
        {
            FileOpenPicker picker = new FileOpenPicker();
            picker.ViewMode = PickerViewMode.Thumbnail;
            picker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
            picker.FileTypeFilter.Add(".jpg");
            picker.FileTypeFilter.Add(".png");
            picker.FileTypeFilter.Add(".jpeg");
            StorageFile file = await picker.PickSingleFileAsync();

            if (file != null)
            {
                ImageFrontItem = await CommonHelper.ConvertImageToByte(file);
                DoesShowImageFront = true;
            }
        }));
        #endregion

        #region Lấy ảnh từ Folder và hiển thị ImageBack
        private byte[] _imageBackItem;
        public byte[] ImageBackItem
        {
            get => _imageBackItem;
            set => SetProperty(ref _imageBackItem, value);
        }
        private RelayCommand _addPictureBack;
        public RelayCommand AddPictureBack => _addPictureBack ?? (_addPictureBack = new RelayCommand(async () =>
        {
            FileOpenPicker picker = new FileOpenPicker();
            picker.ViewMode = PickerViewMode.Thumbnail;
            picker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
            picker.FileTypeFilter.Add(".jpg");
            picker.FileTypeFilter.Add(".png");
            picker.FileTypeFilter.Add(".jpeg");
            StorageFile file = await picker.PickSingleFileAsync();

            if (file != null)
            {
                ImageBackItem = await CommonHelper.ConvertImageToByte(file);
                DoesShowImageBack = true;
            }
        }));
        #endregion

        #region Xử lý hủy hiển thị ImageFront
        private bool _doesShowImageFront;
        public bool DoesShowImageFront
        {
            get => _doesShowImageFront;
            set => SetProperty(ref _doesShowImageFront, value);
        }

        private RelayCommand _cancelShowImageFront;
        public RelayCommand CancelShowImageFront => _cancelShowImageFront ?? (_cancelShowImageFront = new RelayCommand(() =>
        {
            DoesShowImageFront = false;
            ImageFrontItem = null;
        }));
        #endregion

        #region Xử lý hủy hiển thị ImageBack
        private bool _doesShowImageBack;
        public bool DoesShowImageBack
        {
            get => _doesShowImageBack;
            set => SetProperty(ref _doesShowImageBack, value);
        }

        private RelayCommand _cancelShowImageBack;
        public RelayCommand CancelShowImageBack => _cancelShowImageBack ?? (_cancelShowImageBack = new RelayCommand(() =>
        {
            DoesShowImageBack = false;
            ImageBackItem = null;
        }));
        #endregion

        #region Xử lý lưu dữ liệu
        private RelayCommand<ContentDialog> _acceptSaveData;
        public RelayCommand<ContentDialog> AcceptSaveData => _acceptSaveData ?? (_acceptSaveData = new RelayCommand<ContentDialog>((dialog) =>
        {
            CardItem.ImageFront = ImageFrontItem;
            CardItem.ImageBack = ImageBackItem;
            _dataService.UpdateCardData(CardItem);
            dialog.Hide();
        }));
        #endregion

        #region Đóng Dialog
        private RelayCommand<ContentDialog> _cancelCommand;
        public RelayCommand<ContentDialog> CancelCommand => _cancelCommand ?? (_cancelCommand = new RelayCommand<ContentDialog>((dialog) =>
        {
            dialog.Hide();
        }));
        #endregion

    }
}
