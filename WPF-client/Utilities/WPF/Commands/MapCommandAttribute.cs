using System;

namespace WPF_client.Utilities.WPF.Commands
{
    public class MapCommandAttribute : Attribute
    {
        public string CommandMethodName { get; private set; }

        public MapCommandAttribute(string commandMethodName)
        {
            CommandMethodName = commandMethodName;
        }
    }
}