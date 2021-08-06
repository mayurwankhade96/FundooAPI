using CommonLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interfaces
{
    public interface INoteBL
    {
        bool CreateNewNote(AddNote notes, int userId);
        List<NotesModel> GetAllNotes(int userId);
        List<NotesModel> GetBinNotes();
        List<NotesModel> GetArchiveNotes();
        bool InOutFromBin(int noteId);
    }
}
