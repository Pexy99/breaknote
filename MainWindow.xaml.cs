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
        TxtLog.Text = string.Empty; // Clear logs for new run

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
        var tcs = new TaskCompletionSource<bool>();

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
            Arguments = $"-u \"{scriptPath}\" \"{inputPath}\"",
            WorkingDirectory = File.Exists(venvPath) ? projectRoot : AppDomain.CurrentDomain.BaseDirectory,
            UseShellExecute = false,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            CreateNoWindow = true
        };

        Process? process = new Process { StartInfo = start, EnableRaisingEvents = true };

        process.OutputDataReceived += (s, e) => {
            if (e.Data != null) {
                Dispatcher.Invoke(() => {
                    TxtLog.AppendText(e.Data + Environment.NewLine);
                    
                    // Simple regex to catch Whisper's verbose output: [00:00.000 --> 00:05.000] Text
                    var match = System.Text.RegularExpressions.Regex.Match(e.Data, @"^\[\d{2}:.*-->.*\d{2}:.*\](.*?)$");
                    if (match.Success) {
                        TxtTranscript.AppendText(match.Groups[1].Value.Trim() + " ");
                    }
                });
            }
        };
        process.ErrorDataReceived += (s, e) => {
            if (e.Data != null) Dispatcher.Invoke(() => TxtLog.AppendText("[Error] " + e.Data + Environment.NewLine));
        };
        
        process.Exited += (s, e) => {
            if (process.ExitCode != 0)
                tcs.SetException(new System.Exception($"Python process exited with code {process.ExitCode}."));
            else
                tcs.SetResult(true);
            process.Dispose();
        };

        try {
            process.Start();
            process.BeginOutputReadLine();
            process.BeginErrorReadLine();
        } catch (System.Exception ex) {
            tcs.SetException(ex);
        }

        return tcs.Task;
    }
}