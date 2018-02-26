using System;

namespace WPF_client.Utilities.Formaters
{
    public class FormaterDay : IFormater
    {
        public string DateFormatter(double timeInSeconds)
        {
            return new DateTime((long)timeInSeconds).ToString("ddd, dd MMM yy");
        }

        public string SimpleDateFormatter(double timeInSeconds)
        {
            return new DateTime((long)timeInSeconds).ToString("dd MMM yy");
        }
    }
}