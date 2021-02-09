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
using Converter.Properties;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;
using System.IO;

namespace Converter
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
        String filenames;
        private void BtnSelectTTS_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Please Select the TTS exe File";

            if (openFileDialog.ShowDialog() == true)
            {
                if (System.IO.Path.GetExtension(openFileDialog.FileName) == ".exe")
                {
                    Settings.Default.TTSApp = openFileDialog.FileName;
                    Settings.Default.Save();
                    MessageBox.Show(Settings.Default.TTSApp + " has been selected", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Please Select a .exe File", "Wrong Selection", MessageBoxButton.OK, MessageBoxImage.Error);
                }

            }
        }

        private void BtnSelect_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            if (openFileDialog.ShowDialog() == true)
            {
                filenames = openFileDialog.FileName;
                if (System.IO.Path.GetExtension(filenames) == ".gif")
                {
                    System.IO.File.Copy(filenames, @"Temp/A.gif");
                }
                else
                {
                    System.IO.File.Copy(filenames, @"Temp/B.mp3");
                }
            }
        }

        private void BtnSubmit_Click(object sender, RoutedEventArgs e)
        {
            //Application application = Application.AttachOrLaunch(new ProcessStartInfo(Settings.Default.TTSApp));
            //TestStack.White.UIItems.WindowItems.Window window = application.GetWindow("BinaryMark Text to MP3 Converter 2.0", InitializeOption.NoCache);
            //TestStack.White.UIItems.TextBox textBox = window.Get<TestStack.White.UIItems.TextBox>(TestStack.White.UIItems.Finders.SearchCriteria.Indexed(0));
            //textBox.Enter(TxtContent.Text);
            //TestStack.White.UIItems.Button button = window.Get<TestStack.White.UIItems.Button>(TestStack.White.UIItems.Finders.SearchCriteria.ByText("Convert to MP3..."));
            //button.Click();
        }

        private void BtnSelectTwo_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Please Select a GIF File";

            if (openFileDialog.ShowDialog() == true)
            {
                if (System.IO.Path.GetExtension(openFileDialog.FileName) == ".gif")
                {
                    gifName = string.Format("\"{0}\"", openFileDialog.FileName);

                    MessageBox.Show(gifName + " has been selected", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Please Select a GIF File", "Wrong Selection", MessageBoxButton.OK, MessageBoxImage.Error);
                }

            }
        }

        private void BtnSubmitTwo_Click(object sender, RoutedEventArgs e)
        {
            string fileName = "Video-" + DateTime.Now.ToString("yyyy-MM-dd-hh-mm-ss");
            try
            {

                Process.Start("cmd", @"/c ffmpeg -ss 1 -i " + @videoName + @" -ignore_loop 0 -i " + gifName + @" -filter_complex ""[1:v]hue = s = 0[base];[0:v] scale=iw/2:-1[vid];[base] [vid] overlay=(W-w)/2:(H-h)/2:shortest=1"" -codec:v libx264 -c:a copy Temp\Video\" + fileName + ".mp4");

                if (MessageBox.Show("File has been generated Sucessfully. Please Visit this file Temp\\Video" + fileName, "Information", MessageBoxButton.OK, MessageBoxImage.Information) == MessageBoxResult.OK)
                {

                }

            }
            catch (Exception)
            {
                MessageBox.Show("Please Select the Correct file", "Warning", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnGenerate_Click(object sender, RoutedEventArgs e)
        {
            string fileName = "Video-" + DateTime.Now.ToString("yyyy-MM-dd-hh-mm-ss");

            try
            {

                Process.Start("cmd", @"/c ffmpeg -i Temp/B.mp3 -ignore_loop 0 -i Temp/A.gif -vf ""scale = trunc(iw / 2) * 2:trunc(ih / 2) * 2"" -shortest -strict -2 -c:v libx264 -threads 4 -c:a aac -b:a 192k -pix_fmt yuv420p -shortest Temp\Output\" + fileName + ".mp4");
                if (MessageBox.Show("File has been generated Sucessfully. Please Visit the Temp\\Output" + fileName, "Information", MessageBoxButton.OK, MessageBoxImage.Information) == MessageBoxResult.OK)
                {
                    System.IO.File.Delete(@"Temp/A.gif");
                    System.IO.File.Delete(@"Temp/B.mp3");
                }

            }
            catch (Exception)
            {
                MessageBox.Show("Please Select the Correct file", "Warning", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnAddLogo_Click(object sender, RoutedEventArgs e)
        {
            string fileName = "Video-" + DateTime.Now.ToString("yyyy-MM-dd-hh-mm-ss");
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Please Select a .mp4 File";

            if (openFileDialog.ShowDialog() == true)
            {
                if (System.IO.Path.GetExtension(openFileDialog.FileName) == ".mp4")
                {
                    Process.Start("cmd", @"/c ffmpeg -i " + string.Format("\"{0}\"", openFileDialog.FileName) + @" -i " + Settings.Default.logo + @" -map 1 -map 0 -c copy -disposition:0 attached_pic Temp\Logo\" + fileName + ".mp4");


                    MessageBox.Show(fileName + " has been Generated", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Please Select a GIF File", "Wrong Selection", MessageBoxButton.OK, MessageBoxImage.Error);
                }

            }
        }

        private void BtnLogo_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Please Select a .png or jpeg File";

            if (openFileDialog.ShowDialog() == true)
            {
                if (System.IO.Path.GetExtension(openFileDialog.FileName) == ".png" | System.IO.Path.GetExtension(openFileDialog.FileName) == ".jpeg")
                {
                    Settings.Default.logo = openFileDialog.FileName;
                    Settings.Default.Save();

                    MessageBox.Show(Settings.Default.logo + " has been selected", "Information", MessageBoxButton.OK, MessageBoxImage.Information);

                    var brush = new ImageBrush();
                    brush.ImageSource = new BitmapImage(new Uri(Settings.Default.logo, UriKind.RelativeOrAbsolute));
                    BtnLogo.Background = brush;
                }
                else
                {
                    MessageBox.Show("Please Select a Logo File", "Wrong Selection", MessageBoxButton.OK, MessageBoxImage.Error);
                }

            }
        }

        string gifName, videoName;

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
           
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!System.IO.Directory.Exists(@"Temp/Output"))
                {
                    Directory.CreateDirectory(@"Temp/Output");
                }
                if (!System.IO.Directory.Exists(@"Temp/Logo"))
                {
                    Directory.CreateDirectory(@"Temp/Logo");
                }
                if (!System.IO.Directory.Exists(@"Temp/Video"))
                {
                    Directory.CreateDirectory(@"Temp/Video");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error In Installing the other Components " + ex);
            }


            try
            {
                if (Settings.Default.logo != "")
                {
                    var brush = new ImageBrush();
                    brush.ImageSource = new BitmapImage(new Uri(Settings.Default.logo, UriKind.RelativeOrAbsolute));
                    BtnLogo.Background = brush;
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Please Select the Logo File" + ex);

            }
        }

        private void BtnSelectVideo_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Please Select a .mp4 File";

            if (openFileDialog.ShowDialog() == true)
            {
                if (System.IO.Path.GetExtension(openFileDialog.FileName) == ".mp4")
                {
                    videoName = string.Format("\"{0}\"", openFileDialog.FileName);
                    MessageBox.Show(videoName + " has been selected", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Please Select a .mp4 File", "Wrong Selection", MessageBoxButton.OK, MessageBoxImage.Error);
                }

            }

        }
    }
}
