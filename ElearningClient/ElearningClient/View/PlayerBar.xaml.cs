using ElearningClient.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace ElearningClient.View
{
    public partial class PlayerBar : Frame
    {
        public PlayerBar()
        {
            InitializeComponent();
            //BindingContext = ViewModelHost.AfxGetViewModelHost().GetDetailViewModel();
        }
        void OnimgPrevTapped(object sender, EventArgs e)
        {
            ViewModelHost.AfxGetViewModelHost().GetDetailViewModel().DoPrevAction();
        }
        void OnimgNextTapped(object sender, EventArgs e)
        {
            ViewModelHost.AfxGetViewModelHost().GetDetailViewModel().DoNextAction();
        }
    }
}
