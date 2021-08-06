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

        public List<NotesModel> GetAllNotes()
        {
            try
            {
                return this._notes.GetAllNotes();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<NotesModel> GetNoteById(int noteId)
        {
            try
            {
                return this._notes.GetNoteById(noteId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
