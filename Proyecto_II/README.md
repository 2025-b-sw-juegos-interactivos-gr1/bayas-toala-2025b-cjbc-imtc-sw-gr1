# Threshold of Silence

**Top-Down 2D Stealth Action**

![Portada del Juego](./Threshold%20of%20silence.png)

---

## � Vista de Gameplay

![Captura de Gameplay](./Threshold%20of%20Silence_Gameplay.png)

---

## �🎯 ¿Qué es?

*Threshold of Silence* es un videojuego de sigilo táctico donde el jugador se infiltra en escenarios deterministas evitando detección enemiga mediante:

- **Movimiento preciso** (2 velocidades: normal 3.5 u/s, sigilo 2.0 u/s)
- **Detección legible** (proximidad + visión + ruido, 0–100 acumulativo)
- **IA predecible** (FSM: Patrol → Suspicious → Alert → Confirmed)
- **Aprendizaje iterativo** (Game Over inmediato, reinicio sin fricción)

**Filosofía:** Silencio, observación y planificación táctica sobre reacción apresurada.

---

## 🚀 ¿Por Dónde Empiezo?

### Para Tu Rol

| Rol | Empieza Aquí | Tiempo |
|-----|--------------|--------|
| **Manager/Stakeholder** | [Index.md](./Index.md#🎯-inicio-rápido) → Executive_summary.md | 15 min |
| **Programador** | [Index.md](./Index.md#🎯-inicio-rápido) → Technical_Architecture.md | 20 min |
| **Game Designer** | [Index.md](./Index.md#🎯-inicio-rápido) → GDD_Diseño_de_Juego.md | 20 min |
| **Artista/Audio** | [Index.md](./Index.md#🎯-inicio-rápido) → Art_and_Audio_specification.md | 15 min |
| **Project Manager** | [Index.md](./Index.md#🎯-inicio-rápido) → Project_Management.md | 15 min |
| **QA/Tester** | [Index.md](./Index.md#🎯-inicio-rápido) → Métricas de Éxito | 10 min |

**Atajo:** [→ Abre Index.md para navegación centralizada](./Index.md)

---

## 📚 Documentos Principales

| Documento | Propósito | Tamaño |
|-----------|----------|--------|
| **[Index.md](./Index.md)** | 🗺️ Navegación centralizada, búsqueda por concepto, trazabilidad | 500 líneas |
| **[Executive_summary.md](./Executive_summary.md)** | 📋 Síntesis ejecutiva: visión, mecánicas, arquitectura, métricas, riesgos | 560 líneas |
| **[GDD_Diseño_de_Juego.md](./GDD_Diseño_de_Juego.md)** | 🎮 Game Design Document completo: mecánicas, IA, level design, backlog | 1100+ líneas |
| **[Technical_Architecture.md](./Technical_Architecture.md)** | ⚙️ Especificación técnica: módulos, patrones, stack, setup | 420+ líneas |
| **[Art_and_Audio_specification.md](./Art_and_Audio_specification.md)** | 🎨 Dirección visual y audio: paleta, sprites, SFX, HUD, FSM animaciones | 500+ líneas |
| **[Project_Management.md](./Project_Management.md)** | 📊 Plan de producción: 10 sprints, 23 user stories, riesgos, PM | 280+ líneas |

---

## ⚡ Parámetros Clave (Memotécnica)

```
Movimiento:       3.5 u/s (normal)  |  2.0 u/s (sigilo)
Detección:        4.0u (proximidad) | 60°/6.0u (FOV) | +1.5u/+0.5u (ruido)
Acumulación:      100 puntos = Game Over
FSM Timers:       2.0s (Suspicious) | 3.0s (Alert) | 1.5s (Cooldown)
Métricas:         ≥70% finalización | ≥4.0/5 tensión | ≤15% unfair | 100% uptime
```

---

## 🔍 Búsqueda Rápida

**¿Necesitas encontrar algo específico?**

- **¿Cómo se mueve el jugador?** → [GDD § II](./GDD_Diseño_de_Juego.md#ii-sistemas-de-juego) o [Index búsqueda rápida](./Index.md#búsqueda-rápida)
- **¿Cómo funciona la detección?** → [GDD § II](./GDD_Diseño_de_Juego.md#ii-sistemas-de-juego) + [Arch DetectionSystem](./Technical_Architecture.md)
- **¿Cuáles son los sprites?** → [Arte § II](./Art_and_Audio_specification.md#ii-especificación-de-sprites)
- **¿Dónde está el backlog?** → [PM § II](./Project_Management.md#ii-backlog-detallado)
- **¿Cuáles son las métricas?** → [Executive § VI](./Executive_summary.md#vi-métricas-de-éxito)
- **¿Cuáles son los riesgos?** → [Executive § VII](./Executive_summary.md#vii-gestión-de-riesgos)

**Más búsquedas:** [→ Ve a Index.md § BÚSQUEDA RÁPIDA](./Index.md#🎯-búsqueda-rápida)

---

## ⚙️ Setup (Developer)

### Requisitos
- Unity 2020+ o Godot 4.0+
- C# 8.0 / GDScript
- Git (versión control)

### Primeros Pasos
1. Clonar repositorio
2. Abrir proyecto en Unity
3. Crear escena base (`MainScene`)
4. Implementar `PlayerController` (Sprint 2)

**Setup completo:** [→ Technical_Architecture.md § VI](./Technical_Architecture.md#vi-guía-de-setup)

---

## 📖 Cómo Leer Este Proyecto

1. **Eres nuevo aquí?** → Lee README.md (este archivo) + [Index.md](./Index.md)
2. **Necesitas detalles?** → Abre el documento para tu rol (ver tabla arriba)
3. **Buscas un concepto?** → [Index.md § BÚSQUEDA RÁPIDA](./Index.md#🎯-búsqueda-rápida)
4. **Validas coherencia?** → [Index.md § CHECKLIST](./Index.md#📋-checklist-de-coherencia)
5. **Navegas entre docs?** → [Index.md § REFERENCIAS CRUZADAS](./Index.md#📞-referencias-cruzadas-enlaces-internos)

---

## 📞 Contacto & Responsabilidades

**Proyecto:** Threshold of Silence — Prototype II  
**Equipo:** Carlos Bayas e Ismael Toala 
**Estado:** Listo para su revisión e implementación 


**¿Listo?** [→ Abre Index.md para empezar](./Index.md)
