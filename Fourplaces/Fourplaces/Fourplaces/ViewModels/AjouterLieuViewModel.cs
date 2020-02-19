using Fourplaces.DTOs;
using Fourplaces.Services;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Plugin.Permissions;
using Storm.Mvvm;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using static Android.Manifest;

namespace Fourplaces.ViewModels
{
    class AjouterLieuViewModel : ViewModelBase
    {
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

        private double _latitude;
        public double Latitude
        {
            get => _latitude;
            set => SetProperty(ref _latitude, value);
        }

        private double _longitude;
        public double Longitude
        {
            get => _longitude;
            set => SetProperty(ref _longitude, value);
        }

        private string _imageSource;
        public string SourceImage
        {
            get => _imageSource;
            set => SetProperty(ref _imageSource, value);
        }

        public ICommand PositionActuelle { get; }
        public ICommand OuvrirPhoto { get; }
        public ICommand PrendrePhoto { get; }
        public ICommand AjouterLieu { get; }

        public AjouterLieuViewModel()
        {
            PositionActuelle = new Command(PositionActuelleAction);
            OuvrirPhoto = new Command(OuvrirPhotoAction);
            PrendrePhoto = new Command(PrendrePhotoAction);
            AjouterLieu = new Command(AjouterLieuAction);
        }


        private async void PositionActuelleAction()
        {
            var location = await Geolocation.GetLastKnownLocationAsync();
            Latitude = location.Latitude;
            Longitude = location.Longitude;
        }

        private async void OuvrirPhotoAction()
        {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                await Application.Current.MainPage.DisplayAlert("No Camera", ":( No camera available.", "OK");
                return;
            }
            var mediaOptions = new PickMediaOptions()
            {
                PhotoSize = PhotoSize.Medium
            };
            var selectedImageFile = await CrossMedia.Current.PickPhotoAsync(mediaOptions);
            SourceImage = selectedImageFile.Path;
        }

        private async void PrendrePhotoAction()
        {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsTakePhotoSupported || !CrossMedia.Current.IsCameraAvailable)
            {
                await Application.Current.MainPage.DisplayAlert("No Camera", ":( No camera available.", "OK");
                return;
            }
            var mediaOptions = new StoreCameraMediaOptions()
            {
                PhotoSize = PhotoSize.Small
            };
            var selectedImageFile = await CrossMedia.Current.TakePhotoAsync(mediaOptions);
            SourceImage = selectedImageFile.Path;
            Console.WriteLine("Le path de la photo : " + SourceImage);
        }

        private async void AjouterLieuAction()
        {
            ApiClient apiClient = new ApiClient();
            HttpResponseMessage response = await apiClient.UploadImage(SourceImage);
            Response<ImageItem> result = await apiClient.ReadFromResponse<Response<ImageItem>>(response);

            Int32 imageId = result.Data.Id;
            Console.WriteLine("Id de l'image upload : " + imageId);

            HttpResponseMessage response1 = await apiClient.Execute(HttpMethod.Post, "https://td-api.julienmialon.com/places", new CreatePlaceRequest() { Title = this.Title, Description = this.Description, ImageId = imageId, Latitude = this.Latitude, Longitude = this.Longitude }, token: App.Access_token);
            Response result1 = await apiClient.ReadFromResponse<Response>(response1);

            if (result1.IsSuccess)
            {
                Console.WriteLine("Le lieu à bien été créé ! ");
            }
            else
            {
                Console.WriteLine("Pas de po");
            }
        }

    }
}
