
**mamba.Store** is an advanced economy and administration expansion for *Space Engineers*, built around an extended Store Block concept and a redesigned Safe Zone system.

The goal of this mod is to turn trading, grid selling, and server infrastructure into a **clear, controlled, and immersive system**, without breaking vanilla gameplay principles.

---

## 🔹 Store Block Expansion

This mod introduces a **custom Store Block** that behaves like the vanilla Store Block, but with **additional functionality**.

### New Tabs

#### 1️⃣ Sell Your Grid

Players can sell their own grids directly to a Store Block.

**Rules:**

* Only grids owned by the player
* Grid must be within a configurable radius
* Static grids and the grid containing the Store Block are excluded

**Admin-configurable behavior (per Store Block):**

* **Destroy mode**

  * Grid is deleted after sale
  * Player receives money
  * Store owner does NOT pay anything
  * Default for NPC stations

* **Recycle mode**

  * Grid is dismantled into components
  * Components are stored in cargo containers whose name **contains** `Cargo4Store`
  * Store owner pays the player
  * If there is no cargo space → transaction is cancelled

NPC Store Blocks always operate in **Destroy mode** and have unlimited money.

---

#### 2️⃣ Grid Purchase / Buy Ship *(In Development)*

Future feature.

This tab will allow players to **buy pre-built ships or grids**, similar to an auto dealership.

Status: **UI placeholder only**

---

## 🔹 Store Administration Improvements

The admin panel keeps the **vanilla Store logic**, but extends it with:

* Adjustable grid detection radius (slider)
* Ability to **increase OR decrease item prices**
* Automatic item listing from cargo containers

### Cargo4Store Logic

Any cargo container on the Store grid whose **custom name CONTAINS the string**:

```
Cargo4Store
```

(for example:
`Baza1 Cargo Large Ingots (Cargo4Store)`
`Baza1 Cargo Large Components (Cargo4Store)`)

will automatically:

* Be detected by the Store Block
* Be scanned for inventory contents
* Add its items to the Store offer list
* Use predefined base prices

Multiple cargo containers are supported, as long as their name **contains** `Cargo4Store` anywhere in the block name.

---

## 🛡️ Safe Zone System (Two Variants)

This mod introduces **two distinct Safe Zone blocks**.

---

### 1️⃣ Player Safe Zone (Vanilla Replacement)

Replaces the default vanilla Safe Zone block.

**Changes:**

* Zone Chip duration: **1440 minutes (24 hours)**
* Larger default radius
* Lower power drain
* Reduced activation time

**Modified Values:**

* Max radius: 500
* Min radius: 10
* Default radius: 100
* Activation time: 30s
* Upkeep time: 1440 min

Designed for long-term base protection with reduced micromanagement.

---

### 2️⃣ Admin Safe Zone (Unlimited)

Admin-only Safe Zone block.

**Features:**

* No Zone Chips required
* Very low power consumption
* Unlimited runtime
* Intended for NPC stations and server infrastructure

Admin Safe Zones do not participate in the economy and are not balanced for survival use.

---

## 🔹 Additional Admin Blocks

The mod also includes multiple **admin-focused blocks**, designed to help server operators create functional NPC or admin stations:

* **Admin Reactor**

  * Does not consume uranium
  * Power output up to several GW (`MaxPowerOutput = 99999999`)

* **Admin Ore Detector**

  * Large grid: 500m
  * Small grid: 250m

* **Admin Refinery / Assembler / Oxygen Generator**

  * Extremely fast processing speeds
  * Intended for supply hubs and NPC stations

---

## 🔹 Custom Cargo Blocks

The mod includes **three tiers of Large Cargo Containers** for players:

* MK1
* MK2
* MK3

Each tier provides progressively higher storage capacity.

---

## Mod Metadata

```xml
<?xml version="1.0" encoding="utf-8"?>
<ModMetadata xmlns:xsd="http://www.w3.org/2001/XMLSchema" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">
  <ModVersion>0.1</ModVersion>
</ModMetadata>
```

---

## Current Status

* Core systems defined
* GUI and logic under active development
* Grid Purchase (Buy Ship) planned for future updates
