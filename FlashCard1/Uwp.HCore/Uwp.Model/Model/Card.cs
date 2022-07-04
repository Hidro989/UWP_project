using Microsoft.Toolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Uwp.Model.Model
{
    public class Card : ObservableObject
    {
        public int Id { get; set; }

        private string _nameFront;
        public string NameFront
        {
            get => _nameFront;
            set => SetProperty(ref _nameFront, value);
        }

        private string _nameBack;
        public string NameBack
        {
            get => _nameBack;
            set => SetProperty(ref _nameBack, value);
        }

        private string _desFront;
        public string DesFront
        {
            get => _desFront;
            set => SetProperty(ref _desFront, value);
        }

        private string _desBack;
        public string DesBack
        {
            get => _desBack;
            set => SetProperty(ref _desBack, value);
        }

        private byte[] _imageFront;
        public byte[] ImageFront
        {
            get => _imageFront;
            set => SetProperty(ref _imageFront, value);
        }

        private byte[] _imageBack;
        public byte[] ImageBack
        {
            get => _imageBack;
            set => SetProperty(ref _imageBack, value);
        }

        private int _progress;
        public int Progress
        {
            get => _progress;
            set => SetProperty(ref _progress, value);
        }

        private int _views;
        public int Views
        {
            get => _views; 
            set => SetProperty(ref _views, value);
        }

        public int TopicId { get; set; }
        public Topic Topic { get; set; }
    }
}
