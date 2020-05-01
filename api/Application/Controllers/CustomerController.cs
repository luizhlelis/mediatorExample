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
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerDomainService _customerDomainService;
        private readonly IEntityRepository<Customer> _customerGenericRepository;

        public CustomerController(IEntityRepository<Customer> customerGenericRepository,
            ICustomerDomainService customerDomainService)
        {
            _customerGenericRepository = customerGenericRepository;
            _customerDomainService = customerDomainService;
        }

        // GET api/customer
        [HttpGet]
        public ActionResult<IEnumerable<Customer>> Get()
        {
            return Ok(_customerGenericRepository.FindAll());
        }

        // GET api/customer/5
        [HttpGet("{id}")]
        public ActionResult<Customer> Get(int id)
        {
            return Ok(_customerGenericRepository.Find(id));
        }

        // POST api/customer
        [HttpPost]
        public void Post([FromBody] Customer user)
        {
            _customerGenericRepository.Add(user);
        }

        // PUT api/customer/5
        [HttpPut("{id}")]
        public void Put([FromBody] Customer user)
        {
            _customerGenericRepository.Add(user);
        }

        // DELETE api/customer/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            Customer customerToDelete = _customerGenericRepository.Find(id);
            _customerGenericRepository.Remove(customerToDelete);
        }
    }
}
