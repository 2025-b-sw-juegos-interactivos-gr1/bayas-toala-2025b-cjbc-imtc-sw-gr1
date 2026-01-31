# Threshold of Silence 🎮

![Unity](https://img.shields.io/badge/Unity-2020%2B-black?logo=unity)
![Platform](https://img.shields.io/badge/Platform-Windows-blue)
![Genre](https://img.shields.io/badge/Genre-Stealth%20Top--Down-green)

> Un juego de sigilo táctico top-down 2D donde el silencio es tu mejor arma.

---

## 📖 Descripción

**Threshold of Silence** es un videojuego de sigilo táctico donde el jugador debe infiltrarse en escenarios evitando la detección enemiga mediante:

- 🚶 **Movimiento preciso** (velocidad normal y modo sigilo)
- 👁️ **Sistema de detección** (proximidad + visión + ruido)
- 🤖 **IA predecible** (patrones de patrullaje legibles)
- 💀 **Game Over inmediato** al ser detectado
- 🔄 **Reinicio rápido** para aprendizaje iterativo

---

## 🖼️ Screenshots

*[Agregar screenshots del juego aquí]*

---

## 🎯 Objetivo del Juego

Llega al **punto de extracción** (marcador amarillo/naranja) sin ser detectado por los enemigos. Observa sus patrones, planifica tu ruta y ejecuta con precisión.

---

## 🕹️ Controles

| Tecla | Acción |
|-------|--------|
| `W A S D` | Movimiento |
| `Shift` | Modo Sigilo (más lento, menos ruido) |
| `R` | Reiniciar nivel |
| `ESC` | Reiniciar / Menú |

---

## 💻 Requisitos del Sistema

### Mínimos:
- **SO:** Windows 10
- **Procesador:** Intel Core i3 o equivalente
- **Memoria:** 4 GB RAM
- **Gráficos:** Tarjeta gráfica con soporte DirectX 11
- **Almacenamiento:** 500 MB disponibles

### Recomendados:
- **SO:** Windows 10/11
- **Procesador:** Intel Core i5 o equivalente
- **Memoria:** 8 GB RAM
- **Gráficos:** GTX 750 Ti o equivalente

---

## 🛠️ Requisitos para Desarrollo

### Software Necesario:

| Software | Versión | Enlace |
|----------|---------|--------|
| Unity Hub | Última | [Descargar](https://unity.com/download) |
| Unity Editor | 2020.3 LTS o superior | Instalar desde Unity Hub |
| Visual Studio | 2019/2022 | [Descargar](https://visualstudio.microsoft.com/) |
| Git | Última | [Descargar](https://git-scm.com/) |

### Paquetes de Unity Requeridos:
- **Input System** (com.unity.inputsystem)
- **Universal RP** (com.unity.render-pipelines.universal)

---

## 🚀 Instalación y Ejecución

### Opción 1: Clonar el Repositorio

```bash
# Clonar el repositorio
git clone https://github.com/[usuario]/threshold-of-silence.git

# Navegar al directorio
cd threshold-of-silence
```

### Opción 2: Descargar ZIP

1. Click en **Code** → **Download ZIP**
2. Extraer en una ubicación deseada

### Abrir en Unity:

1. Abrir **Unity Hub**
2. Click en **Add** → **Add project from disk**
3. Seleccionar la carpeta `JuegosBayasToala`
4. Abrir el proyecto (Unity descargará dependencias automáticamente)
5. En el panel **Project**, navegar a `Assets/Scenes/`
6. Doble click en la escena principal
7. Presionar **Play** ▶️

---

## 📁 Estructura del Proyecto

```
JuegosBayasToala/
├── Assets/
│   ├── Scenes/              # Escenas del juego
│   ├── Scripts/             # Código C#
│   │   ├── Player/          # Control del jugador
│   │   ├── Enemy/           # IA enemiga
│   │   ├── Core/            # Sistemas centrales
│   │   ├── Level/           # Elementos del nivel
│   │   ├── UI/              # Interfaz de usuario
│   │   └── Visuals/         # Efectos visuales
│   ├── Sprites/             # Imágenes y texturas
│   └── Settings/            # Configuración de render
├── Packages/                # Dependencias de Unity
├── ProjectSettings/         # Configuración del proyecto
└── README.md               # Este archivo
```

---

## 🎮 Cómo Jugar

1. **Observa** - Estudia el patrón de patrullaje del enemigo
2. **Planifica** - Identifica la ruta más segura hacia la meta
3. **Ejecuta** - Muévete con cuidado, usa sigilo en zonas de riesgo
4. **Adapta** - Si fallas, aprende del error y reintenta

### Tips:
- 🟢 El modo sigilo reduce tu radio de ruido
- 🔴 Evita el cono de visión rojo del enemigo
- 🟡 Usa las paredes como cobertura
- ⚡ El reinicio es instantáneo - no temas fallar

---

## 🏗️ Compilar el Juego

1. En Unity: **File** → **Build Settings**
2. Seleccionar **PC, Mac & Linux Standalone**
3. Click en **Build**
4. Seleccionar carpeta de destino
5. Esperar a que compile

---

## 👥 Autores

| Nombre | Rol |
|--------|-----|
| **Carlos Bayas** | Desarrollador |
| **Ismael Toala** | Desarrollador |

---

## 📄 Licencia

Este proyecto fue creado con fines académicos.

---

*Threshold of Silence - 2025*
