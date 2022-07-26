﻿using CommonLayer.Model;
using Microsoft.AspNetCore.Http;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface INotesRL
    {
        public NotesEntity AddNotes(NotesModel notesModel, long userId);
        public IEnumerable<NotesEntity> ReadNotes(long userId);
        public bool DeleteNotes(long userId, long noteId);
        public NotesEntity UpdateNote(NotesModel noteModel, long NoteId, long userId);
        public bool PinToDashboard(long NoteID, long userId);
        public bool Archive(long NoteID, long userId);
        public bool Trash(long NoteID, long userId);
        public NotesEntity NoteColor(long NoteId, string color);
        public string AddImage(IFormFile image, long noteID, long userID);


    }
}
