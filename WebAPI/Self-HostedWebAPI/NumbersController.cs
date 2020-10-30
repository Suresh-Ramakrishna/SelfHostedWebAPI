using Self_HostedWebAPI.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace SelfHostedWebAPI
{
    public interface IRepository
    {
        IEnumerable<int> GetAll();
    }

    public class NumbersRepository : IRepository
    {
        public IEnumerable<int> GetAll()
        {
            return Enumerable.Range(0, 10);
        }
    }
    [LoggerActionFilter]
    [BasicAuthentication]
    [Authorize(Users = "Hello")]
    [BasicAuthorization]
    public class NumbersController : ApiController
    {
        IRepository Repository;
        public NumbersController()
        {
        }
        public NumbersController(IRepository repository)
        {
            Repository = repository;
        }
        public IEnumerable<int> Get()
        {
            return Repository.GetAll();
        }
    }
}
