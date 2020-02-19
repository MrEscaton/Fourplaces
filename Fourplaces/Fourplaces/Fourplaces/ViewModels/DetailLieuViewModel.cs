using Fourplaces.DTOs;
using Fourplaces.Services;
using Storm.Mvvm;
using Storm.Mvvm.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Fourplaces.ViewModels
{


    class DetailLieuViewModel : ViewModelBase
    {
        [NavigationParameter("PlaceItemSummary")]
        public PlaceItemSummary PlaceItemSummary { get; set; }

        private string _pageName;
        public string PageName
        {
            get => _pageName;
            set => SetProperty(ref _pageName, value);
        }

        private PlaceItem _currentPlace;
        public PlaceItem CurrentPlace
        {
            get => _currentPlace;
            set => SetProperty(ref _currentPlace, value);
        }

        private Int32 _imageId;
        public Int32 ImageId
        {
            get => _imageId;
            set => SetProperty(ref _imageId, value);
        }

        private string _title;
        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        private string _description;
        public string Description
        {
            get => _description;
            set => SetProperty(ref _description, value);
        }

        private List<CommentItem> _listeCommentaires;
        public List<CommentItem> ListeCommentaires
        {
            get => _listeCommentaires;
            set => SetProperty(ref _listeCommentaires, value);
        }

        public ICommand GOMaps { get; }

        public DetailLieuViewModel()
        {
            GOMaps = new Command(OuvrirMapsAction);
        }

        public override void Initialize(Dictionary<string, object> navigationParameters)
        {
            base.Initialize(navigationParameters);

            if(PlaceItemSummary != null)
            {
                PageName = "Détail";
                RecupererPlaceItem();
            }
            else
            {
                PageName = "Création";
            }

        }



        private async void RecupererPlaceItem()
        {
            ApiClient apiClient = new ApiClient();
            HttpResponseMessage response = await apiClient.Execute(HttpMethod.Get, "https://td-api.julienmialon.com/places/" + PlaceItemSummary.Id);
            Response<PlaceItem> result = await apiClient.ReadFromResponse<Response<PlaceItem>>(response);

            CurrentPlace = result.Data;
            ImageId = CurrentPlace.ImageId;
            Title = CurrentPlace.Title;
            Description = CurrentPlace.Description;
            ListeCommentaires = CurrentPlace.Comments;
        }


        private async void OuvrirMapsAction()
        {
            await Map.OpenAsync(CurrentPlace.Latitude, CurrentPlace.Longitude, new MapLaunchOptions { Name = "Test" });
        }
    }
}
