using System;

namespace WPF_client.Utilities.WPF.Commands
{
    /// <summary> Атрибут, который позволяет через рефлекшен привязать методы к командам </summary>
    public class MapCommandAttribute : Attribute
    {
        /// <summary> Имя функции, которая реализует команду </summary>
        public string CommandMethodName { get; private set; }

        /// <summary> Задать соответствие команды методу </summary>
        /// <param name="commandMethodName">Имя метода, который будет вызываться командой</param>
        public MapCommandAttribute(string commandMethodName)
        {
            CommandMethodName = commandMethodName;
        }
    }
}