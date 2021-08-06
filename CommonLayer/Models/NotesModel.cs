using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CommonLayer
{
    public class NotesModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int NoteId { get; set; }
        public string Title { get; set; }
        public string WrittenNote { get; set; }
        public DateTime Reminder { get; set; }
        public string Collaborator { get; set; }
        public string Color { get; set; }
        public string Image { get; set; }
        [DefaultValue(false)]
        public bool IsArchive { get; set; }
        [DefaultValue(false)]
        public bool IsPin { get; set; }
        [DefaultValue(false)]
        public bool IsBin { get; set; }
        public User Users { get; set; }
    }
}
