using System;
using System.Runtime.CompilerServices;

namespace Logic.Interfaces
{
    /// <summary>
    /// Сервис логгирования
    /// </summary>
    public interface ILoggerService
    {
        void Error(Exception ex, string message = null, [CallerMemberName]string methodName = "", [CallerFilePath]string classPath = "", object valueForLog = null);

        void Warning(string message, [CallerMemberName]string methodName = "", [CallerFilePath]string classPath = "", object valueForLog = null);

        void Info(string message, [CallerMemberName]string methodName = "", [CallerFilePath]string classPath = "", object valueForLog = null);
    }
}