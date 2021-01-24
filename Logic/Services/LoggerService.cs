using System;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using Logic.Interfaces;
using NLog.Web;

namespace Logic.Services
{
    public class LoggerService : ILoggerService
    {
        private static readonly NLog.Logger Logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();

        #region | Public methods |

        public void Error(Exception ex, string message = null, [CallerMemberName]string methodName = "", [CallerFilePath]string classPath = "", object valueForLog = null)
        {
            Logger.Error(ex, $"\n<Method: {methodName} Class: {GetClassNameByPathToFile(classPath)}>\nDevMessage: <{message}>\n", valueForLog);
        }

        public void Error(string message = null, string methodName = "", string classPath = "", object valueForLog = null)
        {
            Logger.Error($"\n<Method: {methodName} Class: {GetClassNameByPathToFile(classPath)}>\nDevMessage: <{message}>\n", valueForLog);
        }

        public void Warning(string message, [CallerMemberName]string methodName = "", [CallerFilePath]string classPath = "", object valueForLog = null)
        {
            Logger.Warn($"\n<Method: {methodName} Class: {GetClassNameByPathToFile(classPath)}>\nDevMessage: <{message}>\n", valueForLog);
        }

        public void Info(string message, [CallerMemberName]string methodName = "", [CallerFilePath]string classPath = "", object valueForLog = null)
        {
            Logger.Info($"\n<Method: {methodName} Class: {GetClassNameByPathToFile(classPath)}>\nDevMessage: <{message}>\n", valueForLog);
        }

        #endregion

        #region | Private methods |

        private static string GetClassNameByPathToFile(string path)
        {
            return Regex.Match(path, @".+\/(.+)\.cs").Groups[1].Value;
        }

        #endregion
    }
}