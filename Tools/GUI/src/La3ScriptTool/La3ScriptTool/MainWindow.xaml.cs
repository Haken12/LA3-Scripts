using System;
using System.Diagnostics;
using AdonisUI.Controls;
using System.IO;
using System.Runtime.InteropServices;

namespace La3ScriptTool
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : AdonisWindow
    {
        private string _input= String.Empty;
        private string _forImport = String.Empty;
        private string _extracted = String.Empty;
        private string _tools = string.Empty;
        private string _forEdit= String.Empty;
        private string _reformat = string.Empty;
        private string _reformatDebug = String.Empty;
        private string _encode = String.Empty;
        private string _encodeDebug = String.Empty;
        private string _strBin = string.Empty;
        private string _strBinDebug = string.Empty;
        private string _logs = string.Empty;

        public MainWindow()
        {
            InitializeComponent();
            CreateDirectories();
            MoveToImport();
        }

        public void CreateDirectories()
        {
            _tools = $@"{Environment.CurrentDirectory}\tools";
            _input = $@"{Environment.CurrentDirectory}\input";
            _forImport = $@"{Environment.CurrentDirectory}\0-import";
            _extracted = $@"{Environment.CurrentDirectory}\1-extracted";
            _forEdit = $@"{Environment.CurrentDirectory}\2-for-editing";
            _reformat = $@"{Environment.CurrentDirectory}\3-reformat";
            _reformatDebug = $@"{Environment.CurrentDirectory}\3-reformat-DEBUG";
            _encode = $@"{Environment.CurrentDirectory}\4-encoded";
            _encodeDebug = $@"{Environment.CurrentDirectory}\4-encoded-DEBUG";
            _strBin = $@"{Environment.CurrentDirectory}\5-strBin";
            _strBinDebug = $@"{Environment.CurrentDirectory}\5-strBin";
            _logs = $@"{Environment.CurrentDirectory}\0-logs";

            if (!Directory.Exists(_tools)) Directory.CreateDirectory(_tools);
            if (!Directory.Exists(_input)) Directory.CreateDirectory(_input);
            if (!Directory.Exists(_forImport)) Directory.CreateDirectory(_forImport);
            if (!Directory.Exists(_extracted)) Directory.CreateDirectory(_extracted);
            if (!Directory.Exists(_forEdit)) Directory.CreateDirectory(_forEdit);
            if (!Directory.Exists(_reformat)) Directory.CreateDirectory(_reformat);
            if (!Directory.Exists(_reformatDebug)) Directory.CreateDirectory(_reformatDebug);
            if (!Directory.Exists(_encode)) Directory.CreateDirectory(_encode);
            if (!Directory.Exists(_encodeDebug)) Directory.CreateDirectory(_encodeDebug);
            if (!Directory.Exists(_strBin)) Directory.CreateDirectory(_strBin);
            if (!Directory.Exists(_strBinDebug)) Directory.CreateDirectory(_strBinDebug);
            if (!Directory.Exists(_logs)) Directory.CreateDirectory(_logs);
        }

        public void MoveToImport()
        {
            foreach (var file in Directory.EnumerateFiles(_forImport))
            {
                File.Delete(file);
            }
            foreach (var file in Directory.EnumerateFiles(_input))
            {
                File.Copy(file, $@"{_forImport}\{Path.GetFileName(file)}");
            }
        }

        private void Step1Extract_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            foreach (var file in Directory.EnumerateFiles(_forImport, "*.bin"))
            {
                ProcessStartInfo start = new ProcessStartInfo();
                start.FileName = $@"{_tools}\dumper.exe";
                start.Arguments = $@"{file} {_extracted}\{Path.GetFileName(file)}.txt";
                start.WindowStyle = ProcessWindowStyle.Hidden;
                start.CreateNoWindow = true;

                Process.Start(start);

                MessageBox.Show("Files Extracted", "", MessageBoxButton.OK);
            }
        }

        private void Step2FriendlyScript_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            foreach (var file in Directory.EnumerateFiles(_extracted, "*.txt"))
            {
                ProcessStartInfo start = new ProcessStartInfo();
                start.FileName = $@"{_tools}\ext2edit.exe";
                start.Arguments = $@"{file} {_forEdit}\{Path.GetFileName(file)}";
                start.WindowStyle = ProcessWindowStyle.Hidden;
                start.CreateNoWindow = true;

                Process.Start(start);
            }
            MessageBox.Show("Files Created for Editing", "", MessageBoxButton.OK);
        }


        private void Step3Format_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            foreach (var file in Directory.EnumerateFiles(_forEdit, "*.txt"))
            {
                ProcessStartInfo start = new ProcessStartInfo();
                start.FileName = $@"{_tools}\formatter.exe";
                start.Arguments = $@"{file} {_reformat}\{Path.GetFileName(file)}";
                start.WindowStyle = ProcessWindowStyle.Hidden;
                start.CreateNoWindow = true;

                Process.Start(start);
            }
            MessageBox.Show("Files Created for Editing", "", MessageBoxButton.OK);
        }

        private void Step3FormatDebug_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            foreach (var file in Directory.EnumerateFiles(_forEdit, "*.txt"))
            {
                ProcessStartInfo start = new ProcessStartInfo();
                start.FileName = $@"{_tools}\formDebug.exe";
                start.Arguments = $@"{file} {_reformatDebug}\{Path.GetFileName(file)}";
                start.WindowStyle = ProcessWindowStyle.Hidden;
                start.CreateNoWindow = true;

                Process.Start(start);
            }
            MessageBox.Show("Files Created for Editing", "", MessageBoxButton.OK);
        }

        private void Step4Encode_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            foreach (var file in Directory.EnumerateFiles(_reformat, "*.txt"))
            {
                ProcessStartInfo start = new ProcessStartInfo();
                start.FileName = $@"{_tools}\encoder.exe";
                start.Arguments = $@"{file} {_encode}\{Path.GetFileName(file)}";
                start.WindowStyle = ProcessWindowStyle.Hidden;
                start.CreateNoWindow = true;

                Process.Start(start);
            }
            MessageBox.Show("Files Created for Editing", "", MessageBoxButton.OK);
        }

        private void Step4EncodeDebug_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            foreach (var file in Directory.EnumerateFiles(_reformatDebug, "*.txt"))
            {
                ProcessStartInfo start = new ProcessStartInfo();
                start.FileName = $@"{_tools}\encoder.exe";
                start.Arguments = $@"{file} {_encodeDebug}\{Path.GetFileName(file)}";
                start.WindowStyle = ProcessWindowStyle.Hidden;
                start.CreateNoWindow = true;

                Process.Start(start);
            }
            MessageBox.Show("Files Created for Editing", "", MessageBoxButton.OK);
        }

        private void Step5ToBin_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            foreach (var file in Directory.EnumerateFiles(_encode, "*.txt"))
            {
                ProcessStartInfo start = new ProcessStartInfo();
                start.FileName = $@"{_tools}\strTabGen.exe";
                start.Arguments = $@"{file} {_strBin}\{Path.GetFileNameWithoutExtension(file)}";
                start.WindowStyle = ProcessWindowStyle.Hidden;
                start.CreateNoWindow = true;

                Process.Start(start);
            }
            MessageBox.Show("Files Created for Editing", "", MessageBoxButton.OK);
        }

        

        private void Step5ConvBinDebug_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            foreach (var file in Directory.EnumerateFiles(_encodeDebug, "*.txt"))
            {
                ProcessStartInfo start = new ProcessStartInfo();
                start.FileName = $@"{_tools}\strTabGen.exe";
                start.Arguments = $@"{file} {_strBinDebug}\{Path.GetFileNameWithoutExtension(file)}";
                start.WindowStyle = ProcessWindowStyle.Hidden;
                start.CreateNoWindow = true;

                Process.Start(start);
            }
            MessageBox.Show("Files Created for Editing", "", MessageBoxButton.OK);
        }
    }
}
