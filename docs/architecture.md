# Architecture

## 1. High-level structure
The app has two main parts:
- WPF desktop UI
- local processing pipeline

Related decisions:
- Decision 001
- Decision 002
- Decision 003
- Decision 004

## 2. WPF responsibilities
The WPF app is responsible for:
- file selection
- status display
- process trigger
- reading outputs
- displaying transcript, summary, and quiz
- basic error handling

Related decisions:
- Decision 001
- Decision 002
- Decision 004

## 3. Python responsibilities
Python scripts are allowed to handle:
- audio preprocessing
- ffmpeg calls
- STT execution
- text post-processing
- structured output writing

Related decisions:
- Decision 003
- Decision 004

## 4. Integration approach
The WPF app calls Python via subprocess.

Expected flow:
1. user selects audio file
2. WPF launches Python processing command
3. Python writes result files
4. WPF reads those files
5. WPF updates the UI

Related decisions:
- Decision 003
- Decision 004

## 5. Suggested folders
- input/
- output/transcripts/
- output/summaries/
- output/quizzes/
- logs/
- scripts/

Related decisions:
- Decision 003
- Decision 004

## 6. Data contract
Suggested output artifacts:
- transcript.txt
- summary.md
- quiz.md
- optional metadata.json
- optional process.log

Related decisions:
- Decision 003
- Decision 004

## 7. Error boundaries
Possible failure layers:
- file access failure
- subprocess launch failure
- Python runtime failure
- missing external tool
- malformed output file
- UI update failure

Each layer should fail with a clear visible message.

Related decisions:
- Decision 003
- Decision 004

## 8. Early architectural constraints
- keep the pipeline simple
- avoid premature plugin systems
- avoid hidden magic behavior
- prefer explicit file-based handoff
- prefer deterministic outputs where possible

Related decisions:
- Decision 002
- Decision 003
- Decision 004