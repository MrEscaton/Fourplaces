using Fourplaces.DTOs;
using Fourplaces.Services;
using Storm.Mvvm;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Fourplaces.Viewmodels
{
    class RegisterViewModel : ViewModelBase
    {
        private string _mail;
        public string Mail 
        {
            get => _mail;
            set => SetProperty(ref _mail, value);
        }

        private string _nom;
        public string Nom
        {
            get => _nom;
            set => SetProperty(ref _nom, value);
        }

        private string _prenom;
        public string Prenom
        {
            get => _prenom;
            set => SetProperty(ref _prenom, value);
        }

        private string _mdp;
        public string Mdp
        {
            get => _mdp;
            set => SetProperty(ref _mdp, value);
        }

        private string _mdpConf;
        public string MdpConf
        {
            get => _mdpConf;
            set => SetProperty(ref _mdpConf, value);
        }

        private string _messageErreur;
        public string MessageErreur
        {
            get => _messageErreur;
            set => SetProperty(ref _messageErreur, value);
        }

        public ICommand CreerUtilisateur { get; }

        public RegisterViewModel()
        {
            CreerUtilisateur = new Command(CreerUtilisateurAction);
        }

        private async void CreerUtilisateurAction()
        {
            if(Mdp == MdpConf)
            {
                ApiClient apiClient = new ApiClient();
                HttpResponseMessage response = await apiClient.Execute(HttpMethod.Post, "https://td-api.julienmialon.com/auth/register", 
                    new RegisterRequest() { Email = Mail, FirstName = Prenom, LastName = Nom, Password = Mdp });
                Response<LoginResult> result = await apiClient.ReadFromResponse<Response<LoginResult>>(response);
                if (result.IsSuccess)
                {
                    MessageErreur = "Le compte a bien été créé";
                }
            }
            else
            {
                MessageErreur = "Les mots de passe sont différents !";
            }
            
        }
    }
}
