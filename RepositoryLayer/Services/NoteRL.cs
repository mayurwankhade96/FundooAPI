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

        User user = new User();

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

        public List<NotesModel> GetAllNotes(int userId)
        {
            try
            {
                var notesOfLoggedInUser = _db.Notes.Where(x => x.IsArchive == false && x.IsBin == false && x.Users.Id == userId).ToList();                

                if (notesOfLoggedInUser.Count != 0)
                {
                    return notesOfLoggedInUser;
                }
                return null;
            }
            catch(Exception)
            {
                throw;
            }
        }

        public List<NotesModel> GetArchiveNotes()
        {
            try
            {
                var notesOfLoggedInUser = _db.Notes.Where(x => x.IsArchive == true).ToList();

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

        public List<NotesModel> GetBinNotes()
        {
            try
            {
                var notesOfLoggedInUser = _db.Notes.Where(x => x.IsBin == true).ToList();

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

        public bool InOutFromBin(int noteId)
        {
            try
            {
                var note = _db.Notes.FirstOrDefault(x => x.NoteId == noteId);

                if (note != null)
                {
                    if (note.IsBin == false)
                    {
                        note.IsBin = true;
                        _db.SaveChanges();
                        return true;
                    }
                    else
                    {
                        note.IsBin = false;
                        _db.SaveChanges();
                        return true;
                    }
                }
                return false;
            }
            catch
            {
                throw;
            }
        }
    }
}
