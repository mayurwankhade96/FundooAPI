using BusinessLayer.Interfaces;
using CommonLayer;
using CommonLayer.Models;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class LabelBL : ILabelBL
    {
        private readonly ILabelRL _label;
        public LabelBL(ILabelRL label)
        {
            this._label = label;
        }

        public bool CreateLabel(int userId, AddLabel label)
        {
            try
            {
                return _label.CreateLabel(userId, label);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool DeleteLabel(int labelId, int userId)
        {
            try
            {
                return _label.DeleteLabel(labelId, userId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool EditLabel(AddLabel label, int labelId, int userId)
        {
            try
            {
                return _label.EditLabel(label, labelId, userId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<LabelModel> GetAllLabels(int userId)
        {
            try
            {
                return _label.GetAllLabels(userId);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
