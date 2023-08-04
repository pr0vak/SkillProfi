using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SkillProfiWebApi.Data;
using SkillProfiWebApi.Models;

namespace SkillProfiWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestsController : ControllerBase
    {
        private DataContext _db;

        public RequestsController(DataContext db)
        {
            _db = db;
        }

        // GET: api/<RequestController>
        [HttpGet]
        public IEnumerable<Request> Get()
        {
            return _db.Requests;
        }

        // GET api/<RequestsController>/5
        [HttpGet("{id}")]
        public async Task<Request> Get(int id)
        {
            return await _db.Requests.FindAsync(id) ?? Models.Request.CreateNullRequest();
        }

        // POST api/<RequestsController>
        [HttpPost]
        public async Task Post([FromBody] Request request)
        {
            await _db.Requests.AddAsync(request);
            await _db.SaveChangesAsync();
        }

        // PUT api/<RequestsController>/5
        [HttpPut("{id}")]
        public async Task Put(int id, [FromBody] Request request)
        {
            _db.Requests.Update(request);
            await _db.SaveChangesAsync();
        }

        // DELETE api/<RequestsController>/5
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            _db.Requests.Remove(await Get(id));
            await _db.SaveChangesAsync();
        }
    }
}
