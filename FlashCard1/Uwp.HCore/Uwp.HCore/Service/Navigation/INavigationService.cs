using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Uwp.HCore.Service.Navigation
{
    public interface INavigationService
    {
        void Navigate(Type sourceTypePage);

        void Navigate(Type sourceType, object parameter);

        void Navigate(object subFrame, Type sourceTypePage);
        void Navigate(object subFrame, Type sourceTypePage, object parameter);

        void GoBack(object subFrame);
        void GoBack();
    }
}
