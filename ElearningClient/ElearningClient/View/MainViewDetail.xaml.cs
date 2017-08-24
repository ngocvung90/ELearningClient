using ElearningClient.Model;
using ElearningClient.ViewModel;
using Sockets.Plugin;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace ElearningClient.View
{
    public partial class MainViewDetail : ContentPage
    {
        public lectureModel _lecture;
        #region TCPClient socket variables
        private CancellationTokenSource _canceller;
        private readonly TcpSocketClient _client;
        string ipServer = "192.168.0.102";
        int portServer = 8080;
        #endregion
        public MainViewDetail()
        {
            InitializeComponent();

            BindingContext = ViewModelHost.AfxGetViewModelHost().GetDetailViewModel(this); ;
            pdfWebView.IsVisible = false;
            handWritingView.IsVisible = true;

            _client = new TcpSocketClient();
            ConnectAndWait();
        }

        async void ConnectAndWait()
        {
            await _client.ConnectAsync(ipServer, portServer);
            _canceller = new CancellationTokenSource();

            Task.Factory.StartNew(() =>
            {
                foreach (var msg in _client.ReadStrings(_canceller.Token))
                {
                    //log here
                }
            }, TaskCreationOptions.LongRunning);
        }

        public void SetLecture(lectureModel lecture)
        {
            _lecture = lecture;
            switch (_lecture.lectureType)
            {
                case LECTURE_TYPE.HAND_WRITING:
                    pdfWebView.IsVisible = false;
                    handWritingView.IsVisible = true;
                    break;
                case LECTURE_TYPE.DOCUMENT_VIEW:
                    pdfWebView.IsVisible = true;
                    handWritingView.IsVisible = false;
                    break;
                default:
                    break;
            }

            ViewModelHost.AfxGetViewModelHost().GetDetailViewModel().SetMainViewDetail(this);

        }
        public WebView GetPdfWebView()
        {
            return pdfWebView;
        }

        public HandWrtingPage GetHandWritingView()
        {
            return handWritingView;
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

        protected override bool OnBackButtonPressed()
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                bool res = await DisplayAlert("Exit", "Are you sure ?", "Yes", "No");
                if (res)
                {
                    ViewModelHost.AfxGetViewModelHost().GetDetailViewModel().StopLecture();
                    await this.Navigation.PopAsync();
                }
            });
            return true;
        }
    }
}
