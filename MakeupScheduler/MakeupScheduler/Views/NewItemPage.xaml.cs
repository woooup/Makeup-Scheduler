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
                Date = DateTime.Now.Date,
                StartTime = new TimeSpan(8, 0, 0),
                EndTime = new TimeSpan(9, 0, 0),
                Price = 100,
                Accessories = false,
                


            };
            
            SaveButton.IsVisible = true;

            BindingContext = this;
        }
        public NewItemPage(Item item)
        {
            InitializeComponent();
            Item = item;
            this.Title = item.Name + ", " + item.Date.ToShortDateString();

            EditButton.IsVisible = true;
            DeleteButton.IsVisible = true;
            BindingContext = this;
        }
        async void Save_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Send(this, "AddItem", Item);
            await Navigation.PopModalAsync();
        }

        async void Edit_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Send(this, "EditItem", Item);
            await Navigation.PopModalAsync();
        }

        async void Delete_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Send(this, "DeleteItem", Item);
            await Navigation.PopModalAsync();
        }
    }
}