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
        private bool _isRunning;
        private List<Ticket> _tickets;

        public TicketPageViewModel(INavigationService navigationService, IApiService apiService)
            : base(navigationService)
        {
            _navigationService = navigationService;
            _apiService = apiService;
            Title = "Tickets";
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
         
        public bool IsRunning
        {
            get => _isRunning;
            set => SetProperty(ref _isRunning, value);
        }
    }
}
