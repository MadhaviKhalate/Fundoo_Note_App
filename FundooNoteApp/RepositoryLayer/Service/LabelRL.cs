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
    }
}
