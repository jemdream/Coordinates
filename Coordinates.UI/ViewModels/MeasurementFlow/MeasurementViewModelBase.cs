using System.Threading.Tasks;
using System.Windows.Input;
using Coordinates.UI.Messages;
using Coordinates.UI.ViewModels.Interfaces;
using Prism.Events;
using Template10.Mvvm;

namespace Coordinates.UI.ViewModels.MeasurementFlow
{
    public abstract class MeasurementViewModelBase : ViewModelBase, IMeasurementViewModelBase
    {
        protected readonly IEventAggregator EventAggregator;

        private AwaitableDelegateCommand _goBackCommand;
        private AwaitableDelegateCommand _goNextCommand;

        protected MeasurementViewModelBase(IEventAggregator eventAggregator)
        {
            EventAggregator = eventAggregator;
        }

        public abstract string Title { get; }

        protected virtual Task<bool> OnPrevious() => Task.FromResult(true);
        protected virtual bool CanOnPrevious() => true;

        protected virtual Task<bool> OnNext() => Task.FromResult(true);
        protected virtual bool CanOnNext() => true;

        protected void UpdateNavigationCommands()
        {
            ((AwaitableDelegateCommand)GoBackCommand).RaiseCanExecuteChanged();
            ((AwaitableDelegateCommand)GoNextCommand).RaiseCanExecuteChanged();
        }

        public ICommand GoBackCommand => _goBackCommand ?? (_goBackCommand = new AwaitableDelegateCommand(async x =>
        {
            if (await OnPrevious())
                EventAggregator
                    .GetEvent<GoBackMeasurementMsg>()
                    .Publish(GetType());
        }, x => CanOnPrevious()));

        public ICommand GoNextCommand => _goNextCommand ?? (_goNextCommand = new AwaitableDelegateCommand(async x =>
        {
            if (await OnNext())
                EventAggregator
                    .GetEvent<GoNextMeasurementMsg>()
                    .Publish(GetType());
        }, x => CanOnNext()));
    }
}
