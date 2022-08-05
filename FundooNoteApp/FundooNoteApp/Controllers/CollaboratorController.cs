using BusinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundooNoteApp.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CollaboratorController : ControllerBase
    {
        private readonly ICollaboratorBL iCollaboratorBL;
        private readonly IMemoryCache memoryCache;
        private readonly IDistributedCache distributedCache;
        private readonly FundooContext fundooContext;

        public CollaboratorController(ICollaboratorBL iCollaboratorBL, IMemoryCache memoryCache,
            IDistributedCache distributedCache, FundooContext fundooContext)
        {
            this.iCollaboratorBL = iCollaboratorBL;
            this.memoryCache = memoryCache;
            this.distributedCache = distributedCache;
            this.fundooContext = fundooContext;
        }

        [HttpPost]
        [Route("Create")]
        public IActionResult Create(CollaboratorModel collaboratorModel)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userID").Value);
                var result = iCollaboratorBL.Create(collaboratorModel, userId);
                if (result != null)
                {
                    return Ok(new { success = true, message = "Email Created Successful", data = result });
                }

                return BadRequest(new { success = false, message = "Email Not Created" });
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpDelete]
        [Route("Delete")]
        public IActionResult Delete(long collaboratorID)
        {
            try
            {
                var result = iCollaboratorBL.Delete(collaboratorID);
                if (result != false)
                {
                    return Ok(new { success = true, message = "Deleted Successfully" });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Delete Failed" });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        [HttpGet]
        [Route("Read")]
        public IActionResult Get(long noteId)
        {
            try
            {
                var result = iCollaboratorBL.Get(noteId);
                if (result != null)
                {
                    return Ok(new { success = true, message = "Get Data Successfully.", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Data Not Getting." });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        [HttpGet("redis")]
        public async Task<IActionResult> GetAllCustomersUsingRedisCache()
        {
            var cacheKey = "CollaboratorList";
            string serializedCollaboratorList;
            var collaboratorList = new List<CollaboratorEntity>();
            var redisCollaboratorList = await distributedCache.GetAsync(cacheKey);
            if (redisCollaboratorList != null)
            {
                serializedCollaboratorList = Encoding.UTF8.GetString(redisCollaboratorList);
                collaboratorList = JsonConvert.DeserializeObject<List<CollaboratorEntity>>(serializedCollaboratorList);
            }
            else
            {
                collaboratorList = fundooContext.CollaboratorEntities.ToList();
                serializedCollaboratorList = JsonConvert.SerializeObject(collaboratorList);
                redisCollaboratorList = Encoding.UTF8.GetBytes(serializedCollaboratorList);
                var options = new DistributedCacheEntryOptions()
                    .SetAbsoluteExpiration(DateTime.Now.AddMinutes(10))
                    .SetSlidingExpiration(TimeSpan.FromMinutes(2));
                await distributedCache.SetAsync(cacheKey, redisCollaboratorList, options);
            }
            return Ok(collaboratorList);
        }
    }
}
