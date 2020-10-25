﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Core;
using Windows.UI.Xaml.Controls;

namespace TravelListApp.Services.Navigation
{
    public static class Navigation
    {
        private static Frame _frame;
        private static readonly EventHandler<BackRequestedEventArgs> _goBackHandler = (s, e) => Navigation.GoBack();

        public static Frame Frame
        {
            get { return _frame; }
            set { _frame = value; }
        }

        public static bool Navigate(Type sourcePageType)
        {
            if (_frame.CurrentSourcePageType == sourcePageType)
            {
                return true;
            }

            return _frame.Navigate(sourcePageType);
        }

        public static bool Navigate(Type sourcePageType, object objectId)
        {
            if (_frame.CurrentSourcePageType == sourcePageType)
            {
                return true;
            }

            return _frame.Navigate(sourcePageType, objectId);
        }

        public static void EnableBackButton()
        {
            var navManager = SystemNavigationManager.GetForCurrentView();
            navManager.AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;
            navManager.BackRequested -= _goBackHandler;
            navManager.BackRequested += _goBackHandler;
        }

        public static void DisableBackButton()
        {
            var navManager = SystemNavigationManager.GetForCurrentView();
            navManager.AppViewBackButtonVisibility = AppViewBackButtonVisibility.Collapsed;
            navManager.BackRequested -= _goBackHandler;
        }

        public static void GoBack()
        {
            if (_frame.CanGoBack)
            {
                _frame.GoBack();
            }
        }
    }
}