using ImageResizer.Configuration;
using ImageResizer.Configuration.Issues;
using ImageResizer.Configuration.Xml;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace ImageResizer.Plugins.Log4net.Tests
{
    [TestClass]
    public class Log4netPluginTests
    {
        [TestMethod]
        public void CanLogFromConfig()
        {
            DeleteExistingLogFile();

            var plugin = GetPlugin();
            var config = Config.Current;

            var originalConfig = config.getConfigXml();
            var issueSink = new IssueSink(null);
            var currentConfig = "<resizer><log4net configFile=\"resources/pluginconfig.xml\" /></resizer>";

            config.setConfigXml(Node.FromXmlFragment(currentConfig, issueSink));            
            plugin.Install(config);
            config.setConfigXml(originalConfig);

            var logger = plugin.GetLogger("Testlogger");
            var testMessage = "Logmessage";

            logger.Info(testMessage);

            var logFile = GetLogFile();

            Assert.IsTrue(logFile.Exists);
            Assert.IsTrue(logFile.Length > 0);

            var logFileContents = File.ReadAllText(logFile.FullName);

            Assert.IsTrue(logFileContents.Contains(testMessage));
        }

        private void DeleteExistingLogFile()
        {
            var logfile = GetLogFile();

            if (logfile.Exists)
                logfile.Delete();
        }

        private FileInfo GetLogFile()
        {
            return new FileInfo("plugin.log");
        }

        private Log4netPlugin GetPlugin()
        {
            return new Log4netPlugin();
        }
    }
}
