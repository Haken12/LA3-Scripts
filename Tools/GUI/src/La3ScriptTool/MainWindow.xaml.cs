using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
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
using Path = System.IO.Path;

namespace La3ScriptConverter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static readonly LogManager _logManager = new LogManager();
        private readonly string[] _exes = new[] { "dumper.exe, ext2edit.exe, formatter.exe, encoder.exe, strTabGen.exe" };
        static readonly object _lock = new object();

        private string _tools = string.Empty;

        private string _input = String.Empty;
        private string _dumped = String.Empty;
        private string _forEdit = String.Empty;
        private string _reFormat = string.Empty;
        private string _reEncode = String.Empty;
        private string _output = string.Empty;


        public MainWindow()
        {
            InitializeComponent();
            Startup();
        }

        private void Startup()
        {
            _logManager.Show();
            CreateDirectories();
            
        }

        public void CreateDirectories()
        {
            _tools = $@"{Environment.CurrentDirectory}\bin";

            _input = $@"{Environment.CurrentDirectory}\0-input";
            _dumped = $@"{Environment.CurrentDirectory}\1-dumped";
            _forEdit = $@"{Environment.CurrentDirectory}\2-for-editing";
            _reFormat = $@"{Environment.CurrentDirectory}\3-reformat";
            _reEncode = $@"{Environment.CurrentDirectory}\4-encoded";
            _output = $@"{Environment.CurrentDirectory}\5-output";

            if (!Directory.Exists(_tools)) Directory.CreateDirectory(_tools);

            if (!Directory.Exists(_input)) Directory.CreateDirectory(_input);
            if (!Directory.Exists(_dumped)) Directory.CreateDirectory(_dumped);
            if (!Directory.Exists(_forEdit)) Directory.CreateDirectory(_forEdit);
            if (!Directory.Exists(_reFormat)) Directory.CreateDirectory(_reFormat);
            if (!Directory.Exists(_reEncode)) Directory.CreateDirectory(_reEncode);
            if (!Directory.Exists(_output)) Directory.CreateDirectory(_output);
        }


        private void CheckBinaries()
        {
            foreach (var exe in _exes)
            {
                if (!File.Exists($@"{Environment.CurrentDirectory}\bin\{exe}"))
                {
                    MessageBox.Show($"Missing {exe}");
                    this.Close();
                }
            }
        }

        private void Log(string data)
        {
            _logManager.txtLogging.Text = _logManager.txtLogging.Text + data + Environment.NewLine;
        }

        private void btnEmpty_Click(object sender, RoutedEventArgs e)
        {
            var result =
                MessageBox.Show("Are you SURE you want to clear out all of the folders? (Except for the tools)",
                    "Clear out folders?", MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
            if (result == MessageBoxResult.Yes)
            {
                foreach (var file in Directory.EnumerateFiles(_input))
                {
                    File.Delete(file);
                }
                foreach (var file in Directory.EnumerateFiles(_dumped))
                {
                    File.Delete(file);
                }
                foreach (var file in Directory.EnumerateFiles(_forEdit))
                {
                    File.Delete(file);
                }
                foreach (var file in Directory.EnumerateFiles(_reFormat))
                {
                    File.Delete(file);
                }
                foreach (var file in Directory.EnumerateFiles(_reEncode))
                {
                    File.Delete(file);
                }
                foreach (var file in Directory.EnumerateFiles(_output))
                {
                    File.Delete(file);
                }

                MessageBox.Show("Files Deleted");
                Log("Files Deleted");
            }
        }

        private void btnDump_Click(object sender, RoutedEventArgs e)
        {
            Log("=====Dump=====");
            var files = Directory.EnumerateFiles(_input, "*.bin").ToList();
            var count = 0;
            DisableControls();
            foreach (var file in files)
            {
                ProcessStartInfo start = new ProcessStartInfo();
                start.FileName = $@"{_tools}\dumper.exe";
                start.Arguments = $@"{file} {_dumped}\{Path.GetFileName(file)}.txt";
                start.WindowStyle = ProcessWindowStyle.Hidden;
                start.CreateNoWindow = true;

                BackgroundWorker worker = new BackgroundWorker();
                worker.DoWork += delegate
                {
                    Process proc = Process.Start(start);
                    proc.Start();
                    proc.WaitForExit();
                };
                worker.RunWorkerCompleted += delegate(object o, RunWorkerCompletedEventArgs args)
                {
                    count += 1;
                    Log($"Dumped {Path.GetFileName(file)}");
                    if (count >= files.Count())
                    {
                        MessageBox.Show("All files dumped", "", MessageBoxButton.OK);
                        EnableControls();
                    }
                    
                };
                worker.RunWorkerAsync();

            }
            
        }


        private void btnForEdit_Click(object sender, RoutedEventArgs e)
        {
            Log("=====For Edit=====");
            var files = Directory.EnumerateFiles(_dumped, "*.txt").ToList();
            var count = 0;
            DisableControls();
            foreach (var file in files)
            {
                ProcessStartInfo start = new ProcessStartInfo();
                start.FileName = $@"{_tools}\ext2edit.exe";
                start.Arguments = $@"{file} {_forEdit}\{Path.GetFileName(file)}";
                start.WindowStyle = ProcessWindowStyle.Hidden;
                start.CreateNoWindow = true;

                BackgroundWorker worker = new BackgroundWorker();
                worker.DoWork += delegate
                {
                    Process proc = Process.Start(start);
                    proc.Start();
                    proc.WaitForExit();
                };
                worker.RunWorkerCompleted += delegate (object o, RunWorkerCompletedEventArgs args)
                {
                    count += 1;
                    Log($"Modified for Edit {Path.GetFileName(file)}");
                    if (count >= files.Count())
                    {
                        MessageBox.Show("All files modified for edit", "", MessageBoxButton.OK);
                        EnableControls();
                    }

                };
                worker.RunWorkerAsync();
            }
            
        }

        private void btnFormat_Click(object sender, RoutedEventArgs e)
        {
            Log("=====Format=====");
            var files = Directory.EnumerateFiles(_forEdit, "*.txt").ToList();
            var count = 0;
            DisableControls();
            foreach (var file in files)
            {
                ProcessStartInfo start = new ProcessStartInfo();
                start.FileName = $@"{_tools}\formatter.exe";
                start.Arguments = $@"{file} {_reFormat}\{Path.GetFileName(file)}";
                start.WindowStyle = ProcessWindowStyle.Hidden;
                start.CreateNoWindow = true;

                BackgroundWorker worker = new BackgroundWorker();
                worker.DoWork += delegate
                {
                    Process proc = Process.Start(start);
                    proc.Start();
                    proc.WaitForExit();
                };
                worker.RunWorkerCompleted += delegate (object o, RunWorkerCompletedEventArgs args)
                {
                    count += 1;
                    Log($"Formatted {Path.GetFileName(file)}");
                    if (count >= files.Count())
                    {
                        MessageBox.Show("All files formatted", "", MessageBoxButton.OK);
                        EnableControls();
                    }

                };
                worker.RunWorkerAsync();
            }
            
        }

        private void btnEncode_Click(object sender, RoutedEventArgs e)
        {
            Log("=====Encode=====");
            var files = Directory.EnumerateFiles(_reFormat, "*.txt").ToList();
            var count = 0;
            DisableControls();
            foreach (var file in files)
            {
                ProcessStartInfo start = new ProcessStartInfo();
                start.FileName = $@"{_tools}\encoder.exe";
                start.Arguments = $@"{file} {_reEncode}\{Path.GetFileName(file)}";
                start.WindowStyle = ProcessWindowStyle.Hidden;
                start.CreateNoWindow = true;

                BackgroundWorker worker = new BackgroundWorker();
                worker.DoWork += delegate
                {
                    Process proc = Process.Start(start);
                    proc.Start();
                    proc.WaitForExit();
                };
                worker.RunWorkerCompleted += delegate (object o, RunWorkerCompletedEventArgs args)
                {
                    count += 1;
                    Log($"Encoded {Path.GetFileName(file)}");
                    if (count >= files.Count())
                    {
                        MessageBox.Show("All files Encoded", "", MessageBoxButton.OK);
                        EnableControls();
                    }

                };
                worker.RunWorkerAsync();
            }
            
        }

        private void btnConvert_Click(object sender, RoutedEventArgs e)
        {
            Log("=====Convert=====");
            var files = Directory.EnumerateFiles(_reEncode, "*.txt").ToList();
            var count = 0;
            DisableControls();
            foreach (var file in Directory.EnumerateFiles(_reEncode, "*.txt"))
            {
                ProcessStartInfo start = new ProcessStartInfo();
                start.FileName = $@"{_tools}\strTabGen.exe";
                start.Arguments = $@"{file} {_output}\{Path.GetFileNameWithoutExtension(file)}";
                start.WindowStyle = ProcessWindowStyle.Hidden;
                start.CreateNoWindow = true;

                BackgroundWorker worker = new BackgroundWorker();
                worker.DoWork += delegate
                {
                    Process proc = Process.Start(start);
                    proc.Start();
                    proc.WaitForExit();
                };
                worker.RunWorkerCompleted += delegate (object o, RunWorkerCompletedEventArgs args)
                {
                    count += 1;
                    Log($"Converted {Path.GetFileName(file)}");
                    if (count >= files.Count())
                    {
                        MessageBox.Show("All files Converted", "", MessageBoxButton.OK);
                        EnableControls();
                    }

                };
                worker.RunWorkerAsync();
            }
            
        }

        private void EnableControls()
        {
            btnConvert.IsEnabled = true;
            btnDump.IsEnabled = true;
            btnEmpty.IsEnabled = true;
            btnEncode.IsEnabled = true;
            btnForEdit.IsEnabled = true;
            btnFormat.IsEnabled = true;
            pgbProgress.Visibility = Visibility.Hidden;
            pgbProgress.IsIndeterminate = false;
        }

        private void DisableControls()
        {
            btnConvert.IsEnabled = false;
            btnDump.IsEnabled = false;
            btnEmpty.IsEnabled = false;
            btnEncode.IsEnabled = false;
            btnForEdit.IsEnabled = false;
            btnFormat.IsEnabled = false;
            pgbProgress.Visibility = Visibility.Visible;
            pgbProgress.IsIndeterminate = true;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            foreach (var exe in _exes)
            {
                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.UseShellExecute = false;
                startInfo.CreateNoWindow = true;
                startInfo.FileName = "taskkill";
                startInfo.Arguments = $"/F /IM {exe}";
                Process.Start(startInfo);
            }
            _logManager.Close();
            
        }
    }
}
