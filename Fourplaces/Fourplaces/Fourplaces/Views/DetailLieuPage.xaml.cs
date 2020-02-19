using Storm.Mvvm.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Fourplaces.ViewModels;

namespace Fourplaces.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetailLieuPage : BaseContentPage
    {
        public DetailLieuPage()
        {
            BindingContext = new DetailLieuViewModel();
            InitializeComponent();
        }
    }
}