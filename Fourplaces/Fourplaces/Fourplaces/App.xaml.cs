using System;
using Storm.Mvvm;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Fourplaces.Views;

namespace Fourplaces
{
    public partial class App : MvvmApplication
    {
        public static string Access_token { get; set; }
        public static string Refresh_token { get; set; }

        public App() : base(() => new LoginPage())
        {
            InitializeComponent();
        }
    }
}
