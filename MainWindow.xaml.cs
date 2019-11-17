using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FileExplorer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// //////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// </summary>
        //переменные
        private System.Collections.ObjectModel.ObservableCollection<FileInfo> ownvar_listOfFiles = new System.Collections.ObjectModel.ObservableCollection<FileInfo>();

        /// <summary>
        /// //////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// </summary>


        private string GetReadableFileSizeString(long fileSizeInBytes)
        {
            int i = -1;
            List<String> byteUnits = new List<string>()
            {
                "kB",  "MB",  "GB",  "TB", "PB", "EB", "ZB", "YB"
            };
            do
            {
                fileSizeInBytes = fileSizeInBytes / 1024;
                i++;
            } while (fileSizeInBytes > 1024);

            return Math.Max(fileSizeInBytes, 0.1).ToString() + byteUnits[i];
        }


        public MainWindow()
        {
            InitializeComponent();
        }

        private void Btn_Open_Click(object sender, RoutedEventArgs e)
        {
            //Clear all items
            ownvar_listOfFiles.Clear();
            list_View1.Items.Clear();

            FolderBrowserDialog fbd = new FolderBrowserDialog();

            if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                path_displayer.Text = fbd.SelectedPath;
               
                foreach (string item in System.IO.Directory.GetFiles(fbd.SelectedPath))
                {
                    
                    FileInfo fi = new FileInfo(item);
                    //ownvar_listOfFiles.Add(fi);//Add file name to list
                    list_View1.Items.Add(fi);


                }
            }
        }

        private void Btn_Back_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Btn_Fwd_Click(object sender, RoutedEventArgs e)
        {

        }

  

        private void List_View1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            String tmp_Fullpath = list_View1.SelectedItem.ToString();
            FileInfo fi = new FileInfo(tmp_Fullpath);
            slctd_FileSize.Text = GetReadableFileSizeString(fi.Length);
            slctd_FullPath.Text = fi.FullName;
        }

        private void opn_file_Click(object sender, RoutedEventArgs e)
        {
            if (list_View1.SelectedItem != null)
            {
                String tmp_Fullpath = list_View1.SelectedItem.ToString();
                //FileInfo fi = new FileInfo(tmp_Fullpath);
                try
                {
                    System.Diagnostics.Process.Start(tmp_Fullpath);
                }
                catch (System.ComponentModel.Win32Exception)
                {
                    System.Windows.MessageBox.Show("Операция была отменена пользователем");                    
                }

                }
                
            }
        }
    }


















    public class Item_For_Listview
    {
         public BitmapSource Picture { get; set; }
         public string Description { get; set; }
               
    }

    public class MyViewModel_For_Listview
    {
        public System.Collections.ObjectModel.ObservableCollection<Item_For_Listview> Images { get; set; }

        public ICommand ButtonClicked { get; set; }

        //... Logic to populate the images
    }


