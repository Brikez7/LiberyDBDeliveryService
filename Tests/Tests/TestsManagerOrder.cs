using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestsDeliveryServiceLibery.TestOptions.Connection;
using TestsDeliveryServiceLibery.TestOptions.OptionForTests;

namespace TestsDeliveryServiceLibery.Tests
{
    public class TestsManagerOrder : IClassFixture<OptionConnectionDatabase>,IClassFixture<OptionsTestAccaounts>, IDisposable
    {
        private readonly OptionConnectionDatabase _optionTestConnectionDB;
        private readonly OptionsTestAccaounts _optionsTestAccaounts;

        public TestsManagerOrder(OptionConnectionDatabase optionTestConnectionDB, OptionsTestAccaounts optionsTestAccaounts)
        {
            _optionTestConnectionDB = optionTestConnectionDB;
            _optionsTestAccaounts = optionsTestAccaounts;
        }

        /*        [Theory]
                [InlineData()]
                public void TestAdd()
                {

                }*/
        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
