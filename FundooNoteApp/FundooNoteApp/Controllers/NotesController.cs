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
    public class NotesController : ControllerBase
    {
        private readonly INotesBL iNotesBL;

        public NotesController(INotesBL iNotesBL)
        {
            this.iNotesBL = iNotesBL;
        }
        
        [HttpPost]
        [Route("Create")]
        public IActionResult CreateNote(NotesModel noteData)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userID").Value);
                var result = iNotesBL.AddNotes(noteData, userId);
                if (result != null)
                {
                    return Ok(new { success = true, message = "Notes Created Successful", data = result });
                }

                return BadRequest(new { success = false, message = "Notes not Created" });
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        [Route("Read")]
        public IActionResult ReadNotes()
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userID").Value);
                var result = iNotesBL.ReadNotes(userId);
                if (result != null)
                {
                    return Ok(new { success = true, message = "Got Notes from database.", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Cannot get notes." });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }

    }
}
