using AdvancedTimer.Forms.Plugin.Abstractions;
using ElearningClient.Model;
using ElearningClient.View;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Xamarin.Forms;

namespace ElearningClient.ViewModel
{
    public class MainViewDetailVM:ViewModelBase
    {
        static MainViewDetail _view;
        static ClassRoom classRoom;
        public MainViewDetailVM(MainViewDetail view)
        {
            _view = view;
            timer = DependencyService.Get<IAdvancedTimer>();
            //var assembly = typeof(App).GetTypeInfo().Assembly;
            //Stream stream = assembly.GetManifestResourceStream("ElearningClient.testText.txt");

            //var serializer = new XmlSerializer(typeof(ClassRoom));
            //var questionData = serializer.Deserialize(stream);
        }

        static IAdvancedTimer timer;

        private RelayCommand _webAction;
        private RelayCommand _onimgPrevTapped;
        private RelayCommand _onimgNextTapped;
        public RelayCommand WebAction
        {
            get
            {
                if (_webAction == null) _webAction = new RelayCommand(DoWebAction);
                return _webAction;
            }
            set
            {
                _webAction = value;
                OnPropertyChanged("WebAction");
            }
        }
        //public RelayCommand OnimgNextTapped
        //{
        //    get { if (_onimgNextTapped == null) _onimgNextTapped = new RelayCommand(DoNextAction); return _onimgNextTapped; }
        //    set { _onimgNextTapped = value; OnPropertyChanged("OnimgNextTapped"); }
        //}

        //public RelayCommand OnimgPrevTapped
        //{
        //    get { if (_onimgPrevTapped == null) _onimgPrevTapped = new RelayCommand(DoPrevAction); return _onimgPrevTapped; }
        //    set { _onimgPrevTapped = value; OnPropertyChanged("OnimgPrevTapped"); }
        //}

        void DoWebAction()
        {
            _view.GetPdfWebView().Eval("goNext()");
        }
        public void DoNextAction()
        {
            _view.GetPdfWebView().Eval("goNext()");
        }
        public void DoPrevAction()
        {
            _view.GetPdfWebView().Eval("goPrevious()");
        }

        public void DoPlayAction()
        {
            timer.initTimer(1000, timerElapsed, false);
            timer.startTimer();
        }
        public static void timerElapsed(object o, EventArgs e)
        {
            Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
            {
                _view.GetPdfWebView().Eval("goToPage(1)");
            });
        }
    }
}
