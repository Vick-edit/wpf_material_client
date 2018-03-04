using System;

namespace WPF_client.Utilities.Formaters
{
    /// <summary> Форматер времени, для возвращения сведений о годе в дате </summary>
    public class FormaterYear : IDateFormater
    {
        /// <summary> Базовый форматер, переводит дату-время из числа тактов в строку сведений о годе в дате </summary>
        /// <param name="timeInSeconds">Время в тактах</param>
        /// <returns>Строка с форматированным времением</returns>
        public string DateFormatter(double timeInSeconds)
        {
            return new DateTime((long)timeInSeconds).ToString("yyyy");
        }

        /// <summary> Упрощенный форматер, переводит дату-время из числа тактов в строку сведений о годе в дате </summary>
        /// <param name="timeInSeconds">Время в тактах</param>
        /// <returns>Строка с форматированным времением</returns>
        public string SimpleDateFormatter(double timeInSeconds)
        {
            return new DateTime((long)timeInSeconds).ToString("yyyy");
        }
    }
}