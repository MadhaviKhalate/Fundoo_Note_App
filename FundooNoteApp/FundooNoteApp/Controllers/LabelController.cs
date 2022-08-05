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

        [HttpPut]
        [Route("Update")]
        public IActionResult UpdateLabel(LabelModel labelModel, long labelID)
        {
            try
            {
                long userid = Convert.ToInt32(User.Claims.FirstOrDefault(r => r.Type == "userID").Value);
                var result = iLabelBL.UpdateLabel(labelModel, labelID);
                if (result != null)
                {
                    return Ok(new { Success = true, message = "Label Updated Successfully", data = result });
                }
                else
                {
                    return NotFound(new { Success = false, message = "Label Not Updated" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpDelete]
        [Route("Delete")]
        public IActionResult DeleteLabel(long labelID)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(r => r.Type == "userID").Value);
                var delete = iLabelBL.DeleteLabel(labelID, userId);
                if (delete != null)
                {
                    return this.Ok(new { Success = true, message = "Label Deleted Successfully" });
                }
                else
                {
                    return this.NotFound(new { Success = false, message = "Label not Deleted" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        [Route("Read")]
        public IActionResult GetAllLabels()
        {
            try
            {
                long userid = Convert.ToInt32(User.Claims.FirstOrDefault(r => r.Type == "userID").Value);
                var labels = iLabelBL.GetLabels(userid);
                if (labels != null)
                {
                    return this.Ok(new { Success = true, Message = " All labels found Successfully", data = labels });
                }
                else
                {
                    return this.NotFound(new { Success = false, Message = "No label found" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
