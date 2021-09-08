using BusinessLayer.Interfaces;
using CommonLayer;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using RepositoryLayer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundooAPI.Controllers
{
    //[EnableCors()]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        private readonly INoteBL _notes;
        private readonly IDistributedCache _distributedCache;
        private readonly FundooContext _context;
        private readonly string cacheKey = "noteList";
        public NotesController(INoteBL notes, FundooContext context, IDistributedCache distributedCache)
        {
            this._notes = notes;
            this._distributedCache = distributedCache;
            this._context = context;            
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
                    //_distributedCache.Remove(cacheKey);
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

        //[HttpGet("redis")]
        //public async Task<IActionResult> GetAllCustomersUsingRedisCache()
        //{
        //    //var cacheKey = "noteList";
        //    string serializedCustomerList;
        //    var noteList = new List<NotesModel>();
        //    var redisNoteList = await _distributedCache.GetAsync(cacheKey);
        //    if (redisNoteList != null)
        //    {
        //        serializedCustomerList = Encoding.UTF8.GetString(redisNoteList);
        //        noteList = JsonConvert.DeserializeObject<List<NotesModel>>(serializedCustomerList);

        //    }
        //    else
        //    {
        //        //noteList = await _context.Notes.ToListAsync();
        //        int userId = GetIdFromToken();
        //        noteList = _notes.GetAllNotes(userId);
        //        serializedCustomerList = JsonConvert.SerializeObject(noteList);
        //        redisNoteList = Encoding.UTF8.GetBytes(serializedCustomerList);
        //        var options = new DistributedCacheEntryOptions()
        //            .SetAbsoluteExpiration(DateTime.Now.AddMinutes(10))
        //            .SetSlidingExpiration(TimeSpan.FromMinutes(2));
        //        await _distributedCache.SetAsync(cacheKey, redisNoteList, options);
        //    }
        //    return Ok(noteList);
        //}
        
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

        [HttpPut("{noteId}/trash-restore")]
        public ActionResult BinRestoreNote(int noteId)
        {
            try
            {
                int userId = GetIdFromToken();
                var noteToBeMoved = _notes.BinRestoreNote(noteId, userId);
                
                if(noteToBeMoved == true)
                {
                    //_distributedCache.Remove(cacheKey);
                    return Ok(new { message = "**Operation successfull**" });
                }
                return BadRequest(new { message = "operation unsuccessfull -_-" });
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }        

        [HttpPut("{noteId}/archive-unarchive")]
        public ActionResult ArchiveUnarchiveNote(int noteId)
        {
            try
            {
                int userId = GetIdFromToken();
                var noteToBeArchived = _notes.ArchiveUnarchiveNote(noteId, userId);

                if (noteToBeArchived == true)
                {
                    //_distributedCache.Remove(cacheKey);
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
                    //_distributedCache.Remove(cacheKey);
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
                    //_distributedCache.Remove(cacheKey);
                    return Ok(new { message = "**Note Updated Successfully**", data = update });
                }
                return BadRequest(new { message = "operation unsuccessfull -_-" });
            }
            catch(Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpPut("{noteId}/pin-unpin")]
        public ActionResult PinUnpinNote(int noteId)
        {
            try
            {
                int userId = GetIdFromToken();
                var noteToBePinned = _notes.PinUnpinNote(noteId, userId);

                if (noteToBePinned == true)
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

        [HttpPut("color")]
        public ActionResult AddColor(int noteId, string color)
        {
            try
            {
                int userId = GetIdFromToken();
                var noteToBeColored = _notes.AddColor(noteId, color, userId);

                if(noteToBeColored == true)
                {
                    return Ok(new { message = color + "added" });
                }
                return BadRequest(new { message = "color not added" });
            }
            catch(Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }
    }
}
