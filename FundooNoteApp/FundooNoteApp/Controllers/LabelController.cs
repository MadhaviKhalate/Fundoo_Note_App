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
    public class LabelController : ControllerBase
    {
        private readonly ILabelBL iLabelBL;

        public LabelController(ILabelBL iLabelBL)
        {
            this.iLabelBL = iLabelBL;
        }

        [HttpPost]
        [Route("Create")]
        public IActionResult Create(LabelModel labelModel)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userID").Value);
                var result = iLabelBL.Create(labelModel, userId);
                if (result != null)
                {
                    return Ok(new { success = true, message = "Label Created Successful", data = result });
                }

                return BadRequest(new { success = false, message = "Label Not Created" });
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
