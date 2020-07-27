using NoteApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NoteApp.ViewModel.Commands
{
    public class NewNoteCommand : ICommand
    {
        public NotesViewModel VM { get; set; }
        public event EventHandler CanExecuteChanged;

        public NewNoteCommand(NotesViewModel vm)
        {
            VM = vm;
        }

        public bool CanExecute(object parameter)
        {
            Notebook selectedNotebook = parameter as Notebook;
            return parameter != null;
        }

        public void Execute(object parameter)
        {
            //TODO: create new note
            Notebook selectedNotebook = parameter as Notebook;
            VM.CreateNote(selectedNotebook.Id);

        }

    }
}
