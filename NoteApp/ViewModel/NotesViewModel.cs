using NoteApp.Model;
using NoteApp.ViewModel.Commands;
using NoteApp.ViewModel.Helpers;
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

        // for the selected NotebookDisplay from the list
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

            Notebooks = new ObservableCollection<Notebook>();
            Notes = new ObservableCollection<Note>();

            ReadNotebooks();
        }

        public void CreateNotebook()
        {
            Notebook newNotebook = new Notebook()
            {
                Name = "New NotebookDisplay"
            };
            DBHelper.Insert(newNotebook);
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
        }

        public void ReadNotebooks()
        {
            using (SQLite.SQLiteConnection conn= new SQLite.SQLiteConnection(DBHelper.dbFile))
            {
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
                    var notes = conn.Table<Note>().Where(n => n.NotebookId == SelectedNotebook.Id).ToList();
                    Notes.Clear();
                    foreach (var note in notes)
                    {
                        Notes.Add(note);
                    }
                }
            }
        }

    }
}
