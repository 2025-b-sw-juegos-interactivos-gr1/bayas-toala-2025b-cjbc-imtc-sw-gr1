# Threshold of Silence - Nivel 1: Documentación Técnica

## 📋 Descripción General

**Threshold of Silence** es un juego de sigilo top-down 2D donde el jugador debe infiltrarse evitando la detección enemiga. El Nivel 1 sirve como introducción a las mecánicas básicas del juego.

---

## 🎮 Mecánicas Implementadas

### 1. Sistema de Movimiento del Jugador

| Parámetro | Valor | Descripción |
|-----------|-------|-------------|
| Velocidad Normal | 3.5 u/s | Movimiento estándar con WASD |
| Velocidad Sigilo | 2.0 u/s | Movimiento lento con Shift |
| Direcciones | 8 | Movimiento en todas direcciones |

**Script:** `PlayerController.cs`

**Características:**
- Movimiento fluido en 8 direcciones
- Modo sigilo que reduce velocidad y ruido
- Compatible con New Input System de Unity
- Rigidbody2D sin gravedad para vista top-down

---

### 2. Sistema de Detección Enemiga (IA)

**Script:** `EnemyAI.cs`

#### Estados de la FSM (Máquina de Estados Finitos):

```
┌─────────────┐    30%     ┌─────────────┐    70%     ┌─────────────┐   100%    ┌─────────────┐
│   PATROL    │ ────────►  │ SUSPICIOUS  │ ────────►  │    ALERT    │ ───────►  │  CONFIRMED  │
│ (Patrullaje)│            │ (Sospechoso)│            │   (Alerta)  │           │ (Game Over) │
└─────────────┘            └─────────────┘            └─────────────┘           └─────────────┘
       ▲                          │                         │
       └──────────────────────────┴─────────────────────────┘
                        (Detección decrece con el tiempo)
```

#### Parámetros de Detección:

| Tipo | Valor | Descripción |
|------|-------|-------------|
| Radio de Proximidad | 4.0 unidades | Detecta al jugador cercano |
| Ángulo de Visión | 60° | Cono de visión frontal |
| Distancia de Visión | 6.0 unidades | Alcance máximo de visión |
| Tasa de Incremento | 70/segundo | Velocidad de detección |
| Tasa de Decremento | 5/segundo | Velocidad de "olvido" |

#### Sistema de Ruido:

| Modo | Radio Extra |
|------|-------------|
| Movimiento Normal | +1.5 unidades |
| Movimiento Sigilo | +0.5 unidades |

---

### 3. Sistema de Patrullaje

**Script:** `EnemyAI.cs` + `PatrolPoint.cs`

- Los enemigos siguen puntos de patrullaje predefinidos
- Rutas cíclicas y predecibles (diseño intencional para permitir aprendizaje)
- Velocidad de patrullaje: 2.0 u/s
- Tiempo de espera en cada punto: 1.0 segundo

---

### 4. Sistema de Game Over

**Script:** `GameManager.cs`

- **Condición:** Nivel de detección alcanza 100%
- **Resultado:** Juego se pausa, se muestra pantalla de "¡DETECTADO!"
- **Reinicio:** Presionar R o ESC

---

### 5. Sistema de Victoria

**Script:** `ExtractionPoint.cs`

- **Condición:** Jugador toca el punto de extracción (trigger)
- **Resultado:** Pantalla de "¡VICTORIA!"
- **Reinicio:** Presionar R o ESC para jugar de nuevo

---

### 6. Sistema de Colisiones

| Objeto | Collider | Is Trigger | Función |
|--------|----------|------------|---------|
| Player | BoxCollider2D | No | Bloquea movimiento |
| Walls | BoxCollider2D | No | Obstáculos sólidos |
| Enemy | BoxCollider2D | No | Cuerpo del enemigo |
| ExtractionPoint | BoxCollider2D | Sí | Detecta llegada del jugador |

---

## 🗂️ Estructura de Scripts

```
Assets/
└── Scripts/
    ├── Player/
    │   └── PlayerController.cs      # Control del jugador
    ├── Enemy/
    │   └── EnemyAI.cs               # IA y detección enemiga
    ├── Core/
    │   ├── GameManager.cs           # Estado del juego
    │   └── ExtractionPoint.cs       # Punto de victoria
    ├── Level/
    │   ├── Obstacle.cs              # Muros/obstáculos
    │   ├── PatrolPoint.cs           # Puntos de patrullaje
    │   └── FloorGrid.cs             # Generador de piso
    ├── UI/
    │   ├── UIManager.cs             # Interfaz de usuario
    │   └── HUDController.cs         # HUD del juego
    └── Visuals/
        └── VisionCone.cs            # Cono de visión visual
```

---

## 🎛️ Controles

| Tecla | Acción |
|-------|--------|
| W | Mover arriba |
| A | Mover izquierda |
| S | Mover abajo |
| D | Mover derecha |
| Shift | Modo sigilo (movimiento lento, menos ruido) |
| R | Reiniciar nivel |
| ESC | Reiniciar / Salir |

---

## 🏗️ Objetos de la Escena

### Hierarchy del Nivel 1:

```
New Scene
├── Main Camera          # Cámara ortográfica
├── Player               # Jugador (cyan)
├── Enemy                # Enemigo (rojo)
├── PatrolPoint1         # Punto de patrullaje 1
├── PatrolPoint2         # Punto de patrullaje 2
├── GameManager          # Gestor del juego
├── UIManager            # Gestor de UI
├── ExtractionPoint      # Meta (amarillo/naranja)
├── Floor                # Piso del nivel
├── Wall1                # Obstáculo 1
├── Wall2                # Obstáculo 2
├── Wall3                # Obstáculo 3
├── Canvas               # Canvas de UI
└── EventSystem          # Sistema de eventos UI
```

---

## 🔧 Configuración de Layers

| Layer | Uso |
|-------|-----|
| Default | Objetos generales |
| Player | Jugador |
| Obstacles | Muros y obstáculos |
| Enemy | Enemigos |

---

## 📊 Parámetros de Balance (Nivel 1 - Fácil)

Este nivel está diseñado como **introducción** con dificultad baja:

| Aspecto | Configuración |
|---------|---------------|
| Enemigos | 1 |
| Puntos de patrullaje | 2-3 |
| Cobertura disponible | Alta |
| Rutas alternativas | Múltiples |
| Tiempo de detección | ~1.5 segundos |

---

## 🎯 Objetivo del Nivel

1. El jugador aparece en el punto de inicio
2. Debe observar el patrón de patrullaje del enemigo
3. Planificar una ruta hacia el punto de extracción
4. Ejecutar el movimiento evitando detección
5. Usar modo sigilo en zonas de riesgo
6. Alcanzar el punto de extracción para ganar

---

## 📝 Notas de Desarrollo

- **Motor:** Unity 2020+
- **Input System:** New Input System Package
- **Render Pipeline:** Universal Render Pipeline (URP)
- **Resolución objetivo:** 1920x1080
- **Plataforma:** PC (Windows)

---

*Documentación creada para Threshold of Silence - Proyecto Académico*
*Autores: Carlos Bayas e Ismael Toala*
