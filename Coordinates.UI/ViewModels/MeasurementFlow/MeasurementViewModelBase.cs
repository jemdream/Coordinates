using System.Windows.Input;
using Coordinates.Measurements;
using Coordinates.UI.Messages;
using Coordinates.UI.ViewModels.Interfaces;
using Prism.Events;
using Template10.Mvvm;

namespace Coordinates.UI.ViewModels.MeasurementFlow
{
    public abstract class MeasurementViewModelBase : ViewModelBase, IMeasurementViewModelBase
    {
        protected readonly IEventAggregator EventAggregator;
        protected IMeasurementManager MeasurementManager;

        private DelegateCommand _goBackCommand;
        private DelegateCommand _goNextCommand;

        protected MeasurementViewModelBase(IEventAggregator eventAggregator, IMeasurementManager measurementManager)
        {
            EventAggregator = eventAggregator;
            MeasurementManager = measurementManager;
        }

        public abstract string Title { get; }

        protected virtual void OnPrevious() { }
        protected virtual bool CanOnPrevious() => true;

        protected virtual void OnNext() { }
        protected virtual bool CanOnNext() => true;

        protected void UpdateCommands()
        {
            _goBackCommand.RaiseCanExecuteChanged();
            _goNextCommand.RaiseCanExecuteChanged();
        }

        public ICommand GoBackCommand => _goBackCommand ?? (_goBackCommand = new DelegateCommand(() =>
        {
            OnPrevious();

            EventAggregator
                .GetEvent<GoBackMeasurementMsg>()
                .Publish(GetType());
        }, CanOnPrevious));

        public ICommand GoNextCommand => _goNextCommand ?? (_goNextCommand = new DelegateCommand(() =>
        {
            OnNext();

            EventAggregator
                .GetEvent<GoNextMeasurementMsg>()
                .Publish(GetType());
        }, CanOnNext));
    }
}
