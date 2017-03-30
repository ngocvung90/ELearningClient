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
using ElearningClient.Interface;

namespace ElearningClient.ViewModel
{
    public class MainViewDetailVM:ViewModelBase
    {
        static MainViewDetail _view;
        static ClassRoom classRoom;
        static int currentPage = 0;
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
            DependencyService.Get<IAudio>().PlayAudioFile("count.mp3");

            timer.initTimer(1000, timerElapsed, true);
            timer.startTimer();
        }
        public static void timerElapsed(object o, EventArgs e)
        {
            currentPage++;
            Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
            {
                string goToPage = String.Format("goToPage({0})", currentPage);
                _view.GetPdfWebView().Eval(goToPage);
            });
        }
    }
}
