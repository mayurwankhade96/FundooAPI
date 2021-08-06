using CommonLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interfaces
{
    public interface INoteRL
    {
        bool CreateNewNote(AddNote notes, int userId);
        List<NotesModel> GetAllNotes(int userId);
        List<NotesModel> GetBinNotes();
        List<NotesModel> GetArchiveNotes();
        bool InOutFromBin(int noteId);
    }
}
