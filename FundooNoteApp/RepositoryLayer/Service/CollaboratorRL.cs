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
    public class CollaboratorRL : ICollaboratorRL
    {
        private readonly FundooContext fundooContext;
        public CollaboratorRL(FundooContext fundooContext)
        {
            this.fundooContext = fundooContext;
        }

        public CollaboratorEntity Create(CollaboratorModel collaboratorModel, long userId)
        {
            try
            {
                var result = fundooContext.NotesEntities.Where(x => x.NoteID == collaboratorModel.NoteID).FirstOrDefault();
                if (result != null)
                {
                    CollaboratorEntity collaboratorEntity = new CollaboratorEntity();
                    collaboratorEntity.NoteID = collaboratorModel.NoteID;
                    collaboratorEntity.CollaboratorMail = collaboratorModel.CollaboratorMail;
                    collaboratorEntity.UserId = userId;
                    fundooContext.CollaboratorEntities.Add(collaboratorEntity);
                    fundooContext.SaveChanges();
                    return collaboratorEntity;
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

        public bool Delete(long collaboratorID)
        {
            try
            {

                var result = fundooContext.CollaboratorEntities.Where(x => x.CollaboratorID == collaboratorID).First();
                if (result != null)
                {
                    fundooContext.CollaboratorEntities.Remove(result);
                    fundooContext.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<CollaboratorEntity> Get(long noteId)
        {
            try
            {
                var result = fundooContext.CollaboratorEntities.Where(x => x.NoteID == noteId);
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
