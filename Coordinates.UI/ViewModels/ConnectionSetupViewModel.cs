using System.Windows.Input;
using Template10.Mvvm;

namespace Coordinates.UI.ViewModels
{
    public class ConnectionSetupViewModel : ViewModelBase, IConnectionSetupViewModel
    {
        private string _someTestBinding;
        private ICommand _shuffleCommand;

        public ConnectionSetupViewModel()
        {
        }

        public ICommand ShuffleCommand => _shuffleCommand ?? (_shuffleCommand = new DelegateCommand(() =>
        {
            SomeTestBinding = "Magic Shuffle Invoked.";
        }));
        public string SomeTestBinding
        {
            get { return _someTestBinding; }
            set { Set(ref _someTestBinding, value); }
        }
    }
}