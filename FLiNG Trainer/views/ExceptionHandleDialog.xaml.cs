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
using System.Windows.Shapes;
using Wpf.Ui.Controls;

namespace FLiNG_Trainer.views
{
    public partial class ExceptionHandleDialog : FluentWindow
    {
        private string _exceptionMessage;
        public string ExceptionMessage 
        {
            get { return _exceptionMessage; }
            set { _exceptionMessage = value; }
        }

        public ExceptionHandleDialog()
        {
            InitializeComponent();
        }

        private void copyExciptionMessageButton_Click(object sender, RoutedEventArgs e)
        {
            if (ExceptionMessage != null)
            {
                Clipboard.SetDataObject(ExceptionMessage);
            }
        }

        private void openFiLNGTrainerGithubButton_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/Melon-Studio/FLiNG-Trainer-Collection/issues");
        }

        private void closeThisDialogButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void FluentWindow_Loaded(object sender, RoutedEventArgs e)
        {
            messageText.Text += ExceptionMessage;
        }
    }
}
