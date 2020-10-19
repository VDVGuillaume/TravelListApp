﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelListApp.Mvvm;

namespace TravelListApp.Mvvm
{
    internal class ViewModelBase : BindableBase
    {
        private static readonly ObservableCollection<MenuItem> AppMenu = new ObservableCollection<MenuItem>();
        private static readonly ObservableCollection<MenuItem> AppSecondMenu = new ObservableCollection<MenuItem>();

        public ViewModelBase()
        { }

        public ObservableCollection<MenuItem> Menu => AppMenu;

        public ObservableCollection<MenuItem> SecondMenu => AppSecondMenu;
    }
}
