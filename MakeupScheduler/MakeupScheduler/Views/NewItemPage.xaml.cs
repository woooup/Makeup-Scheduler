using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using MakeupScheduler.Models;

namespace MakeupScheduler.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewItemPage : ContentPage
    {
        public Item Item { get; set; }

        public NewItemPage()
        {
            InitializeComponent();

            Item = new Item
            {
                Name = "Item name",
                Date = DateTime.Now,
                StartTime = DateTime.Now.TimeOfDay,
                EndTime = DateTime.Now.TimeOfDay,
                Accessories = false,
                Price = 1000
            };

            BindingContext = this;
        }

        async void Save_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Send(this, "AddItem", Item);
            await Navigation.PopModalAsync();
        }
    }
}