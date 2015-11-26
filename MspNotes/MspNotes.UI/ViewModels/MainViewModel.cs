using MspNotes.UI.Model;
using MspNotes.UI.Presentation;
using MspNotes.UI.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Input;

namespace MspNotes.UI.ViewModels
{
    public class MainViewModel : NotifyPropertyChanged
    {
        //Fields
        public ObservableCollection<NoteViewModel> Notes { get; set; }
        public NoteViewModel SelectedNote { get; set; }
        public OneDriveService onedrive { get; set; }

        public MainViewModel()
        {
            onedrive = new OneDriveService();
            SelectedNote = new NoteViewModel();
            LoadNotes();
        }

        async public void LoadNotes()
        {
            Notes = new ObservableCollection<NoteViewModel>();
            var notesResult = await onedrive.GetAllNotes();
            foreach (var note in notesResult)
            {
                Notes.Add(new NoteViewModel
                {
                    Id = note.Id,
                    Description = note.Description,
                    Title = note.Title,
                });

            }
        }
    }
}
