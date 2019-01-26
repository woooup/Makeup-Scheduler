using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using MakeupScheduler.Models;
using MakeupScheduler.ViewModels;

namespace MakeupScheduler.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ItemDetailPage : ContentPage
    {
        ItemDetailViewModel viewModel;

        public ItemDetailPage(ItemDetailViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = this.viewModel = viewModel;
        }

        public ItemDetailPage()
        {
            InitializeComponent();

            var item = new Item
            {
                Name = "Item name",
                Date = DateTime.Now,
                StartTime = DateTime.Now.TimeOfDay,
                EndTime = DateTime.Now.TimeOfDay,
                Accessories = false,
                Price = 1000
            };

            viewModel = new ItemDetailViewModel(item);
            BindingContext = viewModel;
        }
    }
}