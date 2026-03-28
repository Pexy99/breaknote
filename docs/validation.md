# Validation Guide

## 1. Default definition of done
A phase is done only if:
- build passes
- app launches
- target flow works
- changed area has basic failure handling
- result is understandable for later reading

## 2. Primary validation flow
1. build the solution
2. run the app
3. choose sample audio file
4. execute processing
5. verify transcript panel updates
6. verify summary panel updates
7. verify quiz panel updates
8. verify outputs are saved
9. verify error message path when something fails

## 3. Manual smoke test checklist
- app opens successfully
- file picker works
- selected path is visible
- process button works
- status changes during processing
- transcript is not empty
- summary is not empty
- quiz is not empty
- failure state is understandable

## 4. Failure test ideas
- missing file
- unsupported format
- broken Python path
- forced subprocess failure
- empty output file

## 5. Logging expectations
If relevant, logs should help answer:
- what started
- what failed
- where it failed
- what output path was expected

## 6. Stop rule
If validation fails for the current phase:
- do not continue to the next phase
- fix the failure first
- re-run validation
- update docs/progress.md with what failed and how it was fixed

## 7. Checkpoint rule
After each completed phase:
- leave a short progress note
- record changed files
- create a commit or explicit checkpoint
- update docs/tasks.md
- update docs/progress.md

## 8. Context cleanup rule
- Keep detailed progress only for the current track and the next track.
- When a track is stable, move its detailed entries to archive/progress/ and archive/tasks/.
- Leave only a short summary in active docs.
- Keep docs/plan.md detailed for Now, but summarized for Next and Later.

## 9. Current validation limits
Default desktop app validation is:
- build
- run
- manual smoke test

Do not assume reliable native desktop UI automation by default.