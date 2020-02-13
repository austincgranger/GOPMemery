﻿using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using GOPMemery.Services;
using GOPMemery.Models;
using System.Diagnostics;
using GOPMemery.Views;

namespace GOPMemery
{
    public partial class App : Application
    {
        public static Repo Repository;
        public App(string dbPath)
        {
            InitializeComponent();

            Repository = new Repo(dbPath);

            Debug.WriteLine($"Database located at: {dbPath}");
            MainPage = new LoginPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
