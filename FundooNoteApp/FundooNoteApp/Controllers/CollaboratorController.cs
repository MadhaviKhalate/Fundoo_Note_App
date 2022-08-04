using BusinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace FundooNoteApp.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CollaboratorController : ControllerBase
    {
        private readonly ICollaboratorBL iCollaboratorBL;

        public CollaboratorController(ICollaboratorBL iCollaboratorBL)
        {
            this.iCollaboratorBL = iCollaboratorBL;
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
    }
}
