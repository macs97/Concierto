using Concierto.Common.Helpers;
using Concierto.Common.Models;
using Concierto.Common.Responses;
using Concierto.Common.Services;
using Newtonsoft.Json;
using Prism.Navigation;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Essentials;

namespace Concierto.Prism.ViewModels
{
    public class TicketPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IApiService _apiService;
       // private ObservableCollection<Ticket> _tickets;
        private bool _isRunning;
        private List<Ticket> _tickets;

        public TicketPageViewModel(INavigationService navigationService, IApiService apiService)
            : base(navigationService)
        {
            _navigationService = navigationService;
            _apiService = apiService;
            Title = "Tickets";
            //LoadTicketsAsync();
            Load();
        }

        private void Load()
        {
            LoginResponse token = JsonConvert.DeserializeObject<LoginResponse>(Settings.Token);
            Tickets = token.User.Tickets;
        }

        public List<Ticket> Tickets
        {
            get => _tickets;
            set => SetProperty(ref _tickets, value);
        }
         
        /*public ObservableCollection<Ticket> Tickets
        {
            get => _tickets;
            set => SetProperty(ref _tickets, value);
        }*/

        public bool IsRunning
        {
            get => _isRunning;
            set => SetProperty(ref _isRunning, value);
        }


        /*private async void LoadTicketsAsync()
        {
            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                await App.Current.MainPage.DisplayAlert("Error", "Check the internet connection.", "Accept");
                return;
            }
            IsRunning = true;
            string url = App.Current.Resources["UrlAPI"].ToString();
            Response response = await _apiService.GetListAsync<Ticket>(
                url,
                "/api",
                "/ticket");
            IsRunning = false;
            if (!response.IsSuccess)
            {
                await App.Current.MainPage.DisplayAlert(
                    "Error",
                    response.Message,
                    "Accept");
                return;
            }

            List<Ticket> myTickets = (List<Ticket>)response.Result;
            Tickets = new ObservableCollection<Ticket>(myTickets);
        }*/


    }
}
