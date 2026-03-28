---
description: Implement the active track phase-by-phase using the saved project documents
---

# /implement-track

Implement the active track phase-by-phase using the saved project documents.

## Inputs
- Track name or active track
- Optional phase range (for example: Phase A only, or Phase A to B)

## Instructions
1. Read:
   - `docs/spec.md`
   - `docs/plan.md`
   - `docs/architecture.md`
   - `docs/decisions.md`
   - `docs/validation.md`
   - `docs/tasks.md`
   - `docs/progress.md`
2. Identify the current track and current target phase.
3. Implement only the requested phase scope.
4. Keep changes scoped to the current phase and avoid unrelated edits.
5. Before changing structure, re-check:
   - `docs/architecture.md`
   - `docs/decisions.md`
6. After completing the phase:
   - run the validation described in `docs/plan.md` and `docs/validation.md`
   - if validation fails, stop and fix before continuing
   - update `docs/tasks.md`
   - update `docs/progress.md`
   - leave a checkpoint or commit note
7. If multiple phases are requested, repeat the same loop phase-by-phase.
8. If the implementation drifts from the plan, stop and propose the smallest plan correction instead of silently continuing.

## Output format
Return:
- completed phase(s)
- validation results
- changed files
- checkpoint note
- any follow-up risks

## Guardrails
- Keep C# beginner-readable.
- Keep Python practical and modular.
- Prefer the smallest working implementation.
- Do not add dependencies without a clear reason.
- Do not continue past a failed validation.
