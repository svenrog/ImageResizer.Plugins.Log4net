using log4net;
using log4net.Core;
using System;
using System.Globalization;
using ILogger = ImageResizer.Configuration.Logging.ILogger;

namespace ImageResizer.Plugins.Log4net
{
    public class Log4netWrapper : ILogger
    {
        private readonly string _name;
        private readonly ILog _log;
        private readonly LevelMap _levelMap;

        public Log4netWrapper(string loggerName)
        {
            _name = loggerName; 
            _log = LogManager.GetLogger(loggerName);
            _levelMap = LogManager.GetRepository().LevelMap;
        }

        public bool IsTraceEnabled => IsEnabled("Trace");

        public bool IsDebugEnabled => _log.IsDebugEnabled;

        public bool IsInfoEnabled => _log.IsInfoEnabled;

        public bool IsWarnEnabled => _log.IsWarnEnabled;

        public bool IsErrorEnabled => _log.IsErrorEnabled;

        public bool IsFatalEnabled => _log.IsErrorEnabled;

        public string LoggerName
        {
            get => _name;
            set => throw new NotSupportedException("Create a new instance of Log4netWrapper instead.");
        }

        public void Debug(string message)
        {
            _log.Debug(message);
        }

        public void Debug(string message, params object[] args)
        {
            _log.DebugFormat(message, args);
        }

        public void Info(string message)
        {
            _log.Info(message);
        }

        public void Info(string message, params object[] args)
        {
            _log.InfoFormat(message, args);
        }

        public void Warn(string message)
        {
            _log.Warn(message);
        }

        public void Warn(string message, params object[] args)
        {
            _log.WarnFormat(message, args);
        }

        public void Error(string message)
        {
            _log.Error(message);
        }

        public void Error(string message, params object[] args)
        {
            _log.ErrorFormat(message, args);
        }

        public void Fatal(string message)
        {
            _log.Fatal(message);
        }

        public void Fatal(string message, params object[] args)
        {
            _log.FatalFormat(message, args);
        }
        
        public bool IsEnabled(string level)
        {
            var mappedLevel = _levelMap[level];

            return _log.Logger.IsEnabledFor(mappedLevel);
        }

        public void Log(string level, string message)
        {
            var mappedLevel = _levelMap[level];

            _log.Logger.Log(typeof(Log4netWrapper), mappedLevel, message, null);
        }

        public void Trace(string message)
        {
            Log("Trace", message);
        }

        public void Trace(string message, params object[] args)
        {
            Log("Trace", string.Format(CultureInfo.InvariantCulture, message, args));
        }        
    }
}
