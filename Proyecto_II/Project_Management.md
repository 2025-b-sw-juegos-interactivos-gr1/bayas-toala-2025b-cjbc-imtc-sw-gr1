# Project_Management — Threshold of Silence:

## Planificación y Gestión del Proyecto (Project Management)

**Proyecto:** Threshold of Silence (Top-Down 2D / Acción–Sigilo)  
**Objetivo del documento:** Documentar la planificación y gestión del proyecto *Threshold of Silence* bajo un enfoque ágil, evidenciando la organización del trabajo mediante la definición de épicas, user stories, estimaciones de esfuerzo, planificación de sprints, cronograma de producción, métricas de éxito y gestión de riesgos.

---

## 1. Enfoque de gestión del proyecto

### 1.1 Marco de gestión adoptado
El proyecto adopta un **marco ágil híbrido Scrum–Kanban**, en el cual:
- **Scrum** estructura la planificación iterativa por sprints y la definición de entregables.
- **Kanban** permite visualizar y controlar el flujo de trabajo a través del tablero (WIP y estados).

**Cadencia del proyecto:** 10 semanas (**sprints semanales de 1 semana**).

### 1.2 Definición de Done (DoD) global
Una User Story se considera completada únicamente cuando cumple:

- Funcionalidad implementada y validada en escena de prueba (*Playground*).
- Ausencia de errores críticos (crash, softlock o bloqueo de progreso).
- Feedback visual y/o sonoro implementado cuando aplique.
- Parámetros expuestos en configuración (ScriptableObjects / JSON) cuando corresponda.
- Historia actualizada en el tablero con evidencia asociada (commit, captura o nota técnica).

### 1.3 Flujo de trabajo (tablero)
**Columnas del tablero:**
- Backlog  
- To Do (Sprint)  
- In Progress  
- Code Review  
- Testing  
- Done  

**Etiquetas de clasificación:**  
`core`, `ai`, `detection`, `noise`, `ui`, `audio`, `level`, `debug`, `qa`, `docs`

---

## 2. Herramientas de gestión

**Herramienta de gestión ágil:** GitHub Projects / Trello / Jira.  

**Uso de la herramienta:**
- Épicas como contenedores de alto nivel.
- User Stories como unidades planificables por sprint.
- Evidencia por historia: enlaces a commits/tags, capturas del tablero y/o notas de prueba.

**Control de versiones:** Git (GitHub / GitLab).  
**Documentación:** Markdown (GDD y Project Management como documentos vivos).  

---

## 3. Épicas (Epics)

1. **E1 — Core Gameplay (Movimiento y control)**
2. **E2 — IA Enemiga (FSM y patrullaje)**
3. **E3 — Sistema de Detección (proximidad + visión + umbrales)**
4. **E4 — Sistema de Ruido (emisión e influencia en alerta)**
5. **E5 — Game Loop (Game Over + reinicio inmediato)**
6. **E6 — Feedback y UI (alerta, overlays, legibilidad)**
7. **E7 — Audio (SFX, música por estado)**
8. **E8 — Level Design (greybox, rutas, balance)**
9. **E9 — QA y Balanceo (playtesting + métricas)**
10. **E10 — Documentación y Entrega (build, GDD, reporte)**

---

## 4. User Stories con estimaciones (Backlog priorizado)

> Estimación en horas para un entorno académico (1 persona).  
> Prioridad: **P0 (crítico), P1 (alto), P2 (medio), P3 (bajo).**

### E1 — Core Gameplay (Movimiento y control)
| ID | User Story | Criterios de aceptación | Prioridad | Estimación |
|---|---|---|---:|---:|
| US-01 | Como jugador, quiero moverme en 8 direcciones con respuesta inmediata para controlar el sigilo con precisión. | Movimiento 8-dir, sin inercia compleja, colisiones sólidas, cámara sigue al jugador. | P0 | 6h |
| US-02 | Como jugador, quiero alternar entre movimiento normal y modo sigilo para gestionar el riesgo. | Dos velocidades (3.5 / 2.0), input estable (Shift), transición sin bugs. | P0 | 4h |
| US-03 | Como jugador, quiero inputs consistentes (WASD, Shift, E, R, Esc) para jugar sin fricción. | Mapeo completo + remapeo documentado (si aplica). | P1 | 3h |

### E2 — IA Enemiga (FSM y patrullaje)
| ID | User Story | Criterios de aceptación | Prioridad | Estimación |
|---|---|---|---:|---:|
| US-04 | Como jugador, quiero enemigos con patrullaje predecible para poder planificar rutas. | Waypoints/rutas, rotación coherente, repetición estable. | P0 | 6h |
| US-05 | Como diseñador, quiero una FSM con estados Patrol/Suspicious/Alert/Confirmed para controlar escalamiento. | Transiciones por estímulos, timers de degradación, logs de estado. | P0 | 8h |

### E3 — Sistema de Detección (proximidad + visión + umbrales)
| ID | User Story | Criterios de aceptación | Prioridad | Estimación |
|---|---|---|---:|---:|
| US-06 | Como jugador, quiero que el enemigo detecte por proximidad para evitar acercarme demasiado. | Radio base configurable; al entrar incrementa alerta. | P0 | 5h |
| US-07 | Como jugador, quiero que la línea de visión sea el factor más fuerte para entender el peligro. | Cono configurable; raycast bloqueado por obstáculos; prioridad sobre otros factores. | P0 | 8h |
| US-08 | Como diseñador, quiero un medidor de detección 0–100 para balancear dificultad. | Acumulación a umbral 100; decaimiento; parámetros expuestos. | P0 | 6h |

### E4 — Sistema de Ruido (emisión e influencia)
| ID | User Story | Criterios de aceptación | Prioridad | Estimación |
|---|---|---|---:|---:|
| US-09 | Como jugador, quiero que correr genere más ruido para penalizar decisiones apresuradas. | Normal +1.5u, sigilo +0.5u (ajustable); enemigos en radio aumentan alerta. | P0 | 6h |
| US-10 | Como diseñador, quiero que el ruido NO sea detección instantánea sino acelerador de alerta. | Ruido incrementa sospecha/alerta; no confirma por sí solo. | P0 | 4h |

### E5 — Game Loop (Game Over + reinicio)
| ID | User Story | Criterios de aceptación | Prioridad | Estimación |
|---|---|---|---:|---:|
| US-11 | Como jugador, quiero Game Over inmediato al ser detectado para que el juego sea punitivo y claro. | Confirmed dispara evento global; pantalla/estado GameOver. | P0 | 4h |
| US-12 | Como jugador, quiero reinicio instantáneo para iterar y aprender sin fricción. | Tecla R reinicia; estado determinista; sin espera extensa. | P0 | 5h |

### E6 — Feedback y UI (alerta, debug overlays)
| ID | User Story | Criterios de aceptación | Prioridad | Estimación |
|---|---|---|---:|---:|
| US-13 | Como jugador, quiero feedback visual del estado de alerta para entender el riesgo. | UI simple + cambio de estado legible. | P1 | 5h |
| US-14 | Como tester, quiero overlays de depuración (FOV/radio/ruido) para explicar detecciones. | Toggle debug; muestra cono, radios y valor 0–100. | P0 | 6h |
| US-15 | Como jugador, quiero menú pausa básico para controlar sesiones de prueba. | Esc pausa; Resume/Quit. | P2 | 3h |

### E7 — Audio (SFX y música por estado)
| ID | User Story | Criterios de aceptación | Prioridad | Estimación |
|---|---|---|---:|---:|
| US-16 | Como jugador, quiero SFX esenciales para reforzar estados del sistema. | Pasos, alerta, confirmación, game over, reinicio. | P2 | 4h |
| US-17 | Como jugador, quiero música por estado para modular tensión. | Capas o pistas por estados del juego; transición suave. | P3 | 5h |

### E8 — Level Design (greybox, rutas, balance)
| ID | User Story | Criterios de aceptación | Prioridad | Estimación |
|---|---|---|---:|---:|
| US-18 | Como jugador, quiero un nivel greybox con rutas de riesgo para experimentar el core loop. | Nivel 1 con rutas (segura/intermedia/directa); objetivo y salida. | P0 | 8h |
| US-19 | Como jugador, quiero 1–2 niveles adicionales para validar progresión de dificultad. | Mayor complejidad espacial y presión; sin perder legibilidad. | P2 | 10h |

### E9 — QA y Balanceo (playtesting + métricas)
| ID | User Story | Criterios de aceptación | Prioridad | Estimación |
|---|---|---|---:|---:|
| US-20 | Como diseñador, quiero instrumentación simple para medir intentos/tiempo/causa de detección. | Registro local CSV/JSON por sesión. | P1 | 6h |
| US-21 | Como diseñador, quiero 2 rondas de playtesting para iterar el balanceo con evidencia. | 5–10 testers; ajustes documentados; comparación de métricas. | P1 | 8h |

### E10 — Documentación y Entrega
| ID | User Story | Criterios de aceptación | Prioridad | Estimación |
|---|---|---|---:|---:|
| US-22 | Como evaluador, quiero un build estable del prototipo para comprobar el gameplay. | Build ejecutable + instrucciones; sin crashes. | P0 | 4h |
| US-23 | Como evaluador, quiero evidencia documental (GDD + PM + resultados) para validar proceso. | Documentos actualizados + evidencias + resumen de métricas. | P0 | 6h |

---

## 5. Plan de Sprints (Sprint Planning)

**Duración del sprint:** 1 semana  
**Total de sprints:** 10  
**Objetivo global:** Construir incrementos jugables, validando legibilidad y tensión del sigilo mediante iteración controlada.

### Sprint 1 – Preproducción y base del proyecto
**Objetivo:** Establecer repositorio, estructura del proyecto y planificación inicial.
- Setup repositorio, escena base y tablero
- Definición de parámetros iniciales (configs)
- US-23 Documentación base (PM + referencia al GDD)
**Entregable:** Repositorio versionado + tablero operativo + documentación inicial

### Sprint 2 – Core Gameplay
**Objetivo:** Implementar el control del jugador.
- US-01, US-02, US-03  
**Entregable:** Jugador totalmente controlable en escena de prueba

### Sprint 3 – IA Enemiga (FSM)
**Objetivo:** Implementar patrullaje y estados base.
- US-04, US-05  
**Entregable:** IA legible y predecible (patrulla + FSM)

### Sprint 4 – Sistema de Detección
**Objetivo:** Implementar detección determinista (proximidad + visión).
- US-06, US-07, US-08  
**Entregable:** Detección explicable con medidor 0–100

### Sprint 5 – Sistema de Ruido
**Objetivo:** Integrar ruido como variable de riesgo.
- US-09, US-10  
**Entregable:** Ruido integrado al escalamiento de alerta

### Sprint 6 – Game Loop
**Objetivo:** Completar el bucle de juego (fallo + reinicio).
- US-11, US-12  
**Entregable:** Core loop funcional (intento → fallo → reintento)

### Sprint 7 – Feedback y Depuración
**Objetivo:** Asegurar legibilidad del sistema.
- US-13, US-14, US-15  
**Entregable:** UI mínima + overlays debug operativos

### Sprint 8 – Level Design (Nivel 1)
**Objetivo:** Construir nivel principal en greybox.
- US-18 + balance inicial  
**Entregable:** Nivel 1 jugable con rutas de riesgo

### Sprint 9 – QA y Balanceo
**Objetivo:** Ajustar dificultad con evidencia cuantitativa.
- US-20, US-21  
**Entregable:** Métricas + iteraciones de balanceo documentadas

### Sprint 10 – Cierre y Entrega
**Objetivo:** Consolidar entrega final estable.
- US-19 (opcional), US-16/US-17 (si aplica), US-22, US-23  
**Entregable:** Build estable + paquete documental final

---

## 6. Cronograma de Producción (Estimado)

**Supuesto de planificación:** Prototipo académico (*vertical slice*) con 1–3 niveles, sigilo determinista, reinicio rápido, overlays de depuración y playtesting iterativo.

| Semana | Fase | Entregable principal | Actividades clave | Criterio de salida |
|------:|------|---------------------|------------------|-------------------|
| 1 | Preproducción | GDD v1 + Backlog | Alcance, parámetros base, FSM | Documentación estable |
| 2 | Prototipo Core | Movimiento | Movimiento y colisiones | Control funcional |
| 3 | IA Base | Patrullaje + FSM | Estados y rutas | IA legible |
| 4 | Detección | Proximidad y visión | Raycasts y umbrales | Detección explicable |
| 5 | Ruido | Sistema de ruido | Emisión e influencia | Alerta progresiva |
| 6 | Feedback | UI + Audio | Overlays y SFX | Legibilidad del sistema |
| 7 | Nivel 1 | Greybox funcional | Rutas y balance | Nivel jugable |
| 8 | Iteración | Balanceo | Ajuste de parámetros | Métricas mejoradas |
| 9 | Nivel 2–3 | Escalamiento | Complejidad adicional | Progresión clara |
| 10 | QA / Entrega | Build final | Pruebas y documentación | Build estable |

---

## 7. Métricas de Éxito

**Definición de éxito:** El proyecto se considera exitoso si el prototipo es jugable, evaluable y coherente con el modelo MDA, generando tensión constante, aprendizaje iterativo y legibilidad del sistema.

| Métrica | Qué evalúa | Meta |
|---|---|---|
| Tasa de completación | Superabilidad del nivel | ≥ 70% |
| Intentos por completación | Dificultad real | 6–15 |
| Tiempo a primera completación | Curva de aprendizaje | 8–15 min |
| Detección explicable | Legibilidad | ≥ 85% |
| Muertes injustas | Frustración | ≤ 15% |
| Uso del modo sigilo | Relevancia mecánica | 30–60% |
| Estabilidad del build | Calidad técnica | 0 crashes |
| Tensión percibida | Estética MDA | ≥ 4/5 |

### Instrumentación mínima
- Registro local (CSV/JSON): intentos, tiempo, causa de detección y estado final por sesión.
- Etiquetado de causa de fallo (visión/ruido/proximidad/no determinado) para análisis posterior.

---

## 8. Gestión de Riesgos

| Riesgo | Impacto | Probabilidad | Mitigación |
|---|---|---|---|
| Detección no legible | Alto | Media | Overlays, feedback claro, ajuste de umbrales/raycast |
| Dificultad excesiva | Alto | Media | Rutas seguras, ventanas de oportunidad, balanceo iterativo |
| IA inconsistente | Alto | Media | FSM estricta, logs de transición, pruebas controladas |
| Scope creep | Alto | Alta | Congelación de alcance y exclusiones explícitas |
| Falta de playtesting | Alto | Media | 2 rondas mínimas con métricas |
| Tiempo excesivo en arte/audio | Medio | Media | Estilo minimalista y placeholders |
| Rendimiento | Medio | Baja | Optimización básica y límites de entidades |

---

## 9. Resumen de Carga Estimada del Proyecto

| Prioridad | Descripción | Horas estimadas |
|---|---|---:|
| P0 | Funcionalidades críticas del slice | 76 h |
| P1 | QA, métricas y refinamiento | 25 h |
| P2–P3 | Funcionalidades complementarias | 22 h |
| **Total estimado** |  | **123 h** |

---

## 10. Herramientas y Recursos

- **Gestión ágil:** GitHub Projects / Trello / Jira  
- **Control de versiones:** Git (GitHub / GitLab)  
- **Motor:** Unity 2D (principal) / Godot (alternativo)  
- **Arte 2D:** Aseprite / Krita  
- **Audio:** Audacity  
- **Documentación:** Markdown  
- **Métricas y evidencias:** Capturas de tablero, commits/tags, logs CSV/JSON, registro de playtesting

---
