using AutoMoq;
using NUnit.Framework;

namespace Tests.Unit
{
    public class BaseTest
    {
        protected AutoMoqer automoqer;

        [SetUp]
        public void Initialize()
        {
            automoqer = new AutoMoqer();
        }
    }
}