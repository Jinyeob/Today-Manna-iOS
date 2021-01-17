﻿using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using TodaysManna.ViewModel;
using TodaysManna.AppInterfaces;
using System.Linq;
using UIKit;

namespace TodaysManna
{
    public partial class MannaPage : ContentPage
    {
        readonly MannaViewModel mannaViewModel = new MannaViewModel();
        public MannaPage()
        {
            InitializeComponent();

            BindingContext = mannaViewModel;

            UIApplication.SharedApplication.ApplicationIconBadgeNumber = -1;
        }

        private async void OnShareButtonClicked(object sender, EventArgs e)
        {
            await Share.RequestAsync(new ShareTextRequest
            {
                Text = today.Text + "\n\n" + verse.Text + "\n\n" + mannaViewModel.AllString,
                Title = "공유"
            });
        }

        private async void OnItemSelected(object sender, SelectionChangedEventArgs e)
        {
            var view = sender as CollectionView;
            if (view.SelectedItem == null) return;
            if (e.CurrentSelection.FirstOrDefault() == null) return;

            var manna = e.CurrentSelection.FirstOrDefault() as MannaContent;

            var verseText = verse.Text;
            var tmpRangeString = verseText.Substring(0, verseText.IndexOf(":"));

            var shareRangeString = $"({tmpRangeString}:{manna.Number}){manna.MannaString}";

            await Share.RequestAsync(new ShareTextRequest
            {
                Text = shareRangeString,
                Title = "공유"
            });


            if (view.SelectedItem != null)
            {
                view.SelectedItem = null;
            }
        }

       
        async void Button_Clicked(System.Object sender, System.EventArgs e)
        {
            await Navigation.PushModalAsync(new SettingPage());
        }
    }
}
