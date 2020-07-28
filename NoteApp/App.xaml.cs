using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace NoteApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// we will use App.xaml.cs for share information between Views in the program
    /// </summary>
    public partial class App : Application
    {
        public static string UserId = string.Empty;
    }
}
