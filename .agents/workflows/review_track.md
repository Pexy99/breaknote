---
description: Review the current implementation against the active track and phase requirements
---

# /review-track

Review the current implementation against the active track and phase requirements.

## Inputs
- Track name or active track
- Optional phase name

## Instructions
1. Read:
   - `docs/spec.md`
   - `docs/plan.md`
   - `docs/architecture.md`
   - `docs/decisions.md`
   - `docs/validation.md`
   - `docs/tasks.md`
   - `docs/progress.md`
2. Compare the current code and recent changes against:
   - phase scope
   - acceptance criteria
   - validation requirements
   - architecture and decision constraints
3. Check for:
   - scope creep
   - missing validation
   - regressions
   - architecture drift
   - undocumented structural changes
4. Recommend only minimal fixes needed to get the phase back into compliance.
5. Do not implement fixes unless explicitly asked.

## Output format
Return:
- pass/fail verdict
- mismatches
- missing validation
- architecture/decision conflicts
- minimal fix list

## Guardrails
- Review against the active phase, not against an imagined ideal redesign.
- Prefer small corrective actions over broad refactors.
