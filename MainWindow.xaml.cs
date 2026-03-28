using System.Diagnostics;
using System.IO;
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

namespace BreakNote;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void BtnSelectFile_Click(object sender, RoutedEventArgs e)
    {
        var openFileDialog = new Microsoft.Win32.OpenFileDialog
        {
            Filter = "Audio files (*.mp3;*.wav;*.m4a)|*.mp3;*.wav;*.m4a|All files (*.*)|*.*",
            Title = "Select Audio File"
        };

        if (openFileDialog.ShowDialog() == true)
        {
            TxtFilePath.Text = openFileDialog.FileName;
            BtnProcess.IsEnabled = true;
            StatusLabel.Text = $"File Selected: {System.IO.Path.GetFileName(openFileDialog.FileName)}";
        }
    }

    private async void BtnProcess_Click(object sender, RoutedEventArgs e)
    {
        string inputPath = TxtFilePath.Text;
        if (string.IsNullOrEmpty(inputPath) || !File.Exists(inputPath))
        {
            MessageBox.Show("Please select a valid file first.");
            return;
        }

        // UI busy state
        SetUiState(false);
        StatusLabel.Text = "Processing... Please wait.";

        try
        {
            await RunProcessingTask(inputPath);
            StatusLabel.Text = "Processing complete! Check the output tabs (Phase C).";
        }
        catch (System.Exception ex)
        {
            StatusLabel.Text = "Error during processing.";
            MessageBox.Show($"Error: {ex.Message}");
        }
        finally
        {
            SetUiState(true);
        }
    }

    private void SetUiState(bool enabled)
    {
        BtnSelectFile.IsEnabled = enabled;
        BtnProcess.IsEnabled = enabled;
    }

    private Task RunProcessingTask(string inputPath)
    {
        return Task.Run(() =>
        {
            // Determine venv python path
            string venvPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "../../../.venv/Scripts/python.exe");
            
            // If running from bin/Debug/net10.0-windows, we might need to adjust relative path
            string scriptPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "../../../scripts/process.py");

            // Build full paths for reliability
            string fullVenvPath = System.IO.Path.GetFullPath(venvPath);
            string fullScriptPath = System.IO.Path.GetFullPath(scriptPath);

            // If not found in dev path, try current directory (for published/installed app)
            if (!File.Exists(fullVenvPath))
            {
                fullVenvPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ".venv/Scripts/python.exe");
                fullScriptPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "scripts/process.py");
            }

            ProcessStartInfo start = new ProcessStartInfo
            {
                FileName = fullVenvPath,
                Arguments = $"\"{fullScriptPath}\" \"{inputPath}\"",
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                CreateNoWindow = true
            };

            using (Process? process = Process.Start(start))
            {
                if (process == null)
                {
                    throw new System.Exception("Failed to start Python process.");
                }

                using (StreamReader reader = process.StandardOutput)
                {
                    string result = reader.ReadToEnd();
                    // In Phase B, we just wait for exit. 
                    // Future: capture 'result' for logging.
                }
                process.WaitForExit();
                
                if (process.ExitCode != 0)
                {
                    string errors = process.StandardError.ReadToEnd();
                    throw new System.Exception($"Python process exited with code {process.ExitCode}. Errors: {errors}");
                }
            }
        });
    }
}