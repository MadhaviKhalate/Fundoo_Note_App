using CommonLayer.Model;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RepositoryLayer.Service
{
    public class LabelRL : ILabelRL
    {
        public FundooContext fundooContext;
        public LabelRL(FundooContext fundooContext)
        {
            this.fundooContext = fundooContext;
        }

        public LabelEntity Create(LabelModel labelModel, long userId)
        {
            try
            {
                var findNotes = fundooContext.NotesEntities.Where(e => e.NoteID == labelModel.NoteID);
                if(findNotes != null)
                {

                    LabelEntity labelEntity = new LabelEntity();
                    labelEntity.LabelName = labelModel.LabelName;
                    labelEntity.NoteID = labelModel.NoteID;
                    labelEntity.UserId = userId;
                    fundooContext.LabelEntities.Add(labelEntity);
                    fundooContext.SaveChanges();
                    return labelEntity;
                }
                else
                {
                    return null;
                }

            }
            catch (Exception)
            {

                throw;
            }
        }

        public LabelEntity UpdateLabel(LabelModel labelModel, long labelID)
        {
            try
            {
                var update = fundooContext.LabelEntities.Where(r => r.LabelID == labelID).FirstOrDefault();
                if (update != null && update.LabelID == labelID)
                {
                    update.LabelName = labelModel.LabelName;
                    update.NoteID = labelModel.NoteID;

                    fundooContext.SaveChanges();
                    return update;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public LabelEntity DeleteLabel(long labelID, long userId)
        {
            try
            {
                var deleteLabel = fundooContext.LabelEntities.Where(r => r.LabelID == labelID).FirstOrDefault();
                if (deleteLabel != null)
                {
                    fundooContext.LabelEntities.Remove(deleteLabel);
                    fundooContext.SaveChanges();
                    return deleteLabel;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<LabelEntity> GetLabels(long userId)
        {
            try
            {
                var result = fundooContext.LabelEntities.ToList().Where(x => x.UserId == userId);
                if (result != null)
                {
                    return result;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
