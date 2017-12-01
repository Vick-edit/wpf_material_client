using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WPF_client.Utilities.WPF.NotifyPropertyChanged
{
    public abstract class BaseNotifyPropertyChanged : INotifyPropertyChanged
    {
        private readonly Dictionary<string, object> _valueStore = new Dictionary<string, object>();


        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            if (string.IsNullOrEmpty(propertyName))
                throw new ArgumentNullException(nameof(propertyName));

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected Action<PropertyChangedEventArgs> RaisePropertyChanged()
        {
            return args => PropertyChanged?.Invoke(this, args);
        }


        protected T Get<T>([CallerMemberName]string propertyName = null)
        {
            if (string.IsNullOrEmpty(propertyName))
                throw new ArgumentNullException(nameof(propertyName));

            object value = null;
            if (!_valueStore.TryGetValue(propertyName, out value))
                throw new ArgumentNullException(propertyName);

            return (T)value;
        }

        protected void Set<T>(T value, [CallerMemberName]string propertyName = null)
        {
            if (string.IsNullOrEmpty(propertyName))
                throw new ArgumentNullException(nameof(propertyName));

            _valueStore[propertyName] = value;
            OnPropertyChanged(propertyName);
        }
    }
}