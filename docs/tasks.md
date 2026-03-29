# Task Board

## Active track
Track 003 — Auto-Selection of Lecture Materials

### Phase A — Candidate generation
- [x] Parse folder structure & collect candidate files (.pdf, .pptx)
- [x] Implement date/folder and filename keyword heuristics
- [x] Validate Phase A filtering logic (scripts/test_retrieval.py)
- [x] Leave Phase A checkpoint

### Phase B — Text extraction and ranking
- [x] Extract text from PDF/PPTX
- [x] Build transcript query from STT output
- [x] Compute TF-IDF similarity and rank files
- [x] Validate Phase B ranking logs
- [x] Leave Phase B checkpoint

### Phase C — Pipeline integration
- [x] Attach selected material text to summary/quiz payload
- [x] Add threshold fallback behavior
- [x] Add WPF/Settings option for Synced Material Root
- [x] Fix CP949 encoding crash (Force UTF-8 subprocess)
- [x] Add Windows Forms FolderBrowserDialog for path selection
- [x] Implement Yes/No fallback alert when sync folder is empty
- [x] Validate Phase C end-to-end integration
- [x] Leave Phase C checkpoint

### Phase D — Evaluation
- [x] Test on real lecture sessions
- [x] Tune weights and thresholds

## Next tracks
- [ ] Track 004 — Local LLM / Summary & Quiz Integration
- [ ] Track 005 — Long audio support
- [ ] Track 006 — Usability improvements

## Archive policy
- Move completed detailed task lists to archive/tasks/ once the track is stable.
- Keep only the active track and next track in this file.