### Cilj moda – Mamba Store Block GUI

**Što je ovaj mod**

Ovaj mod proširuje Store Block u igri Space Engineers tako da dodaje custom GUI elemente (gumbe / tabove) koji se vide na F ekranu bloka, a ne kroz K (Control Panel).

Cilj je da Store Block ima vlastito sučelje koje igrač vidi kad otvori blok (F), isto kao što ima Refinery, Assembler ili Store – ali s dodatnim funkcijama koje Keen nije predvidio.

**Što mod radi (konceptualno)**
Dodaje vlastite gumbe / tabove na Store Block GUI
GUI se prikazuje direktno na ekranu bloka (F)

Ne koristi Torch
Ne koristi DLL plugin
Radi isključivo kao mod (Scripts unutar modifikacije)
Ne koristi K izbornik
Nema terminalnih kontrola (sliders, checkboxes u Control Panelu)

**Zašto Store Block**
Store Block već ima:
GUI
logiku trgovine
sinkronizaciju s ekonomijom
Mod ga koristi kao bazu, ali dodaje custom ponašanje i UI, npr.:
posebni gumbi
custom akcije
sinkronizacija, posebne ponude, skrivena logika

**Naziv bloka:**
Type:Id StoreBlock
SubtypeId: StoreBlockAdmin

Mod se nalazi u:
SpaceEngineers/Mods/mamba.Blocks/

Logika bloka ide kroz:
MyGameLogicComponent
