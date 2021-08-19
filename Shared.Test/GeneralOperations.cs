using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Shared.Test
{
    /// <summary>
    /// Unit tests on the General operations functions
    /// </summary>
    [TestClass]
    public class GeneralOperations
    {
        [TestMethod]
        public void GeneralOperations_ShortenURL()
        {
            //Test without HTTP

            string URL = "www.everlywell.com";

            string ShortURL = Shared.Operations.General.ShortenURL(URL);

            Assert.IsTrue(!String.IsNullOrWhiteSpace(ShortURL) && !ShortURL.Equals("www.everlywell.com"), "Not shortened without http");

            //Test with HTTP

            URL = "http://www.everlywell.com";

            ShortURL = Shared.Operations.General.ShortenURL(URL);

            Assert.IsTrue(!String.IsNullOrWhiteSpace(ShortURL) && !ShortURL.Equals("http://www.everlywell.com"), "Not shortened with http");

        }
    }
}
