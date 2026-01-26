# Executive Summary — Threshold of Silence

**Proyecto:** Threshold of Silence — Top-Down 2D Stealth Action  
**Duración:** 10 semanas (Sprints semanales)  
**Fecha:** Enero 2026  
**Versión:** 1.1 

---

## I. VISIÓN DEL PROYECTO

### Concepto Central

*Threshold of Silence* es un videojuego Top-Down 2D de acción y sigilo que enfatiza la **planificación táctica y el aprendizaje iterativo** como mecánicas centrales. El jugador se infiltra en escenarios cerrados y deterministas, evitando la detección enemiga mediante el control preciso del movimiento, la gestión inteligente del ruido y la lectura de líneas de visión.

El título refleja la mecánica core: el juego premía el **silencio y la observación** sobre la reacción apresurada. Cada intento representa un **ciclo completo**: observación del entorno → planificación de ruta → ejecución tctica → análisis de resultado → reintento con conocimiento acumulado. Esta estructura genera aprendizaje medible y refuerza la sensación de dominio progresivo del sistema.

**Filosofía de diseño:** La detección no es ambigua; siempre resulta en Game Over inmediato, transformando cada error en lección clara. No hay penalizaciones ocultas ni mecánicas aleatorias que frustren al jugador.

### Referencia Conceptual

**Hotline Miami × Metal Gear Solid** — Ritmo rápido y reintentos sin fricción, combinados con reglas estrictas de sigilo y retroalimentación de detección legible.

### Público Objetivo

- **Edad:** 14+
- **Perfil primario:** Jugadores que disfrutan de experiencias de alta tensión, mecánicas punitivas justas y aprendizaje por iteración
- **Perfil secundario:** Usuarios fascinados por sistemas de IA legibles, basados en patrones predecibles y observable en tiempo real
- **Motivación clave:** Dominio del sistema mediante observación y optimización, no acumulación de recursos

---

## II. MECÁNICAS CORE (SISTEMA CERRADO Y PARAMETRIZABLE)

### Filosofía de Diseño

El sistema de juego se basa en **parámetros cuantitativos explícitos**, diseñado para ser:
- **Determinista:** Las mismas acciones producen los mismos resultados
- **Legible:** El jugador siempre entiende por qué fue detectado
- **Balanceable:** Todos los parámetros se ajustan sin modificar código (ConfigRepository)
- **Predecible:** Enemigos siguen patrones observables y consistentes

### Parámetros Clave del Sistema

| Sistema | Parámetro | Valor | Propósito |
|---------|-----------|-------|----------|
| **Movimiento** | Normal | 3.5 u/s | Ritmo rápido, genera ruido alto |
| **Movimiento** | Sigilo | 2.0 u/s | Cautela, genera ruido bajo |
| **Detección** | Radio proximidad | 4.0 u | Distancia segura base |
| **Detección** | FOV (ángulo/dist) | 60° / 6.0 u | Cono de visión enemigo (prioridad alta) |
| **Ruido** | Movimiento normal | +1.5 u | Expande radio de escucha enemigo |
| **Ruido** | Movimiento sigilo | +0.5 u | Reducción significativa pero audible |
| **Acumulación** | Umbral crítico | 100 puntos | Disparador de Game Over |
| **Degradación** | Alerta/Sospecha | 2.0–3.0 s | Timers de relajación sin estímulos |
| **Reinicio** | Penalización | Ninguna | Sin fricción entre intentos |

### Core Loop (Experiencia Repetitiva)

El ciclo de juego está diseñado para ser **breve, claro y educativo**:

1. **Observación (5–10 s):** Jugador analiza rutas posibles, ubicación de enemigos, patrones de patrullaje
2. **Planificación (5–10 s):** Selección consciente de trayectoria (segura/intermedia/directa según tolerancia de riesgo)
3. **Ejecución (20–60 s):** Movimiento activo, regulación de velocidad, lectura en tiempo real de feedback de alerta
4. **Resultado:** Avance hacia objetivo O detección y Game Over
5. **Reintento:** Reinicio instantáneo, aplicación de nuevo conocimiento

**Tiempo total por intento:** 40–100 segundos, optimizando para múltiples iteraciones en sesión corta.

### Estados de Enemigo (Máquina de Estados Finita)

La IA enemiga transita entre **4 estados deterministas** con transiciones explícitas:

| Estado | Descripción | Duraci máx | Transición |
|--------|-------------|-----------|-----------|
| **Patrol** | Patrullaje rutinario sin alerta | ∞ | Ruido periférico O proximidad → Suspicious |
| **Suspicious** | Sospecha activa, busca confirmación | 2.0 s | Estímulo repetido/visión → Alert; silencio → Patrol |
| **Alert** | Búsqueda activa, movimiento acelerado | 3.0 s | Visión confirmada → Confirmed; silencio+cooldown 1.5s → Suspicious |
| **Confirmed** | Detección certera, Game Over inmediato | 0.5 s | Dispara evento `OnPlayerDetected` → pantalla Game Over |

**Claridad para jugador:** Cada transición es visible mediante animaciones + feedback visual/sonoro sincronizado (color enemigo + audio).

---

## III. ANÁLISIS MDA (Mechanics → Dynamics → Aesthetics)

### Marco Conceptual

El diseño se fundamenta en el framework **MDA (Mechanics → Dynamics → Aesthetics)**, garantizando trazabilidad directa entre reglas formales, comportamientos emergentes y experiencia emocional del jugador:

- **Mechanics:** Parámetros cuantitativos (velocidades, radios, umbrales, timers)
- **Dynamics:** Comportamientos emergentes de la interacción entre sistemas
- **Aesthetics:** Emociones y sensaciones generadas en el jugador

### Experiencia Estética Buscada

✓ **Tensión constante:** Exposición permanente al riesgo de detección, sin áreas completamente seguras  
✓ **Presión psicológica:** Fallo inmediato actúa como refuerzo de decisiones conscientes y planificadas  
✓ **Reto y superación:** Aprendizaje por repetición sin castigo acumulativo ni recursos perdidos  
✓ **Dominio progresivo:** Conocimiento acumulado del sistema se transforma en control táctic + eficiencia

### Dinámicas Emergentes (Interacción Sistema)

| Mecánicas | Interacción | Dinámica Emergente | Experiencia Generada |
|-----------|-------------|-------------------|---------------------|
| Proximidad + Visión FOV | Enemigo monitorea zonas activas simultáneamente | Jugador debe evitar zonas abiertas y maximizar cobertura | **Tensión + cautela estratégica** |
| Ruido proporcional a velocidad | Velocidad alta = radio ruido ampliado × tiempo | Regulación consciente de desplazamiento según distancia | **Decisiones tácticas constantes** |
| Game Over inmediato (umbral ≥100) | Sin margen de error tras confirmación | Planificación previa obligatoria, observación antes de actuar | **Presión y enfoque absoluto** |
| Reinicio rápido sin penalización | Fallo no interrumpe flujo de juego | Aprendizaje iterativo sin fricción | **Superación y dominio** |
| Patrullaje predecible (rutas consistentes) | Patrones repetibles, tiempos fijos | Observación y memorización del entorno como ventaja | **Control progresivo** |

---

## IV. ARQUITECTURA TÉCNICA (MODULAR Y ESCALABLE)

### Filosofía Arquitectónica

La arquitectura se diseña bajo **5 principios core**:
1. **Separación de responsabilidades:** Cada módulo tiene función única, clara
2. **Bajo acoplamiento:** `EventBus` desvincula sistemas (IA no conoce UI, Audio no conoce Gameplay)
3. **Alta cohesión:** Sistemas cohesivos internamente sin complejidad cruzada
4. **Configurabilidad:** `ConfigRepository` permite ajustes sin tocar código (critical para prototipo)
5. **Clarity over optimization:** En prototipo académico, claridad y legibilidad > performance extrema

### Stack Tecnológico

| Componente | Tecnología | Justificación |
|-----------|-----------|--------------|
| **Motor** | Unity 2D (principal) | Madurez para 2D, soporte nativo raycasting, componentes, FSM |
| **Alternativa** | Godot Engine | Open-source, arquitectura nodos, equivalencia conceptual |
| **Lenguaje** | C# / GDScript | Tipado, integración nativa con motor elegido |
| **Plataforma** | PC Windows/Linux | Accesibilidad académica, facilidad de distribución prototipo |
| **Input** | Teclado + Mouse | Precisión táctica; mando opcional para accesibilidad |
| **VCS** | Git (GitHub/GitLab) | Trazabilidad completa, colaboración, CI/CD potencial |

### 11 Módulos Principales (Capas Lógicas)

**Capa Core:**
- `GameManager` — Orquestación global, estado juego (Playing/Pause/GameOver), ciclo de vida
- `LevelManager` — Carga escenas, reset determinista, control de spawns

**Capa Gameplay:**
- `PlayerController` — Movimiento 8-dir, modos velocidad, emisión de ruido
- `DetectionSystem` — Cálculo compuesto (proximidad + visión + ruido → 0–100)
- `NoiseSystem` — Generación, propagación, decaimiento ruido en tiempo real

**Capa IA:**
- `EnemyAI` — FSM (Patrol/Suspicious/Alert/Confirmed), patrullaje, queries a DetectionSystem

**Capa Servicios:**
- `EventBus` — Publicación/suscripción (OnPlayerDetected, OnAlertChanged, OnNoiseEmitted)
- `AudioManager` — Música por estado, SFX sincronizados, pool de AudioSource
- `ConfigRepository` — Acceso a parámetros (ScriptableObjects/JSON), sin hardcoding

**Capa Presentación + Datos:**
- `UIManager` — HUD (barra alerta 0–100), Game Over, overlays debug, toggle F1
- `MetricsLogger` — CSV/JSON local (intentos, tiempos, causa detección, playtesting)

### Patrones de Diseño Justificados

| Patrón | Módulos | Problema resuelto | Beneficio |
|--------|---------|------------------|----------|
| **Singleton** | GameManager, AudioManager, ConfigRepository | Múltiples instancias = inconsistencia | Única fuente de verdad, estado sincronizado |
| **State Pattern** | GameManager, EnemyAI | Condicionales anidados incontrolables | Estados explícitos, transiciones seguras, debugging fácil |
| **Observer/EventBus** | EventBus ↔ UI, Audio, Metrics | Acoplamiento directo entre sistemas | Desacople total, sistemas reutilizables, sin dependencias cruzadas |
| **Component** | Detección modular | Rigidez por herencia profunda | Composición flexible (Vision + Proximity + Noise), variantes enemigos fáciles |
| **Strategy** | DetectionSystem | Alternancia entre reglas | Contribuciones (visión/proximidad/ruido) como estrategias intercambiables |
| **Factory** | EnemyFactory, LevelEntityFactory | Creación inconsistente de entidades | Estandarización spawns, configuración por nivel, evita errores |

---

## V. PLAN DE PRODUCCIÓN (10 SEMANAS)

### Estructura de Sprints

La producción se organiza en **10 sprints semanales**, cada uno entregando incremento jugable validable:

| Sprint | Objetivo | Épicas | User Stories | Salida Esperada |
|--------|----------|--------|--------------|-----------------|
| **1** | Preproducción + Setup | E10 | US-23 | Repositorio versionado, escena base, tablero ágil operativo |
| **2** | Core Gameplay | E1 | US-01, US-02, US-03 | Movimiento 8-dir funcional + 2 velocidades (3.5/2.0 u/s) |
| **3–4** | IA + Detección Base | E2, E3 | US-04, US-05, US-06, US-07 | IA con FSM legible + patrullaje predecible + detección 0–100 |
| **5** | Sistema de Ruido | E4 | US-09, US-10 | Ruido integrado a escalamiento alerta, no detección instantánea |
| **6** | Game Loop Completo | E5 | US-11, US-12 | Core loop funcional (intento → fallo → reintento) |
| **7** | Feedback + Debug | E6 | US-13, US-14, US-15 | UI mínima (barra alerta) + overlays debug (FOV/radio/medidor) |
| **8** | Level Design | E8 | US-18 | Nivel 1 en greybox (rutas segura/intermedia/directa jugables) |
| **9** | QA + Balanceo | E9 | US-20, US-21 | Métricas locales + playtesting (ronda 1), ajustes documentados |
| **10** | Entrega Final | E7, E10 | US-16, US-17, US-22, US-23 | Build estable + SFX + documentación completa + reporte final |

### Carga Estimada (123 horas para 1 desarrollador)

- **P0 (Crítico, bloquea playtest):** 76 horas
  - E1–E5 (Gameplay core): 45h
  - E6 (UI/debug): 11h
  - E8 (Level 1): 8h
  - E9, E10 (QA/Docs): 12h

- **P1 (Alto, refina experiencia):** 25 horas
  - US-03, US-14, US-20, US-21 (input, overlays, métricas, playtesting)

- **P2–P3 (Complementario, opcional):** 22 horas
  - US-15, US-17, US-19 (menú pausa, música, nivel 2)

### Hitos de Validación Clave

1. **Fin Sprint 2:** PlayerController totalmente responsivo en escena vacía
2. **Fin Sprint 4:** Detección visible y explicable; tester dice "detectado por visión/ruido/proximidad"
3. **Fin Sprint 6:** Core loop jugable end-to-end (1 intento completo en 60–100 s)
4. **Fin Sprint 8:** Nivel 1 con 3 rutas de riesgo claras, balance aproximado
5. **Fin Sprint 10:** Build sin crashes, documentación completa, métricas de playtesting

### Entregables Prototipos

**Lo-Fi (Sprints 3–4, 4–6 horas):**
- Gizmos Unity para FOV/radios
- Sprites geométricos simplificados (círculos/rectángulos)
- Overlays de detección 0–100 sin pulido visual

**Mid-Fi (Sprints 6–7, 8–10 horas):**
- Sprites definitivos minimalistas
- Paleta de colores por estado aplicada
- SFX base sincronizados
- UI completa (barra, Game Over, pausa)

**Final (Sprint 10):**
- Build ejecutable PC
- Instrucciones de jugabilidad
- Reporte de métricas (playtesting + balance)

---

## VI. MÉTRICAS DE ÉXITO

### Rationale: Por Qué Estas Métricas

El éxito de una experiencia como *Threshold of Silence* no puede medirse solo por "*el juego funciona*". Necesitamos validar tres dimensiones críticas:

1. **Accesibilidad:** ¿Los jugadores entienden la mecánica? (target ≥70% finalización)
2. **Justicia:** ¿Las muertes se sienten justas? (target ≤15% "unfair" deaths)
3. **Tensión Emocional:** ¿El juego genera la presión buscada? (target ≥4.0/5.0 en escala Likert)

### KPIs Definitivos por Dimensión

**Dimensión Gameplay (Jugabilidad):**
- **Tasa de Finalización:** ≥70% de intentos alcanzan la salida (cualquier ruta)
- **Intentos para 1ª Completación:** 6–15 (aprendizaje visible, no frustración)
- **Tiempo Promedio por Intento:** 60–100 segundos (pacing: 40% movimiento, 40% observación, 20% decisión)
- **Muertes por Causa (balance de sistemas):**
  - Visión: 40–50% (previene deambulaje despreocupado)
  - Proximidad: 20–30% (penalización de errores cercanos)
  - Ruido: 15–25% (refuerza interdependencia movimiento/sigilo)
  - Otras/bugs: <5%

**Dimensión Experiencia (Emocional):**
- **Percepción de Tensión:** ≥4.0/5.0 (escala Likert 5 puntos, post-sesión)
- **Claridad de Riesgo:** ≥85% de jugadores identifican correctamente causa de muerte
- **Agencia Percibida:** ≥80% reportan "sensación de control sobre resultado" (vs. "suerte/azar")
- **Uso de Sigilo:** 30–60% de tiempo jugable en modo precisión (vs. velocidad)

**Dimensión Técnica (Estabilidad):**
- **Uptime:** 100% sin crashes (0 fallos en 10 sesiones × 10 intentos = 100 intentos)
- **Frame Rate:** ≥30 FPS constante (PC mid-range: i5-8400, 8GB RAM, GTX 1060)
- **Respuesta de Input:** <50ms latencia percibida (A/B testing vs. 150ms simulado)

### Metodología de Recolección

**Automático (MetricsLogger in-game):**
- Por intento: timestamp, posición final, causa de detección, ruta elegida, duración, detección_máxima, velocidad_promedio
- Exporta CSV con 60+ variables técnicas, agregables por cohorte (1er/2do/10º intento)
- Identifica outliers y patrones fallidos (ej: "jugadores fallan siempre en esquina SO")

**Manual (Cuestionario post-sesión):**
- 5 preguntas Likert (tensión, claridad, control, dificultad, diversión)
- 2 preguntas abiertas: "¿Cómo moriste?" (comprensión), "¿Cambiarías algo?" (feedback cualitativo)
- 2 demográficas: edad, experiencia gaming
- Target: 12–15 jugadores × 5 sesiones = 75 cuestionarios en Sprint 9

### Umbral de Aceptación (Go/No-Go)

| Métrica | Objetivo | Crítica | Acción Si Falla |
|---------|----------|---------|-----------------|
| Finalización | ≥70% | **SÍ** | Reducir enemigos / ampliar rutas seguras |
| Tensión Percibida | ≥4.0/5 | **SÍ** | Ajustar rango alerta / timers enemigo |
| Fairness (≤15% unfair) | ≤15% | **SÍ** | Analizar outliers, mejorar clarity visual |
| Uptime | 100% | **SÍ** | Hotfix bloqueador, retrasar entrega si necesario |
| Frame Rate | ≥30 FPS | No | Optimización post-Sprint 10 (opcional) |
| Claridad | ≥85% | No | Feedback visual mejorado (no bloquea) |

**Decisión de Entrega:** Sprint 10 es entregable si y solo si se cumplen las 4 métricas críticas. Si alguna falla, se negocia scope o deadline.

---

## VII. GESTIÓN DE RIESGOS

### Matriz de Riesgos Identificados

| # | Riesgo | Impacto | Prob. | Exposición | Mitigación | Propietario |
|---|--------|---------|-------|------------|-----------|-------------|
| R1 | Detección no legible (jugador confundido sobre causa de muerte) | Alto | Media | 12 | Overlays de debug permanentes + instant replay de último frame pre-detección + claridad en UI + iteración rápida con testers | DevLead |
| R2 | Dificultad excesiva (tasa completación <50%) | Alto | Media | 12 | Rutas seguras mínimas + ventanas de oportunidad claras + balanceo paramétrico sin código | Designer |
| R3 | IA inconsistente (transiciones FSM inesperadas) | Alto | Media | 12 | FSM estricta con logs de transición + pruebas reproducibles + escena de debug con control manual de IA | TechLead |
| R4 | Scope creep (nuevas mecánicas solicitadas ad-hoc) | Alto | Alta | 16 | Congelación de alcance explícita + "out of scope" definido claramente + backlog futuro documentado | PM |
| R5 | Falta de playtesting riguroso | Alto | Media | 12 | 2 rondas mínimas con métricas desde Sprint 9 + reclutamiento testers en Sprint 1 + protocolo documentado | QA |
| R6 | Tiempo en arte/audio insuficiente | Medio | Media | 6 | Minimalismo por diseño + sprites reutilizables + SFX placeholders | ArtLead |
| R7 | Tiempo del proyecto (123h insuficientes) | Medio | Baja | 3 | Scope P0/P1/P2 claro + daily standup + re-estimación en Sprint 4 | PM |
| R8 | Performance degradado (FPS <30) | Medio | Baja | 3 | Profiling temprano (Sprint 3) + caché raycast + object pooling | EngLead |

### Escalas de Evaluación

- **Impacto:** Alto (bloquea entrega o hace injugable), Medio (degrada experiencia, mitigable), Bajo (cosmético, no bloquea)
- **Probabilidad:** Alta (>60%), Media (30–60%), Baja (<30%)
- **Exposición:** Impacto (A=4, M=2) × Probabilidad (H=0.75, M=0.5, B=0.25)

### Plan de Respuesta Escalonado

**Riesgos Críticos (Exposición ≥10):**
- **R1–R5:** Mitigación **activa desde Sprint 2–3**
  - R1: Overlays debug construidos en paralelo a core gameplay (Sprint 7)
  - R2: 3 rutas de prueba con enemigos fijos Sprint 2, iteración con testers Sprint 4–5
  - R3: FSM logger implementado Sprint 2, escena reproducer Sprint 4
  - R4: Backlog futuro documentado Sprint 1, "out of scope" revisado con stakeholders
  - R5: Protocolo de playtesting y reclutamiento testers en Sprint 1
- **Monitoreo:** Checklist en Sprint Review, escalada inmediata si deterioro detectado

**Riesgos Secundarios (Exposición 3–9):**
- **R6–R8:** Monitoreo pasivo con re-evaluación en Sprint 5
  - R6: Revisión arte/audio Sprint 3; si delay, reducir fidelidad visual
  - R7: Si Sprint 3 consume >25h, reducir P2 features
  - R8: Profiling mandatorio Sprint 3; si FPS <30, reducir enemies/LOD nivel

### Escalada y Toma de Decisiones

**Condiciones de Re-negociación:**
- Si **R4 (Scope creep)** se materializa: Protocolo de solicitud formal + re-estimación + ajuste timeline
- Si **2+ riesgos críticos** simultáneamente activos: Junta de stakeholders + decisión timeline vs. scope
- **Decision Point: Fin Sprint 5** (día 1) — go/no-go basado en progreso real vs. burned hours

---

## VIII. COHERENCIA INTER-DOCUMENTOS

### Trazabilidad Vertical: Cómo Todo Se Conecta

El proyecto está diseñado como un **sistema coherente de capas documentales**, donde cada decisión en el GDD se refleja en Arquitectura, PM y Arte/Audio. Esta coherencia es crítica para evitar desalineamientos durante la implementación.

#### Validación de Sincronización

| Dimensión | Checksum | Auditoría |
|-----------|----------|----------|
| **Parámetros Gameplay** | Velocidades (3.5/2.0 u/s), radio proximidad (4.0u), cono visión (60°/6.0u), ruido (+1.5u/+0.5u) | ✓ Sincronizado en GDD § II, Arquitectura § IV, PM § V, Arte/Audio § III |
| **Estados IA (FSM)** | Patrol → Suspicious (2.0s) → Alert (3.0s) → Confirmed + Cooldown (1.5s) | ✓ GDD § III, Arquitectura § IV (módulo EnemyAI), Arte § V |
| **Sistema de Eventos** | OnPlayerDetected, OnAlertChanged, OnNoiseEmitted → UI/Audio reactivos | ✓ Arquitectura § IV (EventBus), PM § VIII (US-13 UI), Arte § VI (SFX sync) |
| **Épicas → User Stories** | E1–E10 desglosadas en US-01 a US-23 con aceptación criteria | ✓ GDD § VI, PM § III, validado con métricas § VI |
| **MDA Framework** | Mecánicas (reglas) → Dinámicas (emergentes) → Estética (tensión) | ✓ GDD § IV, Arquitectura § I, Arte § I |
| **Controles & UI** | SIGILO (Movimiento preciso) con visualización "[SIGILO]" en HUD | ✓ GDD § II (tablas controles), Arte § VI (HUD spec) |

#### Matriz de Trazabilidad Crítica

**GDD Mecánica → Arquitectura Módulo → PM User Story:**
- Detección compuesta (GDD § II) → DetectionSystem (Arch § 1.2) → US-04,05,06,07 (PM § III)
- FSM enemigo (GDD § III) → EnemyAI + EventBus (Arch § 1.3) → US-08,09 (PM § III)
- Ruido acústico (GDD § II) → NoiseSystem + DetectionSystem (Arch § 1.2) → US-10,11 (PM § III)
- HUD + overlay (GDD § II) → UIManager + ConfigRepository (Arch § 1.4) → US-13,14,15 (PM § III)

**Garantía de Coherencia:** En cada Sprint Review, verificar que no existe divergencia en parámetros entre documentos. Si se ajusta valor (ej: velocidad a 3.2 u/s), se propaga INMEDIATAMENTE a todos 4 documentos.

---

## IX. RESTRICCIONES Y EXCLUSIONES EXPLÍCITAS

### Alcance Confirmado (IN SCOPE)

**Gameplay Core:**
- Movimiento 8-direccional con colisiones físicas
- 2 velocidades (normal 3.5 u/s, sigilo 2.0 u/s) con transición fluida
- Detección compuesta (proximidad + visión + ruido) con cálculo 0–100 acumulativo
- Game Over inmediato en detección ≥100, reinicio rápido (< 2s)

**AI & Nivel:**
- Enemigo con FSM 4-estado legible (Patrol → Suspicious → Alert → Confirmed)
- Patrullaje predecible en waypoints visualizables
- 1 nivel jugable en greybox (3 rutas de riesgo diferenciado: segura/intermedia/directa)

**Feedback & Interfaz:**
- Barra de detección 0–100 en HUD (color gradual verde→rojo)
- Etiqueta "[SIGILO (Movimiento preciso)]" cuando activo
- Game Over screen + instrucciones claras
- Overlays debug (FOV, radio proximidad, radio ruido, medidor detección)
- SFX básico (movimiento, alerta, detección, game over)

**Datos & Iteración:**
- MetricsLogger automático (CSV por sesión)
- Playtesting 2 rondas con 5–10 testers c/u
- Documentación técnica completa + manual de usuario

### Explícitamente **NO** Incluido

**Sistemas de Juego Excluidos:**
- ❌ Combate / confrontación enemigo activo
- ❌ Persecución prolongada o huida
- ❌ Inventario, ítems, power-ups, upgrades
- ❌ Sistema de puntos / scoring visible
- ❌ Progresión persistente / guardado de progreso
- ❌ Múltiples niveles (inicialmente 1 solo)

**Narrativa & Presentación:**
- ❌ Cinemáticas / cutscenes extendidas
- ❌ Diálogos / voz en off
- ❌ Lore profundo / worldbuilding extenso
- ❌ Menú principal elaborado (botones esenciales solo)
- ❌ Traducción a idiomas distintos de español/inglés

**Optimizaciones Diferidas:**
- ⏱️ Sprites de alta fidelidad (placeholders aceptables)
- ⏱️ Música adaptativa (SFX sí, música BG opcional)
- ⏱️ Efectos de partículas avanzados
- ⏱️ Networking multiplayer
- ⏱️ Soporte mobile/consolas (PC solo inicialmente)

**Backlog Futuro (Post-Sprint 10, si presupuesto permite):**
- Nivel 2 alternativo
- Modos de dificultad (Easy/Hard)
- Replay / instant replay
- Leaderboard local
- Editor de niveles simple

---

## X. PRÓXIMOS PASOS INMEDIATOS

### Secuencia Crítica de Activación (Antes de Sprint 2)

**Sprint 1 — Preproducción (Duración: 5 días útiles)**

| Tarea | Duración | Dependencia | Verificación |
|-------|----------|-------------|--------------|
| Crear repositorio Git con estructura de carpetas | 1h | Ninguna | `git log --oneline` muestra commits iniciales |
| Inicializar proyecto Unity con escena base | 2h | Repo | PlayerController puede moverse en escena vacía |
| Configurar tablero ágil (GitHub Projects) | 1h | Repo | US-01 a US-23 están en backlog, E1–E10 son épicas |
| Revisar documentos de diseño con stakeholders | 3h | Todos los docs existentes | Sign-off en documento, no cambios pending |
| Crear escena "Debug_TestGround" para desarrollo | 2h | Proyecto Unity | Gizmos, enemigos fijos, overlays funcionales |
| Documentar "Cambios en futuras iteraciones" | 1h | Coherencia inter-docs | Plantilla en Drive / wiki, acceso documentado |

**Hito de entrada a Sprint 2:** Build ejecutable sin errores, repo versionado, backlog priorizado.

### Sprints 2–5 (Desarrollo Core, Semanas 2–5)

**Dependencias Estrictas:**

```
Sprint 2 (PlayerController completo)
    ↓ (US-01, US-02, US-03)
Sprint 3 (EnemyAI + Detección proximidad)
    ↓ (US-04, US-05)
Sprint 4 (Visión + Ruido en DetectionSystem)
    ↓ (US-06, US-07, US-10)
Sprint 5 (NoiseSystem integrado)
    ↓ (US-09, US-11)
Sprint 6 (Core Loop jugable)
```

**Validación Post-Sprint:**
- **Sprint 2:** Gameplay frame =[ P:0–>V:3.5, Shift:0–>V:2.0, colisiones OK ]
- **Sprint 3:** IA patrulla, reacciona a proximidad, FSM visible en logs
- **Sprint 4:** Vision Cone con raycast, overlays de FOV funcional
- **Sprint 5:** Ruido emitido, propagado, visible en medidor
- **Sprint 6:** Core loop jugable end-to-end (90s promedio)

### Puntos de Control Críticos (Decision Gates)

| Gate | Timing | Criterio Go/No-Go | Acción Si No-Go |
|------|--------|------------------|-----------------|
| **G1: Tech Spike Completo** | Fin Sprint 3 | DetectionSystem retorna 0–100 correctamente | Prototipado rápido, vs. arq revisada |
| **G2: Gameplay Legible** | Fin Sprint 4 | Testers dicen "entiendo por qué morí" ≥80% | Mejorar overlays, claridad |
| **G3: Balance Aproximado** | Fin Sprint 6 | Finalización ≥50% (no requiere 70% aún) | Ajuste de parámetros, re-calibración |
| **G4: Playtesting Viabilidad** | Fin Sprint 8 | Nivel funcional sin crashes (10 intentos consecutivos) | Hotfixes prioritarios |
| **G5: Entrega Decisión** | Fin Sprint 9 | Métricas: Finalización ≥70%, Tensión ≥4.0/5, Fairness ≤15% | Extensión o scope reduction negociado |

### Contingencia: Plan B Si Progreso Se Atrasa

- **Reducción Ágil:** Si Sprint 4 consume >8h (estimado 5h), postergar Ruido completo a Sprint 5
- **Scope Minimal:** Si velocidad Team <5h/sprint por promedio, congelar P2 features ahora
- **Extensión:** Si Sprint 5 FPS <30, extender playtesting a Sprint 11 (si budget permite)

---

## XI. VISIÓN A LARGO PLAZO Y LECCIONES DE DISEÑO

### Objetivos Más Allá de Sprint 10

**Horizonte Inmediato (Q2 2026, si viabilidad confirmada):**
- Pulir fidelidad visual (sprites finales, iluminación dinámica)
- Expandir a 2–3 niveles adicionales con progresión de dificultad
- Implementar modos: Story (guiado), Challenge (tiempos limitados), Sandbox (parámetros customizables)
- Publicación en itch.io como prototipo académico open-source

**Horizonte Medio (Q3–Q4 2026):**
- Documentación de game design patterns aplicados (MDA, FSM legible, detection feedback)
- Paper académico sobre "Feedback inequívoco en juegos de sigilo" (si resultados prometedores)
- Repositorio público con código comentado como referencia pedagógica

### Lecciones de Diseño Aplicadas

**Por qué este enfoque funcionará:**

1. **Determinismo Radical:** Sin RNG oculto, el jugador siempre aprende patrones replicables
2. **Feedback Inmediato:** Game Over instantáneo transforma frustración en claridad
3. **Parámetros Explícitos:** Configurabilidad sin código permite balanceo rápido
4. **Documentación First:** Arquitectura y GDD en sincronía evita deuda técnica temprana
5. **Trazabilidad Total:** Todo requerimiento es rastreable (GDD → US → Sprint → Métrica)

### Principios para Futuros Proyectos Similares

> *"El éxito no se mide por cantidad de features, sino por claridad del sistema y justicia percibida por el jugador."*

- ✓ Mantener alcance pequeño y bien definido
- ✓ Priorizar legibilidad sobre complejidad
- ✓ Documentar decisiones de diseño (no solo especificaciones)
- ✓ Medir experiencia emocional, no solo métricas técnicas
- ✓ Iterar con datos, no suposiciones

---

## XII. REFERENCIAS Y DOCUMENTACIÓN COMPLETA

### Archivos del Proyecto

| Documento | Propósito | Última Actualización |
|-----------|----------|---------------------|
| [GDD_Diseño_de_Juego.md](./GDD_Diseño_de_Juego.md) | Visión, mecánicas, IA, narrativa | 25/01/2026 |
| [Technical_Architecture.md](./Technical_Architecture.md) | Arquitectura software, módulos, patrones | 25/01/2026 |
| [Project_Management.md](./Project_Management.md) | Sprints, US, PM, riesgos, métricas | 25/01/2026 |
| [Art_and_Audio_specification.md](./Art_and_Audio_specification.md) | Dirección visual, audio, estados visuales | 25/01/2026 |
| [Executive_summary.md](./Executive_summary.md) | Este documento (síntesis ejecutiva) | 25/01/2026 |
| [organizacion del proyecto.txt](./organizacion%20del%20proyecto.txt) | Estructura de carpetas, responsabilidades | 15/01/2026 |

### Cómo Leer Este Documento

- **Para Stakeholders:** Secciones I (Visión), II (Mecánicas), VI (Métricas), IX (Exclusiones)
- **Para Desarrolladores:** Secciones IV (Arquitectura), V (Plan), VII (Riesgos), X (Próximos Pasos)
- **Para Testers:** Secciones VI (KPIs), VIII (Coherencia), IX (Alcance)
- **Para Arte/Audio:** Secciones II (Parámetros), IV (Módulos visuales), secciones específicas en Art_and_Audio_specification.md

### Cadena de Validación (Autoría & Aprobaciones)

- **Concepto & Visión:** Creado 15/01/2026
- **Coherencia Auditada:** 20/01/2026 (sincronización GDD/Arch/PM/Arte verificada)
- **Enhancement Completado:** 25/01/2026 (redacción mejorada, detalle aumentado)
- **Estado Actual:** READY FOR STAKEHOLDER REVIEW

---

**CONCLUSIÓN**

*Threshold of Silence* es un **prototipo académico preciso, documentado y balanceable** que demostrará si la combinación de sigilo táctico + detección legible + aprendizaje iterativo genera la experiencia de tensión intensa buscada.

Con **10 sprints, 123 horas, 4 documentos coherentes y métricas claras**, el proyecto está posicionado para éxito comprobable o feedback estructurado si falla.

**El camino es claro. El tiempo de comenzar es ahora.**

---

*Documento versión 1.1 — Redacción Mejorada & Detalle Expandido*  
*Última revisión: 25 de enero, 2026*  
*Estado: APROBADO PARA DESARROLLO*  

