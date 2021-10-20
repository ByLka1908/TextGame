using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows;


namespace BOMJ_Simulator
{
    class WPF_Misc
    {
		//Метод отвечающий за открытие нового окна
		public static void OpenNewWindow(Window currentWindow, Window windowToOpen, bool isCloseCurrentWindow = true, bool isCopyPreviousParameters = true)
		{
			if (isCopyPreviousParameters)
			{
				windowToOpen.Top = currentWindow.Top;
				windowToOpen.Left = currentWindow.Left;
				windowToOpen.Height = currentWindow.ActualHeight;
				windowToOpen.Width = currentWindow.ActualWidth;

				windowToOpen.Title = currentWindow.Title;
				windowToOpen.BorderThickness = currentWindow.BorderThickness;

				windowToOpen.WindowStyle = currentWindow.WindowStyle;
				windowToOpen.ResizeMode = currentWindow.ResizeMode;
			}

			windowToOpen.WindowStartupLocation = WindowStartupLocation.CenterScreen;

			if (isCloseCurrentWindow)
				currentWindow.Close();

			windowToOpen.Show();
		}
		// Метод, отвечающий за открытие нового маленького окна ("пауза").
		public static void OpenPauseWindow(Window currentWindow, Window windowToOpen, bool isCloseCurrentWindow = false)
		{
			windowToOpen.Top = currentWindow.Top * 1.5;
			windowToOpen.Left = currentWindow.Left * 1.6;
			windowToOpen.Height = 400;
			windowToOpen.Width = 800;

			windowToOpen.Title = currentWindow.Title;

			windowToOpen.BorderThickness = new Thickness(2, 10, 2, 2);

			windowToOpen.WindowStyle = currentWindow.WindowStyle;
			windowToOpen.ResizeMode = currentWindow.ResizeMode;

			if (isCloseCurrentWindow)
				currentWindow.Close();

			windowToOpen.ShowDialog();
		}
		// Нужные вещи для работы следующего метода
		[DllImport("gdi32.dll", EntryPoint = "DeleteObject")]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool DeleteObject([In] IntPtr hObject);

		// Метод, отвечающий за перенос фокуса на указанное окно
		public static void FocusWindow(Window windowToFocus)
		{
			windowToFocus.Owner = Application.Current.MainWindow;
			windowToFocus.ShowInTaskbar = false;
		}
	}
}
