# Structures

## Description

Structures can generate units or resources, grant abilities, multipliers and more.

## List

### Castle

This is your most important structure. It's generated at the beginning of the game and you can only have one. If this structure is destroyed, you lose.

The Castle generates a population of 5 and it also taxes the entire population (this castle's plus others) so it generates coins.

| Life | Can Attack | Attack Interval | Generates                      | Can Attack |
| ---- | ---------- | --------------- | ------------------------------ | ---------- |
| 1000 | -          | -               | Population (5), Money(1pp:10s) | -          |

### Soldier Tent

It generates 1 soldier unit each 5 seconds. Max 1 unit.

| Life | Damage | Damage Interval | Generates                                | Can Attack |
| ---- | ------ | --------------- | ---------------------------------------- | ---------- |
| 30   | -      | -               | [Soldier](../Units/readme.md#soldier)(1) | -          |

### House

It adds 1 to your population.

| Life | Damage | Damage Interval | Generates                              | Can Attack |
| ---- | ------ | --------------- | -------------------------------------- | ---------- |
| 10   | -      | -               | [Person](../Units/readme.md#person)(1) | -          |

### Archer Tower

It uses an [Arrow](../Bullets/readme.md#arrow) projectile to attack light enemies from a distance.

| Life | Damage | Damage Interval | Generates | Can Attack |
| ---- | ------ | --------------- | --------- | ---------- |
| 50   | -      | 1               | -         | Light      |

### Church

It generates Faith.

| Life | Damage | Damage Interval | Generates       | Can Attack |
| ---- | ------ | --------------- | --------------- | ---------- |
| 50   | -      | -               | Faith(0.1pp:1s) | -          |
