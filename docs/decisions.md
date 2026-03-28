# Decision Log

## Decision 001
### Title
Use WPF for the first Windows desktop MVP

### Why
- Windows-only target is acceptable
- good fit for a desktop utility app
- useful for learning C#

### Tradeoff
- less portable than cross-platform UI frameworks
- official onboarding is more Visual Studio-oriented

---

## Decision 002
### Title
Use code-first WPF style

### Why
- works better in Antigravity / VS Code-style workflow
- avoids dependency on designer-centric workflow
- keeps the process explicit

### Tradeoff
- slower for visual tweaking
- more manual XAML editing

---

## Decision 003
### Title
Allow Python subprocess for the processing pipeline

### Why
- fastest way to integrate STT and audio tooling
- easier to use existing Python ecosystem
- lets WPF stay focused on UI

### Tradeoff
- cross-language integration complexity
- subprocess error handling is required

---

## Decision 004
### Title
Start with file-based MVP instead of real-time flow

### Why
- simpler to build
- easier to debug
- easier to validate manually
- better fit for early learning

### Tradeoff
- less automated
- less impressive than a real-time version

---

## Decision 005
### Title
Keep near-term plans strict and far-term plans loose

### Why
- enables fast iteration
- reduces planning overhead
- preserves room for change

### Tradeoff
- far-term architecture may shift later

---

## Decision 006
### Title
Use track-based delivery with phased checkpoints

### Why
- allows broader autonomous implementation
- keeps validation structured
- makes rollback easier
- fits agent-assisted development

### Tradeoff
- slightly more documentation overhead

---

## Decision 007
### Title
Archive completed detailed progress to keep active context small

### Why
- reduces active token load
- keeps current context focused
- makes agent re-reading more reliable

### Tradeoff
- requires light maintenance of archive summaries