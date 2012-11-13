using System.IO;
using System.Reflection;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using jasmine_headless_webkit_dotnet;

namespace Tests.Integrated.Default
{
    [TestClass]
    public class ConfigFileDoesNotExist
    {
        [TestMethod]
        public void GetsAnExceptionWhenConfigFileDoesNotExist()
        {
            var jasmineTestDir = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "FolderThatDoesNotExist");
            var environmentMock = new Moq.Mock<LocalEnvironment> { CallBase = true };
            environmentMock.Setup(e => e.GetRunDir()).Returns(jasmineTestDir);
            var environment = environmentMock.Object;

            try
            {
                new PhantomJSDefault(environment, environment.GetPhantomJSExeFileLocation(), environment.GetRunJasmineTestFileLocation(), VerbosityLevel.Verbose, 10);
                Assert.Fail("Should had failed as file does not exist");
            }
            catch (JasmineConfigurationFileDoesNotExistException exception)
            {
            }
        }
    }
}
