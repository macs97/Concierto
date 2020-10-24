using Concierto.Common.Helpers;
using Concierto.Common.Models;
using Concierto.Common.Responses;
using Concierto.Common.Services;
using Concierto.Prism.Views;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Navigation;
using Xamarin.Essentials;

namespace Concierto.Prism.ViewModels
{
    public class BuyTicketsPageViewModel : ViewModelBase
    {
        private DelegateCommand _buyTickets;
        private readonly INavigationService _navigationService;
        private readonly IApiService _apiService;
        private bool _isRunning;
        public BuyTicketsPageViewModel(INavigationService navigationService, IApiService apiService) : base(navigationService)
        {
            Title = "Buy Tickets";
            _navigationService = navigationService;
            _apiService = apiService;
        }

        public DelegateCommand BuyTicketCommand => _buyTickets ?? (_buyTickets = new DelegateCommand(BuyTicketsAsync));

        public int Cantidad { get; set; }

        public bool IsRunning
        {
            get => _isRunning;
            set => SetProperty(ref _isRunning, value);
        }

        private async void BuyTicketsAsync()
        {
            if (string.IsNullOrEmpty(Cantidad.ToString()))
            {
                await App.Current.MainPage.DisplayAlert(
                    "Error",
                    "You must enter a amount.",
                    "Accept");
                return;
            }
            IsRunning = true;

            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                IsRunning = false;
                await App.Current.MainPage.DisplayAlert("Error", "Check your internet connection.", "Accept");
                return;
            }

            TokenResponse token = JsonConvert.DeserializeObject<TokenResponse>(Settings.Token);
            string url = App.Current.Resources["UrlAPI"].ToString();
            Response response = await _apiService.BuyTicketAsync(url, "api", "/TicketApi/", Cantidad, token.Token);
            IsRunning = false;
            if (!response.IsSuccess)
            {
                await App.Current.MainPage.DisplayAlert("Error", response.Message, "Accept");
                return;
            }
            BuyTicketResponse buyTicketResponse = (BuyTicketResponse)response.Result;
            token.User.Tickets = buyTicketResponse.Tickets;
            Settings.Token = JsonConvert.SerializeObject(token);
            await App.Current.MainPage.DisplayAlert("Success", "Tickets Buy Successfull", "Accept");
            await _navigationService.NavigateAsync($"/{nameof(ConciertoMasterDetailPage)}/NavigationPage/{nameof(TicketPage)}");

        }
    }
}
