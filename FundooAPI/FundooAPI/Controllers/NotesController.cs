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


        [HttpPost("createNote")]
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
                var data = _notes.GetAllNotes();
                return Ok(new { message = "**Notes are as follows**", data = data });
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public ActionResult<IEnumerable<NotesModel>> GetNoteByNoteId(int noteId)
        {
            try
            {
                var isNoteFound = this._notes.GetNoteById(noteId);                                
                return Ok(new { message = "**Here is your note**", data = isNoteFound });                                
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }
    }
}
