﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WeatherAppProject.Views.Fragments
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ConnectionView : Grid
    {
        public ConnectionView()
        {
            InitializeComponent();
        }
    }
}