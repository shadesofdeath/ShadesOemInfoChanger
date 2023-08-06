using Microsoft.Win32;
using System;
using System.Windows;
using Wpf.Ui.Controls;
using SophiApp.Helpers;
using System.IO;

namespace ShadesOemInfoChanger
{
    public partial class MainWindow : UiWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            Wpf.Ui.Appearance.Accent.ApplySystemAccent();
        }

        private void SelectLogoButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "BMP Files (*.bmp)|*.bmp|All files (*.*)|*.*",
                Title = "Select a BMP file"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                string selectedFilePath = openFileDialog.FileName;
                string targetFolderPath = @"C:\Windows\OEM";
                string targetFileName = "Logo.bmp";

                try
                {
                    if (!Directory.Exists(targetFolderPath))
                    {
                        Directory.CreateDirectory(targetFolderPath);
                    }

                    string targetFilePath = Path.Combine(targetFolderPath, targetFileName);

                    File.Copy(selectedFilePath, targetFilePath, true);

                    logoTextBox.Text = targetFilePath;
                }
                catch (Exception)
                {
                }
            }
        }

        private void Oem_Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string manufacturer = logoTextBox.Text;
                string modelNumber = modelNumberTextBox.Text;
                string model = modelTextBox.Text;
                string supportHours = supportHoursTextBox.Text;
                string supportPhone = supportPhoneTextBox.Text;
                string supportURL = supportURLTextBox.Text;

                RegHelper.SetValue(RegistryHive.LocalMachine, @"SOFTWARE\Microsoft\Windows\CurrentVersion\OEMInformation", "Logo", "C:\\Windows\\OEM\\Logo.bmp");
                RegHelper.SetValue(RegistryHive.LocalMachine, @"SOFTWARE\Microsoft\Windows\CurrentVersion\OEMInformation", "Manufacturer", modelNumber);
                RegHelper.SetValue(RegistryHive.LocalMachine, @"SOFTWARE\Microsoft\Windows\CurrentVersion\OEMInformation", "Model", model);
                RegHelper.SetValue(RegistryHive.LocalMachine, @"SOFTWARE\Microsoft\Windows\CurrentVersion\OEMInformation", "SupportHours", supportHours);
                RegHelper.SetValue(RegistryHive.LocalMachine, @"SOFTWARE\Microsoft\Windows\CurrentVersion\OEMInformation", "SupportPhone", supportPhone);
                RegHelper.SetValue(RegistryHive.LocalMachine, @"SOFTWARE\Microsoft\Windows\CurrentVersion\OEMInformation", "SupportURL", supportURL);
                
                System.Windows.MessageBox.Show("OEM Information Changed Successfully!", "ShadesOemInfoChanger", MessageBoxButton.OK);
            }
            catch (Exception)
            {
                System.Windows.MessageBox.Show("There was a problem changing the OEM Information :(", "ShadesOemInfoChanger", MessageBoxButton.OK);
            }
        }
    }
}
