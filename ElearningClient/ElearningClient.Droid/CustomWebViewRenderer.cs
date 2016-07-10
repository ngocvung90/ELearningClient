using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms.Platform.Android;
using ElearningClient.View;
using ElearningClient.Droid;
using Xamarin.Forms;
using System.IO;
using Java.Lang;

[assembly: ExportRenderer(typeof(CustomWebView), typeof(CustomWebViewRenderer))]
namespace ElearningClient.Droid
{
    class CustomWebViewRenderer : WebViewRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<WebView> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null)
            {
                var customWebView = Element as CustomWebView;
                Control.Settings.AllowUniversalAccessFromFileURLs = true;
                Control.Settings.JavaScriptEnabled = true;
                //Control.Settings.BuiltInZoomControls = true;
                Control.SetInitialScale(162);
                string url = string.Format("file:///android_asset/odfviewer/index.html#/sdcard/Android/test.pdf");
                //string url = string.Format("file:///sdcard/Android/pdfjs/web/viewer.html?file=/sdcard/Android/test.pdf");

                string targetfile = "file://" + Android.OS.Environment.DataDirectory.Path;
                Control.LoadUrl(url);
                //Control.EvaluateJavascript("pagesCount()", new JavaScriptHandller());
                //customWebView.Eval("pdfViewProgress(150)");
            }
        }
    }
}