<style>
    .trait-header {
        font-weight:700;
        font-size: 30px;
        color:#99DDAA;
        margin-top: 50px;
    }
</style>

# Unit Traits

#### Traits value range: [1, 100]

<div class="trait-header">Might (MIG)</div>

Determines of spell power.<br>

| StatId      | Formula |
|-------------|---------|
| MaxHealth   | MIG * 2 |
| ActionPower | MIG / 3 |


<div class="trait-header">Constitution (CON)</div>

Defense skill.<br>

| StatId             | Formula    |
|--------------------|------------|
| MaxHealth          | CON * 5    |
| HealthRegain       | CON / 5.0f |
| MaxMana            | CON * 2    |
| PhysicalResistance | CON / 5.0f |


<div class="trait-header">Dexterity (DEX)</div>

Quickness.<br>

| StatId             | Formula      |
|--------------------|--------------|
| MovementSpeed      | DEX / 100.0f |
| ActionSpeed        | DEX / 5.0f   |
| PhysicalResistance | DEX / 10.0f  |

<div class="trait-header">Intellect (INT)</div>

Brilliance and intelligence.<br>

| StatId       | Formula     |
|--------------|-------------|
| MaxMana      | INT * 2     |
| ManaRegain   | INT / 10.0f |
| ActionSpeed  | INT / 10.0f |

<div class="trait-header">Soul (SOU)</div>

Spiritual strength.<br>

| StatId          | Formula     |
|-----------------|-------------|
| MaxMana         | SOU * 5     |
| ManaRegain      | SOU / 5.0f  |
| HealthRegain    | SOU / 10.0f |
| MagicResistance | SOU / 5.0f  |


<div style="display: none">
## UTIL

| Syntax    | Description |   Test Text |
|:----------|:-----------:|------------:|
| Header    |    Title    | Here's this |
| Paragraph |    Text     |    And more |

```json
{
  "firstName": "John",
  "lastName": "Smith",
  "age": 25
}
```

Here's a simple footnote,[^1] and here's a longer one.[^bignote]

[^1]: This is the first footnote.

[^bignote]: Here's one with multiple paragraphs and code.

    Indent paragraphs to include them in the footnote.

    `{ my code }`

    Add as many paragraphs as you like.

</div>