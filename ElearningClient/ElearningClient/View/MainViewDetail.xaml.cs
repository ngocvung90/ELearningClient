using ElearningClient.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace ElearningClient.View
{
    public partial class MainViewDetail : ContentPage
    {
        public MainViewDetail()
        {
            InitializeComponent();
           
            BindingContext = ViewModelHost.AfxGetViewModelHost().GetDetailViewModel(this); ;
            // Load the HTML file embedded as a resource in the PCL
            var assembly = typeof(MainViewDetail).GetTypeInfo().Assembly;
            var stream = assembly.GetManifestResourceStream("ElearningClient.View.testText.txt");
            pdfWebView.IsVisible = false;
            handWritingView.IsVisible = true;
            int b = 1;
            // pdfWebView.Source = LoadHTMLFileFromResource();
        }

        public WebView GetPdfWebView()
        {
            return pdfWebView;
        }
        private HtmlWebViewSource LoadHTMLFileFromResource()
        {

#if __IOS__
var resourcePrefix = "ElearningClient.pdfviewer.iOS.";
#endif
#if __ANDROID__
var resourcePrefix = "ElearningClient.pdfviewer.Droid.";
#endif
#if WINDOWS_PHONE
var resourcePrefix = "ElearningClient.pdfviewer.WinPhone.";
#endif
            var source = new HtmlWebViewSource();

            // Load the HTML file embedded as a resource in the PCL
            var assembly = typeof(MainViewDetail).GetTypeInfo().Assembly;
            var stream = assembly.GetManifestResourceStream("ElearningClient.pdfviewer." + "index.html");
            using (var reader = new StreamReader(stream))
            {
                source.Html = reader.ReadToEnd();
            }
            return source;
        }
    }
}
