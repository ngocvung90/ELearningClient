using ElearningClient.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElearningClient.ViewModel
{
    public class ViewModelHost
    {
        MainViewModel _mainviewVM;
        MainViewDetailVM _detailVM;
        private static ViewModelHost gThis;
        public  MainViewModel GetMainViewModel()
        {
            if (_mainviewVM == null)
                _mainviewVM = new MainViewModel();
            return _mainviewVM;
        }

        public MainViewDetailVM GetDetailViewModel(MainViewDetail view = null)
        {
            if (_detailVM == null) _detailVM = new MainViewDetailVM(view);
            return _detailVM;
        }
        public static ViewModelHost AfxGetViewModelHost()
        {
            if (gThis == null) gThis = new ViewModelHost();
            return gThis;
        }
    }
}
