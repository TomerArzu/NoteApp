using NoteApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NoteApp.ViewModel.Commands
{
    public class HasEditedCommand : ICommand
    {
        public NotesViewModel VM { get; set; }
        public event EventHandler CanExecuteChanged;

        public HasEditedCommand(NotesViewModel vm)
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
            VM.HasRenamed(notebook);
        }
    }
}
