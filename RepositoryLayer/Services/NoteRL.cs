using CommonLayer;
using Microsoft.EntityFrameworkCore;
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
            catch(Exception)
            {
                throw;
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

        public List<NotesModel> GetArchiveNotes(int userId)
        {
            try
            {
                var notesOfLoggedInUser = _db.Notes.Where(x => x.IsArchive == true && x.Users.Id == userId && x.IsBin == false).ToList();

                if (notesOfLoggedInUser.Count != 0)
                {
                    return notesOfLoggedInUser;
                }
                return null;
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
                var notesOfLoggedInUser = _db.Notes.Where(x => x.IsBin == true && x.Users.Id == userId).ToList();

                if (notesOfLoggedInUser.Count != 0)
                {
                    return notesOfLoggedInUser;
                }
                return null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool BinRestoreNote(int noteId, int userId)
        {
            try
            {
                var note = _db.Notes.FirstOrDefault(x => x.NoteId == noteId && x.Users.Id == userId);

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
            catch(Exception)
            {
                throw;
            }
        }

        public bool ArchiveUnarchiveNote(int noteId, int userId)
        {
            try
            {
                var note = _db.Notes.FirstOrDefault(x => x.NoteId == noteId && x.Users.Id == userId);

                if (note != null)
                {
                    if (note.IsArchive == false)
                    {
                        note.IsArchive = true;
                        _db.SaveChanges();
                        return true;
                    }
                    else
                    {
                        note.IsArchive = false;
                        _db.SaveChanges();
                        return true;
                    }
                }
                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }                        

        public bool DeleteNote(int noteId, int userId)
        {
            try
            {
                var noteToBeRemoved = _db.Notes.FirstOrDefault(x => x.NoteId == noteId && x.Users.Id == userId && x.IsBin == true);

                if (noteToBeRemoved != null)
                {
                    _db.Notes.Remove(noteToBeRemoved);
                    _db.SaveChanges();
                    return true;                    
                }
                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool UpdateNote(UpdateNote update, int noteId, int userId)
        {
            try
            {                
                var noteToBeUpdated = _db.Notes.FirstOrDefault(x => x.NoteId == noteId && x.Users.Id == userId && x.IsBin == false);

                noteToBeUpdated.Title = update.Title;
                noteToBeUpdated.WrittenNote = update.WrittenNote;                

                if (update.Title != null || update.WrittenNote != null)
                {
                    _db.SaveChanges();
                    return true;
                }
                return false;
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
                var note = _db.Notes.Where(x => x.IsArchive == false && x.IsBin == false && x.Users.Id == userId && x.NoteId == noteId).ToList();

                if (note.Count != 0)
                {
                    return note;
                }
                return null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool PinUnpinNote(int noteId, int userId)
        {
            try
            {
                var note = _db.Notes.FirstOrDefault(x => x.NoteId == noteId && x.Users.Id == userId);

                if (note != null)
                {
                    if (note.IsPin == false)
                    {
                        note.IsPin = true;
                        _db.SaveChanges();
                        return true;
                    }
                    else
                    {
                        note.IsPin = false;
                        _db.SaveChanges();
                        return true;
                    }
                }
                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool AddColor(int noteId, string color, int userId)
        {
            try
            {
                var note = _db.Notes.SingleOrDefault(x => x.NoteId == noteId && x.IsBin == false);

                if(note != null)
                {
                    note.Color = color;
                    _db.Entry(note).State = EntityState.Modified;
                    _db.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
