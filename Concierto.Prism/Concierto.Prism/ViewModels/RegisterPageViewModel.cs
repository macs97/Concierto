using Concierto.Common.Requests;
using Concierto.Common.Responses;
using Concierto.Common.Services;
using Concierto.Prism.Helpers;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace Concierto.Prism.ViewModels
{
    public class RegisterPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IApiService _apiService;
        private readonly IRegexHelper _regexHelper;
        private bool _isRunning;
        private bool _isEnabled;
        private UserRequest _user;
        private DelegateCommand _registerCommand;


        public RegisterPageViewModel(INavigationService navigationService, IApiService apiService,
            IRegexHelper regexHelper)
            : base(navigationService)
        {
            Title = "Register";
            _navigationService = navigationService;
            _apiService = apiService;
            _regexHelper = regexHelper;
            IsEnabled = true;
            User = new UserRequest();
        }

        public DelegateCommand RegisterCommand => _registerCommand ?? (_registerCommand = new DelegateCommand(RegisterAsync));


        public UserRequest User
        {
            get => _user;
            set => SetProperty(ref _user, value);
        }

        public bool IsRunning
        {
            get => _isRunning;
            set => SetProperty(ref _isRunning, value);
        }

        public bool IsEnabled
        {
            get => _isEnabled;
            set => SetProperty(ref _isEnabled, value);
        }

        private async void RegisterAsync()
        {
            bool isValid = await ValidateDataAsync();
            if (!isValid)
            {
                return;
            }

            IsRunning = true;
            IsEnabled = false;

            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                IsRunning = false;
                IsEnabled = true;
                await App.Current.MainPage.DisplayAlert("Error", "Check your network", "Accept");
                return;
            }

            string url = App.Current.Resources["UrlAPI"].ToString();

            Response response = await _apiService.RegisterUserAsync(url, "/api", "/AccountApi", User);
            IsRunning = false;
            IsEnabled = true;

            if (!response.IsSuccess)
            {
                await App.Current.MainPage.DisplayAlert("Error", response.Message, "Accept");
                return;
            }

            await App.Current.MainPage.DisplayAlert("Ok", "Register successful", "Accept");
            await _navigationService.GoBackAsync();

        }


        private async Task<bool> ValidateDataAsync()
        {
            
            if (string.IsNullOrEmpty(User.Email) || !_regexHelper.IsValidEmail(User.Email))
            {
                await App.Current.MainPage.DisplayAlert("Error", "Email Error", "Accept");
                return false;
            }

            if (string.IsNullOrEmpty(User.Password) || User.Password?.Length < 6)
            {
                await App.Current.MainPage.DisplayAlert("Error", "Password Error", "Accept");
                return false;
            }

            if (string.IsNullOrEmpty(User.PasswordConfirm))
            {
                await App.Current.MainPage.DisplayAlert("Error", "Password Confirm Error", "Accept");
                return false;
            }

            if (User.Password != User.PasswordConfirm)
            {
                await App.Current.MainPage.DisplayAlert("Error", "Password Confirm Error", "Accept");
                return false;
            }

            return true;
        }
    }
}
