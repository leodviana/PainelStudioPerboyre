using PainelStudioPerboyre.Models;
using PainelStudioPerboyre.Services;
using PainelStudioPerboyre.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Net;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using Xamarin.Forms;

namespace PainelStudioPerboyre.Views
{
    public partial class Imagens : ContentPage
    {
        [DataContract]
        class ImageList
        {
            [DataMember(Name = "photos")]
            public List<string> Photos = null;
        }
        public Imagens()
        {
            InitializeComponent();
            
        }

        private void ListViewExames_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            ListViewExames.SelectedItem = null;
        }
    }
}

