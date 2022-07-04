using FlashCard1.Pages.CardPage;
using FlashCard1.Pages.CardPage.Dialog;
using FlashCard1.Pages.LearningPage;
using FlashCard1.Pages.LearningPage.Dialog;
using FlashCard1.Pages.TopicPage;
using FlashCard1.Pages.TopicPage.Dialog;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Toolkit.Mvvm.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Uwp.HCore.Service.Database;
using Uwp.HCore.Service.Navigation;

namespace FlashCard1.VML
{
    public class ViewModelLocator
    {
        public ViewModelLocator()
        {
            Ioc.Default.ConfigureServices(new ServiceCollection().
                AddSingleton<INavigationService, NavigationService>()
                .AddSingleton<IDataService, DataService>().AddTransient<MainPageViewModel>()
                .AddSingleton<IDataService, DataService>().AddTransient<AddTopicDialogViewModel>()
                .AddSingleton<IDataService, DataService>().AddTransient<EditTopicDialogViewModel>()
                .AddSingleton<INavigationService, NavigationService>()
                .AddSingleton<IDataService,DataService>().AddTransient<MainCardPageViewModel>()
                .AddSingleton<IDataService, DataService>().AddTransient<AddCardDialogViewModel>()
                .AddSingleton<IDataService, DataService>().AddTransient<EditCardDialogViewModel>()
                .AddSingleton<IDataService, DataService>().AddSingleton<INavigationService, NavigationService>().AddTransient<MainLearnPageViewModel>()
                .BuildServiceProvider());
        }


        #region ViewModel of MainPage
        public MainPageViewModel Main => Ioc.Default.GetService<MainPageViewModel>();

        public AddTopicDialogViewModel AddTopicDialog => Ioc.Default.GetService<AddTopicDialogViewModel>();

        public DeleteTopicDialogViewModel DeleteTopicDialog => new DeleteTopicDialogViewModel();

        public EditTopicDialogViewModel EditTopicDialog => Ioc.Default.GetService<EditTopicDialogViewModel>();
        #endregion

        #region ViewModel of CardPage
        public MainCardPageViewModel CardPage => Ioc.Default.GetService<MainCardPageViewModel>();

        public AddCardDialogViewModel AddCardDialog => Ioc.Default.GetService<AddCardDialogViewModel>();
        public EditCardDialogViewModel EditCardDialog => Ioc.Default.GetService<EditCardDialogViewModel>();

        #endregion

        #region ViewModel of LearnPage
        public MainLearnPageViewModel MainLearnPage => Ioc.Default.GetService<MainLearnPageViewModel>();

        #endregion
    }
}
