using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nylas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nylas.Tests
{
    [TestClass()]
    public class RequestContentTests
    {
        [TestMethod()]
        public void ToStringContentTest()
        {
            AuthorizationCodeRequest request = new AuthorizationCodeRequest
            {
                client_id = "abcd1234",
                client_secret = "0987654321",
                grant_type = "authorization_code",
                code = "abcd1234"
            };
            var content = request.ToStringContent();
            Assert.IsNotNull(content);
            var contentStr = content.ReadAsStringAsync().Result;
            Assert.IsTrue(contentStr.Contains(request.code));
        }
    }
}