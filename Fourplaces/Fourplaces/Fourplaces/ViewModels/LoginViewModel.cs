using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Storm.Mvvm;
using Fourplaces.Views;
using System.Windows.Input;
using Storm.Mvvm.Services;
using Fourplaces.Services;
using System.Net.Http;
using Fourplaces.DTOs;

namespace Fourplaces.ViewModels
{
    class LoginViewModel : ViewModelBase
    {
        private string _mail;
        public string Mail
        {
            get => _mail;
            set => SetProperty(ref _mail, value);
        }

        private string _mdp;
        public string Mdp
        {
            get => _mdp;
            set => SetProperty(ref _mdp, value);
        }




        public ICommand GOCreerUtilisateur { get; }
        public ICommand Connexion { get; }
        public LoginViewModel()
        {
            GOCreerUtilisateur = new Command(GOCreerUtilisateurAction);
            Connexion = new Command(ConnexionAction);
        }


        private async void GOCreerUtilisateurAction()
        {
            await NavigationService.PushAsync(new RegisterPage());
        }

        private async void ConnexionAction()
        {
            /*Mail = "mail@mail.com";
            Mdp = "mdp";*/

            INavigationService ins = DependencyService.Get<INavigationService>();

            ApiClient apiClient = new ApiClient();
            HttpResponseMessage response = await apiClient.Execute(HttpMethod.Post, "https://td-api.julienmialon.com/auth/login", new LoginRequest() { Email = Mail, Password = Mdp});
            Response<LoginResult> result = await apiClient.ReadFromResponse<Response<LoginResult>>(response);

            if (result.IsSuccess)
            {
                App.Access_token = result.Data.AccessToken;
                App.Refresh_token = result.Data.RefreshToken;
                await ins.PushAsync<ListeLieuxPage>();
            }
            
        }
    }
}
