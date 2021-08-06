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


        [HttpPost]
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
                int userId = GetIdFromToken();
                var data = _notes.GetArchiveNotes(userId);

                if (data != null)
                {
                    return Ok(new { message = "**Archive notes are as follows**", data = data });
                }
                return BadRequest(new { message = "**Archive notes not available**" });
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpGet("trash")]
        public ActionResult GetBinNotes()
        {
            try
            {
                int userId = GetIdFromToken();
                var data = _notes.GetBinNotes(userId);

                if (data != null)
                {
                    return Ok(new { message = "**Bin notes are as follows**", data = data });
                }
                return BadRequest(new { message = "**Bin notes not available**" });
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpPut("{noteId}/trash")]
        public ActionResult MoveToBin(int noteId)
        {
            try
            {
                int userId = GetIdFromToken();
                var note = _notes.MoveToBin(noteId, userId);
                
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
