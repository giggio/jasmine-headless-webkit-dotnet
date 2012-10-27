using Args;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using jasmine_headless_webkit_dotnet;

namespace Tests
{
    [TestClass]
    public class BindsToArrayOfStrings
    {
        private IModelBindingDefinition<Arguments> bindingDefinition;

        [TestInitialize]
        public void Binding()
        {
            bindingDefinition = Args.Configuration.Configure<Arguments>()
                .AsFluent()
                .ParsesArgumentsWith(typeof(string[]), new ArrayOfStringConverter())
                .Initialize();
        }
        [TestMethod]
        public void ConverterBinds()
        {
            bindingDefinition.TypeConverters.Count.Should().Be(1);
            bindingDefinition.TypeConverters.ContainsKey(typeof(string[])).Should().BeTrue();
            bindingDefinition.TypeConverters[typeof(string[])].Should().BeOfType<ArrayOfStringConverter>();
        }

        [TestMethod]
        public void SourceFilesBindsCorrectly()
        {
            var args = new[] { "/SourceFiles", "\"abc,def\"" };
            var argsConverted = bindingDefinition.CreateAndBind(args);
            argsConverted.SourceFiles.Length.Should().Be(2);
        }
        [TestMethod]
        public void TimeoutBindsCorrectly()
        {
            var args = new[] { "/Timeout", "1" };
            var argsConverted = bindingDefinition.CreateAndBind(args);
            argsConverted.Timeout.Should().Be(1);
        }

    }
}