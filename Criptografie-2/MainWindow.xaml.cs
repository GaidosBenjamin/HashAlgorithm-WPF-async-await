using Microsoft.Win32;
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

using System.Security.Cryptography;
using System.IO;

namespace Criptografie_2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void ComboBoxItem_Selected(object sender, RoutedEventArgs e)
        {

        }

        private void AlgorithmComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void InputChooseButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.DefaultExt = ".txt";

            Nullable<bool> result = fileDialog.ShowDialog();

            if (result == true)
            {
                string filename = fileDialog.FileName;
                this.InputTextBox.Text = filename;
            }
        }

        private async void RunButton_Click(object sender, RoutedEventArgs e)
        {
            await hashAlgorithmAsync();   
        }

        private async Task hashAlgorithmAsync()
        {
            HashAlgorithm hash = HashAlgorithm.Create(AlgorithmComboBox.Text);
            string message = MessageTextBox.Text;

            if (message != null && message != "") {
                await GetHashForString(hash, message);
            }
            else {
                FileStream fileStream = File.OpenRead(InputTextBox.Text);
                await GetHashForFile(hash, fileStream);
            }

        }

        private async Task GetHashForString(HashAlgorithm hashAlgorithm, string input)
        {
            byte[] data = await Task.Run(() => hashAlgorithm.ComputeHash(Encoding.UTF8.GetBytes(input)));
            await byteToString(data);
        }

        private async Task GetHashForFile(HashAlgorithm hashAlgorithm, FileStream stream)
        {
            byte[] data = await Task.Run(() => hashAlgorithm.ComputeHash(stream));
            await byteToString(data);
        }

        private async Task byteToString(byte[] data)
        {
            var sBuilder = new StringBuilder();

            for (int i = 0; i < data.Length; i++)
            {
                await Task.Run(() => sBuilder.Append(data[i].ToString("x2")));
            }

            this.OutputTextBox.Text = sBuilder.ToString();
        }

        
    }
}
