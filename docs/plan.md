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

### Track 003 — Auto-Selection of Lecture Materials (Reference Augmentation)
Goal:
- Automatically select the most relevant lecture materials (PDF/PPTX) for the current lecture from a synced local folder using a two-stage retrieval pipeline, without requiring perfect metadata or manual selection.

Included:
- Candidate generation (Phase A): Rules-based folder and filename filtering.
- Relevance scoring (Phase B): Content-based ranking via TF-IDF / Cosine Similarity.
- Pipeline integration (Phase C): Attach matched texts for later LLM generation.
- Evaluation (Phase D): Tune heuristics against sample data.

Excluded:
- Pure semantic reranking with embeddings (future path).
- Direct Microsoft Graph/API integration (MVP relies on local synced folders).

#### Phase A — Candidate generation
Tasks:
- Parse folder structure of a designated local material root.
- Implement heuristic filters (date-range folder proximity, filename keyword overlap, modification bias).
- Collect and return top N candidate files (PDF/PPTX).

Acceptance criteria:
- Algorithm correctly narrows down a large synced folder into a small (<10) pool of plausible files without crashing.

Validation:
- Unit test function on a mock folder tree.

Checkpoint:
- Update docs.

#### Phase B — Text extraction and ranking
Tasks:
- Extract textual content from candidate PDFs and PPTXs.
- Build transcription query from the first few minutes of STT output.
- Compute TF-IDF vectorization and cosine similarity between query and candidates.
- Apply weighted scoring algorithm (folder + filename + content score).

Acceptance criteria:
- Candidates are properly ranked without taking an unreasonable amount of time.

Validation:
- Script logs final scores of candidate files correctly.

Checkpoint:
- Update docs.

#### Phase C — Pipeline Integration
Tasks:
- Attach selected top 1~3 documents text output to the final pipeline artifact.
- Implement fallback (proceed audio-only if confidence is too low).
- Integrate WPF parameter for the synced local folder path.
- **[Refined]** Implement Yes/No alert dialog fallback when sync folder is empty, instead of hard blocking.
- **[Undocumented Fix]** Use Windows Forms `FolderBrowserDialog` for actual UI directory picking instead of manual text entry.
- **[Undocumented Fix]** Force UTF-8 encoding on standard output/error to prevent `cp949` crashes during subprocess Python calls.

Acceptance criteria:
- The app handles materials dynamically alongside STT.
- Users can easily pick paths with a native dialog.
- The app warns users when running without materials but allows them to proceed.

Validation:
- End-to-end processing app test with local files.

#### Phase D — Evaluation
Tasks:
- Test on real lecture sessions.
- Tune weights and thresholds for TF-IDF vs filenames.

## Next
### Track 004 — Local LLM / Summary & Quiz Integration
Possible scope:
- Connect to local LLM or API to generate real summaries based on the transcript and attached lecture materials.

### Track 005 — Long audio support
Possible scope:
- chunk processing
- per-chunk outputs
- merged display strategy

### Track 006 — Usability improvements
Possible scope:
- retry flow
- progress status parsing
- lightweight settings (e.g., select Whisper model size)