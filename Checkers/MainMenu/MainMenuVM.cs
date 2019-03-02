using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Checkers
{
    internal class MainMenuVM : BaseVM
    {
        private string startButtonText = "Start";
        public string StartButtonText
        {
            get => startButtonText;
            private set
            {
                startButtonText = value;
                OnPropertyChanged();
            }
        }

        private ICommand startCommand;
        public ICommand StartCommand { get => startCommand ?? (startCommand = new RelayCommand(() => Start())); }

        private void Start()
        {
            StartButtonText = "Started";
            var board = new BoardWindow();
            board.Show();
        }
    }
}
