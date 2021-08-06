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
    public class NotesController : ControllerBase
    {
        private readonly INoteBL _notes;
        public NotesController(INoteBL notes)
        {
            this._notes = notes;
        }


        [HttpPost("create")]
        public ActionResult CreateNote(AddNote notes)
        {
            try
            {                
                int userId = GetIdFromToken();
                var isNoteAdded = this._notes.CreateNewNote(notes, userId);

                if (isNoteAdded == true)
                {
                    return Ok(new { message = "**Note Added**", data = notes });
                }
                return BadRequest(new { message = "Failed -_-" });
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        private int GetIdFromToken()
        {
            return Convert.ToInt32(User.FindFirst(x => x.Type == "userId").Value);
        }

        [HttpGet]
        public ActionResult GetAllNotes()
        {
            try
            {
                int userId = GetIdFromToken();
                var data = _notes.GetAllNotes(userId);

                if(data != null)
                {
                    return Ok(new { message = "**Notes are as follows**", data = data });
                }
                return BadRequest(new { message = "**Notes not available**" });
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpGet("archive")]
        public ActionResult GetArchiveNotes()
        {
            try
            {
                var data = _notes.GetArchiveNotes();

                if (data != null)
                {
                    return Ok(new { message = "**Notes are as follows**", data = data });
                }
                return BadRequest(new { message = "**Notes not available**" });
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpGet("bin")]
        public ActionResult GetBinNotes()
        {
            try
            {
                var data = _notes.GetBinNotes();

                if (data != null)
                {
                    return Ok(new { message = "**Notes are as follows**", data = data });
                }
                return BadRequest(new { message = "**Notes not available**" });
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpPut("trash")]
        public ActionResult MoveToBin(int noteId)
        {
            try
            {
                var note = _notes.InOutFromBin(noteId);
                
                if(note == true)
                {
                    return Ok(new { message = "**Operation successfull**" });
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
