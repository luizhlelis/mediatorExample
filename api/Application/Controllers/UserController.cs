using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatorExample.Domain.Data.SqlServerRepositoryContract.Generic;
using MediatorExample.Domain.Model.Entities;
using MediatorExample.Domain.Services.Contract;
using Microsoft.AspNetCore.Mvc;

namespace MediatorExample.Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserDomainService _userDomainService;
        private readonly IEntityRepository<User> _userGenericRepository;

        public UserController(IEntityRepository<User> userGenericRepository,
            IUserDomainService userDomainService)
        {
            _userGenericRepository = userGenericRepository;
            _userDomainService = userDomainService;
        }

        // GET api/user
        [HttpGet]
        public ActionResult<IEnumerable<User>> Get()
        {
            return Ok(_userGenericRepository.FindAll());
        }

        // GET api/user/5
        [HttpGet("{id}")]
        public ActionResult<User> Get(int id)
        {
            return Ok(_userGenericRepository.Find(id));
        }

        // POST api/user
        [HttpPost]
        public void Post([FromBody] User user)
        {
            _userGenericRepository.Add(user);
        }

        // PUT api/user/5
        [HttpPut("{id}")]
        public void Put([FromBody] User user)
        {
            _userGenericRepository.Add(user);
        }

        // DELETE api/user/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            User userToDelete = _userGenericRepository.Find(id);
            _userGenericRepository.Remove(userToDelete);
        }
    }
}
