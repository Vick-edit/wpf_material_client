using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WPF_client.Utilities.WPF.NotifyPropertyChanged
{
    /// <summary> Реализация уведомления об изменении поля в WPF через расширение </summary>
    public static class NotifyPropertyChangedExtension
    {
        /// <summary>
        /// Задать новое значение поля и вызывать сообщение о его изменении
        /// </summary>
        /// <typeparam name="TField">Тип свойства, которое необходимо обновить</typeparam>
        /// <param name="instance">Объект, реализующий INotifyPropertyChanged к которому добавляется расширение</param>
        /// <param name="field">Свойство которое необходимо обновить</param>
        /// <param name="newValue">Новое значение свойства</param>
        /// <param name="raise">Метод, вызывающий уведомление об изменении свойства</param>
        /// <param name="propertyName">Имя свойства из которого было вызвано расширение</param>
        public static void ChangeProperty<TField>(this INotifyPropertyChanged instance, ref TField field, TField newValue, Action<PropertyChangedEventArgs> raise, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<TField>.Default.Equals(field, newValue)) return;
            field = newValue;
            raise?.Invoke(new PropertyChangedEventArgs(propertyName));
        }
    }
}