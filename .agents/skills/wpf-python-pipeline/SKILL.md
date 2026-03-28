# WPF + Python Subprocess Skill

Use this skill when working on the desktop app and its local processing pipeline.

## Purpose
Keep the WPF UI and Python processing pipeline integrated in a simple, readable way.

## Architecture intent
- WPF is responsible for UI, status, file selection, and rendering outputs.
- Python is allowed to handle STT, ffmpeg, preprocessing, and structured output generation.
- File-based handoff is preferred over hidden or implicit coupling.

## C# guidelines
- Keep code beginner-readable.
- Prefer explicit names and straightforward control flow.
- Avoid premature abstraction and heavy MVVM unless there is a clear benefit.
- Prefer small, understandable files.

## Python guidelines
- Prefer CLI-style scripts with clear input/output.
- Use simple argument parsing or deterministic input paths.
- Write stable output files that WPF can consume.
- Include minimal logging and clear error paths.

## Integration guidelines
- Pass explicit file paths.
- Handle subprocess exit codes.
- Fail clearly when dependencies or outputs are missing.
- Keep output artifacts deterministic when possible.

## Validation expectations
At minimum:
- build the project
- run the app
- test one sample file end-to-end
- verify transcript, summary, and quiz rendering
