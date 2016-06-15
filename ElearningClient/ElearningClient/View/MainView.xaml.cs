using ElearningClient.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace ElearningClient.View
{
    public partial class MainView : MasterDetailPage
    {
        MainViewModel _vm;
        public MainView()
        {
           // InitializeComponent();
            _vm = new MainViewModel();
            BindingContext = _vm;
            Title = "Jobs";
            Master = new ContentPage
            {
                Content = new StackLayout
                {
                    VerticalOptions = LayoutOptions.Center,
                    Children = {
                        new Label {
                            HorizontalTextAlignment = TextAlignment.Center,
                            Text = "Welcome to Xamarin Forms!"
                        }
                    }
                }
            };
            Master.Title = "Jobs2";

            Detail = new ContentPage
            {
                Content = new StackLayout
                {
                    VerticalOptions = LayoutOptions.Center,
                    Children = {
                        new Label {
                            HorizontalTextAlignment = TextAlignment.Center,
                            Text = "Welcome to Xamarin Forms 2!"
                        }
                    }
                }
            };

        }
    }
}
