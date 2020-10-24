using Concierto.Common.Helpers;
using Concierto.Common.Models;
using Concierto.Common.Responses;
using Concierto.Prism.ItemViewModels;
using Concierto.Prism.Views;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Concierto.Prism.ViewModels
{
    public class ConciertoMasterDetailPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private User _user;

        public ConciertoMasterDetailPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            _navigationService = navigationService;
            LoadMenus();
            LoadUser();
        }
        public ObservableCollection<MenuItemViewModel> Menus { get; set; }

        public User User
        {
            get => _user;
            set => SetProperty(ref _user, value);
        }

        private void LoadUser()
        {
            if (Settings.IsLogin)
            {
                TokenResponse token = JsonConvert.DeserializeObject<TokenResponse>(Settings.Token);
                User = token.User;
            }
        }


        private void LoadMenus()
        {
            List<Menu> menus = new List<Menu>
        {
            new Menu
            {
                PageName = $"{nameof(TicketPage)}",
                Title = "Tickets",
                IsLoginRequired = true
            },
            new Menu
            {
                PageName = $"{nameof(BuyTicketsPage)}",
                Title = "Buy Tickets",
                IsLoginRequired = true
            },
            new Menu
            {
                PageName = $"{nameof(LoginPage)}",
                Title =  Settings.IsLogin ? "Logout" :"Login"
            }
        };

            Menus = new ObservableCollection<MenuItemViewModel>(
                menus.Select(m => new MenuItemViewModel(_navigationService)
                {
                    PageName = m.PageName,
                    Title = m.Title,
                    IsLoginRequired = m.IsLoginRequired
                }).ToList());
        }
    }
}
