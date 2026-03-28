# Plan

## Now

### Track 001 — File-based processing MVP
Goal:
- User can select one audio file and see transcript, summary, and quiz in the app.

Included:
- main UI skeleton
- subprocess integration
- result display
- basic failure handling

Excluded:
- chunking
- real-time capture
- settings UI
- history UI

#### Phase A — UI skeleton
Tasks:
- create main window layout
- add file picker button
- add selected file path display
- add transcript panel
- add summary panel
- add quiz panel
- add status label

Acceptance criteria:
- project builds
- app launches
- main window renders correctly
- file picker UI is visible

Validation:
- dotnet build
- run app and verify main window opens
- verify required UI elements are present

Checkpoint:
- create a commit or explicit checkpoint note
- update docs/tasks.md
- update docs/progress.md

#### Phase B — Processing integration
Tasks:
- define Python script interface
- call Python from C#
- pass input file path
- receive output file paths or known output locations
- handle subprocess exit codes

Acceptance criteria:
- app can launch the processing script
- script receives the selected file path
- output files are produced or a clear failure is reported

Validation:
- dotnet build
- run app with one sample file
- verify subprocess starts
- verify expected output files are created

Checkpoint:
- create a commit or explicit checkpoint note
- update docs/tasks.md
- update docs/progress.md

#### Phase C — Result rendering and failure flow
Tasks:
- load transcript output into UI
- load summary output into UI
- load quiz output into UI
- show status updates
- show one clear failure path

Acceptance criteria:
- transcript panel is populated
- summary panel is populated
- quiz panel is populated
- one expected failure path shows a useful message

Validation:
- dotnet build
- run app with sample file
- verify transcript, summary, and quiz appear
- simulate one failure and verify message

Checkpoint:
- create a commit or explicit checkpoint note
- update docs/tasks.md
- update docs/progress.md

## Next
### Track 002 — Long audio support
Possible scope:
- chunk processing
- per-chunk outputs
- merged display strategy
- basic chunk progress feedback

### Track 003 — Usability improvements
Possible scope:
- retry flow
- output folder shortcut
- better status and logs
- lightweight settings

## Later
- system audio capture workflow
- semi-automatic chunking rules
- review mode / quiz retry mode
- export format improvements
- smarter orchestration