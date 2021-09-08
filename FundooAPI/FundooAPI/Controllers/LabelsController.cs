using BusinessLayer.Interfaces;
using CommonLayer;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FundooAPI.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class LabelsController : ControllerBase
    {
        private readonly ILabelBL _labels;
        public LabelsController(ILabelBL labels)
        {
            this._labels = labels;
        }

        private int GetIdFromToken()
        {
            return Convert.ToInt32(User.FindFirst(x => x.Type == "userId").Value);
        }

        [HttpPost]
        public ActionResult CreateLabel(AddLabel label)
        {
            try
            {
                int userId = GetIdFromToken();
                var isLabelCreated = this._labels.CreateLabel(userId, label);

                if(isLabelCreated == true)
                {
                    return Ok(new { message = "Label Created!", data = label });
                }
                return BadRequest(new { message = "Error Occurred!" });
            }
            catch(Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpGet]
        public ActionResult GetLabels()
        {
            try
            {
                int userId = GetIdFromToken();
                var labels = _labels.GetAllLabels(userId);

                if(labels != null)
                {
                    return Ok(new { message = "Labels are as follows : ", data = labels });
                }
                return BadRequest(new { message = "Labels not found" });
            }
            catch(Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpDelete]
        public ActionResult DeleteLabel(int labelId)
        {
            try
            {
                int userId = GetIdFromToken();
                var label = _labels.DeleteLabel(labelId, userId);

                if(label == true)
                {
                    return Ok(new { message = "Label deleted successfully..."});
                }
                return BadRequest(new { message = "operation unsuccessfull -_-" });
            }
            catch(Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpPut("{labelId}/update")]
        public ActionResult UpdateNote(AddLabel label, int labelId)
        {
            try
            {
                int userId = GetIdFromToken();
                var labelToBeEdited = _labels.EditLabel(label, labelId, userId);

                if (labelToBeEdited == true)
                {
                    return Ok(new { message = "**Label Updated Successfully**", data = label });
                }
                return BadRequest(new { message = "operation unsuccessfull -_-" });
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }
    }
}
