using CommonLayer;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RepositoryLayer.Services
{
    public class NoteRL : INoteRL
    {
        private readonly FundooContext _db;
        public NoteRL(FundooContext db)
        {
            this._db = db;
        }

        public bool CreateNewNote(AddNote newNote, int userId)
        {
            try
            {
                NotesModel notes = new NotesModel()
                {
                    Title = newNote.Title,
                    WrittenNote = newNote.WrittenNote,
                    Reminder = newNote.Reminder,
                    Collaborator = newNote.Collaborator,
                    Color = newNote.Color,
                    Image = newNote.Image,
                    IsArchive = newNote.IsArchive,
                    IsPin = newNote.IsPin,
                    IsBin = newNote.IsBin
                };

                User user = new User();
                //{
                //    Id = userId
                //};
                user = _db.Users.FirstOrDefault(x => x.Id == userId);

                notes.Users = user;
                if(newNote.Title != null || newNote.WrittenNote != null)
                {
                    _db.Notes.Add(notes);
                    _db.SaveChanges();
                    return true;
                }
                return false;
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
                //return _db.Notes.ToList();
                var notesOfLoggedInUser = _db.Notes.Where(x => x.IsArchive == false && x.IsBin == false).ToList();

                if (notesOfLoggedInUser.Count != 0)
                {
                    return notesOfLoggedInUser;
                }
                return null;
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
                var noteById =  _db.Notes.Where(x => x.NoteId == noteId);
                return noteById.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
