using BusinessLayer.Interfaces;
using CommonLayer;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class NoteBL : INoteBL
    {
        private readonly INoteRL _notes;
        public NoteBL(INoteRL notes)
        {
            this._notes = notes;
        }

        public bool CreateNewNote(AddNote notes, int userId)
        {
            try
            {
                return this._notes.CreateNewNote(notes, userId);
            }
            catch(Exception)
            {
                throw;
            }
        }

        public bool DeleteNote(int noteId, int userId)
        {
            try
            {
                return _notes.DeleteNote(noteId, userId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<NotesModel> GetAllNotes(int userId)
        {
            try
            {
                return this._notes.GetAllNotes(userId);
            }
            catch(Exception)
            {
                throw;
            }
        }

        public List<NotesModel> GetArchiveNotes(int userId)
        {
            try
            {
                return this._notes.GetArchiveNotes(userId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<NotesModel> GetBinNotes(int userId)
        {
            try
            {
                return this._notes.GetBinNotes(userId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<NotesModel> GetNoteByNoteId(int userId, int noteId)
        {
            try
            {
                return this._notes.GetNoteByNoteId(userId, noteId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool ArchiveUnarchiveNote(int noteId, int userId)
        {
            try
            {
                return this._notes.ArchiveUnarchiveNote(noteId, userId);
            }
            catch
            {
                throw;
            }
        }

        public bool BinRestoreNote(int noteId, int userId)
        {
            try
            {
                return this._notes.BinRestoreNote(noteId, userId);
            }
            catch
            {
                throw;
            }
        }        

        public bool UpdateNote(UpdateNote update, int noteId, int userId)
        {
            try
            {
                return this._notes.UpdateNote(update, noteId, userId);
            }
            catch
            {
                throw;
            }
        }

        public bool PinUnpinNote(int noteId, int userId)
        {
            try
            {
                return this._notes.PinUnpinNote(noteId, userId);
            }
            catch
            {
                throw;
            }
        }

        public bool AddColor(int noteId, string color, int userId)
        {
            try
            {
                return this._notes.AddColor(noteId, color, userId);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
