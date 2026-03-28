---
description: Create or refine a new implementation track using the existing project documents.
---

# /start-track

Create or refine a new implementation track using the existing project documents.

## Inputs
- Track title or short feature description
- Optional user constraints
- Optional requested delivery scope

## Instructions
1. Read:
   - `docs/spec.md`
   - `docs/plan.md`
   - `docs/architecture.md`
   - `docs/decisions.md`
2. Understand the requested feature in terms of one coherent user-visible result.
3. Propose or update one track in `docs/plan.md` with:
   - Goal
   - Included scope
   - Excluded scope
   - Phase breakdown
   - Acceptance criteria per phase
   - Validation per phase
   - Checkpoint rule per phase
4. Update `docs/tasks.md` to reflect the current active track and phase checklists.
5. If needed, update `docs/spec.md` only where the new track changes product scope.
6. Do not implement code in this workflow unless explicitly requested.
7. If requirements are underspecified, produce a short list of only the missing decisions that block track planning.

## Output format
Return:
- track summary
- proposed phases
- blocking questions (only if necessary)
- files updated

## Guardrails
- Keep the MVP small.
- Prefer one coherent track over many fragmented tasks.
- Keep near-term phases concrete and far-term phases loose.
- Do not expand scope without a reason.
