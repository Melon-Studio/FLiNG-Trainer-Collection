using FLiNG_Trainer.views;
using System;
using System.Windows;
using System.Windows.Threading;

namespace FLiNG_Trainer
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            DispatcherUnhandledException += AppDispatcherUnhandledException;
        }

        private void AppDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            ExceptionHandleDialog dialog = new ExceptionHandleDialog();
            dialog.ExceptionMessage = e.Exception.Message.ToString();
            dialog.ShowDialog();
            e.Handled = true;
        }
    }
}
