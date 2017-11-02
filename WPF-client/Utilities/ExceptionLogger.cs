using System;
using System.IO;

namespace WPF_client.Utilities
{
    //Пока это статический класс, потому что так удобнее работать, но если возникнет можно инкапсулировать 
    //возможность логирования различными способами, а в качестве параметра принимать объект, реализующий интерфейс логирования.
    //Либо можно будет заменить все на фабрику. Но пока достаточно такой реализации "В лоб"
    /// <summary> Класс для потокобезопасного логирования ошибок. </summary>
    public static class ExceptionLogger
    {
        private static readonly object _syncRoot = new Object();
        private static int _maxFileSize = 1024 * 1024; // 1MB
        private static string _logFileName = "_log.txt";
        private static string _logFolder = "_ArchiveLogFolder_";


        /// <summary> Потокобезопасное логирование ошибки в текстовый файл </summary>
        /// <param name="e">Возникшая ошибка</param>
        /// <param name="additionalInfo">Дополнительные сведения</param>
        public static void Log(Exception e, string additionalInfo = null)
        {
            lock (_syncRoot)
            {
                try
                {
                    CheckFile();

                    using (var writer = File.AppendText(_logFileName))
                    {
                        writer.WriteLine();
                        writer.WriteLine();
                        writer.WriteLine("################################");
                        writer.WriteLine("Date: " + DateTime.Now);
                        if (!string.IsNullOrEmpty(additionalInfo))
                            writer.WriteLine("Дополнительная информация: " + additionalInfo);

                        var exception = e;
                        while (exception != null)
                        {
                            writer.WriteLine();
                            writer.WriteLine(exception.Message);
                            writer.WriteLine(exception.Source);
                            writer.WriteLine(exception.StackTrace);
                            exception = exception.InnerException;
                        }
                        writer.WriteLine("################################");
                    }
                }
                catch (Exception) {/*Нужно, чтобы не вылетело приложение*/}
            }
        }

        //Проверяем, что существует файл, в который можно записать лог
        private static void CheckFile()
        {
            //Если файла нет - создаём
            if (!File.Exists(_logFileName))
                File.Create(_logFileName).Close();

            //Если файл слишком вырос, то переносим в архив
            var fileSize = new FileInfo(_logFileName).Length;
            if (fileSize > _maxFileSize)
            {
                //Проверяем, что директория существует
                if (!Directory.Exists(_logFolder))
                    Directory.CreateDirectory(_logFolder);

                //Получаем информацию для нового названия файла
                int count = Directory.GetFiles(_logFolder).Length + 1;
                string name = Path.GetFileNameWithoutExtension(_logFileName);
                string ext = Path.GetExtension(_logFileName);

                //Удаляем чёрточку в имени файла
                name = name?.TrimStart('_');

                //Перемещаем старый и создаём новый лог
                File.Move(_logFileName, string.Format("{0}\\{1}-{3}{2}", _logFolder, name, ext, count));
                File.Create(_logFileName).Close();
            }
        }
    }
}