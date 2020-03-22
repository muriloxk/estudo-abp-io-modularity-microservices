using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace AbpMicroRabbit.Banking.HttpApi.Controllers
{
    [ApiController]
    [Route("api/test")]
    public class TestController : AccountController
    {
        [HttpGet]
        [Route("")]
        public async Task<List<TestModel>> GetAsync()
        {
            return new List<TestModel>
            {
                new TestModel {Name = "John", BirthDate = new DateTime(1942, 11, 18)},
                new TestModel {Name = "Adams", BirthDate = new DateTime(1997, 05, 24)}
            };
        }
    }

    public class TestModel
    {
        public string Name { get; set; }

        public DateTime BirthDate { get; set; }
    }
 
}
