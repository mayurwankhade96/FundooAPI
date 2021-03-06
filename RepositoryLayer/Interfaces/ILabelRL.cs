using CommonLayer;
using CommonLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interfaces
{
    public interface ILabelRL
    {
        bool CreateLabel(int userId, AddLabel label);
        List<LabelModel> GetAllLabels(int userId);
        bool DeleteLabel(int labelId, int userId);
        bool EditLabel(AddLabel label, int labelId, int userId);
    }
}
