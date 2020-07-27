﻿using NoteApp.Model;
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
    /// Interaction logic for NoteControl.xaml
    /// </summary>
    public partial class NoteControl : UserControl
    {
        public NoteControl()
        {
            InitializeComponent();
        }



        public Note Note
        {
            get { return (Note)GetValue(NoteProperty); }
            set { SetValue(NoteProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Note.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty NoteProperty =
            DependencyProperty.Register("Note", typeof(Note), typeof(NoteControl), new PropertyMetadata(null, SetNoteValues));

        private static void SetNoteValues(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            NoteControl noteControl = (d as NoteControl);
            if(noteControl!=null)
            {
                noteControl.noteTitle.Text = (e.NewValue as Note).Title;
                noteControl.noteEdited.Text = (e.NewValue as Note).UpdateTime.ToShortDateString();
                noteControl.noteContent.Text = (e.NewValue as Note).Title; // TODO: Title temporery
            }
        }
    }
}
