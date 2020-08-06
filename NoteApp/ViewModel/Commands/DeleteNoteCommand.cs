using NoteApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NoteApp.ViewModel.Commands
{
    public class DeleteNoteCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        public NotesViewModel VM { get; set; }
        public DeleteNoteCommand(NotesViewModel vm)
        {
            VM = vm;
        }
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            Note note = parameter as Note;
            if (note != null)
            {
                VM.DeleteNotebook(note);
            }
        }
    }
}
