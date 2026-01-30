# 📑 ÍNDICE DEL PROYECTO — Threshold of Silence

**Proyecto:** Threshold of Silence — Top-Down 2D Stealth Action  
**Fecha:** Enero 2026  
**Estado:** Esperando Apobación para desarrollo  
**Versión de Índice:** 1.0

---

## 🎨 GALERÍA DE IMÁGENES

### Portada del Juego
![Portada](./Threshold%20of%20silence.png)

### Captura de Gameplay
![Gameplay](./Threshold%20of%20Silence_Gameplay.png)

---

## 🎯 INICIO RÁPIDO

### Para Diferentes Públicos

| Rol | Documento Prioritario | Secciones Clave | Tiempo |
|-----|----------------------|-----------------|--------|
| **Stakeholder/Productor** | [Executive_summary.md](./Executive_summary.md) | I (Visión), VI (Métricas), VII (Riesgos) | 15 min |
| **Desarrollador** | [Technical_Architecture.md](./Technical_Architecture.md) | II–V (Módulos, Patrones), VI (Setup) | 20 min |
| **Game Designer** | [GDD_Diseño_de_Juego.md](./GDD_Diseño_de_Juego.md) | II–IV (Mecánicas, IA, Niveles) | 20 min |
| **Artista / Audio** | [Art_and_Audio_specification.md](./Art_and_Audio_specification.md) | II–V (Visual, Audio, Estados) | 15 min |
| **Project Manager** | [Project_Management.md](./Project_Management.md) | III–V (Sprints, US, Riesgos) | 15 min |
| **Tester / QA** | [Executive_summary.md](./Executive_summary.md#vi-métricas-de-éxito) → [Project_Management.md](./Project_Management.md) | VI (Métricas), III (US aceptación) | 10 min |

---

## 📚 ESTRUCTURA DE DOCUMENTOS

### 1. [Executive_summary.md](./Executive_summary.md) — Síntesis Estratégica
**Propósito:** Visión completa del proyecto en 1 documento para stakeholders y decisiones.

**Índice de Secciones:**
- [I. VISIÓN DEL PROYECTO](#) — Concepto, referencia, público objetivo
- [II. MECÁNICAS CORE](#) — Parámetros, core loop, FSM enemigo
- [III. ANÁLISIS MDA](#) — Framework Mechanics→Dynamics→Aesthetics
- [IV. ARQUITECTURA TÉCNICA](#) — Stack, 11 módulos, patrones
- [V. PLAN DE PRODUCCIÓN](#) — 10 sprints, carga, hitos
- [VI. MÉTRICAS DE ÉXITO](#) — KPIs por dimensión, umbrales Go/No-Go
- [VII. GESTIÓN DE RIESGOS](#) — Matriz R1–R8, mitigación, escalada
- [VIII. COHERENCIA INTER-DOCUMENTOS](#) — Trazabilidad vertical
- [IX. RESTRICCIONES Y EXCLUSIONES](#) — IN SCOPE vs NO INCLUIDO
- [X. PRÓXIMOS PASOS INMEDIATOS](#) — Sprint 1, gates de decisión
- [XI. VISIÓN A LARGO PLAZO](#) — Q2–Q4 2026, lecciones
- [XII. REFERENCIAS](#) — Enlaces a otros documentos

**Tabla de Parámetros Críticos (Sección II):**
- Movimiento: Normal 3.5 u/s, Sigilo 2.0 u/s
- Detección: Proximidad 4.0u, FOV 60°/6.0u, Ruido +1.5u/+0.5u
- Acumulación: 100 puntos Game Over
- FSM: Patrol → Suspicious (2.0s) → Alert (3.0s) → Confirmed

**Decisiones Críticas:**
- Métricas Go/No-Go: Finalización ≥70%, Tensión ≥4.0/5, Fairness ≤15%, Uptime 100%

---

### 2. [GDD_Diseño_de_Juego.md](./GDD_Diseño_de_Juego.md) — Game Design Document
**Propósito:** Definición completa de mecánicas, narrativa, IA y nivel design.

**Índice de Secciones:**
- **I. VISIÓN Y CONCEPTOS**
  - Concepto Core
  - Influencias
  - Target Audience
  - Core Loop explicado

- **II. SISTEMAS DE JUEGO**
  - Movimiento (8-dir, 2 velocidades)
  - Detección (Proximidad, Visión, Ruido)
  - Balanceo y escala

- **III. COMPORTAMIENTO ENEMIGO (IA)**
  - FSM estados (Patrol/Suspicious/Alert/Confirmed)
  - Patrullaje predecible
  - Toma de decisiones

- **IV. ANÁLISIS MDA**
  - Mecánicas → Dinámicas → Estética

- **V. LEVEL DESIGN**
  - Concepto nivel
  - Rutas: Segura / Intermedia / Directa
  - Colocación enemigos

- **VI. BACKLOG & ÉPICAS**
  - E1–E10 con descripción
  - Prioridad P0/P1/P2
  - Aceptación criteria

- **VII. CAMBIOS Y FUTURO**
  - Nivel 2, modos alternativos

**Parámetros de Referencia:**
- Controles: WASD (movimiento), Shift (sigilo), E (interacción), R (reinicio), Esc (pausa)
- Estados UI: "[SIGILO (Movimiento preciso)]" visible cuando Shift activo

**Nota para Arquitectura:** Todas las velocidades y radios deben extraerse de ConfigRepository, nunca hardcodeadas.

---

### 3. [Technical_Architecture.md](./Technical_Architecture.md) — Especificación Técnica
**Propósito:** Diseño software, módulos, patrones, setup de desarrollo.

**Índice de Secciones:**
- **I. PRINCIPIOS ARQUITECTÓNICOS**
  - Separación de responsabilidades
  - Bajo acoplamiento, alta cohesión
  - Configurabilidad, clarity over optimization

- **II. STACK TECNOLÓGICO**
  - Motor: Unity 2D (principal) / Godot (alternativa)
  - Lenguaje: C# / GDScript
  - VCS: Git

- **III. COMPONENTES CLAVE**
  - EventBus (pub-sub central)
  - ConfigRepository (parámetros)
  - MetricsLogger (datos playtesting)

- **IV. 11 MÓDULOS PRINCIPALES**
  - **Capa Core:** GameManager, LevelManager
  - **Capa Gameplay:** PlayerController, DetectionSystem, NoiseSystem
  - **Capa IA:** EnemyAI
  - **Capa Servicios:** EventBus, AudioManager, ConfigRepository
  - **Capa Presentación:** UIManager, MetricsLogger

- **V. PATRONES DE DISEÑO**
  - Singleton (GameManager, AudioManager, ConfigRepository)
  - State (GameManager, EnemyAI FSM)
  - Observer/EventBus (UI, Audio, Metrics)
  - Component (Detección modular)
  - Strategy (DetectionSystem)
  - Factory (EnemyFactory, LevelEntityFactory)

- **VI. GUÍA DE SETUP**
  - Requisitos (Unity 2020+, C# 8.0)
  - Estructura de carpetas
  - Primeros pasos (escena base, PlayerController)

**Eventos Principales:**
- `OnPlayerDetected`
- `OnAlertChanged`
- `OnNoiseEmitted`
- `OnRestartRequested`

**Módulos Críticos con Parámetros:**
- **DetectionSystem:** Proximidad (4.0u), FOV (60°/6.0u), Umbral (100 puntos)
- **NoiseSystem:** Emisión (+1.5u normal, +0.5u sigilo), decaimiento
- **EnemyAI FSM:** Timers (Suspicious 2.0s, Alert 3.0s, Cooldown 1.5s)

---

### 4. [Art_and_Audio_specification.md](./Art_and_Audio_specification.md) — Dirección Visual & Audio
**Propósito:** Especificación de arte, audio, states visuales y traceabilidad arquitectónica.

**Índice de Secciones:**
- **I. DIRECCIÓN VISUAL**
  - Estilo: Top-Down Minimalista
  - Paleta de colores
  - Proporción: 16:9

- **II. ESPECIFICACIÓN DE SPRITES**
  - Player (4 direcciones)
  - Enemigo (8 direcciones + animaciones FSM)
  - Obstáculos, elementos nivel

- **III. ESPECIFICACIÓN DE AUDIO**
  - Música por estado
  - SFX (movimiento, alerta, detección, game over)
  - Volúmenes y atenuación 3D

- **IV. MAPEO DE ESTADOS VISUALES**
  - Detección 0–25 (Verde)
  - Detección 26–50 (Amarillo)
  - Detección 51–75 (Naranja)
  - Detección 76–100 (Rojo)

- **V. ANIMACIONES DE IA**
  - Estados FSM con sprites correspondientes
  - Transiciones suaves
  - Timers: Suspicious 2.0s, Alert 3.0s, Cooldown 1.5s

- **VI. HUD & OVERLAYS**
  - Barra de detección 0–100
  - Etiqueta "[SIGILO (Movimiento preciso)]"
  - Overlays debug (FOV, radio proximidad, radio ruido)
  - Game Over screen

- **VII. TRAZABILIDAD CON ARQUITECTURA TÉCNICA**
  - Módulo UIManager → elementos HUD
  - Módulo EnemyAI → sprites de estados
  - Módulo AudioManager → sincronización SFX

**Paleta Core:**
- Verde: Seguridad (0–25 alerta)
- Amarillo: Precaución (26–50)
- Naranja: Peligro (51–75)
- Rojo: Crítico (76–100)

---

### 5. [Project_Management.md](./Project_Management.md) — Planificación & Seguimiento
**Propósito:** Sprints, user stories, estimaciones, riesgos, métricas de producción.

**Índice de Secciones:**
- **I. ESTRUCTURA DE SPRINTS**
  - 10 sprints semanales
  - Tabla de épicas, salidas esperadas

- **II. BACKLOG DETALLADO**
  - 23 User Stories (US-01 a US-23)
  - Épicas E1–E10
  - Aceptación criteria por US
  - Prioridad P0/P1/P2

- **III. ESTIMACIONES**
  - P0: 76 horas (crítico)
  - P1: 25 horas (alto)
  - P2–P3: 22 horas (complementario)
  - Total: 123 horas

- **IV. DEPENDENCIAS Y CAMINO CRÍTICO**
  - Sprint 2 → Sprint 3 → Sprint 4 → Sprint 5 → Sprint 6 (secuencia bloqueante)
  - Hitos de validación

- **V. GESTIÓN DE RIESGOS**
  - Riesgos R1–R8 con probabilidad/impacto
  - Mitigaciones por fase
  - Escalada de decisiones

- **VI. MÉTRICAS DE PRODUCCIÓN**
  - Velocidad esperada: 5–8 horas/sprint
  - Burn-down por sprint
  - Criterios de aceptación

- **VII. ROLES Y RESPONSABILIDADES**
  - DevLead, Designer, TechLead, PM, QA, ArtLead, EngLead

- **VIII. STAKEHOLDERS**
  - Contactos, aprobaciones

**User Stories Críticas (P0):**
- US-01: PlayerController movimiento 8-dir
- US-04: DetectionSystem proximidad
- US-05: Vision FOV
- US-09: NoiseSystem integrado
- US-13: UIManager + HUD

**Gates de Decisión:**
- G1 (Fin Sprint 3): DetectionSystem retorna 0–100 ✓
- G2 (Fin Sprint 4): Gameplay legible (80% testers entienden) ✓
- G3 (Fin Sprint 6): Balance aproximado (50% finalización) ✓
- G4 (Fin Sprint 8): Nivel sin crashes ✓
- G5 (Fin Sprint 9): Métricas finales (70% finalización, 4.0/5 tensión, ≤15% unfair) ✓

---

### 6. Estructura de Proyecto (Recomendada para Unity)

**Propósito:** Organización física de archivos recomendada para implementación.

**Estructura Típica (Unity):**
```
Proyecto_II_DJI/
├── Assets/
│   ├── Scripts/
│   │   ├── Core/           (GameManager, LevelManager)
│   │   ├── Gameplay/       (PlayerController, DetectionSystem, NoiseSystem)
│   │   ├── AI/             (EnemyAI, FSM)
│   │   ├── Services/       (EventBus, AudioManager, ConfigRepository, MetricsLogger)
│   │   └── UI/             (UIManager, Overlays)
│   ├── Sprites/            (Player, Enemy, Obstacles, UI elements)
│   ├── Audio/              (Music, SFX)
│   ├── Prefabs/            (PlayerPrefab, EnemyPrefab, LevelElements)
│   ├── Scenes/             (MainScene, Level1, Debug_TestGround)
│   └── ScriptableObjects/  (ConfigRepository, Enemy templates)
├── Scenes/                 (Scenes principales)
├── Documentation/          (Todos los .md: GDD, Arch, PM, Arte/Audio, Executive, INDEX)
├── Builds/                 (Builds ejecutables)
└── ProjectSettings/        (Configuración Unity)
```

---

## 🔗 MATRIZ DE TRAZABILIDAD

### Cómo Todo Se Conecta (GDD → Arch → PM → Arte/Audio)

| Concepto | GDD | Arquitectura | PM | Arte/Audio |
|----------|-----|--------------|----|----|
| **Movimiento** | § II (8-dir, velocidades) | PlayerController | US-01, US-02, US-03 | § I (Sprites 4-dir) |
| **Detección** | § II (Proximidad/FOV/Ruido) | DetectionSystem | US-04–US-07 | § VII (Sincronización) |
| **IA FSM** | § III (4 estados) | EnemyAI | US-08, US-09 | § V (Animaciones) |
| **Ruido Acústico** | § II (Emisión/Propagación) | NoiseSystem | US-10, US-11 | § III (SFX) |
| **UI/HUD** | § II (Barra, Etiqueta SIGILO) | UIManager | US-13–US-15 | § VI (Diseño) |
| **Nivel Design** | § V (3 rutas) | LevelManager | US-18 | Sprites obstáculos |
| **Métricas** | § IV (MDA) | MetricsLogger | § III (Definición) | — |
| **Audio** | § IV (Atmósfera) | AudioManager | § VIII (Riesgos) | § III (Especificación) |

**Garantía:** Si parámetro cambia (ej: velocidad 3.5 → 3.2 u/s), se propaga a **TODOS 6 documentos** simultáneamente.

---

## ⚡ PARÁMETROS CRÍTICOS (FUENTE ÚNICA DE VERDAD)

Todos estos valores deben sincronizarse en GDD, Arquitectura, PM, Arte/Audio y Executive Summary:

### Movimiento & Detección
| Parámetro | Valor | Ubicación GDD | Ubicación Arch | Ubicación PM |
|-----------|-------|---------------|----------------|-------------|
| Velocidad normal | 3.5 u/s | § II | PlayerController | E1 |
| Velocidad sigilo | 2.0 u/s | § II | PlayerController | E1 |
| Radio proximidad | 4.0 u | § II | DetectionSystem | E2 |
| FOV ángulo | 60° | § II | DetectionSystem | E3 |
| FOV distancia | 6.0 u | § II | DetectionSystem | E3 |
| Ruido normal | +1.5 u | § II | NoiseSystem | E4 |
| Ruido sigilo | +0.5 u | § II | NoiseSystem | E4 |
| Umbral detección | 100 pts | § II | DetectionSystem | E5 |

### FSM Timers
| Estado | Timer | Ubicación GDD | Ubicación Arch | Ubicación Arte |
|--------|-------|---------------|----------------|----------------|
| Suspicious | 2.0 s | § III | EnemyAI | § V |
| Alert | 3.0 s | § III | EnemyAI | § V |
| Cooldown | 1.5 s | § III | EnemyAI | § V |

### Métricas de Éxito
| KPI | Objetivo | Ubicación Executive | Ubicación PM |
|-----|----------|-------------------|----------|
| Finalización | ≥70% | § VI | § III |
| Tensión percibida | ≥4.0/5 | § VI | § VI |
| Fairness (unfair muertes) | ≤15% | § VI | § III |
| Uptime | 100% | § VI | § VI |

---

## 🎯 BÚSQUEDA RÁPIDA

### Por Concepto

**Movimiento & Control**
- [GDD § II](./GDD_Diseño_de_Juego.md#ii-sistemas-de-juego) — Cómo se mueve el jugador
- [Arch PlayerController](./Technical_Architecture.md#playercontroller) — Implementación
- [PM US-01–US-03](./Project_Management.md#user-stories-p0-crítico) — Tasks
- [Arte § I](./Art_and_Audio_specification.md#i-dirección-visual) — Sprites

**Detección Enemiga**
- [GDD § II](./GDD_Diseño_de_Juego.md#ii-sistemas-de-juego) — Reglas
- [Arch DetectionSystem](./Technical_Architecture.md#detectionsystem) — Lógica
- [PM US-04–US-07](./Project_Management.md#user-stories-p0-crítico) — Sprints
- [Arte § VII](./Art_and_Audio_specification.md#vii-trazabilidad-con-arquitectura-técnica) — Sincronización

**IA & Comportamiento**
- [GDD § III](./GDD_Diseño_de_Juego.md#iii-comportamiento-enemigo-ia) — FSM completo
- [Arch EnemyAI](./Technical_Architecture.md#enemyai) — Implementación
- [PM US-08, US-09](./Project_Management.md#user-stories-p0-crítico) — Tasks
- [Arte § V](./Art_and_Audio_specification.md#v-animaciones-de-ia) — Animaciones

**Audio & Atmosfera**
- [GDD § IV](./GDD_Diseño_de_Juego.md#iv-análisis-mda) — Propósito emocional
- [Arch AudioManager](./Technical_Architecture.md#audiomanager) — Módulo
- [PM § VIII](./Project_Management.md#viii-riesgos) — Risk management
- [Arte § III](./Art_and_Audio_specification.md#iii-especificación-de-audio) — Especificación

**UI & Feedback**
- [GDD § II](./GDD_Diseño_de_Juego.md#ii-sistemas-de-juego) — HUD display
- [Arch UIManager](./Technical_Architecture.md#uimanager) — Módulo
- [PM US-13–US-15](./Project_Management.md#user-stories-p0-crítico) — Tasks
- [Arte § VI](./Art_and_Audio_specification.md#vi-hud--overlays) — Diseño visual

**Nivel Design**
- [GDD § V](./GDD_Diseño_de_Juego.md#v-level-design) — Concepto, rutas
- [Arch LevelManager](./Technical_Architecture.md#levelmanager) — Loader
- [PM US-18](./Project_Management.md#user-stories-p0-crítico) — Sprint 8
- [Arte Sprites](./Art_and_Audio_specification.md#ii-especificación-de-sprites) — Obstáculos

**Métricas & QA**
- [Executive § VI](./EXECUTIVE_SUMMARY.md#vi-métricas-de-éxito) — Definición completa
- [Arch MetricsLogger](./Technical_Architecture.md#metricslogger) — Implementación
- [PM § III](./Project_Management.md#ii-backlog-detallado) — Aceptación criteria
- [PM § VI](./Project_Management.md#vi-métricas-de-producción) — Seguimiento

**Riesgos & Decisiones**
- [Executive § VII](./EXECUTIVE_SUMMARY.md#vii-gestión-de-riesgos) — Matriz R1–R8
- [PM § V](./Project_Management.md#v-gestión-de-riesgos) — Detalles
- [Executive § X](./EXECUTIVE_SUMMARY.md#x-próximos-pasos-inmediatos) — Gates G1–G5

---

## 📊 CRONOGRAMA DE SPRINTS (RUTA CRÍTICA)

```
Sprint 1 (Semana 1)     — Setup & Preproducción
    ↓
Sprint 2 (Semana 2)     — PlayerController completo ← HITO G1
    ↓
Sprint 3 (Semana 3)     — EnemyAI + Proximidad
    ↓
Sprint 4 (Semana 4)     — Vision + Ruido ← HITO G2
    ↓
Sprint 5 (Semana 5)     — NoiseSystem integrado
    ↓
Sprint 6 (Semana 6)     — Core Loop jugable ← HITO G3
    ↓
Sprint 7 (Semana 7)     — UI + Overlays debug
    ↓
Sprint 8 (Semana 8)     — Level Design ← HITO G4
    ↓
Sprint 9 (Semana 9)     — QA + Playtesting ← HITO G5
    ↓
Sprint 10 (Semana 10)   — Entrega Final
```

**Dependencias Bloqueantes:**
- Sprint 2 → 3: PlayerController debe funcionar antes de testear IA
- Sprint 3 → 4: Proximidad antes de integrar Visión
- Sprint 4 → 5: Visión/Ruido antes de optimizar noiseDecay
- Sprint 6 → 7: Loop jugable antes de refinar UI

---

## 📋 CHECKLIST DE COHERENCIA

Usa esto para validar que todos los documentos siguen sincronizados:

### Parámetros
- [ ] Velocidad normal = 3.5 u/s en GDD, Arch, PM, Executive
- [ ] Velocidad sigilo = 2.0 u/s en GDD, Arch, PM, Executive
- [ ] Radio proximidad = 4.0u en GDD, Arch, Arte, Executive
- [ ] FOV = 60°/6.0u en GDD, Arch, Arte, Executive
- [ ] Ruido normal/sigilo = +1.5u / +0.5u en GDD, Arch, PM, Executive
- [ ] Umbral = 100 pts en GDD, Arch, PM, Executive

### FSM Timers
- [ ] Suspicious = 2.0s en GDD, Arch, Arte, Executive
- [ ] Alert = 3.0s en GDD, Arch, Arte, Executive
- [ ] Cooldown = 1.5s en GDD, Arch, Arte, Executive

### Épicas & User Stories
- [ ] E1–E10 consistentes entre GDD § VI y PM § II
- [ ] US-01–US-23 con acceptance criteria en PM § II
- [ ] Estimaciones sumadas = 123 horas en PM § III

### Métricas
- [ ] ≥70% finalización en Executive § VI y PM § III
- [ ] ≥4.0/5 tensión en Executive § VI y PM § VI
- [ ] ≤15% unfair en Executive § VI y PM § III

### Riesgos
- [ ] R1–R8 consistentes entre Executive § VII y PM § V
- [ ] Mitigaciones específicas en ambos documentos
- [ ] Gates G1–G5 definidos en Executive § X y PM § IV

---

## 🔄 CÓMO USAR ESTE ÍNDICE

1. **Necesito información sobre [concepto]?**
   → Busca en "BÚSQUEDA RÁPIDA" arriba, click en enlace

2. **Tengo que actualizar un parámetro?**
   → Edita en 1 documento, luego verifica "CHECKLIST DE COHERENCIA"
   → Propaga el cambio a los otros 5 documentos

3. **¿Qué documento debo leer para [rol]?**
   → Mira "INICIO RÁPIDO" arriba, selecciona tu rol

4. **¿Cómo se conecta [módulo] con [mecánica]?**
   → Consulta "MATRIZ DE TRAZABILIDAD" para ver GDD→Arch→PM→Arte

5. **¿Dónde está el deadline del [Sprint X]?**
   → Mira "CRONOGRAMA DE SPRINTS" o [Project_Management.md § I](./Project_Management.md#i-estructura-de-sprints)

---

## 📞 REFERENCIAS CRUZADAS (ENLACES INTERNOS)

Para navegar rápidamente:

### Documentos Principales
- 📄 [Executive_summary.md](./Executive_summary.md) — Síntesis ejecutiva completa
- 🎮 [GDD_Diseño_de_Juego.md](./GDD_Diseño_de_Juego.md) — Design document
- ⚙️ [Technical_Architecture.md](./Technical_Architecture.md) — Arquitectura software
- 🎨 [Art_and_Audio_specification.md](./Art_and_Audio_specification.md) — Arte & Audio
- 📊 [Project_Management.md](./Project_Management.md) — PM & Sprints
- 📑 [Index.md](./Index.md) — Este documento (navegación centralizada)

### Acceso Directo a Parámetros
- [Velocidades en GDD § II](./GDD_Diseño_de_Juego.md#ii-sistemas-de-juego)
- [ConfigRepository en Arch § III](./Technical_Architecture.md#componentes-clave)
- [Estimaciones en PM § III](./Project_Management.md#iii-estimaciones)
- [Paleta en Arte § I](./Art_and_Audio_specification.md#i-dirección-visual)

### Acceso Directo a Decisiones
- [Métricas Go/No-Go en Executive § VI](./Executive_summary.md#vi-métricas-de-éxito)
- [Riesgos R1–R8 en Executive § VII](./Executive_summary.md#vii-gestión-de-riesgos)
- [Gates G1–G5 en Executive § X](./Executive_summary.md#x-próximos-pasos-inmediatos)

---

