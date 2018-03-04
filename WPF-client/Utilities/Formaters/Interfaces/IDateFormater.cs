namespace WPF_client.Utilities.Formaters
{
    /// <summary> Временной интерфейс форматирования дат </summary>
    public interface IDateFormater
    {
        /// <summary> Базовый форматер, переводит дату-время из числа тактов в строку </summary>
        /// <param name="timeInSeconds">Время в тактах</param>
        /// <returns>Строка с форматированным времением</returns>
        string DateFormatter(double timeInSeconds);

        /// <summary> Упрощенный форматер, переводит дату-время из числа тактов в строку </summary>
        /// <param name="timeInSeconds">Время в тактах</param>
        /// <returns>Строка с форматированным времением</returns>
        string SimpleDateFormatter(double timeInSeconds);

    }
}