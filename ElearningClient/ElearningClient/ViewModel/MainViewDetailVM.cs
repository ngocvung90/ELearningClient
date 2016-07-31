using ElearningClient.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElearningClient.ViewModel
{
    public class MainViewDetailVM:ViewModelBase
    {
        MainViewDetail _view;

        public MainViewDetailVM(MainViewDetail view)
        {
            _view = view;
        }
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
            for(int i = 1; i < 10; i ++)
            {
                if(i % 2 != 0) _view.GetPdfWebView().Eval("goPrevious(" + i.ToString() + ")");
            }
        }
    }
}
