using NoteApp.Model;
using NoteApp.ViewModel.Commands;
using NoteApp.ViewModel.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
    public class NotesViewModel : BaseViewModel
    {
        public ObservableCollection<Notebook> Notebooks { get; set; } //will be bound to listview that displaying the notebooks



        private bool isEditedNotebook;

        public bool IsEditedNotebook
        {
            get { return isEditedNotebook; }
            set 
            { 
                isEditedNotebook = value;
                OnPropertyChanged();
            }
        }



        // for the selected NotebookDisplay from the list
        private Notebook selectedNotebook;

        public Notebook SelectedNotebook
        {
            get { return selectedNotebook; }
            set
            {
                selectedNotebook = value;
                ReadNote();
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
        public BeginEditCommand beginEditCommand { get; set; }
        public HasEditedCommand hasEditedCommand { get; set; }
        public DeleteNotebookCommand deleteNotebookCommand { get; set; }
        public DeleteNoteCommand deleteNoteCommand { get; set; }

        public NotesViewModel()
        {
            IsEditedNotebook = false;

            NewNotebookCommand = new NewNotebookCommand(this);
            NewNoteCommand = new NewNoteCommand(this);
            beginEditCommand = new BeginEditCommand(this);
            hasEditedCommand = new HasEditedCommand(this);
            deleteNotebookCommand = new DeleteNotebookCommand(this);
            deleteNoteCommand = new DeleteNoteCommand(this);

            Notebooks = new ObservableCollection<Notebook>();
            Notes = new ObservableCollection<Note>();

            ReadNotebooks();
            ReadNote();
        }

        public void CreateNotebook()
        {
            Notebook newNotebook = new Notebook()
            {
                Name = "New NotebookDisplay",
                UserId = int.Parse(App.UserId)
            };
            DBHelper.Insert(newNotebook);
            ReadNotebooks();
        }

        public void CreateNote(int notebookId)
        {
            Note newNote = new Note()
            {
                NotebookId = notebookId,
                CreatedTime = DateTime.Now,
                UpdateTime = DateTime.Now,
                Title = "New Note"
                
            };

            DBHelper.Insert(newNote);
            ReadNote();
        }

        public void ReadNotebooks()
        {
            using (SQLite.SQLiteConnection conn= new SQLite.SQLiteConnection(DBHelper.dbFile))
            {
                conn.CreateTable<Notebook>();
                var notebooks = conn.Table<Notebook>().ToList();
                Notebooks.Clear();
                foreach (var notebook in notebooks)
                {
                    Notebooks.Add(notebook);
                }
            }
        }

        public void ReadNote()
        {
            using (SQLite.SQLiteConnection conn = new SQLite.SQLiteConnection(DBHelper.dbFile))
            {
                if (selectedNotebook != null)
                {
                    conn.CreateTable<Note>();
                    var notes = conn.Table<Note>().Where(n => n.NotebookId == SelectedNotebook.Id).ToList();
                    Notes.Clear();
                    foreach (var note in notes)
                    {
                        Notes.Add(note);
                    }
                }
            }
        }

        public void StartEditing()
        {
            IsEditedNotebook = true;
        }

        public void HasRenamed(Notebook notebook)
        {
            if(notebook!=null)
            {
                DBHelper.Update(notebook);
                IsEditedNotebook = false;
                ReadNotebooks();
            }
        }

        public void DeleteNotebook(Notebook notebook)
        {
            if(notebook!=null)
            {
                DeleteNotesOfSelectedNotebook();
                DBHelper.Delete<Notebook>(notebook);
                ReadNotebooks();
            }
        }

        private void DeleteNotesOfSelectedNotebook()
        {
            if (Notes != null)
            {
                foreach (Note note in Notes)
                {
                    DBHelper.Delete<Note>(note);
                }
                ReadNote();
            }
        }

        public void DeleteNotebook(Note note)
        {
            DBHelper.Delete<Note>(note);
            ReadNote();
        }
    }
}
