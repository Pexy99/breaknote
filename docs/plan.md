# Plan

## Now

### Track 002 — Local STT Integration (Whisper)
Goal:
- Replace the mock processing script with a real speech-to-text engine (OpenAI Whisper) to generate actual transcripts from audio files.

Included:
- openai-whisper package setup in `.venv`
- `ffmpeg` dependency handling (documentation or automatic check)
- `process.py` updates to run Whisper transcription
- Output real transcript to `transcript.txt`

Excluded:
- Long audio chunking (memory management for huge files)
- Real LLM integration for Summary and Quiz (keep these as mocks for this track)
- UI progress bar for Whisper inference mapping

#### Phase A — Python STT environment setup
Tasks:
- Create `requirements.txt` with `openai-whisper` (and `torch`)
- Install dependencies into `.venv`
- Ensure `ffmpeg` is available on the path or document how to install it.

Acceptance criteria:
- `requirements.txt` exists
- Python environment can import `whisper` without errors

Validation:
- Run `python -c "import whisper"` in `.venv`

Checkpoint:
- Update docs/tasks.md and docs/progress.md

#### Phase B — Real Transcription implementation
Tasks:
- Update `scripts/process.py` to load a small Whisper model (e.g., `base` or `small`).
- Pass the input file to Whisper and generate the actual text.
- Save the Whisper output to `output/transcripts/transcript.txt`.
- Keep the `summary` and `quiz` generation as mock for now.

Acceptance criteria:
- Processing an audio file generates a real transcript.
- Terminal/process output shows model loading and transcribing status.

Validation:
- Run the WPF app, select a real audio file (e.g., short 10-second test), click Process.
- Real transcript is shown in Transcript tab.

Checkpoint:
- Create a commit and update progress.

#### Phase C — Real-time Logging & Transcript UI
Tasks:
- Add a Log tab to the WPF UI (`MainWindow.xaml`).
- Wire the Python subprocess `StandardOutput` and `StandardError` to display in real-time in the new Log tab.
- Use the `-u` flag on the Python subprocess to ensure output is not buffered.
- Add `verbose=True` to the Whisper transcribe call.
- Parse real-time `[00:00.000 --> 00:05.000]` Whisper outputs using Regex and append the text string immediately to the Transcript tab.

Acceptance criteria:
- User can see live output from Python (such as model loading and transcription status) in the Log tab without buffering delay.
- Sentences appear one by one inside the Transcript tab as soon as they are inferred by the model.

Validation:
- Run the app, select a file, click Process, and verify the Log tab populates sequentially and Transcript text updates progressively.

Checkpoint:
- Update docs/tasks.md and docs/progress.md

## Next
### Track 003 — Local LLM / Summary & Quiz Integration
Possible scope:
- Connect to local LLM or API to generate real summaries based on the transcript.

### Track 004 — Long audio support
Possible scope:
- chunk processing
- per-chunk outputs
- merged display strategy

### Track 005 — Usability improvements
Possible scope:
- retry flow
- progress status parsing
- lightweight settings (e.g., select Whisper model size)