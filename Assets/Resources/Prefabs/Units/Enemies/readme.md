# Enemies

## Descriptions

The enemies main goal is to destroy your castle at all cost. They may use different means, and that's why there are multiple types of enemies, each one with different habilities.

## List

### Enemy

This is the basic kind of enemy. It attacks only the Castle.

| Life | Damage | Damage Interval | Speed | Can Attack                                  |
| ---- | ------ | --------------- | ----- | ------------------------------------------- |
| 1    | 1      | 1               | 0.5   | [Castle](../../Structures/readme.md#castle) |

### Large Enemy

It's a larger variant of the basic Enemy, bigger in size and life but slower.

| Life | Damage | Damage Interval | Speed | Can Attack                                  |
| ---- | ------ | --------------- | ----- | ------------------------------------------- |
| 5    | 3      | 1               | 0.3   | [Castle](../../Structures/readme.md#castle) |

### Tank

The tank is a heavy machine that can attack any structure, plus the castle. Basic soldiers are helpless against this enemy. Only towers and other antitank units can attack it.

The tank uses the Tank Bullet as projectile to deal damage.

| Life | Damage | Damage Interval | Speed | Can Attack                                                                            |
| ---- | ------ | --------------- | ----- | ------------------------------------------------------------------------------------- |
| 30   | -      | 5               | 0.3   | [Structures](../../Structures/readme.md), [Castle](../../Structures/readme.md#castle) |
