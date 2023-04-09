## Unit Stats Formulas

| StatId             | Value Range  | Start Value | Formula                     |
|--------------------|--------------|-------------|-----------------------------|
| MaxHealth          | (1, +inf)    | 10          | MIG * 2 + CON * 3           |
| HealthRegain       | (0.0f, +inf) | 0.5f        | CON / 5.0f + SOU / 10.0f    |
| MaxMana            | (1, +inf)    | 10          | CON * 1 + INT * 1 + SOU * 3 |
| ManaRegain         | (0.0f, +inf) | 0.25f       | SOU / 5.0f + INT / 10.0f    |
| ActionPower        | (1, +inf)    | 1           | MIG / 3                     |
| ActionSpeed        | (1.0f, +inf) | 1.0f        | DEX / 5.0f + INT / 10.0f    |
| MagicResistance    | (0, +inf)    | 0           | SOU / 5                     |
| PhysicalResistance | (0, +inf)    | 0           | CON / 5 + DEX / 10          |
| MovementSpeed      | (1.0f, +inf) | 1.0f        | DEX / 100.0f                |
