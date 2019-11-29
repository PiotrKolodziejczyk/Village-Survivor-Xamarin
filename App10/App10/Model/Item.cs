using App10.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;
using Xamarin.Forms;

namespace App10.Model
{
    class Item: BaseViewModel
    {
        public string ImageUrl { get; set; }
        public string Name { get; set; }

        public int DefaultTime { get; set; }

        int time;
        public int Time
        {
            get
            {
                return time;
            }


            set
            {
                if (time != value)
                {
                    time = value;
                    RaisePropertyChanged(nameof(Time));

                }
            }
        }
        int count;
        public int Count {
            get
            {
                return count;
            }


            set
            {
                if (count != value)
                {
                    count = value;
                    RaisePropertyChanged(nameof(Count));

                }
            }
        }

        public Item(string img,string _name,int _time,int _defaultTime)
        {
            ImageUrl = img;
            Name = _name;
            Time = _time;
            Count = 1;
            DefaultTime = _defaultTime;
            IsVisible = true;

        }

        public Timer Timer;

        bool _isVisible;
        public bool IsVisible
        {
            get
            {
                return _isVisible;
            }
            set
            {
                _isVisible = value;
                RaisePropertyChanged(nameof(IsVisible));
            }
        }

        public Command ItemCommand { get; set; }
    }
}
