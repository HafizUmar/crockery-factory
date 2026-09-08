# Practice tasks

Ten exercises, roughly in order of difficulty. Each one teaches a different part of
Claude Code, not just a different part of the app. Do them in order the first time —
the later ones assume you know the earlier mechanics.

Open the folder in the desktop app's **Code** tab, or run `claude` from the repo root.

---

## 1. Read before you write

Don't change anything yet. Ask:

> Walk me through this codebase. What are the main pieces, how does data flow from the
> database to the grid, and what would surprise a new developer?

Then follow up on whatever it says. The point is to see how it finds files on its own —
you never tell it which ones to open.

Try `/help` to see the session commands, and `Shift+Tab` to cycle permission modes.

**Learning:** how a session starts, how Claude explores, what `CLAUDE.md` does for it.

---

## 2. A change you can check in ten seconds

> Deleting a product happens with no confirmation. Add a confirmation dialog that names
> the product being deleted.

Watch the diff before accepting. Then build and click it.

**Learning:** reading and approving edits; the edit/build/verify loop.

---

## 3. A real bug

`ProductionViewModel.AdvanceStage` books every planned cup as good when a batch
completes. Kilns don't work that way.

> When a batch completes, the operator should enter how many cups passed inspection and
> how many were scrapped, instead of assuming zero scrap. Add a dialog for it and
> validate that good + scrapped is not more than the planned quantity.

**Learning:** asking for a behaviour change rather than a code change, and letting
Claude decide the shape.

---

## 4. The bug you'll only find by using the app

Edit a product's name in the right-hand panel. The grid on the left doesn't update
until you save. Ask Claude why, then have it fix the cause rather than the symptom.

**Learning:** describing a symptom without naming the fix. (The answer involves
`INotifyPropertyChanged`, but don't say that — see whether it gets there.)

---

## 5. Validation

> Saving a product with a duplicate SKU throws a `DbUpdateException` and crashes.
> Typing letters in the price box silently does nothing. Add proper validation with
> messages shown next to the fields.

**Learning:** a task that touches models, view models and XAML at once. Watch how it
sequences the work.

---

## 6. Migrations

> Replace `EnsureCreated()` with EF Core migrations. Add the tooling package, create the
> initial migration, and apply migrations on startup.

This one needs terminal commands, not just edits. Let it run `dotnet ef` itself.

**Learning:** how Claude uses the shell, and what it asks permission for.

---

## 7. A feature, in a worktree

This is the big one. Before starting, check the **worktree** box in the desktop app —
Claude will work on a `claude/...` branch in a separate directory, so your main checkout
stays clean.

> Add customers and orders. A customer has a name, country and contact email. An order
> has a customer, an order date, a status, and lines of product plus quantity. Add an
> Orders tab that lists orders and lets me create one. Confirming an order should
> decrease stock on each product, and refuse if there isn't enough stock.

**Learning:** worktree isolation, and how a multi-file feature request goes. Review the
diff properly — this is where you find out how much you trust it.

---

## 8. Tests

> Add an xUnit test project for the view models, using an in-memory or temporary-file
> SQLite database. Cover the stage transitions and the stock decrement on order
> confirmation.

**Learning:** letting Claude create a whole project and wire it into the solution.

---

## 9. Git

> Commit this work in logical commits with good messages, then write a PR description
> covering what changed and what a reviewer should look at.

**Learning:** Claude Code as a git front end. Compare its commit boundaries to yours.

---

## 10. Make it yours

> Update CLAUDE.md so it matches what the code does now.

Then add your own conventions — how you like error handling, whether you want async
data access, your naming preferences. Every future session picks them up.

**Learning:** the thing that actually changes your day-to-day. A good `CLAUDE.md` is the
difference between fighting the tool and not.

---

## After that

Two ideas worth trying with the same repo, since they're closer to real work than the
tasks above:

- Point it at a **production concern**: "every database call runs on the UI thread — make
  data access async without breaking the binding."
- Ask for a **review**: "review this codebase as a senior .NET reviewer would. What would
  you refuse to merge?" Then argue with it about the answers you disagree with.
