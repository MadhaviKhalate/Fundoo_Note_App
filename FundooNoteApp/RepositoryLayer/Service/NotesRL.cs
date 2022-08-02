﻿using CommonLayer.Model;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RepositoryLayer.Service
{
    public class NotesRL : INotesRL
    {
        private readonly FundooContext fundooContext;
        public NotesRL(FundooContext fundooContext)
        {
            this.fundooContext = fundooContext;
        }
        public NotesEntity AddNotes(NotesModel notesModel, long userId)
        {
            try
            {
                NotesEntity notesEntity = new NotesEntity();
                //var result = fundooContext.UserEntities.FirstOrDefault(e => e.UserId == userId);
                if (result != null)
                {
                    notesEntity.Title = notesModel.Title;
                    notesEntity.Description = notesModel.Description;
                    notesEntity.Reminder = notesModel.Reminder;
                    notesEntity.Color = notesModel.Color;
                    notesEntity.Image = notesModel.Image;
                    notesEntity.Archive = notesModel.Archive;
                    notesEntity.Pin = notesModel.Pin;
                    notesEntity.Trash = notesModel.Trash;
                    notesEntity.Created = notesModel.Created;
                    notesEntity.Updated = notesModel.Updated;
                    notesEntity.UserId = userId;
                    fundooContext.NotesEntities.Add(notesEntity);
                    fundooContext.SaveChanges();
                    return notesEntity;
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

        public IEnumerable<NotesEntity> ReadNotes(long userId)
        {
            try
            {
                var result = this.fundooContext.NotesEntities.Where(x => x.UserId == userId);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
