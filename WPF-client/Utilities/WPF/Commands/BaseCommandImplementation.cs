using System;
using System.Collections.Generic;
using System.Windows.Input;

namespace WPF_client.Utilities.WPF.Commands
{
    public class BaseCommandImplementation : ICommand
    {
        #region Data
        private readonly Action<object> _executeMethod;
        private readonly Func<object, bool> _canExecuteMethod;

        private bool _isAutomaticRequeryDisabled = false;
        private List<WeakReference> _canExecuteChangedHandlers;
        #endregion

        #region Constructors
        public BaseCommandImplementation(Action<object> executeMethod) 
            : this(executeMethod, null, false)
        {
        }

        public BaseCommandImplementation(Action<object> executeMethod, Func<object, bool> canExecuteMethod) 
            : this(executeMethod, canExecuteMethod, false)
        {
        }

        public BaseCommandImplementation(Action<object> executeMethod, Func<object, bool> canExecuteMethod, bool isAutomaticRequeryDisabled)
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
            _executeMethod?.Invoke(parameter);
        }

        public bool CanExecute(object parameter)
        {
            if (_canExecuteMethod != null)
            {
                return _canExecuteMethod(parameter);
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