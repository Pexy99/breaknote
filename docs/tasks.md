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
- [ ] Update `scripts/process.py` to use Whisper
- [ ] Load Whisper model and process input audio
- [ ] Save actual text to `transcript.txt`
- [ ] Keep summary and quiz generation as mock
- [ ] Validate Phase B (Run app, check transcript)
- [ ] Leave Phase B checkpoint

## Next tracks
- [ ] Track 003 — Local LLM / Summary & Quiz Integration
- [ ] Track 004 — Long audio support
- [ ] Track 005 — Usability improvements

## Archive policy
- Move completed detailed task lists to archive/tasks/ once the track is stable.
- Keep only the active track and next track in this file.