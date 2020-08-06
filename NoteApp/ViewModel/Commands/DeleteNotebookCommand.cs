using NoteApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NoteApp.ViewModel.Commands
{
    
    public class DeleteNotebookCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        public NotesViewModel VM { get; set; }

        public DeleteNotebookCommand(NotesViewModel vm)
        {
            VM = vm;
        }

        public bool CanExecute(object parameter)
        {
                return true;
        }

        public void Execute(object parameter)
        {
            Notebook notebook = parameter as Notebook;
            VM.DeleteNotebook(notebook);
        }

    }
}
