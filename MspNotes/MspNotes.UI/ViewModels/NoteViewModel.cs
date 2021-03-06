﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

using System.Windows.Input;
using MspNotes.UI.Pages;
using MspNotes.UI.Presentation;

namespace MspNotes.UI.ViewModels
{
    public class NoteViewModel : NotifyPropertyChanged
    {
        //Fields
        private string id;
        public string Id
        {
            get { return id; }
            set { if (Set(ref id, value)) { OnPropertyChanged("Id"); } }
        }
        private string title;
        public string Title
        {
            get { return title; }
            set { if (Set(ref title, value)) { OnPropertyChanged("Title"); } }
        }
        private string description;
        public string Description
        {
            get { return description; }
            set { if (Set(ref description, value)) { OnPropertyChanged("Description"); } }
        }

        //Commands
        public ICommand SaveNoteCommand { get; set; }
        public ICommand UpdateNoteCommand { get; set; }
        public ICommand DeleteNoteCommand { get; set; }

        public NoteViewModel()
        {
            SaveNoteCommand = new Command(SaveNote);
            UpdateNoteCommand = new Command(UpdateNote);
            DeleteNoteCommand = new Command(DeleteNote);
        }

        async private void SaveNote()
        {
            if (string.IsNullOrWhiteSpace(this.Id))
            {
                await App.MainViewModel.onedrive.SaveNote(new Model.Note
                {
                    Id = Guid.NewGuid().ToString(),
                    Description = this.Description,
                    Title = this.title
                });
            }
            else
            {
                await App.MainViewModel.onedrive.UpdateNote(new Model.Note
                {
                    Id = this.Id,
                    Description = this.Description,
                    Title = this.Title
                });
            }
            NavigateToNotes();
        }
        async private void DeleteNote()
        {
            await App.MainViewModel.onedrive.DeleteNote(this.Id);
            NavigateToNotes();
        }

        private void UpdateNote()
        {
            App.MainViewModel.SelectedNote = this;
            var frame = (Shell)Windows.UI.Xaml.Window.Current.Content;
            frame.ViewModel.SelectedMenuItem = frame.ViewModel.MenuItems.Where(x => x.PageType == typeof(NotePage)).FirstOrDefault();
        }

        public void NavigateToNotes()
        {
            var frame = (Shell)Windows.UI.Xaml.Window.Current.Content;
            frame.ViewModel.SelectedMenuItem = frame.ViewModel.MenuItems.Where(x => x.PageType == typeof(NotesPage)).FirstOrDefault();
        }
    }
}
