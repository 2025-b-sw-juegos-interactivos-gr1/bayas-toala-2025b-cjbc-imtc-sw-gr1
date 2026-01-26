# ART & AUDIO SPECIFICATION — Threshold of Silence
## Dirección de Arte, Diseño Visual y Sonoro

---

## I. DIRECCIÓN DE ARTE

### 1. Concepto Visual General

**Estilo:**  
Minimalismo funcional 2D Top-Down orientado a legibilidad sistémica, claridad espacial y comunicación de estados.

**Premisa visual central:**  
El arte no busca realismo ni ornamentación. Cada elemento visual existe para explicar el sistema de sigilo, reforzar la lectura del riesgo y permitir al jugador anticipar consecuencias antes de actuar.

**Alineación MDA (desde GDD):**  
Esta dirección visual materializa la experiencia estética de *Threshold of Silence*:
- **Tensión constante:** Colores sistémicos escalan con el riesgo → feedback inmediato del peligro.
- **Presión psicológica:** Ausencia de ornamentación reduce escape visual → foco total en el sistema.
- **Reto y superación:** Claridad de estados permite inferir causa de fallo → mejora iterativa.

**Influencias visuales principales:**
- *Hotline Miami* — lectura rápida del espacio, contraste fuerte  
- *Mark of the Ninja* — color como lenguaje de sigilo  
- *Door Kickers* — claridad táctica top-down  
- Interfaces de sistemas de vigilancia y CCTV  

**Filosofía de diseño visual:**
- "El jugador debe entender el peligro en menos de un segundo".
- El color, la forma y la animación comunican estado, no decoración.
- El entorno es hostil, controlado y determinista, no narrativo.
- La ausencia de detalle refuerza la tensión (menos ruido visual → más foco).

**Trazabilidad con Arquitectura Técnica:**

| Elemento visual | Módulo técnico | Responsabilidad |
|---|---|---|
| Paleta por estado | `UIManager` + `DetectionSystem` | Comunicar escalamiento de alerta (0–100) |
| Overlay FOV/radio | `UIManager` (debug) | Visualizar cono visión (60°, 6.0u) y radio proximidad (4.0u) |
| Animaciones de estado | `EnemyAI` (FSM) | Reflejar transiciones Patrol → Suspicious → Alert → Confirmed |
| Feedback detección | `EventBus` → `UIManager` | Reacción visual inmediata ante `OnPlayerDetected` |

---

### 2. Paleta de Colores Sistémica (por estado del juego)

> La paleta no evoluciona por actos narrativos, sino por estado del sistema de detección.

**Mapeo de estados (DetectionSystem):**  
Los colores están directamente ligados al valor de alerta acumulativo (0–100) medido por el `DetectionSystem`:
- **0–25:** Exploración Controlada (seguro)
- **26–50:** Sospecha / Riesgo Emergente (atención)
- **51–75:** Alerta / Peligro Inminente (urgencia)
- **76–100:** Detección Confirmada / Game Over (castigo)

#### Estado Base — Exploración Controlada
- **Colores dominantes:** grises fríos, azul oscuro  
- **Saturación:** baja (20–30 %)  
- **Contraste:** medio  
- **Feeling:** calma tensa, vigilancia latente  

Suelo neutro: #2E3440
Muros: #3B4252
Sombras: #1E222A
Iluminación base: #A3B1C6

**Implementación técnica:** Estos colores son la paleta base. Los enemigos usan `Color.base` (blanco con saturación baja). El entorno no cambia en este estado.

#### Estado de Sospecha — Riesgo Emergente
- **Colores dominantes:** amarillo / ámbar  
- **Saturación:** media  
- **Contraste:** medio–alto  
- **Feeling:** advertencia, atención obligatoria  

Alerta suave: #EBCB8B
Pulso warning: #D08770

**Implementación técnica:** Los enemigos transicionan a pulso visual lento (0.5–1.0s). El entorno permanece sin cambios. El UI muestra barra de alerta amarilla.

#### Estado de Alerta — Peligro Inminente
- **Colores dominantes:** naranja → rojo  
- **Saturación:** alta  
- **Contraste:** alto  
- **Feeling:** presión psicológica, urgencia  

Alerta alta: #BF616A
Rojo crítico: #8F2D2D

**Implementación técnica:** Los enemigos transicionan a pulso rápido (0.2–0.4s). El entorno inicia titileo sutil. El UI muestra barra naranja/rojo.

#### Detección Confirmada — Game Over
- **Colores dominantes:** rojo intenso + negro  
- **Saturación:** máxima  
- **Contraste:** extremo  
- **Feeling:** fallo inmediato, castigo claro  

Rojo detección: #FF3B3B
Negro absoluto: #000000

**Implementación técnica:** Flash rojo extremo (255, 59, 59) durante 0.2–0.3s. Transición a pantalla Game Over con fondo negro + texto centrado.

**Regla inquebrantable:**  
Ningún color se utiliza si no comunica riesgo, estado o interacción.

---

### 3. Estilo Visual de Elementos del Juego

#### 3.1 Entorno (Niveles)
- **Formato:** 2D Top-Down modular  
- **Estilo:** Geometría simple, alto contraste  

**Características:**
- Tiles modulares (pared / suelo / cobertura)
- Bordes duros y legibles
- Iluminación falsa (no realista) para lectura
- Ausencia de detalles decorativos pequeños

**Zonas visuales clave:**

| Zona | Tratamiento visual | Función |
|---|---|---|
| Pasillos | Oscuros, estrechos | Control del ritmo |
| Áreas abiertas | Iluminadas, expuestas | Alto riesgo |
| Coberturas | Contraste fuerte | Planeación |
| Esquinas | Sombras marcadas | Observación |

**Parámetros de diseño espacial (desde GDD):**  
El layout debe permitir las siguientes rutas de riesgo:
- **Segura:** Cobertura continua, distancia > 6.0u de enemigos, velocidad baja.  
- **Intermedia:** Exposición parcial, entrada a radio proximidad (4.0u), necesita timing.  
- **Directa:** Área abierta, exposición total, requiere velocidad normal y ventana temporal.  

#### 3.2 Protagonista
- Silueta simple  
- Sin rostro ni detalles identificables  
- Animaciones mínimas (idle, movimiento)  

**Colores:** neutros (gris claro / azul apagado)  
**Intención:** no competir visualmente con enemigos ni señales de alerta.

**Tamaño y silhueta:**  
- Tamaño base: ~0.5u (compatible con grid modular).  
- Forma: Círculo u óvalo simple, sin detalles internos.  
- Animaciones: Idle (sin movimiento) + Movimiento (cambio de sprite cada 4–6 frames @ 10 FPS) + Movimiento preciso (cambio cada 8–10 frames).  

**Criterio de legibilidad:** El jugador debe distinguir al protagonista de enemigos en < 0.5s incluso en esquinas u oclusiones parciales.

#### 3.3 Enemigos

| Estado | Representación visual |
|---|---|
| Patrullaje | Color base estable |
| Sospecha | Pulso lento amarillo |
| Alerta | Pulso rápido naranja/rojo |
| Confirmado | Rojo intenso + flash |

**Parámetros de animación (sincronizados con FSM):**  

| Estado | Velocidad pulso | Color base | Efecto adicional |
|---|---|---|---|
| Patrol | N/A | Gris base (#4C566A) | Ninguno |
| Suspicious | 1.0 ciclo/s | Amarillo (#EBCB8B) | Pulso suave |
| Alert | 2.5 ciclos/s | Naranja (#D08770) | Pulso marcado |
| Confirmed | Flash 10 Hz | Rojo (#FF3B3B) | Destello crítico |

**Timers FSM (coherentes con Arquitectura Técnica):**  
- `Suspicious`: hasta 2.0 s sin estímulos antes de degradar a `Patrol`.  
- `Alert`: hasta 3.0 s sin estímulos antes de degradar a `Suspicious`/`Patrol`.  
- `Cooldown`: 1.5 s tras reducción de estímulos antes de bajar de estado.  

**Integración arquitectónica:** El módulo `UIManager` recibe notificaciones del `EnemyAI` (FSM state) via `EventBus` y aplica el shader/material correspondiente en tiempo real.

---

## II. PROTOTIPOS Y MOCKUPS

### 1. Prototipos de Baja Fidelidad (Lo-Fi)

**Objetivo:** Validar sigilo, detección y lectura espacial.

**Incluye:**
- Greybox de niveles
- Sprites geométricos simples
- Overlays de detección:
  - Cono de visión
  - Radio de proximidad
  - Radio de ruido
  - Medidor 0–100

**Criterio de aceptación:**  
Un tester puede responder:  
> "Fui detectado por visión / ruido / proximidad".

**Sprint asociado:** Sprint 3–4 (IA Enemiga + Detección).  
**Duración estimada:** 4–6 horas (sprites geométricos + overlays gizmos Unity).

---

### 2. Prototipos de Media Fidelidad (Mid-Fi)

**Objetivo:** Consolidar el lenguaje visual definitivo.

**Incluye:**
- Sprites definitivos simplificados
- Paleta por estado aplicada
- UI mínima funcional
- Feedback audiovisual sincronizado

**Sprint asociado:** Sprint 6–7 (Feedback + Debug overlays).  
**Duración estimada:** 6–8 horas.  
**Criterios de aceptación (DoD):**  
- [ ] Paleta aplicada a enemigos, UI y overlays.  
- [ ] Transiciones de color sin artifacts.  
- [ ] Overlays debug visualizan correctamente cono (60°, 6.0u) y radio (4.0u).  
- [ ] Audio sincronizado con cambios de estado visual.  

---

### 3. Wireframes de Interfaz (UI)

#### HUD Gameplay

**Layout en pantalla:**

```
┌─────────────────────────────────────┐
│ ALERT: ░░░░░░░░░░░░░░░░░░░░░ 0/100 │
│                                       │
│ (Escenario Top-Down)                  │
│                                       │
│ [SHIFT] SIGILO | [ESC] PAUSA         │
└─────────────────────────────────────┘
```

**Especificación técnica:**  
- **Barra de alerta:** Anchura 200px, altura 20px, posición superior izquierda (10, 10).  
- **Relleno:** Actualiza cada frame según `DetectionSystem.alertValue`.  
- **Colores de relleno:** Verde (0–25) → Amarillo (26–50) → Naranja (51–75) → Rojo (76–100).  
**Indicador de modo:** Muestra "[SIGILO (Movimiento preciso)]" si Shift está presionado (color: azul #A3B1C6).  
**Referencia cruzada:** Ver mapeo de controles en [GDD_Diseño_de_Juego.md](c:\Users\IsmaelMToalaC\Downloads\Proyecto_II_DJI\GDD_Diseño_de_Juego.md).

**Renderización:** Implementado via `Canvas` (UI system Unity) + `Image` component con `fillAmount`.

#### Game Over

**Layout:**

```
┌─────────────────────────────────────┐
│ DETECTED                              │
│ Cause: LINE OF SIGHT                  │
│ Attempts: 5                           │
│                                       │
│ [R] Restart | [M] Menu               │
└─────────────────────────────────────┘
```

**Especificación técnica:**  
- **Pantalla:** Fondo negro (#000000) con 80% opacidad.  
- **Texto:** Blanco (#FFFFFF), fuente monoespaciada.  
- **Causa de detección:** Etiqueta dinámica desde `MetricsLogger` (valores: "LINE OF SIGHT", "PROXIMITY", "NOISE", "UNKNOWN").  
- **Contador:** Muestra número de intentos acumulados en la sesión actual.  

**Acción de reinicio:** Presionar R dispara `GameManager.RestartLevel()` via input handling en `UIManager`.

---

### 3.1 Debug Overlays (Opcional, usado durante desarrollo y QA)

**Habilitación:** Toggle con tecla F1 o desde menú de pausa.

#### Overlay 1: Campo de Visión Enemigo (Vision Cone)
- **Visualización:** Sector circular de 60°, radio 6.0u, origen en enemigo.  
- **Color:** Verde translúcido (#00FF00, α=0.3) si el jugador NO está visible; Rojo (#FF0000, α=0.3) si está visible.  
- **Implementación:** `Handles.DrawSolidArc()` (Editor) o líneas con `Gizmos` en `OnDrawGizmosSelected()` para aproximar el sector.  

#### Overlay 2: Radio de Proximidad
- **Visualización:** Círculo de radio 4.0u alrededor del enemigo.  
- **Color:** Amarillo translúcido (#FFFF00, α=0.3).  
- **Implementación:** `Gizmos.DrawWireSphere()`.  

#### Overlay 3: Radio de Ruido
- **Visualización:** Círculo dinámico alrededor del jugador que expande/contrae según nivel de ruido.  
- **Radio base:** 0.5u (sigilo) a 1.5u (movimiento normal).  
- **Color:** Azul translúcido (#0000FF, α=0.3).  
- **Implementación:** Actualiza cada frame en `NoiseSystem.Update()`.  

#### Overlay 4: Medidor de Detección
- **Visualización:** Número flotante sobre el enemigo mostrando valor actual de alerta (0–100).  
- **Color del texto:** Verde → Amarillo → Rojo según escala.  
- **Implementación:** `GUI.Label()` o `3D Text Mesh` en mundo.  

**Criterio de aceptación:** Overlays son legibles y no causan lag perceptible (< 2ms adicionales).

---

## III. DIRECCIÓN DE AUDIO

### 1. Concepto Sonoro General

**Estilo:**  
Minimalista, funcional, orientado a telemetría emocional.

**Filosofía:**
- El sonido anticipa peligro.
- El silencio incrementa tensión.
- Cada SFX corresponde a una mecánica.
- La duración del audio refuerza el ritmo de reintento rápido.

**Alineación MDA:**  
El audio refuerza la **dinámica** de escalamiento de riesgo y la **estética** de tensión:
- Drone suave (Exploración) → pulsos leves (Sospecha) → ritmo marcado (Alerta) → corte abrupto (Confirmado).  
- Cada transición es inmediata y perceptible en < 0.5s.  
- El silencio es usado estratégicamente para amplificar momentos críticos.  

---

### 2. Música por Estado del Juego (con duración)

| Estado | Características | Duración |
|---|---|---|
| Exploración | Drone suave, casi inaudible | 60–120 s (loop) |
| Sospecha | Pulsos rítmicos leves | 30–60 s (loop corto) |
| Alerta | Ritmo más marcado, capas | 20–45 s (loop) |
| Detección | Corte abrupto / stinger | 0.5–1.5 s |

**Parámetros técnicos (implementación AudioManager):**  

| Estado | Volumen | Fade-in | Fade-out | Disparador técnico |
|---|---|---|---|---|
| Patrol | -12 dB | N/A | 1.0 s | Inicio de nivel / `EnemyAI.state == Patrol` |
| Suspicious | -8 dB | 0.5 s | 0.3 s | `DetectionSystem.alertValue >= 25` |
| Alert | -3 dB | 0.2 s | 0.2 s | `DetectionSystem.alertValue >= 50` |
| Confirmed | 0 dB (stinger) | N/A | N/A | `OnPlayerDetected` event |

**Gestión de transiciones:**  
- Las transiciones entre estados de música se disparan via `EventBus.Publish()`.  
- El `AudioManager` suscrito a `OnAlertChanged` e `OnPlayerDetected` cambia la pista de música activa.  
- Crossfade de 0.5s entre pistas para evitar cortes abruptos (excepto en Confirmed).  

**Producción de referencias:**  
- Exploración: Sine wave drone a 50 Hz, modulación LFO suave (~2 Hz).  
- Sospecha: Pulsos de kick sintético (70–120 BPM), sin melodía.  
- Alerta: Pulse rápida (140–180 BPM) + bajo sintetizado.  
- Detección: Stinger de 1.0s (crash + reversa, final abrupto).  

---

### 3. Catálogo de Efectos de Sonido (SFX) con duración

**Organización del catálogo:**  
Los SFX están agrupados por mecánica y mapeados directamente a eventos del sistema via `EventBus`.

#### Movimiento del jugador
| SFX | Uso | Duración |
|---|---|---|
| Pasos suaves | Modo sigilo | 0.10–0.20 s |
| Pasos rápidos | Movimiento normal | 0.12–0.25 s |

**Parámetros de disparo:**  

| SFX | Evento trigger | Volumen | Pitch | Pan |
|---|---|---|---|---|
| Pasos suaves | `PlayerController.MoveWithMode(STEALTH)` cada ~0.4 s | -15 dB | 1.0 ± 0.1 (variación) | Center |
| Pasos rápidos | `PlayerController.MoveWithMode(NORMAL)` cada ~0.3 s | -10 dB | 0.9 ± 0.15 | Center |

**Implementación técnica:**  
Cada SFX se dispara desde `PlayerController.OnFootstep()` que emite evento al `EventBus`. El `AudioManager` reproduce el clip correspondiente.

#### Enemigos
| SFX | Uso | Duración |
|---|---|---|
| Paso enemigo | Posicionamiento | 0.15–0.30 s |
| Alerta leve | Sospecha | 0.40–0.80 s |
| Alerta fuerte | Detección cercana | 0.60–1.20 s |

**Parámetros de disparo:**  

| SFX | Evento trigger | Volumen | Pan | Frecuencia |
|---|---|---|---|---|
| Paso enemigo | `EnemyAI` completa movimiento cada ~1.5 s | -12 dB | Spatial 3D | 1 vez por paso |
| Alerta leve | `EnemyAI.state: Patrol → Suspicious` | -8 dB | Spatial 3D | 1 vez |
| Alerta fuerte | `EnemyAI.state: Suspicious → Alert` | -3 dB | Spatial 3D | 1 vez |

**Audio 3D:** Los pasos de enemigos usan panning 3D según distancia del jugador (atenuación 1/distancia).

#### Sistema
| SFX | Uso | Duración |
|---|---|---|
| Detección confirmada | Trigger crítico | 0.20–0.50 s |
| Game Over | Fallo | 0.80–1.60 s |
| Restart | Reintento inmediato | 0.30–0.80 s |
| Interacción | Objetivo | 0.25–0.60 s |

**Parámetros de disparo:**  

| SFX | Evento trigger | Volumen | Duración real | Interrupción anterior |
|---|---|---|---|---|
| Detección confirmada | `OnPlayerDetected` | 0 dB | 0.3 s | Detiene música |
| Game Over | `GameManager.ShowGameOver()` | -2 dB | 1.2 s | Detiene SFX de movimiento |
| Restart | `GameManager.RestartLevel()` input | -6 dB | 0.5 s | No interrumpe |
| Interacción (objetivo) | `OnObjectiveInteracted` | -8 dB | 0.4 s | Oclusionado por ruido enemigo |

**Implementación técnica:**  
Todos estos SFX se disparan mediante `AudioManager.PlaySFX(name)` con parámetros opcionales (volumen, pitch, delay).

---

### 4. Audio Espacial (2D)

- Paneo izquierda/derecha
- Atenuación por distancia
- Prioridad sonora a enemigos
- Los sonidos críticos nunca superan los 2 segundos

**Especificación técnica:**  

| Parámetro | Valor | Justificación |
|---|---|---|
| **Distancia de paneo** | 0 a 12 unidades desde cámara | Rango visual del jugador |
| **Curva de atenuación** | Logarítmica (1/distancia^1.5) | Realismo perceptual sin exceso |
| **Max simultáneos** | 8 SFX 3D + 2 música | Evitar sobrecarga de recursos |
| **Prioridad** | Enemigos > Música > Pasos jugador | Crítico para legibilidad |

**Instanciación de Audio Source:**  
El `AudioManager` mantiene un pool de ~6 `AudioSource` reutilizables para SFX 3D. La música ocupa 2 pistas (crossfade entre estados).

---

## IV. PIPELINE DE PRODUCCIÓN

**Trazabilidad con Project Management:**  
El pipeline se alinea con los sprints de desarrollo (véase Project_Management.md):

| Fase | Sprint | User Story | Duración estimada | Salida |
|---|---|---|---|---|
| Prototipos Lo-Fi | 3–4 | US-05, US-06 | 4–6 h | Gizmos + sprites geométricos |
| Prototipos Mid-Fi | 6–7 | US-13, US-14, US-16 | 8–10 h | Sprites + UI + SFX base |
| Pulido final | 8–9 | US-17, US-21 | 4–6 h | Sonidos balanceados + sprites finales |

### Arte

Referencia visual
→ Greybox
→ Sprite base
→ Ajuste de contraste
→ Integración en motor

**Herramientas:** Aseprite (sprites) / Krita (UI mocks).  
**Criterios de salida:**  
- [ ] Sprites sin anti-aliasing (pixelart nítido).  
- [ ] Paleta exactamente acorde a especificación RGB.  
- [ ] Animaciones (4–8 frames @ 10 FPS).  
- [ ] Compatible con importación Unity (PNG con transparency).  

### Audio

Referencia
→ Edición básica
→ Normalización
→ Implementación
→ Pruebas de legibilidad

**Herramientas:** Audacity (síntesis + edición) / FMOD Studio (opcional para avanzado).  
**Criterios de salida:**  
- [ ] Sonidos en formato WAV/OGG 44.1 kHz mono.  
- [ ] Normalización a -3 dB pico.  
- [ ] Duración dentro de especificaciones.  
- [ ] Prueba de sincronia con eventos (Delay < 50 ms).  

---

## V. CRITERIOS DE CALIDAD Y ACEPTACIÓN

**Integración con Definition of Done (DoD) del Project Management:**  
Toda entrega de arte/audio debe cumplir estos criterios antes de ser marcada como "Done":

### Arte
- [ ] Lectura clara del riesgo
- [ ] Contraste suficiente
- [ ] Coherencia con estados
- [ ] Sin ruido visual innecesario
- [ ] Sprites importados y sin errores en Unity (no pixelated scaling)
- [ ] Animaciones sincronizadas con FSM de enemigos
- [ ] Overlays debug activos y sin lag perceptible
- [ ] Testeado con 2+ testers para legibilidad

### Audio
- [ ] Sonido explica estado
- [ ] Duraciones acordes al ritmo del juego
- [ ] Sin saturación
- [ ] Silencio usado estratégicamente
- [ ] Eventos sincronizados (Delay < 50 ms) con sistemas de gameplay
- [ ] Volumen balanceado (ratio música/SFX = 1:1.2)
- [ ] Atenuación 3D aplicada correctamente a enemigos
- [ ] Transiciones de música suave (fade 0.5s mín)

---

## VI. REFERENCIAS CRUZADAS Y TRAZABILIDAD

**Documentos relacionados:**  
1. **GDD_Diseño_de_Juego.md** — Parámetros del sistema (radios 4.0–6.0u, ángulos 60°, umbrales 0–100).  
2. **Technical_Architecture.md** — Módulos `AudioManager`, `UIManager`, `DetectionSystem`, `EventBus`.  
3. **Project_Management.md** — Sprints 3–10, user stories US-05 a US-23, DoD.  
4. **organizacion del proyecto.txt** — Asignación de responsables y recursos.  

**Cambios en futuras iteraciones:**  
Si se modifican parámetros del `DetectionSystem` (radios, umbrales), esta especificación debe actualizarse en las secciones:
- 2.2 (Mapeo de estados)
- 3.1 (Parámetros de diseño espacial)
- 3.3 (Animación de enemigos con timings)
- III.2 (Volumen de música)
- III.3 (Disparadores de SFX)
