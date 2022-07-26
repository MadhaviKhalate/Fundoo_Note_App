﻿using BusinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Http;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Service
{
    public class NotesBL : INotesBL
    {
        private readonly INotesRL iNotesRL;

        public NotesBL(INotesRL iNotesRL)
        {
            this.iNotesRL = iNotesRL;
        }

        public NotesEntity AddNotes(NotesModel notesModel, long userId)
        {
            try
            {
                return iNotesRL.AddNotes(notesModel, userId);
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
                return iNotesRL.ReadNotes(userId);
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
                return iNotesRL.DeleteNotes(userId, noteId);
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
                return iNotesRL.UpdateNote(noteModel, NoteId, userId);
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
                return iNotesRL.PinToDashboard(NoteID, userId);
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
                return iNotesRL.Archive(NoteID, userId);
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
                return iNotesRL.Trash(NoteID, userId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public NotesEntity NoteColor(long NoteId, string color)
        {
            try
            {
                return iNotesRL.NoteColor(NoteId, color);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string AddImage(IFormFile image, long noteID, long userID)
        {
            try
            {
                return iNotesRL.AddImage(image, noteID, userID);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
