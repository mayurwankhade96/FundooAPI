using CommonLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interfaces
{
    public interface INoteBL
    {
        bool CreateNewNote(AddNote notes, int userId);
        List<NotesModel> GetAllNotes();
        List<NotesModel> GetNoteById(int noteId);
    }
}
