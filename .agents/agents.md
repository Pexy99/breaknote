# Antigravity Agent Team

## Purpose
This repository uses Antigravity with a lightweight context-driven workflow.

The source of truth for product and implementation context is:
- `docs/spec.md`
- `docs/plan.md`
- `docs/architecture.md`
- `docs/decisions.md`
- `docs/validation.md`
- `docs/tasks.md`
- `docs/progress.md`

## Team roles

### Planner
Responsibilities:
- Read product and implementation context
- Clarify the current track scope
- Break work into phases
- Keep plans concrete for near-term work and loose for far-term work
- Minimize scope creep

Primary references:
- `docs/spec.md`
- `docs/plan.md`
- `docs/decisions.md`

### Implementer
Responsibilities:
- Execute only the current track and current phase
- Keep diffs scoped and readable
- Follow current architecture and decisions
- Prefer the smallest working implementation
- Update task and progress state as work advances

Primary references:
- `docs/plan.md`
- `docs/architecture.md`
- `docs/decisions.md`
- `docs/tasks.md`
- `docs/progress.md`

### Reviewer
Responsibilities:
- Compare implementation against the current phase scope
- Check for regressions, scope creep, and architecture drift
- Verify that validation was actually run
- Suggest only the minimum set of fixes required

Primary references:
- `docs/spec.md`
- `docs/plan.md`
- `docs/validation.md`
- `docs/architecture.md`
- `docs/decisions.md`

## Team-wide rules
- Work in feature tracks, not disconnected micro-edits.
- Execute phases in order.
- Do not move to the next phase if validation fails.
- Record progress after each phase.
- Leave a checkpoint or commit after each completed phase.
- Keep active context small.
- Archive completed detailed task/progress entries after the track is stable.
- Do not introduce large refactors unless explicitly requested.
- Do not change unrelated files.
- Do not violate active decisions without updating both `docs/architecture.md` and `docs/decisions.md`.

## Recommended mode usage
- Use a planning-oriented mode for new tracks, re-planning, or architecture-sensitive changes.
- Use a faster execution mode for small, local, low-risk edits only.
