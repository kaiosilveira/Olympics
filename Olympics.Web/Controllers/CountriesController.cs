using Olympics.Domain.Contracts.Interfaces;
using Olympics.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
using System.Web.Mvc;

namespace Olympics.Web.Controllers
{
    public class CountriesController : ApiController
    {
        protected readonly ICountriesRepository CountriesRepository;

        public CountriesController(ICountriesRepository repository)
        {
            this.CountriesRepository = repository;
        }

        // GET api/<controller>
        public IHttpActionResult Get()
        {
            return Ok(this.CountriesRepository.List());
        }

        // GET api/<controller>/5
        public IHttpActionResult Get(int id)
        {
            if (id == 0)
                return BadRequest("Invalid identifier");

            var country = this.CountriesRepository.Get(id);

            if (country != null)
                return Ok(country);
            else
                return NotFound();
        }

        // POST api/<controller>
        public IHttpActionResult Post([FromBody]Country country)
        {
            if (country == null)
                return BadRequest("Invalid parameters");

            return Ok(this.CountriesRepository.Create(country));
        }

        // PUT api/<controller>/5
        public IHttpActionResult Put(int id, [FromBody] Country country)
        {
            if (country == null || id == 0)
                return BadRequest("Invalid parameters");
            
            return Ok(this.CountriesRepository.Update(country));
        }

        // DELETE api/<controller>/5
        public IHttpActionResult Delete(int id)
        {
            if (id == 0)
                return BadRequest("Invalid parameters");

            return Ok(this.CountriesRepository.Delete(id));
        }
    }
}