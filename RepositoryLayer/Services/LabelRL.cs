using CommonLayer;
using CommonLayer.Models;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RepositoryLayer.Services
{
    public class LabelRL : ILabelRL
    {
        private readonly FundooContext _db;
        public LabelRL(FundooContext db)
        {
            this._db = db;
        }

        User user = new User();
        public bool CreateLabel(int userId, AddLabel label)
        {
            try
            {
                LabelModel labelModel = new LabelModel()
                {
                    LabelName = label.LableName
                };
                
                user = _db.Users.FirstOrDefault(x => x.Id == userId);
                labelModel.Users = user;

                if(label.LableName != null)
                {
                    _db.Labels.Add(labelModel);
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

        public List<LabelModel> GetAllLabels(int userId)
        {
            try
            {
                var labels = _db.Labels.Where(x => x.Users.Id == userId);

                if(labels != null)
                {
                    return labels.ToList();
                }
                return null;
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
                var label = _db.Labels.Where(x => x.LabelId == labelId && x.Users.Id == userId).SingleOrDefault();

                if (label != null)
                {
                    _db.Labels.Remove(label);
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

        public bool EditLabel(AddLabel label, int labelId, int userId)
        {
            try
            {
                var labelToBeEdited = _db.Labels.FirstOrDefault(x => x.LabelId == labelId && x.Users.Id == userId);

                labelToBeEdited.LabelName = label.LableName;

                if (label.LableName != null)
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
    }
}
