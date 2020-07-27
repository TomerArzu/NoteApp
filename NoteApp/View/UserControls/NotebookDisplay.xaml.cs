using NoteApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NoteApp.View.UserControls
{
    /// <summary>
    /// Interaction logic for NotebookDisplay.xaml
    /// </summary>
    public partial class NotebookDisplay : UserControl
    {
        public NotebookDisplay()
        {
            InitializeComponent();
        }





        public Notebook Notebook
        {
            get { return (Notebook)GetValue(NotebookProperty); }
            set { SetValue(NotebookProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Notebook.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty NotebookProperty =
            DependencyProperty.Register("Notebook", typeof(Notebook), typeof(NotebookDisplay), new PropertyMetadata(null, SetValues));

        private static void SetValues(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            NotebookDisplay notebookDisplay = d as NotebookDisplay;
            if(notebookDisplay!=null)
            {
                notebookDisplay.notebookNameTextBlock.Text = (e.NewValue as Notebook).Name;
            }
        }
    }
}
