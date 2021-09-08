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
        List<NotesModel> GetBinNotes(int userId);
        List<NotesModel> GetArchiveNotes(int userId);
        bool BinRestoreNote(int noteId, int userId);
        bool ArchiveUnarchiveNote(int noteId, int userId);
        //bool RestoreNote(int noteId, int userId);
        //bool UnarchiveNote(int noteId, int userId);
        bool DeleteNote(int noteId, int userId);
        bool UpdateNote(UpdateNote update, int noteId, int userId);
        List<NotesModel> GetNoteByNoteId(int userId, int noteId);
        bool PinUnpinNote(int noteId, int userId);
        bool AddColor(int noteId, string color, int userId);
    }
}
