using Fourplaces.DTOs;
using Fourplaces.Services;
using Fourplaces.Views;
using Storm.Mvvm;
using Storm.Mvvm.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Fourplaces.ViewModels
{
    class ListeLieuxViewModel : ViewModelBase
    {
        private ObservableCollection<PlaceItemSummary> _placeItems;
        public ObservableCollection<PlaceItemSummary> PlaceItems 
        { 
            get => _placeItems; 
            set => SetProperty(ref _placeItems, value); 
        }

        private PlaceItemSummary _placeItemSelected;
        public PlaceItemSummary PlaceItemSelected
        {
            get => _placeItemSelected;
            set
            {
                if (SetProperty(ref _placeItemSelected, value) && value != null)
                {
                    ListView_ItemSelected(value);
                    PlaceItemSelected = null;
                }
            }
        }

        public ICommand GoAjouterLieu { get; }
        public ListeLieuxViewModel()
        {
            GoAjouterLieu = new Command(GoAjouterLieuAction);
            RecupererLieux();
        }

        private async void RecupererLieux()
        {
            ApiClient apiClient = new ApiClient();
            HttpResponseMessage response = await apiClient.Execute(HttpMethod.Get, "https://td-api.julienmialon.com/places");
            Response<ObservableCollection<PlaceItemSummary>> result = await apiClient.ReadFromResponse<Response<ObservableCollection<PlaceItemSummary>>>(response);

            PlaceItems = result.Data;
        }

        private async void ListView_ItemSelected(PlaceItemSummary placeItem)
        {
            INavigationService ins = DependencyService.Get<INavigationService>();
            await ins.PushAsync<DetailLieuPage>(new System.Collections.Generic.Dictionary<string, object>
            {
                {"PlaceItemSummary",placeItem }
            });
        }
        private async void GoAjouterLieuAction()
        {
            INavigationService ins = DependencyService.Get<INavigationService>();
            await ins.PushAsync<AjouterLieuPage>();
        }
    }
}
