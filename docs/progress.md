# Progress Log

## Active track summary
- current_track: Track 003
- current_phase: None
- overall_status: Track 003 is fully completed and validated. Automatic lecture material selection based on folder proximity and TF-IDF content similarity is functional.

## Track 003 — Auto-Selection of Lecture Materials

### Phase A
```yaml
status: completed
goal: Candidate generation
changed_files:
  - requirements.txt
  - scripts/retrieval.py (new)
  - scripts/test_retrieval.py (validation)
validation:
  unit_test: passed (test_retrieval.py)
checkpoint:
  type: Phase A Complete
  ref: candidate-gen-ready
notes:
  - 의존성 라이브러리(PyMuPDF, python-pptx, scikit-learn) 설치 완료.
  - 정규식을 활용하여 N월 및 MMDD 폴더 내 pdf, pptx 탐색 로직 작성.
```

### Phase B
```yaml
status: completed
goal: Text extraction and ranking
changed_files:
  - scripts/retrieval.py
validation:
  logs_check: passed (User verified TF-IDF ranking in logs)
checkpoint:
  type: Phase B Complete
  ref: ranking-logic-ready
notes:
  - PDF/PPTX의 최대 10페이지 추출 로직 구현.
  - Whisper transcript 쿼리와 코사인 유사도(TF-IDF) 매칭 후 상위 3개 선별 로직(Threshold 0.05) 반영.
```

### Phase C
```yaml
status: completed
goal: Pipeline Integration
changed_files:
  - scripts/process.py
  - MainWindow.xaml
  - MainWindow.xaml.cs
validation:
  e2e_run: passed (User verified folder dialog and processing flow)
checkpoint:
  type: Phase C Complete
  ref: integration-complete
notes:
  - WPF 화면에 `[Select Sync Folder]` 버튼/텍스트 박스 신설.
  - Windows Forms의 `FolderBrowserDialog`를 사용하여 실제 폴더 선택 창 구현.
  - 폴더 미지정 시 강제 차단 대신 **Yes/No 선택형 경고창(Alert)**을 띄우는 예외 처리(Fallback) 적용.
  - process.py에서 `syncFolder` Argument 수신 및 `retrieve_materials` 호출 적용.
```

## Recently Completed Tracks

### Track 002 — Local STT Integration (Whisper)
- Status: Completed
- Goal: Real transcription implementation with Whisper Base/Medium model, real-time unbuffered log streaming.
- Highlights: Process.py updated, WPF Log tab added, streaming Regex implemented, final validation passed.

### Track 001 — File-based processing MVP
- Status: Completed
- Goal: Basic WPF UI and mock Python integration for end-to-end flow.
- Highlights: Initialized WPF project, integrated mock process.py, and verified result rendering in tabs.

---

## Archive policy
* Move completed track details to `archive/progress/` once stable.
* Keep only the active track and next track in this file.
