using System;

namespace WPF_client.Extensions
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