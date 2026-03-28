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

        // Reset previous results
        TxtTranscript.Text = string.Empty;
        TxtSummary.Text = string.Empty;
        TxtQuiz.Text = string.Empty;

        // UI busy state
        SetUiState(false);
        StatusLabel.Text = "Processing... Please wait.";

        try
        {
            await RunProcessingTask(inputPath);
            await LoadResultsAsync();
            StatusLabel.Text = "Processing complete! Results loaded.";
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

    private async Task LoadResultsAsync()
    {
        // Define paths based on project root (assuming output/ is in the same directory as scripts/)
        string projectRoot = System.IO.Path.GetFullPath(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "../../../"));
        
        // Fallback for compiled/published app
        if (!Directory.Exists(System.IO.Path.Combine(projectRoot, "output")))
        {
            projectRoot = AppDomain.CurrentDomain.BaseDirectory;
        }

        string transcriptPath = System.IO.Path.Combine(projectRoot, "output/transcripts/transcript.txt");
        string summaryPath = System.IO.Path.Combine(projectRoot, "output/summaries/summary.md");
        string quizPath = System.IO.Path.Combine(projectRoot, "output/quizzes/quiz.md");

        TxtTranscript.Text = await TryReadFileAsync(transcriptPath);
        TxtSummary.Text = await TryReadFileAsync(summaryPath);
        TxtQuiz.Text = await TryReadFileAsync(quizPath);
    }

    private async Task<string> TryReadFileAsync(string path)
    {
        try
        {
            if (File.Exists(path))
            {
                return await File.ReadAllTextAsync(path);
            }
            return $"[Error] Result file not found at: {path}";
        }
        catch (System.Exception ex)
        {
            return $"[Error] Could not read file: {ex.Message}";
        }
    }

    private Task RunProcessingTask(string inputPath)
    {
        return Task.Run(() =>
        {
            // Determine venv python path
            string projectRoot = System.IO.Path.GetFullPath(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "../../../"));
            
            string venvPath = System.IO.Path.Combine(projectRoot, ".venv/Scripts/python.exe");
            string scriptPath = System.IO.Path.Combine(projectRoot, "scripts/process.py");

            // If not found in dev path, try current directory (for published/installed app)
            if (!File.Exists(venvPath))
            {
                venvPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ".venv/Scripts/python.exe");
                scriptPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "scripts/process.py");
            }

            ProcessStartInfo start = new ProcessStartInfo
            {
                FileName = venvPath,
                Arguments = $"\"{scriptPath}\" \"{inputPath}\"",
                WorkingDirectory = File.Exists(venvPath) ? projectRoot : AppDomain.CurrentDomain.BaseDirectory,
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