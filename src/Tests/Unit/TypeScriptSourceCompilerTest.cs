using System;
using System.IO;
using System.Reflection;
using NUnit.Framework;
using jasmine_headless_typeScript_compiler;

namespace Tests.Unit
{
    [TestFixture]
    public class TypeScriptSourceCompilerTest
    {
        private static string GetBaseDirectory()
        {
            return Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath) + "\\..\\..";
        }

        private static string GetTypeScriptTestFileLocation()
        {
            return Path.Combine(GetBaseDirectory(), "TypeScriptTests", "WithHtml", "Scripts", "calculator.ts");
        }

        public static string GetJavaScriptCompiledTestFile()
        {
            return "var Calculator = (function () {\r\n    function Calculator() { }\r\n    Calculator.prototype.sum = function (a, b) {\r\n        return a + b;\r\n    };\r\n    Calculator.prototype.divide = function (a, b) {\r\n        return a / b;\r\n    };\r\n    Calculator.prototype.multiply = function (a, b) {\r\n        return a * b;\r\n    };\r\n    Calculator.prototype.subtract = function (a, b) {\r\n        return a - b;\r\n    };\r\n    return Calculator;\r\n})();";
        }

        [Test]
        public void WhenTestAcceptToTypeScriptFileMustBeTrue()
        {
            var file = "file.ts";
            var compiler = new TypeScriptSourceCompiler();
            Assert.IsTrue(compiler.Accept(file));
        }

        [Test]
        public void WhenTestAcceptToNonTypeScriptFileMustBefalse()
        {
            var file = "file.tsxxx";
            var compiler = new TypeScriptSourceCompiler();
            Assert.IsFalse(compiler.Accept(file));
        }

        [Test]
        public void WhenTestAcceptToNullOrEmptyFileArgumentMustBeFalse()
        {
            string file = null;
            var compiler = new TypeScriptSourceCompiler();
            Assert.IsFalse(compiler.Accept(file));

            file = "";
            Assert.IsFalse(compiler.Accept(file));
        }

        [Test]
        public void MustCompileAValidTypeScriptFile()
        {
            var compiler = new TypeScriptSourceCompiler();
            var coffeScriptFile = GetTypeScriptTestFileLocation();
            var compiledFile = compiler.Compile(coffeScriptFile);

            Assert.AreEqual(GetJavaScriptCompiledTestFile(), compiledFile);
        }
    }
}