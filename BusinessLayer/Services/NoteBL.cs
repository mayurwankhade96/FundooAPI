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
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<NotesModel> GetAllNotes(int userId)
        {
            try
            {
                return this._notes.GetAllNotes(userId);
            }
            catch
            {
                throw;
            }
        }

        public List<NotesModel> GetArchiveNotes()
        {
            try
            {
                return this._notes.GetArchiveNotes();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<NotesModel> GetBinNotes()
        {
            try
            {
                return this._notes.GetBinNotes();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool InOutFromBin(int noteId)
        {
            try
            {
                return this._notes.InOutFromBin(noteId);
            }
            catch
            {
                throw;
            }
        }        
    }
}
