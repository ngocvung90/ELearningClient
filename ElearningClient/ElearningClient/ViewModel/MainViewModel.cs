using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElearningClient.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        public MainViewModel()
        {
            MainText = "This is Elearning !";
        }
        private string mainText = "";
        public string MainText
        {
            get { return mainText; }
            set
            {
                mainText = value;
                OnPropertyChanged("MainText");
            }
        }
    }
}
