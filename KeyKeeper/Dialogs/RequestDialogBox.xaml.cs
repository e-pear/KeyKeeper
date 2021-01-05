using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Shapes;
using KeyKeeper.ViewModel;

namespace KeyKeeper.Dialogs
{
    /// <summary>
    /// Interaction and Creation logic for RequestDialogBox.xaml
    /// This "little fella" is able to draw/render itself during runtime. It is "one for all" dialog box for retreiving additional informations from user. 
    /// Object is meant to render itself according to needs of the moment. Constructor accepts a collection of needed parameters along with collection of corresponding validation rules.
    /// Then it renders a pair of Label-TextBox pairs in dialog window. So the requested parameters become a label descriptions for proper in-parameter textboxes. Validation rules are applied(or not) by index matching.
    /// Object is well... "MVVM" a little :) and not so SOLID, at least for the near future. And it's not perfect it won't auto resize to fit all content, so be carefull with passing extremely long user messages to it. 
    /// </summary>
    public partial class RequestDialogBox : Window
    {
        // Hlepers, both store input informations passed via constructor:
        private List<string> _parametersIndexes;
        private List<ValidationRule> _validationRules;
        
        // Hleper, rememberes xaml controls which have any validation rule set <- the confirm's command canexecute method uses it.
        private List<TextBox> _validatedObjects;
        // Our little ViewModel. It only holds some handy indexer for us.
        private readonly RequestDialogBoxContext _dialogContext;
        // This one only simplifies retreiving data from dialog. It is possible to use it like: parameter = dialogBox[parameterName]. Quite handy!
        public string this[int parameterIndex]
        {
            get 
            {
                if (parameterIndex >= 0) return _dialogContext.StringParameter[parameterIndex];
                else return "";
            }
        }
        //Constructor:
        public RequestDialogBox(string userMessage, IEnumerable<string> collectionOfRequestedParameters, IEnumerable<ValidationRule> collectionOfCorrespondingValidationRules)
        {
            _dialogContext = new RequestDialogBoxContext();
            _parametersIndexes = collectionOfRequestedParameters.ToList();
            _validationRules = collectionOfCorrespondingValidationRules.ToList();
            _validatedObjects = new List<TextBox>();

            DialogGrid = new Grid(); // it's reference lives in xaml code.

            this.Owner = Application.Current.MainWindow;
            this.Content = DialogGrid;
            this.DataContext = _dialogContext;

            InitializeComponent();

            // render parts of dialogBox: head, body and... bottom

            double[] firstRowMargin = new double[] { 20, 10, 20, 0 };
            double[] margin = new double[] { 20, 0, 20, 0 };
            double[] lastRowMargin = new double[] { 20, 10, 20, 10 };

            bool headerRenderResult = RenderHeader(userMessage, firstRowMargin);
            RenderParametersRowsInDialog(headerRenderResult, margin);
            RenderControlRowInDialog(lastRowMargin);           
        }
        // Renders header part od dialog box. Returns false if heder rendered without header/user message.
        private bool RenderHeader(string headerMessage, double[] margin)
        {
            // columns:
            ColumnDefinition col1 = new ColumnDefinition();
            ColumnDefinition col2 = new ColumnDefinition();
            col1.Width = new GridLength(1, GridUnitType.Star);
            col2.Width = new GridLength(1, GridUnitType.Star);
            DialogGrid.ColumnDefinitions.Add(col1);
            DialogGrid.ColumnDefinitions.Add(col2);
            // head row if needed:
            if(headerMessage != null && headerMessage != "")
            {
                // defining head row:
                RowDefinition headRow = new RowDefinition();
                headRow.Height = new GridLength(2,GridUnitType.Star);
                DialogGrid.RowDefinitions.Add(headRow);
                // defining information text box:
                TextBox textBox = new TextBox();
                textBox.Style = Application.Current.FindResource("AppStyle_TextBoxDialog") as Style;
                textBox.Margin = new Thickness(margin[0], margin[1], margin[2], margin[3]);
                textBox.Text = headerMessage;
                // positioning text block in datagrid:
                Grid.SetRow(textBox, 0);
                Grid.SetColumnSpan(textBox, 2);
                DialogGrid.Children.Add(textBox);
         
                return true; // header render accomplished with aditional information rendered
            }
            return false; // header render accomplished without additional information
        }
        //Backbone renderer - renders middle part of dialogbox where all those labels and textboxes are...
        private void RenderParametersRowsInDialog(bool headerRenderResult, double[] margin)
        {
            int rowCounter;
            if (headerRenderResult) rowCounter = 1;
            else rowCounter = 0; 

            for (int i = 0; i < _parametersIndexes.Count(); i++)
            {
                _dialogContext.StringParameter[i] = null; // add null to parameter collection
                
                this.Height += 30; // little extension of dialog box

                // creating necessary instances:
                RowDefinition headerRow = new RowDefinition();
                RowDefinition contentRow = new RowDefinition();
                
                Label label = new Label();
                
                TextBox textBox = new TextBox();
                Binding binding = new Binding();

                // styling and populating label control:
                label.Style = Application.Current.FindResource("AppStyle_Label") as Style;
                label.Content = _parametersIndexes[i] + ": ";
                label.Margin = new Thickness(margin[0],margin[1],margin[2],margin[3]);

                // configurating binding element:
                binding.Path = new PropertyPath(string.Format("StringParameter[{0}]",i));
                binding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
                binding.Mode = BindingMode.TwoWay;
                if (i < _validationRules.Count() && _validationRules[i] != null) //  setting of corresponding validation rule
                {
                    binding.ValidatesOnDataErrors = true;
                    binding.NotifyOnValidationError = true;
                    binding.ValidationRules.Add(_validationRules[i]);
                }
                // styling textBox contlor:
                textBox.Style = Application.Current.FindResource("AppStyle_TextBox") as Style;
                textBox.Margin = new Thickness(margin[0], margin[1], margin[2], margin[3]);
                // binding textbox object:
                textBox.SetBinding(TextBox.TextProperty, binding);
                // adding potentially validation error generating object to dialogBox buffer <- I konow it can be optimized to add only those which has actually a validation rule implemented... maybe later
                _validatedObjects.Add(textBox);

                // adding new "labeling" row:
                headerRow.Height = new GridLength(1, GridUnitType.Star);
                DialogGrid.RowDefinitions.Add(headerRow);
                // positioning label in dialogbox grid:
                Grid.SetRow(label, rowCounter);
                Grid.SetColumnSpan(label, 2);
                // adding new "content" row:
                contentRow.Height = new GridLength(1, GridUnitType.Star);
                DialogGrid.RowDefinitions.Add(contentRow);
                // positioning textbox in dialogbox grid:
                Grid.SetRow(textBox, ++rowCounter);
                Grid.SetColumnSpan(textBox, 2);
                // adding label and textbox object to dialogbox grid's children collection:
                DialogGrid.Children.Add(label);
                DialogGrid.Children.Add(textBox);
                
                rowCounter++;
            }
        }
        // The bottom renderer - renders last row with control buttons in it.
        private void RenderControlRowInDialog(double[] margin)
        {
            this.Height += 60; // extension of dialog box

            int lastRowIndex = this.DialogGrid.RowDefinitions.Count;

            // creating necessary instances:
            RowDefinition controlRow = new RowDefinition();
            Button cancelButton = new Button();
            Button confirmButton = new Button();
            // defining and adding last row:
            controlRow.Height = new GridLength(60);
            DialogGrid.RowDefinitions.Add(controlRow);
            // defining cancell button:
            cancelButton.Style = Application.Current.FindResource("AppStyle_SideMenuButton") as Style;
            cancelButton.Content = "Anuluj";
            cancelButton.ToolTip = "Zaniechaj wprowadzania danych.";
            cancelButton.Foreground = Brushes.DarkRed;
            cancelButton.Margin = new Thickness(margin[0], margin[1], margin[2], margin[3]);
            cancelButton.Command = new CommandRelay(Cancel, () => true);
            // defining confirm button:
            confirmButton.Style = Application.Current.FindResource("AppStyle_SideMenuButton") as Style;
            confirmButton.Content = "Zastosuj";
            confirmButton.ToolTip = "Potwierdź wprowadzone dane.";
            confirmButton.Foreground = Brushes.DarkRed;
            confirmButton.Margin = new Thickness(margin[0], margin[1], margin[2], margin[3]);
            confirmButton.Command = new CommandRelay(Confirm, CanExecuteConfirm);
            
            // positioning both buttons in grid:
            Grid.SetRow(cancelButton, lastRowIndex);
            Grid.SetColumn(cancelButton, 0);

            Grid.SetRow(confirmButton, lastRowIndex);
            Grid.SetColumn(confirmButton, 1);

            // adding buttons to grid's childrens collection:
            DialogGrid.Children.Add(cancelButton);
            DialogGrid.Children.Add(confirmButton);
        }
        // command method for cancellation button.
        public void Cancel()
        {
            this.DialogResult = false;
            this.Close();
        }
        // command method for confirmation button (doh!).
        public void Confirm()
        {
            this.DialogResult = true;
            this.Close();
        }
        // confirmation command has it's condition checker: 
        public bool CanExecuteConfirm()
        {
            bool result = true;

            foreach(TextBox textBox in _validatedObjects)
            {
                if(Validation.GetHasError(textBox)) result = false;
            }

            return result;
        }
    }
}
