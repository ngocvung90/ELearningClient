﻿using ElearningClient.ViewModel;
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
    public enum LECTURE_TYPE
    {
        HAND_WRITING = 0,
        DOCUMENT_VIEW
    }
    public partial class MainViewDetail : ContentPage
    {
        public LECTURE_TYPE _type;
        public MainViewDetail()
        {
            InitializeComponent();
           
            BindingContext = ViewModelHost.AfxGetViewModelHost().GetDetailViewModel(this); ;
            pdfWebView.IsVisible = false;
            handWritingView.IsVisible = true;
        }

        public void SetLecture(LECTURE_TYPE type)
        {
            _type = type;
            switch (type)
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
    }
}
