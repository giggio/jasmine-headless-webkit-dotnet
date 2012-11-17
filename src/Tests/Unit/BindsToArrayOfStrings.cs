using Args;
using FluentAssertions;
using NUnit.Framework;
using jasmine_headless_webkit_dotnet;

namespace Tests.Unit
{
    [TestFixture]
    public class BindsToArrayOfStrings
    {
        private static IModelBindingDefinition<Arguments> bindingDefinition;

        [TestFixtureSetUp]
        public static void Binding()
        {
            bindingDefinition = Args.Configuration.Configure<Arguments>()
                .AsFluent()
                .ParsesArgumentsWith(typeof(string[]), new ArrayOfStringConverter())
                .Initialize();
        }
        [Test]
        public void ConverterBinds()
        {
            bindingDefinition.TypeConverters.Count.Should().Be(1);
            bindingDefinition.TypeConverters.ContainsKey(typeof(string[])).Should().BeTrue();
            bindingDefinition.TypeConverters[typeof(string[])].Should().BeOfType<ArrayOfStringConverter>();
        }

        [Test]
        public void SourceFilesBindsCorrectly()
        {
            var args = new[] { "/SourceFiles", "\"abc,def\"" };
            var argsConverted = bindingDefinition.CreateAndBind(args);
            argsConverted.SourceFiles.Length.Should().Be(2);
        }
        [Test]
        public void TimeoutBindsCorrectly()
        {
            var args = new[] { "/Timeout", "1" };
            var argsConverted = bindingDefinition.CreateAndBind(args);
            argsConverted.Timeout.Should().Be(1);
        }

    }
}