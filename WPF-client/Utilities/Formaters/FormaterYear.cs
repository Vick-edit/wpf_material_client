using System;

namespace WPF_client.Utilities.Formaters
{
    public class FormaterYear : IFormater
    {
        public string DateFormatter(double timeInSeconds)
        {
            return new DateTime((long)timeInSeconds).ToString("yyyy");
        }

        public string SimpleDateFormatter(double timeInSeconds)
        {
            return new DateTime((long)timeInSeconds).ToString("yyyy");
        }
    }
}