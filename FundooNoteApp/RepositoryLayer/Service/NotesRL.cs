using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using CommonLayer.Model;
using Microsoft.AspNetCore.Http;
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
        private readonly IConfiguration configuration;
        public NotesRL(FundooContext fundooContext, IConfiguration configuration)
        {
            this.fundooContext = fundooContext;
            this.configuration = configuration;
        }
        public NotesEntity AddNotes(NotesModel notesModel, long userId)
        {
            try
            {
                NotesEntity notesEntity = new NotesEntity();
                notesEntity.Title = notesModel.Title;
                notesEntity.Description = notesModel.Description;
                notesEntity.Reminder = notesModel.Reminder;
                notesEntity.Color = notesModel.Color;
                notesEntity.Image = notesModel.Image;
                notesEntity.Created = DateTime.Now;
                notesEntity.Updated = DateTime.Now;
                notesEntity.Archive = notesModel.Archive;
                notesEntity.Pin = notesModel.Pin;
                notesEntity.Trash = notesModel.Trash;
                notesEntity.UserId = userId;
                //notesEntity.User = fundooContext.userEntities.Where(user => user.UserId == UserID).FirstOrDefault();
                fundooContext.NotesEntities.Add(notesEntity);
                int result = fundooContext.SaveChanges();
                if (result != 0)
                {
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

        public bool DeleteNotes(long userId, long noteId)
        {
            try
            {

                var result = fundooContext.NotesEntities.Where(x => x.UserId == userId && x.NoteID == noteId).FirstOrDefault();
                if (result != null)
                {
                    fundooContext.NotesEntities.Remove(result);
                    this.fundooContext.SaveChanges();
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

        public NotesEntity UpdateNote(NotesModel noteModel, long NoteId, long userId)
        {
            try
            {
                var result = fundooContext.NotesEntities.Where(note => note.UserId == userId && note.NoteID == NoteId).FirstOrDefault();
                if (result != null)
                {
                    result.Title = noteModel.Title;
                    result.Description = noteModel.Description;
                    result.Reminder = noteModel.Reminder;
                    result.Updated = DateTime.Now;
                    result.Color = noteModel.Color;
                    result.Image = noteModel.Image;

                    this.fundooContext.SaveChanges();
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

        public bool PinToDashboard(long NoteID, long userId)
        {
            try
            {
                var result = fundooContext.NotesEntities.Where(x => x.UserId == userId && x.NoteID == NoteID).FirstOrDefault();

                if (result.Pin == true)
                {
                    result.Pin = false;
                    fundooContext.SaveChanges();
                    return false;
                }
                else
                {
                    result.Pin = true;
                    fundooContext.SaveChanges();
                    return true;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool Archive(long NoteID, long userId)
        {
            try
            {
                var result = fundooContext.NotesEntities.Where(x => x.UserId == userId && x.NoteID == NoteID).FirstOrDefault();

                if (result.Archive == true)
                {
                    result.Archive = false;
                    fundooContext.SaveChanges();
                    return false;
                }
                else
                {
                    result.Archive = true;
                    fundooContext.SaveChanges();
                    return true;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool Trash(long NoteID, long userId)
        {
            try
            {
                var result = fundooContext.NotesEntities.Where(x => x.UserId == userId && x.NoteID == NoteID).FirstOrDefault();

                if (result.Trash == true)
                {
                    result.Trash = false;
                    fundooContext.SaveChanges();
                    return false;
                }
                else
                {
                    result.Trash = true;
                    fundooContext.SaveChanges();
                    return true;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public NotesEntity NoteColor(long NoteId, string color)
        {
            var result = fundooContext.NotesEntities.Where(r => r.NoteID == NoteId).FirstOrDefault();
            if (result != null)
            {
                if (color != null)
                {
                    result.Color = color;
                    fundooContext.NotesEntities.Update(result);
                    fundooContext.SaveChanges();
                    return result;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        public string AddImage(IFormFile image, long noteID, long userID)
        {
            try
            {
                var result = fundooContext.NotesEntities.Where(x => x.UserId == userID && x.NoteID == noteID).FirstOrDefault();
                if (result != null)
                {
                    Account account = new Account(
                        this.configuration["CloudinaryAccount:CloudName"],
                        this.configuration["CloudinaryAccount:APIKey"],
                        this.configuration["CloudinaryAccount:APISecret"]);

                    Cloudinary cloudinary = new Cloudinary(account);
                    var uploadParameters = new ImageUploadParams()
                    {
                        File = new FileDescription(image.FileName, image.OpenReadStream()),
                    };
                    var uploadResult = cloudinary.Upload(uploadParameters);
                    string imagePath = uploadResult.Url.ToString();
                    result.Image = imagePath;
                    fundooContext.SaveChanges();
                    return "Image uploaded successfully";
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}

