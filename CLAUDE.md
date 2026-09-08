# Crockery Factory — project notes

A WPF desktop app for a small factory that makes ceramic cups and mugs. It tracks the
product catalogue and the production batches moving through the kilns.

This is a practice codebase. It is deliberately unfinished — see `PRACTICE-TASKS.md`.

## Stack

- .NET 8, WPF (`net8.0-windows`)
- EF Core 8 with SQLite (no server to install; the file lives in
  `%LOCALAPPDATA%\CrockeryFactory\factory.db`)
- Hand-rolled MVVM — `ObservableObject` and `RelayCommand` in `ViewModels/`. No MVVM
  framework, no DI container. Keep it that way unless a task says otherwise.

## Layout

```
src/CrockeryFactory.Desktop/
  Models/      Product, ProductionBatch, enums
  Data/        FactoryDbContext, SeedData
  ViewModels/  one view model per view, plus MVVM plumbing
  Views/       UserControls, one per tab
  MainWindow   shell with a TabControl; sets DataContext to MainViewModel
```

`MainWindow.xaml` hands each `UserControl` its view model through `DataContext`
binding, so the views themselves never construct a view model.

## Conventions

- View models own all data access. Views have empty code-behind apart from
  `InitializeComponent`.
- Each operation opens a short-lived `FactoryDbContext` in a `using` and disposes it.
  There is no long-lived context.
- Reads use `AsNoTracking`. Writes re-fetch the entity by id before mutating it.
- After any write, call `Load()` so the grid reflects what is actually in the database.
- File-scoped namespaces, nullable reference types on, `var` for locals.

## Domain vocabulary

- **Clay body** — the ceramic material: earthenware, stoneware, porcelain, bone china.
- **Bisque firing** — first firing, before glaze. **Glost firing** — second firing,
  after glaze. A batch goes Planned → Forming → BisqueFiring → Glazing → GlostFiring →
  Completed.
- **Scrapped** — cups lost to cracks, warping or glaze faults. Scrap rate is normal and
  varies by clay body; porcelain scraps more than stoneware.

## Known gaps

The database is created with `EnsureCreated()`, not migrations. There is no
validation, no test project, and no customers or orders yet. Batch completion books
every planned cup as good, which is not how a kiln behaves.
