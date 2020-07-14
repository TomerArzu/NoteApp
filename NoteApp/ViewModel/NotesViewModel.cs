using NoteApp.Model;
using NoteApp.ViewModel.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteApp.ViewModel
{
    /// <summary>
    /// in this window we are going to:
    /// list notebooks
    /// list notes
    /// display text
    /// commands like:
    /// create new notebook
    /// create new note
    /// (and more...)
    /// </summary>
    public class NotesViewModel
    {
        public ObservableCollection<Notebook> Notebooks { get; set; } //will be bound to listview that displaying the notebooks

        // for the selected Notebook from the list
        private Notebook selectedNotebook;

        public Notebook SelectedNotebook
        {
            get { return selectedNotebook; }
            set
            {
                selectedNotebook = value;
                //TODO: get notes when notebook selected
            }
        }

        public ObservableCollection<Note> Notes { get; set; }

        private Notebook selectedNote;

        public Notebook SelectedNote
        {
            get { return selectedNote; }
            set
            {
                selectedNote = value;
                //TODO: show selected note
            }
        }

        public NewNotebookCommand NewNotebookCommand { get; set; }

        public NewNoteCommand NewNoteCommand { get; set; }

        public NotesViewModel()
        {
            NewNotebookCommand = new NewNotebookCommand(this);
            NewNoteCommand = new NewNoteCommand(this);
        }

    }
}
