using NUnit.Framework;
using NXMobileHandsOn.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Tests
{
    [TestFixture]
    public class LoginServiceTests
    {
        [Test]
        public async Task LoginAsync_Normal()
        {
            var service = new LoginService();
            var result = await service.LoginAsync("NXSYS", "cfo", "cfo", DateTime.Now);
        }

        //[Test]
        //public async void LoginAsync_Error()
        //{
        //    var service = new LoginService();
        //    var result = await service.LoginAsync("NXSYS", "cfo", "cfo", DateTime.Now);
        //    Assert.Null(result.NshKey);
        //}
    }
}
