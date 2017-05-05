using System;
using GalaSoft.MvvmLight;

namespace RRLauncher.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        public MainViewModel()
        {
            Message = "Hello World!";
        }

        private string _message;
        public String Message
        {
            get { return _message; }
            set { Set(ref _message, value); }
        }
    }
}