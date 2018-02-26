namespace WPF_client.Utilities.Formaters
{
    /// <summary> Временный интерфейс форматирования дат </summary>
    public interface IFormater
    {
        /// <summary> Базовый форматер, переводит дату-время из числа секунд в строку </summary>
        /// <param name="timeInSeconds">Время в секундах</param>
        /// <returns>Строка с форматированным времением</returns>
        string DateFormatter(double timeInSeconds);

        /// <summary> Упрощенный форматер, переводит дату-время из числа секунд в строку </summary>
        /// <param name="timeInSeconds">Время в секундах</param>
        /// <returns>Строка с форматированным времением</returns>
        string SimpleDateFormatter(double timeInSeconds);

    }
}