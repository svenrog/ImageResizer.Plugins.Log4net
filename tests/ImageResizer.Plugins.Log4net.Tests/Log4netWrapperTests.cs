using log4net.Config;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace ImageResizer.Plugins.Log4net.Tests
{
    [TestClass]
    public class Log4netWrapperTests
    {
        [TestMethod]
        public void LogMethodLogsAllLevels()
        {
            var configFile = new FileInfo("resources/wrapperconfig.xml");

            XmlConfigurator.Configure(configFile);

            var logFile = new FileInfo("wrapper.log");
            if (logFile.Exists)
                logFile.Delete();

            var logger = GetLogger();
            var levels = new []
            {
                "TRACE",
                "DEBUG",
                "INFO",
                "WARN",
                "ERROR",
                "FATAL"
            };

            foreach (var level in levels)
            {
                var message = $"{level}-log-message";

                logger.Log(level, message);

                AssertFileExists(logFile);
                AssertFileContains(logFile, message);
            }
        }

        [TestMethod]
        public void LogSpecificLevelMethodsLogsAllLevels()
        {
            var configFile = new FileInfo("resources/wrapperconfig.xml");

            XmlConfigurator.Configure(configFile);

            var logFile = new FileInfo("wrapper.log");
            if (logFile.Exists)
                logFile.Delete();

            var logger = GetLogger();
            var levels = new[]
            {
                "TRACE",
                "DEBUG",
                "INFO",
                "WARN",
                "ERROR",
                "FATAL"
            };

            foreach (var level in levels)
            {
                var message = $"{level}-log-message";

                switch (level)
                {
                    case "TRACE": logger.Trace(message); break;
                    case "DEBUG": logger.Debug(message); break;
                    case "INFO": logger.Info(message); break;
                    case "WARN": logger.Warn(message); break;
                    case "ERROR": logger.Error(message); break;
                    case "FATAL": logger.Fatal(message); break;
                }

                AssertFileExists(logFile);
                AssertFileContains(logFile, message);
            }
        }

        [TestMethod]
        public void CanFilterLevels()
        {
            var configFile = new FileInfo("resources/levelconfig.xml");

            XmlConfigurator.Configure(configFile);

            var logger = GetLogger();

            Assert.IsFalse(logger.IsTraceEnabled);
            Assert.IsFalse(logger.IsDebugEnabled);
            Assert.IsFalse(logger.IsInfoEnabled);
            Assert.IsTrue(logger.IsWarnEnabled);
            Assert.IsTrue(logger.IsErrorEnabled);
            Assert.IsTrue(logger.IsFatalEnabled);

            Assert.IsFalse(logger.IsEnabled("TRACE"));
            Assert.IsFalse(logger.IsEnabled("DEBUG"));
            Assert.IsFalse(logger.IsEnabled("INFO"));
            Assert.IsTrue(logger.IsEnabled("WARN"));
            Assert.IsTrue(logger.IsEnabled("ERROR"));
            Assert.IsTrue(logger.IsEnabled("FATAL"));
        }

        private void AssertFileExists(FileInfo file)
        {
            Assert.IsTrue(file.Exists);
            Assert.IsTrue(file.Length > 0);
        }

        private void AssertFileContains(FileInfo file, string message)
        {
            var logFileContents = File.ReadAllText(file.FullName);

            Assert.IsTrue(logFileContents.Contains(message));
        }

        private Log4netWrapper GetLogger()
        {
            return new Log4netWrapper("Testlogger");
        }
    }
}
