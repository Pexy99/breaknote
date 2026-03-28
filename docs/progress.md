# Progress Log

## Active track summary
- current_track: Track 001
- current_phase: Phase A
- overall_status: not_started

## Track 001

### Phase A

```yaml
status: completed
goal: establish the visible app shell
changed_files:
  - MainWindow.xaml
  - MainWindow.xaml.cs
  - BreakNote.csproj
validation:
  build: passed
  run: passed
  smoke_test: passed
checkpoint:
  type: Phase A Complete
  ref: initial-ui-skeleton
notes:
  - WPF project initialized with dotnet new wpf.
  - UI includes file selection, tabs for output, and status bar.
  - File picker button logic implemented.
```

### Phase B

```yaml
status: completed
goal: connect the WPF app to the local processing script
changed_files:
  - MainWindow.xaml
  - MainWindow.xaml.cs
  - scripts/process.py
  - .venv/
validation:
  build: passed
  run: passed
  smoke_test: passed
checkpoint:
  type: Phase B Complete
  ref: python-integration-done
notes:
  - Python venv (가상환경) 생성 및 연동 완료.
  - 별도 scripts/process.py를 통해 처리 시뮬레이션 구현.
  - C#에서 가상환경의 python.exe를 호출하여 오디오 파일 경로 전달 성공.
  - output/ 폴더에 더미 결과 파일 생성 확인.
```

- current_track: Track 001
- current_phase: Phase C (Completed)
- overall_status: Track 001 Done

## Track 001

### Phase A
... (생략) ...

### Phase B
... (생략) ...

### Phase C

```yaml
status: completed
goal: render results and verify one failure path
changed_files:
  - MainWindow.xaml.cs
validation:
  build: passed
  run: passed
  smoke_test: passed
checkpoint:
  type: Track 001 Complete
  ref: mvp-base-done
notes:
  - 파이썬 결과 파일(transcript, summary, quiz) 로드 및 UI 탭 매핑 완료.
  - 파일 읽기 실패 시 에러 메시지 표시 로직 구현.
  - Track 001 MVP 전체 플로우 검증 완료.
```

## Archive policy

* Keep detailed entries only for the active track and the next track.
* Move completed track details to `archive/progress/` once stable.
* Leave a short summary below after archiving.

## Archived track summaries

* none yet

