
using App10.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Xamarin.Forms;

namespace App10.ViewModels
{
    class GameViewModel :BaseViewModel
    {
        int ss = 0;
        int mm = 0;
        public Timer aTimer;
        public Item Wood { get; set; }
        public Item Stone { get; set; }
        public Item Steel { get; set; }
        public Item Gold { get; set; }
        public Item Field { get; set; }
        public Item Axe { get; set; }
        public Item Pick { get; set; }
        public Item Hammer { get; set; }
        public Item Sieve { get; set; }
        public Item Peasant { get; set; }
        public Item Orchard { get; set; }
        public Item Butcher { get; set; }
        public Item Sausage { get; set; }
        public Item Bakery { get; set; }
        public Item Bread { get; set; }

        int food;
        public int Food
        {
            get
            {
                return food;
            }
            set
            {
                if (food != value)
                {
                    food = value;
                    
                    RaisePropertyChanged(nameof(Food));
                }
            }
        }
      
        string time;
        public string Time {
            get
            {
                return time;
            }
            set
            {
                time = value;  
                RaisePropertyChanged(nameof(Time));
            }
        }

        string nick;
        public string Nick
        {
            get
            {
                return nick;
            }
            set
            {
                nick = value;
                RaisePropertyChanged(nameof(Nick));
            }
        }
 
        public bool Menu { get; set; }

        private bool nameStackLayout;
        public bool NameStackLayout
        {
            get
            {
                return nameStackLayout;
            }

            set
            {
                nameStackLayout = value;
                RaisePropertyChanged(nameof(NameStackLayout));
            }
        }

        private bool scrollViewVisible;
        public bool ScrollViewVisible {
            get
            {
                return scrollViewVisible;
            }
            set
            {
                scrollViewVisible = value;
                RaisePropertyChanged(nameof(ScrollViewVisible));
            }
        }

        public Command StartGameCommand { get; set; }
        public Command BackToMenu { get; set; }

        public GameViewModel()
        {
            Menu = false;
            Wood = new Item("wood.jpg", "Wood", 3,3);
            Stone = new Item("stone.jpg", "Stone", 5,5);
            Steel = new Item("steel.jpg", "Steel", 10, 10);
            Gold = new Item("gold.jpg", "Gold", 20, 20);
            Field = new Item("field.jpg", "Field", 10,10);
            Axe = new Item("axe.jpg", "Axe", 5, 5);
            Pick = new Item("pick.jpg", "Pick", 7, 7);
            Hammer = new Item("hammer.jpg", "Hammer", 10, 10);
            Sieve = new Item("sieve.jpg", "Sieve", 30,30);
            Peasant = new Item("peasant.jpg", "Peasant", 10, 10);
            Orchard = new Item("orchard.jpg", "Orchard", 20, 20);
            Butcher = new Item("butcher.jpg", "Butcher", 15, 15);
            Sausage = new Item("sausage.jpg", "Sausage", 10, 10);
            Bakery = new Item("bakery.jpg", "Bakery", 20, 20);
            Bread = new Item("bread.jpg", "Bread", 7, 7);


            Wood.ItemCommand= new Command(() => ItemTime(Wood,0));
            Stone.ItemCommand = new Command(() => ItemTime(Stone,0));
            Steel.ItemCommand = new Command(() => ItemTime(Steel,0));
            Gold.ItemCommand = new Command(() => ItemTime(Gold,0));
            Field.ItemCommand = new Command(() => ItemTime(Field,Peasant.Count));
            Axe.ItemCommand = new Command(() => Counting(Axe, Wood, Steel, 5, 1,0));
            Pick.ItemCommand = new Command(() => Counting(Pick, Wood, Steel, 7, 2,0));
            Hammer.ItemCommand = new Command(() => Counting(Hammer, Wood, Steel, 10, 3,0));
            Sieve.ItemCommand = new Command(() => Counting(Sieve, Stone, Steel,20 , 15,0));
            Peasant.ItemCommand = new Command(() => Counting(Peasant, Wood, Steel, 10, 10,0));
            Orchard.ItemCommand = new Command(() => Counting(Orchard,Wood,Wood,10,0,10));
            Butcher.ItemCommand = new Command(() => Counting(Butcher, Stone, Gold,12, 5,0));
            Sausage.ItemCommand = new Command(() => ItemTime(Sausage,Butcher.Count));
            Bakery.ItemCommand = new Command(() => Counting(Bakery, Stone, Gold, 15, 10,0));
            Bread.ItemCommand = new Command(() => ItemTime(Bread,Bakery.Count));
            StartGameCommand = new Command(() => StartGame());
            BackToMenu = new Command(() => ToMenuMethod());
            Food = 100;
            Wood.Count--;
            Stone.Count--;
            Steel.Count--;
            Gold.Count--;
            Nick = "";
            NameStackLayout = true;
            ScrollViewVisible = false;

            
            

           

        }
        public async void ToMenuMethod()
        {
            Menu = true;
            await Task.Run(() =>
            {
                Device.BeginInvokeOnMainThread(async () => 
                {
                
                await App.Current.MainPage.Navigation.PopAsync();

                }
            );

            });
        }

        public void Counting(Item item,Item one,Item two,int count,int count2,int food)
        {
            if (one.Count >= count && two.Count >= count2)
            {
                one.Count -= count;
                two.Count -= count2;
                ItemTime(item, food);
            }
            
        }
       
        public void ItemTime(Item item ,int addFood)
        {
            
            item.IsVisible = false;
               item.Timer = new Timer(1000);
               item.Timer.Elapsed += (object sender, ElapsedEventArgs e) => {
                    item.Time--;
                    
                    if (item.Time <= 0)
                    {
                       if(addFood>0)
                       {
                          
                           Food += addFood;
                          
                       }
                       else
                       {
                           if (item.Name == "Wood")
                               item.Count += Axe.Count;
                           else if (item.Name == "Stone")
                               item.Count += Pick.Count;
                           else if (item.Name == "Steel")
                               item.Count += Hammer.Count;
                           else if (item.Name == "Gold")
                               item.Count += Sieve.Count;
                           
                           else
                           {
                               item.Count++;
                           }
                       }
                        
                        item.Timer.Stop();
                        item.Time = item.DefaultTime;
                        item.IsVisible = true;
                      
                    }
                };
                item.Timer.Start();
            
        }
       
        private void T_Tick(object sender ,EventArgs e)
        {
            if (Food ==1 )
            {
                if(Menu==false)
                EndGame();

                aTimer.Stop();
            }
            ss++;
            Food--;
            
            if (ss > 59)
              {
                mm++;
                ss = 0;
                
              }

            Time = string.Format("{0:00}:{1:00}", mm, ss);
        }
     
        private async void EndGame()
        {
            
                await Task.Run(() =>
                {
                    Device.BeginInvokeOnMainThread(async () => {
                        await App.Current.MainPage.DisplayAlert("YOU LOSE !", "Your Time is : " + Time,
                                                    "OK");
                        var timeToDataBase = new BestTimes
                        {

                            Times = Time,
                            Name = Nick,
                            TimeSp = new TimeSpan(0, mm, ss)

                        };
                        
                        await App.LocalDB.SaveTimeAsync(timeToDataBase);
                       
                        await App.Current.MainPage.Navigation.PopAsync();
                   }
                );

                });
            
           
           
        }
        private async void StartGame()
        {
            if (Nick.Length >= 3)
            {
                ScrollViewVisible = true;
                aTimer = new Timer(1000);
                aTimer.Elapsed += T_Tick;
                aTimer.Start();
                NameStackLayout = false;
            }
            else
            {
               Device.BeginInvokeOnMainThread(async () =>
                {
                    await App.Current.MainPage.DisplayAlert("Warning", "Write your name ! Minimum 3 Characters ", "OK");
                });
            }
        }

     
    }

   
        
    
}
 