using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WPF_client.Utilities.WPF.NotifyPropertyChanged
{
    /// <summary>
    /// Реализация <see cref="INotifyPropertyChanged"/> через дженериг методы 
    /// <see cref="Get{T}(string)"/> и <see cref="Set{T}(T, string)"/>
    /// а так же традиционный метод <see cref="OnPropertyChanged(string)"/>
    /// </summary>
    public abstract class BaseNotifyPropertyChanged : INotifyPropertyChanged
    {
        /// <summary> Контейнер всех значений всех свойств для которых необходимо вызывать OnPropertyChanged </summary>
        private readonly Dictionary<string, object> _valueStore = new Dictionary<string, object>();


        #region INotifyPropertyChanged classic members
        /// <summary> Событие, позволяющее подписать на изменение свойства </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary> Обертка оповещения подписантов в Action </summary>
        /// <returns>Экшен, который вызывает поповещение подписантов на событие</returns>
        protected Action<PropertyChangedEventArgs> RaisePropertyChanged()
        {
            return args => PropertyChanged?.Invoke(this, args);
        }

        /// <summary> Метод, вызывающий оповещение об изменении свойства </summary>
        /// <param name="propertyName"></param>
        protected void OnPropertyChanged(string propertyName)
        {
            if (string.IsNullOrEmpty(propertyName))
                throw new ArgumentNullException(nameof(propertyName));

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion


        #region INotifyPropertyChanged через контейнер свойств
        /// <summary> Задает вызывать сообщение об ошибке, если происходит попытка получить доступ к непроинициализированному свойству, по умолчанию - да </summary>
        protected bool RaiseNotInitializedException = true;

        /// <summary> Получить данные свойства </summary>
        /// <typeparam name="T">Тип данныз свойства</typeparam>
        /// <param name="propertyName">Имя свойства, вызвавшего этот метод</param>
        /// <returns>Значение своства</returns>
        protected T Get<T>([CallerMemberName]string propertyName = null)
        {
            if (string.IsNullOrEmpty(propertyName))
                throw new ArgumentNullException(nameof(propertyName));

            object value = null;
            if (!_valueStore.TryGetValue(propertyName, out value))
                if (RaiseNotInitializedException)
                    throw new ArgumentNullException(propertyName);
                else
                    value = default(T);


            return (T)value;
        }

        /// <summary> Сохранить значение свойства в контейнер и вызвать OnPropertyChanged </summary>
        /// <typeparam name="T">Тип данныз свойства</typeparam>
        /// <param name="value">Новое значение свойства</param>
        /// <param name="propertyName">Имя свойства, чье значение изменяется</param>
        protected void Set<T>(T value, [CallerMemberName]string propertyName = null)
        {
            if (string.IsNullOrEmpty(propertyName))
                throw new ArgumentNullException(nameof(propertyName));

            _valueStore[propertyName] = value;
            OnPropertyChanged(propertyName);
        } 
        #endregion
    }
}