using ElearningClient.ViewModel;
using Sockets.Plugin;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            InitializeComponent();
            _vm = new MainViewModel();
            BindingContext = _vm;
            //pdfWebView.Source = "https://www.google.com.vn";
        }
    }
}
