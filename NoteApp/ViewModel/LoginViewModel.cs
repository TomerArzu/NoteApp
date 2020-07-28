using NoteApp.Model;
using NoteApp.ViewModel.Commands;
using NoteApp.ViewModel.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteApp.ViewModel
{
    /// <summary>
    ///     user can login or register
    /// </summary>
    public class LoginViewModel:BaseViewModel
    {
        public event EventHandler HasLoggedin;

        private User user;

        public User User
        {
            get { return user; }
            set { user = value; }
        }

        public RegisterCommand RegisterCommand { get; set; }

        public LoginCommand LoginCommand { get; set; }

        public LoginViewModel()
        {
            RegisterCommand = new RegisterCommand(this);
            LoginCommand = new LoginCommand(this);

            User = new User();
        }

        public void Login()
        {
            using(SQLite.SQLiteConnection conn= new SQLite.SQLiteConnection(DBHelper.dbFile))
            {
                conn.CreateTable<User>();
                var user= conn.Table<User>().Where(u => u.Username == User.Username).FirstOrDefault();
                if(user.Password == User.Password)
                {
                    App.UserId = user.Id.ToString();
                    HasLoggedin(this, new EventArgs());
                }
            }
        }

        public void Register()
        {
            using (SQLite.SQLiteConnection conn = new SQLite.SQLiteConnection(DBHelper.dbFile))
            {
                conn.CreateTable<User>();
                var insertResult = DBHelper.Insert(User);

                if(insertResult)
                {
                    App.UserId = user.Id.ToString();
                    HasLoggedin(this, new EventArgs());
                }
            }
        }
    }
}
