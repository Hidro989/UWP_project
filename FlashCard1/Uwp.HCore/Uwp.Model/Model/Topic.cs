using Microsoft.Toolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Uwp.Model.Model
{
    public class Topic : ObservableObject
    {
        public int Id { get; set; }

        private string _name;
        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        private string _color;
        public string Color
        {
            get => _color;
            set => SetProperty(ref _color, value);
        }

        public ObservableCollection<Card> Cards { get; set; } = new ObservableCollection<Card>();
    }
}
