# Crockery Factory

A small WPF app for a ceramics factory: a catalogue of cups and mugs, and the
production batches moving through the kilns. It exists so you have a real codebase to
point Claude Code at while you learn the tool.

## Run it

Windows only — WPF does not build on Linux or macOS.

```powershell
git init
git add .
git commit -m "Initial commit"

dotnet restore
dotnet run --project src\CrockeryFactory.Desktop
```

You need the .NET 8 SDK. `git init` matters: Claude Code's worktree isolation and the
branch chip in the desktop app both need a repository.

First launch creates the SQLite database and fills it with eight products and seven
batches. To reset, delete `%LOCALAPPDATA%\CrockeryFactory\factory.db` and run again.

## What works

- **Products tab** — search, create, edit, delete cups. Each has a SKU, clay body,
  capacity, glaze colour, price and stock level.
- **Production tab** — batches with their stage, kiln, and good/scrapped counts.
  "Advance stage" moves a batch to the next step; completing it books the cups into
  stock.

## What doesn't

Plenty, on purpose. `PRACTICE-TASKS.md` has ten graded exercises that walk you through
Claude Code — from reading an unfamiliar codebase to running parallel worktrees.
Start there.

## Layout

See `CLAUDE.md`. Claude Code reads that file automatically at the start of every
session, which is the point of it.
