namespace Pllab
{
    [TestFixture]
    public class LogTests
    {
        [Test]
        public void ConsoleM_ValidMessage_NoException()
        {
            // Arrange
            var log = new Log();
            var message = "Test message";

            // Act
            log.ConsoleM(message);

            // Assert
            Assert.DoesNotThrow(() => log.ConsoleM(message));
        }

        [Test]
        public void WriteLog_ValidMessage_LogFileUpdated()
        {
            // Arrange
            var log = new Log();
            var message = "Test message";
            var logFilePath = "Logs.txt";

            // Act
            log.WriteLog(message);

            // Assert
            Assert.That(File.Exists(logFilePath), Is.True);
            Assert.That(File.ReadAllText(logFilePath), Contains.Substring(message));
        }

        [Test]
        public void Error_ValidMessage_LogFileUpdatedWithError()
        {
            // Arrange
            var log = new Log();
            var message = "Test error message";
            var logFilePath = "Logs.txt";

            // Act
            log.Error(message);

            // Assert
            Assert.That(File.Exists(logFilePath), Is.True);
            Assert.That(File.ReadAllText(logFilePath), Contains.Substring(message));
            Assert.That(File.ReadAllText(logFilePath), Contains.Substring("ERROR:"));
        }
    }
}
