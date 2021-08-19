using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

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

        [TestMethod]
        public void GeneralOperations_ParseHeaders()
        {
            //Check everylwell (1)

            String URL = "http://www.everlywell.com";

            IEnumerable<string> headers = Shared.Operations.General.ParseHeaders(URL);

            Assert.IsTrue(headers.Count() == 1, "Everlywell should have only 1 header");

            //Check google (0)

            URL = "http://www.google.com";

            headers = Shared.Operations.General.ParseHeaders(URL);

            Assert.IsTrue(!headers.Any(), "Google should have no headers");

            //Check yahoo (lots)

            URL = "http://www.yahoo.com";

            headers = Shared.Operations.General.ParseHeaders(URL);

            Assert.IsTrue(headers.Count() > 5, "Yahoo should have several headers");


        }
    }
}
