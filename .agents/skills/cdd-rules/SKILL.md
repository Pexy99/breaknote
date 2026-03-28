# Context-Driven Development Rules

Use this skill whenever work spans more than a trivial local edit.

## Purpose
Apply the repository's context-driven workflow consistently.

## Source of truth
Always prioritize:
1. `docs/spec.md`
2. `docs/plan.md`
3. `docs/architecture.md`
4. `docs/decisions.md`
5. `docs/validation.md`
6. `docs/tasks.md`
7. `docs/progress.md`

## Operating rules
- Work in tracks.
- Implement in ordered phases.
- Use acceptance criteria and validation from the current phase.
- Stop if validation fails.
- Update state documents after each phase.
- Keep active context small.
- Archive completed detailed entries after the track is stable.

## Progress file rules
`docs/progress.md` should remain machine-readable.
Use the existing YAML block structure.
Prefer these status values:
- `not_started`
- `in_progress`
- `blocked`
- `done`

## Architecture safety
Before changing structure:
- read `docs/architecture.md`
- read `docs/decisions.md`
- avoid conflicting with active decisions
- if structure must change, update both documents
