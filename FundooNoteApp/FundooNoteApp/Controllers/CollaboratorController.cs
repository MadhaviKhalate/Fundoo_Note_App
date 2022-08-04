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
    }
}
