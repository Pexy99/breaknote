# Progress Log

## Active track summary
- current_track: Track 002
- current_phase: None
- overall_status: Track 002 code implementation is complete. Python STT, real-time logging, un-buffered output, and real-time transcript streaming are functional. Awaiting final user confirmation in WPF to close Track 002.

## Track 002 — Local STT Integration (Whisper)

### Phase A

```yaml
status: completed
goal: Python STT environment setup
changed_files:
  - requirements.txt
  - .venv/ (updated)
validation:
  import_whisper: passed
  ffmpeg_check: passed
checkpoint:
  type: Phase A Complete
  ref: whisper-env-ready
notes:
  - winget을 통해 시스템에 ffmpeg 설치 완료.
  - requirements.txt에 openai-whisper 추가 및 .venv에 설치 완료.
  - 가상환경에서 whisper 라이브러리 정상 로드 확인.
```

### Phase B

```yaml
status: completed
goal: Real Transcription implementation
changed_files:
  - scripts/process.py
validation:
  build: passed
  run: passed
  smoke_test: passed
checkpoint:
  type: Phase B Complete
  ref: whisper-integrated
notes:
  - scripts/process.py에 whisper 라이브러리 연동 완료.
  - medium 모델을 사용하여 한국어 포함 다국어 음성 인식 지원.
  - 실제 오디오 파일에서 추출된 텍스트를 transcript.txt에 저장하도록 수정.
  - 요약 및 퀴즈는 모델 연동 전까지 더미 유지.
```

### Phase C

```yaml
status: completed
goal: Real-time Logging & Transcript UI
changed_files:
  - MainWindow.xaml
  - MainWindow.xaml.cs
  - scripts/process.py
validation:
  build: passed
  run: passed
  live_logging: passed
  live_transcript: passed
checkpoint:
  type: Phase C Complete
  ref: realtime-transcript-ui
notes:
  - WPF TabControl에 Log 탭을 추가하고 비동기 이벤트(OutputDataReceived) 실시간 연동.
  - process.py에서 whisper 모델에 `verbose=True` 옵션 추가 및 MainWindow.xaml.cs 내 ProcessStartInfo에 `-u` 플래그 적용 (Python stdout 버퍼링 해제).
  - C# 정규식(Regex)을 사용하여 Whisper의 타임스탬프 문장을 캡처하고, 문장이 나오는 즉시 Transcript 탭에 실시간 작성되도록 조치.
```

## Recently Completed Tracks

### Track 001 — File-based processing MVP
- Status: Completed
- Goal: Basic WPF UI and mock Python integration for end-to-end flow.
- Highlights: Initialized WPF project, integrated mock process.py, and verified result rendering in tabs.

---

## Archive policy
* Move completed track details to `archive/progress/` once stable.
* Keep only the active track and next track in this file.
