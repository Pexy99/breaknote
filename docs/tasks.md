# Task Board

## Active track
Track 002 — Local STT Integration (Whisper)

### Phase A — Python STT environment setup
- [x] Create `requirements.txt` with `openai-whisper`
- [x] Install dependencies into `.venv`
- [x] Check/Document `ffmpeg` requirement
- [x] Validate Phase A
- [x] Leave Phase A checkpoint

### Phase B — Real Transcription implementation
- [x] Update `scripts/process.py` to use Whisper
- [x] Load Whisper model and process input audio
- [x] Save actual text to `transcript.txt`
- [x] Keep summary and quiz generation as mock
- [ ] Validate Phase B (Run app, check transcript)
- [ ] Leave Phase B checkpoint

### Phase C — Real-time Logging & Transcript UI
- [x] Add Log tab to `MainWindow.xaml`
- [x] Implement real-time stdout/err text append in `MainWindow.xaml.cs`
- [x] Add `-u` flag to Python invocation to fix buffering issues
- [x] Add `verbose=True` to Whisper call
- [x] Implement Regex parsing to append real-time text to Transcript tab
- [x] Validate Phase C (Live feedback and Transcript updates work)
- [x] Leave Phase C checkpoint

## Next tracks
- [ ] Track 003 — Local LLM / Summary & Quiz Integration
- [ ] Track 004 — Long audio support
- [ ] Track 005 — Usability improvements

## Archive policy
- Move completed detailed task lists to archive/tasks/ once the track is stable.
- Keep only the active track and next track in this file.