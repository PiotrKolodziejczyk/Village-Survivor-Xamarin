using App10.Model;
using App10.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App10
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RankingPage : ContentPage
    {
        List<BestTimes> listTimes;
        public RankingPage()
        {
            InitializeComponent();
            BindingContext = new RankingViewModel();
            
        }

        private async Task GetData()
        {
            
            listTimes = new List<BestTimes>();
           
            listTimes = await App.LocalDB.GetTimesAsync<BestTimes>();
            List.ItemsSource= listTimes.OrderByDescending(x => x.TimeSp);
        }

       private async void Delete_Ranking(object sender, EventArgs e)
        {
            await App.LocalDB.DeleteItemsAsync<BestTimes>();
            await Navigation.PopAsync();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await GetData();
        }
    }
}