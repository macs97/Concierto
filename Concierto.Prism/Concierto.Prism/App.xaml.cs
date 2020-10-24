using Prism;
using Prism.Ioc;
using Concierto.Prism.ViewModels;
using Concierto.Prism.Views;
using Xamarin.Essentials.Interfaces;
using Xamarin.Essentials.Implementation;
using Xamarin.Forms;
using Concierto.Common.Services;
using Syncfusion.Licensing;
using Concierto.Prism.Helpers;

namespace Concierto.Prism
{
    public partial class App
    {
        public App(IPlatformInitializer initializer)
            : base(initializer)
        {
        }

        protected override async void OnInitialized()
        {
            SyncfusionLicenseProvider.RegisterLicense("MzM2MjIyQDMxMzgyZTMzMmUzMG1aSkc4dEdSNFRPWmcxRnh2d21GWUFVNHV4ZEJUWThZcVFIM3hCMTd6eUU9");
            InitializeComponent();

            await NavigationService.NavigateAsync($"{nameof(ConciertoMasterDetailPage)}/NavigationPage/{nameof(MainPage)}");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IAppInfo, AppInfoImplementation>();
            containerRegistry.Register<IApiService, ApiService>();
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>();
            containerRegistry.RegisterForNavigation<TicketPage, TicketPageViewModel>();
            containerRegistry.RegisterForNavigation<LoginPage, LoginPageViewModel>();
            containerRegistry.RegisterForNavigation<BuyTicketsPage, BuyTicketsPageViewModel>();
            containerRegistry.RegisterForNavigation<RegisterPage, RegisterPageViewModel>();
            containerRegistry.RegisterForNavigation<ConciertoMasterDetailPage, ConciertoMasterDetailPageViewModel>();
            containerRegistry.Register<IRegexHelper, RegexHelper>();
        }
    }
}
