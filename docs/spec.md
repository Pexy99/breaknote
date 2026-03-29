# Product Spec

## 1. One-line description
A Windows WPF app that turns lecture audio files into transcript, summary, and quiz outputs for personal study.

## 2. Problem
Current workflow is fragmented and manual:
- record or collect lecture audio
- organize files manually
- run transcription manually
- summarize manually
- create quiz or review materials manually

This takes time and breaks study flow.

## 3. Target user
Primary user:
- a single personal user
- wants fast iteration
- wants a practical study tool
- wants to learn C# while building

## 4. Goals
### Primary goals
- reduce manual study prep steps
- get a working desktop MVP quickly
- keep the code understandable enough to learn from later

### Success signals
- audio file can be processed end-to-end
- transcript appears in UI
- summary appears in UI
- quiz appears in UI
- outputs can be saved locally

## 5. MVP scope
### Must-have
- choose audio file
- run transcription pipeline
- show transcript in UI
- show summary in UI
- show quiz in UI
- save result files
- show processing status
- show basic error messages

### Nice-to-have
- automatic selection of relevant lecture materials (PDF/PPTX) from local sync folder
- chunk-based processing for long audio
- processing history list
- output folder shortcut
- simple retry button

## 6. Out of scope
- true real-time transcription
- automatic system-audio capture
- automatic browser integration
- cloud upload or sync
- user accounts
- advanced settings UI
- mobile version

## 7. Main user flow
1. user opens app
2. user selects an audio file
3. user starts processing
4. app runs local pipeline
5. transcript is generated
6. summary is generated
7. quiz is generated
8. outputs are shown in UI
9. outputs are saved locally

## 8. Delivery unit
A coherent user-visible result should be delivered as one track.

Example:
- file selection
- processing trigger
- result display

These can belong to one track if they form one complete user flow.

## 9. Failure cases
- invalid file path
- unsupported file format
- subprocess failure
- missing dependency
- empty transcription result
- summary or quiz generation failure

The app should fail clearly and show a useful message.

## 10. UX principles
- simple and obvious
- low cognitive load
- fast feedback
- visible status
- easy retry
- readable text-first interface

## 11. Non-functional priorities
- understandable code
- small iterative progress
- local-first workflow
- stable manual testing