
using App10.View;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Xamarin.Forms;

namespace App10.ViewModels
{
    class StartPageViewModel
    {
        public Command StartGame { get; set; }
        public Command Ranking { get; set; }
        public Command Instruction { get; set; }
        public Command Exit { get; set; }
        public StartPageViewModel()
        {
            StartGame = new Command(() => App.Current.MainPage.Navigation.PushAsync(new GamePage()));
            Ranking = new Command(() => App.Current.MainPage.Navigation.PushAsync(new RankingPage()));
            Instruction = new Command(() => App.Current.MainPage.Navigation.PushAsync(new InstructionPage()));
            Exit = new Command(() => Thread.CurrentThread.Abort());
          

        }
    }
}
