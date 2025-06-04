using System.Transactions;
using UnityEngine;

namespace Utils
{
    public static class MyLogger
    {
        static bool _loggingEnabled;
        
        public static void Log(object message)
        {
            if(_loggingEnabled)
                Debug.Log(message);
        }

        public static void EnableLogger() => _loggingEnabled = true;
        public static void DisableLogger() => _loggingEnabled = false;
    }
}