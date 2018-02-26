using System;

namespace WPF_client.Utilities.Formaters
{
    public class FormaterMonth : IFormater
    {
        public string DateFormatter(double timeInSeconds)
        {
            return new DateTime((long)timeInSeconds).ToString("MMMM yyyy");
        }

        public string SimpleDateFormatter(double timeInSeconds)
        {
            return new DateTime((long)timeInSeconds).ToString("MMM yyyy");
        }
    }
}