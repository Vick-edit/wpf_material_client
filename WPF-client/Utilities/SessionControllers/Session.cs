using System;
using System.Threading;
using MaterialDesignThemes.Wpf;

namespace WPF_client.Utilities
{
    /// <summary> Сведения о текущей рабочей сессии </summary>
    public class Session
    {
        #region Singleton
        private static volatile Session instance;
        private static readonly object SyncRoot = new Object();

        private Session() { }

        public static Session Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (SyncRoot)
                    {
                        if (instance == null)
                            instance = new Session();
                    }
                }

                return instance;
            }
        }
        #endregion


        /// <summary> Текущий объект прогнозирования </summary>
        public long ActiveForecastObjectId { get; set; }


        private readonly ReaderWriterLockSlim _snackbarLock = new ReaderWriterLockSlim();
        private SnackbarMessageQueue _snackbarMessageQueue;
        /// <summary> Потокобезопасный доступ к очереди всплывающих сообщений </summary>
        public SnackbarMessageQueue SnackbarMessageQueue
        {
            get
            {
                _snackbarLock.EnterReadLock();
                try
                {
                    return _snackbarMessageQueue;
                }
                finally
                {
                    _snackbarLock.ExitReadLock();
                }
            }
            set
            {
                _snackbarLock.EnterWriteLock();
                try
                {
                    _snackbarMessageQueue = value;
                }
                finally
                {
                    _snackbarLock.ExitWriteLock();
                }
            }
        }

    }
}