﻿using NoteApp.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Speech.Recognition;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace NoteApp.View
{
    /// <summary>
    /// Interaction logic for NoteView.xaml
    /// </summary>
    public partial class NotesView : Window
    {
        SpeechRecognitionEngine recognizer;
        NotesViewModel viewModel;
        public NotesView()
        {
            InitializeComponent();
            viewModel = _mainContainer.DataContext as NotesViewModel;
            //viewModel = new NotesViewModel();
            //_mainContainer.DataContext = viewModel;
            viewModel.selectedNoteChanged += ViewModel_selectedNoteChanged;

            var currentCulture = (from r
                                 in SpeechRecognitionEngine.InstalledRecognizers()
                                 where r.Culture.Equals(Thread.CurrentThread.CurrentCulture)
                                 select r).FirstOrDefault();

            recognizer = new SpeechRecognitionEngine(currentCulture);

            recognizer.SpeechRecognized += Recognizer_SpeechRecognized;

            GrammarBuilder grammarBuilder = new GrammarBuilder();
            grammarBuilder.AppendDictation();
            Grammar grammar = new Grammar(grammarBuilder);
            recognizer.LoadGrammar(grammar);

            //recognizer.SetInputToDefaultAudioDevice();
            recognizer.SetInputToNull();

            var fontFamilies = Fonts.SystemFontFamilies.OrderBy(f => f.Source);
            FontFamilyCB.ItemsSource = fontFamilies;
            List<double> fontsSizes = new List<double>() { 8, 9, 10, 11, 12, 14, 16, 18, 48, 62 };
            FontSizeCB.ItemsSource = fontsSizes;
        }

        private void ViewModel_selectedNoteChanged(object sender, EventArgs e)
        {
            if (viewModel.SelectedNote != null && !string.IsNullOrEmpty(viewModel.SelectedNote.FileLocation))
            {
                FileStream fileStream = new FileStream(viewModel.SelectedNote.FileLocation, FileMode.Open); //opens the file in the file location
                TextRange range = new TextRange(contentRichTextBox.Document.ContentStart, contentRichTextBox.Document.ContentEnd); //Define two points that the text is going to be set on (like pointer to the start and the end of the text place holder)
                range.Load(fileStream, DataFormats.Rtf); // load the text in the file in the format of Rich Text Format (RTF)
                fileStream.Close();
            }
            else
            {
                contentRichTextBox.Document.Blocks.Clear();
            }
        }
        private void saveNoteContentButton_Click(object sender, RoutedEventArgs e)
        {
            string rtfFile = System.IO.Path.Combine(Environment.CurrentDirectory, $"{viewModel.SelectedNote.Id}.rtf"); // saves the ~path~ to the text in the rich text box in the current directory
            viewModel.SelectedNote.FileLocation = rtfFile;

            FileStream fileStream = new FileStream(rtfFile, FileMode.Create); // if the file exist- override it, else create new one
            TextRange range = new TextRange(contentRichTextBox.Document.ContentStart, contentRichTextBox.Document.ContentEnd); //Select the content in the rich text box
            range.Save(fileStream, DataFormats.Rtf); // save the selected content as RTF file

            viewModel.UpdatedSelectedNote(); //we need to update because we change the FileLocation propery of the note

            statusTextBlock.Text = $"{viewModel.SelectedNote.Title} Saved!      File Location: {viewModel.SelectedNote.FileLocation}";
        }

        protected override void OnActivated(EventArgs e)
        {
            base.OnActivated(e);
            if(string.IsNullOrEmpty(App.UserId))
            {
                LoginView loginWindow = new LoginView();
                loginWindow.ShowDialog(); //return only if this window is closed
            }
        }

        private void Recognizer_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            string recognizedText = e.Result.Text;
            contentRichTextBox.Document.Blocks.Add(new Paragraph(new Run(recognizedText)));
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void contentRichTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            int ammountOfChars = new TextRange(contentRichTextBox.Document.ContentStart, contentRichTextBox.Document.ContentEnd).Text.Length;
            statusTextBlock.Text = $"Document Length: {ammountOfChars} charecters";
        }

        private void BoldButton_Click(object sender, RoutedEventArgs e)
        {
            ToggleButton toggle = sender as ToggleButton;
            bool isButtonChecked = toggle.IsChecked ?? false;
            if(isButtonChecked)
                contentRichTextBox.Selection.ApplyPropertyValue(Inline.FontWeightProperty, FontWeights.Bold);
            else
            {
                contentRichTextBox.Selection.ApplyPropertyValue(Inline.FontWeightProperty, FontWeights.Normal);
            }
        }

        private void speechButton_Click(object sender, RoutedEventArgs e)
        {
            ToggleButton toggle = sender as ToggleButton;
            bool isButtonChecked = toggle.IsChecked ?? false;// toggle.IsChecked is from type bool? (nullable boolean), if statment expect to true/false (not null)
                                                             // the two questions mark null-coalescing that says: if toggle.IsChecked is null assign false
            if (isButtonChecked) 
            {
                recognizer.RecognizeAsync(RecognizeMode.Multiple);
            }
            else
            {
                recognizer.RecognizeAsyncStop();
            }
        }

        private void contentRichTextBox_SelectionChanged(object sender, RoutedEventArgs e)
        {
            var selectadeBoldState = contentRichTextBox.Selection.GetPropertyValue(Inline.FontWeightProperty);
            var selectadeItalicState = contentRichTextBox.Selection.GetPropertyValue(Inline.FontStyleProperty);
            var selectadeUnderlineState = contentRichTextBox.Selection.GetPropertyValue(Inline.TextDecorationsProperty);

            BoldButton.IsChecked = (selectadeBoldState != DependencyProperty.UnsetValue) && (selectadeBoldState.Equals(FontWeights.Bold));
            ItalicButton.IsChecked = (selectadeItalicState != DependencyProperty.UnsetValue) && (selectadeItalicState.Equals(FontStyles.Italic));
            UnderlineButton.IsChecked = (selectadeUnderlineState != DependencyProperty.UnsetValue) && (selectadeUnderlineState.Equals(TextDecorations.Underline));

            FontFamilyCB.SelectedItem = contentRichTextBox.Selection.GetPropertyValue(Inline.FontFamilyProperty);
            FontSizeCB.Text = contentRichTextBox.Selection.GetPropertyValue(Inline.FontSizeProperty).ToString();
        }

        private void ItalicButton_Click(object sender, RoutedEventArgs e)
        {
            ToggleButton toggle = sender as ToggleButton;
            bool isButtonChecked = toggle.IsChecked ?? false;
            if (isButtonChecked)
                contentRichTextBox.Selection.ApplyPropertyValue(Inline.FontStyleProperty, FontStyles.Italic);
            else
            {
                contentRichTextBox.Selection.ApplyPropertyValue(Inline.FontStyleProperty, FontStyles.Normal);
            }
        }

        private void UnderlineButton_Click(object sender, RoutedEventArgs e)
        {
            ToggleButton toggle = sender as ToggleButton;
            bool isButtonChecked = toggle.IsChecked ?? false;
            if (isButtonChecked)
                contentRichTextBox.Selection.ApplyPropertyValue(Inline.TextDecorationsProperty, TextDecorations.Underline);
            else
            {
                contentRichTextBox.Selection.ApplyPropertyValue(Inline.TextDecorationsProperty,null);
            }
        }

        private void FontFamilyCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(FontFamilyCB.SelectedItem!=null)
            {
                contentRichTextBox.Selection.ApplyPropertyValue(Inline.FontFamilyProperty, FontFamilyCB.SelectedItem);
            }
        }

        private void FontSizeCB_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(FontSizeCB.SelectedItem!=null)
            {
                contentRichTextBox.Selection.ApplyPropertyValue(Inline.FontSizeProperty, FontSizeCB.Text);
            }
        }
    }
}
