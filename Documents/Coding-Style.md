## Coding Style

### Formatting

* Keep simple expressions inline when readability is not affected.

```csharp
public int MaxHealth => GetFinalStat(StatType.MaxHealth);
```

* Avoid unnecessary blank lines. Separate only major sections:

  * Fields
  * Properties/events
  * Unity lifecycle methods
  * Public methods
  * Private methods

* Do not split method calls unless the line becomes difficult to read.

Prefer:

```csharp
_modifierContainer.AddModifier(Source, new StatModifier(StatType.MaxHealth, ModifierType.Flat, value));
```

Instead of:

```csharp
_modifierContainer.AddModifier(
    Source,
    new StatModifier(
        StatType.MaxHealth,
        ModifierType.Flat,
        value));
```

* Keep related logic close together.
* Prefer early returns over deep nesting.
* Use explicit names over overly compressed code.

---

### Comments

Every class should have a short class-level comment explaining its purpose.

Example:

```csharp
/// <summary>
/// Stores runtime character attributes.
/// Converts progression data into usable character values.
/// </summary>
public class CharacterAttributes : MonoBehaviour
```

Avoid comments that only describe obvious code.

Bad:

```csharp
// Add strength
Strength += amount;
```

Good:

```csharp
// Notify stat providers that attribute values changed.
OnChanged?.Invoke();
```

---

### Performance Notes

Add `// PERF` comments for areas that are simple currently but may require optimization later.

Example:

```csharp
// PERF: Cache calculated values if stats are queried frequently during combat.
private float GetFinalStat(StatType type)
{
    ...
}
```
