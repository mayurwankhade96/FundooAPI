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

        public bool MoveToBin(int noteId, int userId)
        {
            try
            {
                return this._notes.MoveToBin(noteId, userId);
            }
            catch
            {
                throw;
            }
        }        
    }
}
