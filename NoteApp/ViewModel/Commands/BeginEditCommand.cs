using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NoteApp.ViewModel.Commands
{
    public class BeginEditCommand : ICommand
    {
        public NotesViewModel VM { get; set; }
        public event EventHandler CanExecuteChanged;
        public BeginEditCommand(NotesViewModel vm)
        {
            VM = vm;
        }
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            VM.StartEditing();
        }
    }
}
