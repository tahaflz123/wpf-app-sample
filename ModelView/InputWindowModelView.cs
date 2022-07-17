using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Windows.Input;
using WpfApp1.View;

namespace WpfApp1.ModelView
{
    public class InputWindowModelView : INotifyPropertyChanged
    {

        private InputWindow _inputWindow { get; set; }

        private bool okClicked { get; set; }

        private int[] allowedCharacters = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 0 };

        public string errorText { get; set; }
        private string _inputText { get; set; }

        public string inputText { get => _inputText;
            set  {

                if (!checkInputText(value))
                {
                    errorText = "Please use integer";
                }
                errorText = "";
                PropertyChange("inputText");
                _inputText = value;
            }}


        public DelegateCommand _okClick, _cancelClick;

        public ICommand okClick => _okClick;

        public ICommand cancelClick => _cancelClick;

        public event PropertyChangedEventHandler PropertyChanged;

        protected void PropertyChange(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public InputWindowModelView() { }

        public InputWindowModelView(InputWindow inputWindow)
        {
            _inputWindow = inputWindow;
            _okClick = new DelegateCommand(ok_Click);
            _cancelClick = new DelegateCommand(cancel_Click);
            okClicked = false;
            _inputWindow.Focus();
        }



        public void ok_Click(object e)
        {
            okClicked = true;
        }

        public void cancel_Click(object e)
        {
            okClicked = true;
            closeWindow();
        }

        public void Show()
        {
            _inputWindow.Show();
        }

        public bool isOkClicked()
        {
            return okClicked ? true : false;
        }

        public void closeWindow()
        {
            _inputWindow.Close();
        }

        public string getInputText()
        {
            return _inputText;
        }

        public bool checkInputText(string value)
        {
            for (int i = 0; i < value.Length; i++)
            {
                if (!value.Contains((char) allowedCharacters[i]))
                {
                    return false;
                }
            }
            return true;
        }

    }
}
