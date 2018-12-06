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
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.IO;
using Microsoft.Win32;

namespace AccessWeb
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<Domain> list_Domain;
        public MainWindow()
        {
            InitializeComponent();
            InitListView_domain();
            DatePicker_interval.SelectedDateFormat = DatePickerFormat.Short;
        }

        public void InitListView_domain()
        {
            list_Domain = new List<Domain>();
            listView_domain.ItemsSource = list_Domain;
        }

        private void button_deleteAll_Click(object sender, RoutedEventArgs e)
        {
            list_Domain.Clear();
            listView_domain.Items.Refresh();
        }

        private void button_delete_Click(object sender, RoutedEventArgs e)
        {
            if (listView_domain.SelectedItem != null)
            {
                list_Domain.RemoveAt(listView_domain.SelectedIndex);
            }
            listView_domain.Items.Refresh();
        }

        private void button_Import_Click(object sender, RoutedEventArgs e)
        {
            TextRange textRange = new TextRange(richTextBox_domain.Document.ContentStart, richTextBox_domain.Document.ContentEnd);
            string[] arrayDomain = textRange.Text.Split(new string[] {"\r\n"}, StringSplitOptions.None);

            int nCount = 0;
            

            foreach (var item in arrayDomain)
            {
                if (item != String.Empty && item.Contains("."))
                {


                    Domain domain;
                    if (list_Domain.Count == 0)
                    {
                        domain = new Domain(list_Domain.Count+1, item);    
                    }
                    else
                    {
                        domain = new Domain(list_Domain.Last().ID + 1, item);
                    }
                    list_Domain.Add(domain);
                    listView_domain.Items.Refresh();
                    nCount++;
                }
            }

            textRange.Text = String.Empty;

            string strInfo = String.Format("成功导入 {0} 个地址。", nCount);
            TextRange textRange_log = new TextRange(richTextBox_log.Document.ContentStart, richTextBox_log.Document.ContentEnd);
            //MessageBox.Show(strInfo, "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            textRange_log.Text = strInfo +"\r\n"+ textRange_log.Text;
        }

        private void button_importTXT_Click(object sender, RoutedEventArgs e)
        {
            LoadTxt();
        }

        private void button_run_Click(object sender, RoutedEventArgs e)
        {
            //richTextBox_domain.
        }

        private void button_stop_Click(object sender, RoutedEventArgs e)
        {

        }

        private void LoadTxt()
        {
            Microsoft.Win32.OpenFileDialog openfile = new Microsoft.Win32.OpenFileDialog();
            openfile.DefaultExt = ".txt";
            openfile.Filter = "Text documents (.txt)|*.txt";
            bool? result = openfile.ShowDialog();
            if (result == true)
            {
                //textBlock.Text = openfile.FileName;
                StreamReader readfile = new StreamReader(openfile.FileName);

                TextRange textRange = new TextRange(richTextBox_domain.Document.ContentStart, richTextBox_domain.Document.ContentEnd);
                textRange.Text = readfile.ReadToEnd();
            }
        }

    }
}
