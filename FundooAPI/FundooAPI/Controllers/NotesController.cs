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
                var notes = _notes.GetAllNotes(userId);

                if(notes != null)
                {
                    return Ok(new { message = "**Notes are as follows**", data = notes });
                }
                return BadRequest(new { message = "**Notes not available**" });
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpGet("{noteId}")]
        public ActionResult GetNoteByNoteId(int noteId)
        {
            try
            {
                int userId = GetIdFromToken();
                var note = _notes.GetNoteByNoteId(userId, noteId);

                if (note != null)
                {
                    return Ok(new { message = "**Note is as following**", data = note });
                }
                return BadRequest(new { message = "**Note not available**" });
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
                var archiveNotes = _notes.GetArchiveNotes(userId);

                if (archiveNotes != null)
                {
                    return Ok(new { message = "**Archive notes are as follows**", data = archiveNotes });
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
                var trashNotes = _notes.GetBinNotes(userId);

                if (trashNotes != null)
                {
                    return Ok(new { message = "**Bin notes are as follows**", data = trashNotes });
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
                var noteToBeMoved = _notes.MoveToBin(noteId, userId);
                
                if(noteToBeMoved == true)
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

        [HttpPut("{noteId}/restore")]
        public ActionResult RestoreNote(int noteId)
        {
            try
            {
                int userId = GetIdFromToken();
                var noteToBeRestore = _notes.RestoreNote(noteId, userId);

                if (noteToBeRestore == true)
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

        [HttpPut("{noteId}/archive")]
        public ActionResult MoveToArchive(int noteId)
        {
            try
            {
                int userId = GetIdFromToken();
                var noteToBeArchived = _notes.MoveToArchive(noteId, userId);

                if (noteToBeArchived == true)
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

        [HttpPut("{noteId}/unarchive")]
        public ActionResult UnarchiveNote(int noteId)
        {
            try
            {
                int userId = GetIdFromToken();
                var noteToBeUnarchived = _notes.UnarchiveNote(noteId, userId);

                if (noteToBeUnarchived == true)
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

        [HttpDelete("{noteId}")]
        public ActionResult DeleteNote(int noteId)
        {
            try
            {
                int userId = GetIdFromToken();
                var noteToBeDeleted = _notes.DeleteNote(noteId, userId);

                if(noteToBeDeleted == true)
                {
                    return Ok(new { message = "**Note Deleted Successfully**" });
                }
                return BadRequest(new { message = "operation unsuccessfull -_-" });
            }
            catch(Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpPut("{noteId}/update")]
        public ActionResult UpdateNote(UpdateNote update, int noteId)
        {
            try
            {
                int userId = GetIdFromToken();
                var noteToBeUpdated = _notes.UpdateNote(update, noteId, userId);

                if (noteToBeUpdated == true)
                {
                    return Ok(new { message = "**Note Updated Successfully**", data = update });
                }
                return BadRequest(new { message = "operation unsuccessfull -_-" });
            }
            catch(Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }
    }
}
