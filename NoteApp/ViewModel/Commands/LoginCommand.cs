using NoteApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NoteApp.ViewModel.Commands
{
    public class LoginCommand : ICommand
    {
        public LoginViewModel VM { get; set; }
        public event EventHandler CanExecuteChanged;

        public LoginCommand(LoginViewModel vm)
        {
            VM = vm;
        }

        public bool CanExecute(object parameter)
        {
            //var user = parameter as User;
            //if (user != null)
            //{
            //    if (string.IsNullOrEmpty(user.Username))
            //    {
            //        return false;
            //    }
            //    if (string.IsNullOrEmpty(user.Password))
            //    {
            //        return false;
            //    }
            //}
                return true;
        }

        public void Execute(object parameter)
        {
            //TODO: Login functionality
            VM.Login();
        }
    }
}
