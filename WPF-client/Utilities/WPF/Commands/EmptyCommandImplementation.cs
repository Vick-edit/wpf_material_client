using System;
using System.Collections.Generic;
using System.Windows.Input;

namespace WPF_client.Utilities.WPF.Commands
{
    /// <summary> Реализация команд без параметров </summary>
    public class EmptyCommandImplementation : ICommand
    {
        #region Data
        private readonly Action _executeMethod;
        private readonly Func<bool> _canExecuteMethod;

        private bool _isAutomaticRequeryDisabled = false;
        private List<WeakReference> _canExecuteChangedHandlers;
        #endregion

        #region Constructors
        public EmptyCommandImplementation(Action executeMethod) 
            : this(executeMethod, null, false)
        {
        }

        public EmptyCommandImplementation(Action executeMethod, Func<bool> canExecuteMethod) 
            : this(executeMethod, canExecuteMethod, false)
        {
        }

        public EmptyCommandImplementation(Action executeMethod, Func<bool> canExecuteMethod, bool isAutomaticRequeryDisabled)
        {
            if (executeMethod == null)
            {
                throw new ArgumentNullException(nameof(executeMethod));
            }

            _executeMethod = executeMethod;
            _canExecuteMethod = canExecuteMethod;
            _isAutomaticRequeryDisabled = isAutomaticRequeryDisabled;
        }
        #endregion

        #region Public Methods
        /// <summary> Property to enable or disable CommandManager's automatic requery on this command </summary>
        public bool IsAutomaticRequeryDisabled
        {
            get
            {
                return _isAutomaticRequeryDisabled;
            }
            set
            {
                if (_isAutomaticRequeryDisabled != value)
                {
                    if (value)
                    {
                        CommandManagerHelper.RemoveHandlersFromRequerySuggested(_canExecuteChangedHandlers);
                    }
                    else
                    {
                        CommandManagerHelper.AddHandlersToRequerySuggested(_canExecuteChangedHandlers);
                    }
                    _isAutomaticRequeryDisabled = value;
                }
            }
        }

        /// <summary> Raises the CanExecuteChaged event </summary>
        public void RaiseCanExecuteChanged()
        {
            OnCanExecuteChanged();
        }

        /// <summary> Protected virtual method to raise CanExecuteChanged event </summary>
        protected virtual void OnCanExecuteChanged()
        {
            CommandManagerHelper.CallWeakReferenceHandlers(_canExecuteChangedHandlers);
        }
        #endregion

        #region ICommand Members
        public void Execute(object parameter)
        {
            _executeMethod?.Invoke();
        }

        public bool CanExecute(object parameter)
        {
            if (_canExecuteMethod != null)
            {
                return _canExecuteMethod();
            }
            return true;
        }

        public event EventHandler CanExecuteChanged
        {
            add
            {
                if (!_isAutomaticRequeryDisabled)
                {
                    CommandManager.RequerySuggested += value;
                }
                CommandManagerHelper.AddWeakReferenceHandler(ref _canExecuteChangedHandlers, value, 2);
            }
            remove
            {
                if (!_isAutomaticRequeryDisabled)
                {
                    CommandManager.RequerySuggested -= value;
                }
                CommandManagerHelper.RemoveWeakReferenceHandler(_canExecuteChangedHandlers, value);
            }
        }
        #endregion
    }
}