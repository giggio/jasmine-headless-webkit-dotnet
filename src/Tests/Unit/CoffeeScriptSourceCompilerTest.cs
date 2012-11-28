using System;
using System.IO;
using System.Reflection;
using NUnit.Framework;
using jasmine_headless_webkit_dotnet;

namespace Tests.Unit
{
    [TestFixture]
    public class CoffeeScriptSourceCompilerTest
    {
        private static string GetTestFilesLocation()
        {
            return Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath);
        }

        private static string GetCoffeeTestFileLocation()
        {
            return Path.Combine(GetTestFilesLocation(), "JasmineTests", "WithHtml", "Scripts", "calculator.coffee");
        }

        public static string GetJavaScriptCompiledTestFile()
        {
            return "(function() {\n\n  window.Calculator = (function() {\n\n    Calculator.name = 'Calculator';\n\n    function Calculator() {}\n\n    Calculator.prototype.sum = function(a, b) {\n      return a + b;\n    };\n\n    Calculator.prototype.divide = function(a, b) {\n      return a / b;\n    };\n\n    Calculator.prototype.multiply = function(a, b) {\n      return a * b;\n    };\n\n    Calculator.prototype.subtract = function(a, b) {\n      return a1b;\n    };\n\n    return Calculator;\n\n  })();\n\n}).call(this);\n";
        }

        [Test]
        public void WhenTestAcceptToCoffeeScriptFileMustBeTrue()
        {
            var file = "file.coffee";
            var compiler = new CoffeeScriptSourceCompiler();
            Assert.IsTrue(compiler.Accept(file));
        }

        [Test]
        public void WhenTestAcceptToNonCoffeeScriptFileMustBefalse()
        {
            var file = "file.coffeexxx";
            var compiler = new CoffeeScriptSourceCompiler();
            Assert.IsFalse(compiler.Accept(file));
        }

        [Test]
        public void WhenTestAcceptToNullOrEmptyFileArgumentMustBeFalse()
        {
            string file = null;
            var compiler = new CoffeeScriptSourceCompiler();
            Assert.IsFalse(compiler.Accept(file));

            file = "";
            Assert.IsFalse(compiler.Accept(file));
        }

        [Test]
        public void MustCompileAValidCoffeeScriptFile()
        {
            var compiler = new CoffeeScriptSourceCompiler();
            var coffeScriptFile = GetCoffeeTestFileLocation();
            var compiledFile = compiler.Compile(coffeScriptFile);

            Assert.AreEqual(GetJavaScriptCompiledTestFile(), compiledFile);
        }
    }
}