using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Uwp.HCore.Service.Navigation
{
    public class NavigationService : INavigationService
    {
        private Frame _farme;

        public Frame Farme => _farme ?? (_farme = Window.Current.Content as Frame);

        public void GoBack()
        {   
            Farme.GoBack();
        }

        public void GoBack(object subFrame)
        {
            Frame frame = (Frame)subFrame;
            frame.GoBack();
        }

        public void Navigate(Type sourceTypePage)
        {
            Farme.Navigate(sourceTypePage);
        }

        public void Navigate(Type sourceTypePage, object parameter)
        {
            Farme.Navigate(sourceTypePage, parameter);
        }

        public void Navigate(object subFrame, Type sourceTypePage)
        {
            Frame frame = subFrame as Frame;
            frame.Navigate(sourceTypePage);
        }

        public void Navigate(object subFrame, Type sourceTypePage, object parameter)
        {
            Frame frame = subFrame as Frame;
            frame.Navigate(sourceTypePage, parameter);
        }
    }
}
