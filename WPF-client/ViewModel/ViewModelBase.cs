﻿using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using WPF_client.Extensions;

namespace WPF_client.ViewModel
{
    /// <summary> Базовый класс - контейнер для всех VM </summary>
    public abstract class ViewModelBase
    {
        protected ViewModelBase()
        {
            MapAllCommands();
        }

        /// <summary> Мапит все команды с соответствующим атрибутом </summary>
        public void MapAllCommands()
        {
            var thisType = this.GetType();
            var propertyType = typeof (ICommand);
            var attrType = typeof (MapCommandAttribute);

            var allPropertiesWithAttr = thisType.GetProperties()
                .Where(x => x.PropertyType == propertyType && Attribute.IsDefined(x, attrType));

            foreach (var propertyInfo in allPropertiesWithAttr)
            {
                var attribute = propertyInfo.GetCustomAttributes(false).OfType<MapCommandAttribute>().FirstOrDefault();
                if (attribute == null)
                    continue;

                var methodForCommand = thisType.GetMethod(attribute.CommandMethodName, BindingFlags.NonPublic | BindingFlags.Instance);
                if (methodForCommand == null)
                    throw new ArgumentException(attribute.CommandMethodName);
                var methodParams = methodForCommand.GetParameters();
                if (methodParams.Length != 1)
                    throw new Exception("Неверное число параметров метода");

                Action<object> action = o => methodForCommand.Invoke(this, new[] {o});
                propertyInfo.SetValue(this, new BaseCommandImplementation(action));
            }
        }
    }
}