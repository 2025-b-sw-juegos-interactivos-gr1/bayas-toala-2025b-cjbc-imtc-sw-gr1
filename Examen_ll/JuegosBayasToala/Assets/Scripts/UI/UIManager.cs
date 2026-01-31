using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using System.Collections;

/// <summary>
/// Controlador de UI del juego - Threshold of Silence
/// Maneja mensajes de inicio, Game Over, Victoria y controles
/// </summary>
public class UIManager : MonoBehaviour
{
    [Header("Paneles")]
    [SerializeField] private GameObject levelStartPanel;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject victoryPanel;
    [SerializeField] private GameObject hudPanel;
    
    [Header("Textos")]
    [SerializeField] private Text levelStartText;
    [SerializeField] private Text gameOverText;
    [SerializeField] private Text victoryText;
    [SerializeField] private Text instructionsText;
    [SerializeField] private Text stealthIndicator;
    
    [Header("Configuración")]
    [SerializeField] private float levelStartDuration = 2f;
    [SerializeField] private string levelName = "NIVEL 1";
    
    // Input
    private InputAction restartAction;
    private InputAction anyKeyAction;
    
    // Estado
    private bool gameStarted = false;
    private bool gameEnded = false;
    
    public static UIManager Instance { get; private set; }
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        
        SetupInput();
        CreateUI();
    }
    
    private void SetupInput()
    {
        // Acción para reiniciar (R o Escape)
        restartAction = new InputAction("Restart", InputActionType.Button);
        restartAction.AddBinding("<Keyboard>/r");
        restartAction.AddBinding("<Keyboard>/escape");
        restartAction.Enable();
        
        // Cualquier tecla para continuar
        anyKeyAction = new InputAction("AnyKey", InputActionType.Button);
        anyKeyAction.AddBinding("<Keyboard>/anyKey");
        anyKeyAction.Enable();
    }
    
    private void Start()
    {
        StartCoroutine(ShowLevelStart());
    }
    
    private void Update()
    {
        // Reiniciar con R o Escape cuando el juego ha terminado
        if (gameEnded && restartAction.WasPressedThisFrame())
        {
            RestartGame();
        }
        
        // Actualizar indicador de sigilo
        UpdateStealthIndicator();
    }
    
    private void OnDestroy()
    {
        if (restartAction != null) restartAction.Disable();
        if (anyKeyAction != null) anyKeyAction.Disable();
    }
    
    private IEnumerator ShowLevelStart()
    {
        // Pausar el juego durante la intro
        Time.timeScale = 0f;
        gameStarted = false;
        
        // Mostrar panel de inicio
        if (levelStartPanel != null)
        {
            levelStartPanel.SetActive(true);
        }
        if (gameOverPanel != null) gameOverPanel.SetActive(false);
        if (victoryPanel != null) victoryPanel.SetActive(false);
        if (hudPanel != null) hudPanel.SetActive(false);
        
        // Esperar duración (usando unscaledTime porque el juego está pausado)
        yield return new WaitForSecondsRealtime(levelStartDuration);
        
        // Ocultar panel de inicio y comenzar juego
        if (levelStartPanel != null)
        {
            levelStartPanel.SetActive(false);
        }
        if (hudPanel != null) hudPanel.SetActive(true);
        
        Time.timeScale = 1f;
        gameStarted = true;
    }
    
    public void ShowGameOver()
    {
        if (gameEnded) return;
        gameEnded = true;
        
        Time.timeScale = 0f;
        
        if (levelStartPanel != null) levelStartPanel.SetActive(false);
        if (gameOverPanel != null) gameOverPanel.SetActive(true);
        if (victoryPanel != null) victoryPanel.SetActive(false);
        if (hudPanel != null) hudPanel.SetActive(false);
    }
    
    public void ShowVictory()
    {
        if (gameEnded) return;
        gameEnded = true;
        
        Time.timeScale = 0f;
        
        if (levelStartPanel != null) levelStartPanel.SetActive(false);
        if (gameOverPanel != null) gameOverPanel.SetActive(false);
        if (victoryPanel != null) victoryPanel.SetActive(true);
        if (hudPanel != null) hudPanel.SetActive(false);
    }
    
    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
    private void UpdateStealthIndicator()
    {
        if (stealthIndicator == null) return;
        
        PlayerController player = FindFirstObjectByType<PlayerController>();
        if (player != null && player.IsInStealthMode)
        {
            stealthIndicator.text = "[SIGILO]";
            stealthIndicator.gameObject.SetActive(true);
        }
        else
        {
            stealthIndicator.gameObject.SetActive(false);
        }
    }
    
    private void CreateUI()
    {
        // Buscar o crear Canvas
        Canvas canvas = FindFirstObjectByType<Canvas>();
        if (canvas == null)
        {
            GameObject canvasObj = new GameObject("Canvas");
            canvas = canvasObj.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            canvasObj.AddComponent<CanvasScaler>();
            canvasObj.AddComponent<GraphicRaycaster>();
        }
        
        // Crear paneles si no existen
        if (levelStartPanel == null)
        {
            levelStartPanel = CreatePanel(canvas.transform, "LevelStartPanel", new Color(0, 0, 0, 0.9f));
            levelStartText = CreateText(levelStartPanel.transform, levelName, 72, Color.white, TextAnchor.MiddleCenter);
            
            // Subtítulo
            GameObject subTextObj = new GameObject("SubText");
            subTextObj.transform.SetParent(levelStartPanel.transform);
            Text subText = subTextObj.AddComponent<Text>();
            subText.text = "Prepárate...";
            subText.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
            subText.fontSize = 24;
            subText.color = new Color(0.7f, 0.7f, 0.7f);
            subText.alignment = TextAnchor.MiddleCenter;
            RectTransform subRect = subText.GetComponent<RectTransform>();
            subRect.anchorMin = new Vector2(0, 0);
            subRect.anchorMax = new Vector2(1, 0.4f);
            subRect.offsetMin = Vector2.zero;
            subRect.offsetMax = Vector2.zero;
        }
        
        if (gameOverPanel == null)
        {
            gameOverPanel = CreatePanel(canvas.transform, "GameOverPanel", new Color(0.3f, 0, 0, 0.9f));
            gameOverText = CreateText(gameOverPanel.transform, "¡DETECTADO!", 72, Color.red, TextAnchor.MiddleCenter);
            
            // Instrucciones
            GameObject instructObj = new GameObject("Instructions");
            instructObj.transform.SetParent(gameOverPanel.transform);
            Text instText = instructObj.AddComponent<Text>();
            instText.text = "Presiona R o ESC para reiniciar";
            instText.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
            instText.fontSize = 28;
            instText.color = Color.white;
            instText.alignment = TextAnchor.MiddleCenter;
            RectTransform instRect = instText.GetComponent<RectTransform>();
            instRect.anchorMin = new Vector2(0, 0);
            instRect.anchorMax = new Vector2(1, 0.35f);
            instRect.offsetMin = Vector2.zero;
            instRect.offsetMax = Vector2.zero;
            
            gameOverPanel.SetActive(false);
        }
        
        if (victoryPanel == null)
        {
            victoryPanel = CreatePanel(canvas.transform, "VictoryPanel", new Color(0, 0.2f, 0, 0.9f));
            victoryText = CreateText(victoryPanel.transform, "¡VICTORIA!", 72, Color.green, TextAnchor.MiddleCenter);
            
            // Instrucciones
            GameObject instructObj2 = new GameObject("Instructions");
            instructObj2.transform.SetParent(victoryPanel.transform);
            Text instText2 = instructObj2.AddComponent<Text>();
            instText2.text = "¡Has completado el nivel!\nPresiona R o ESC para jugar de nuevo";
            instText2.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
            instText2.fontSize = 28;
            instText2.color = Color.white;
            instText2.alignment = TextAnchor.MiddleCenter;
            RectTransform instRect2 = instText2.GetComponent<RectTransform>();
            instRect2.anchorMin = new Vector2(0, 0);
            instRect2.anchorMax = new Vector2(1, 0.35f);
            instRect2.offsetMin = Vector2.zero;
            instRect2.offsetMax = Vector2.zero;
            
            victoryPanel.SetActive(false);
        }
        
        if (hudPanel == null)
        {
            hudPanel = CreatePanel(canvas.transform, "HUDPanel", new Color(0, 0, 0, 0));
            
            // Indicador de sigilo (esquina superior derecha)
            GameObject stealthObj = new GameObject("StealthIndicator");
            stealthObj.transform.SetParent(hudPanel.transform);
            stealthIndicator = stealthObj.AddComponent<Text>();
            stealthIndicator.text = "[SIGILO]";
            stealthIndicator.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
            stealthIndicator.fontSize = 24;
            stealthIndicator.color = new Color(0, 1, 0.5f);
            stealthIndicator.alignment = TextAnchor.UpperRight;
            RectTransform stealthRect = stealthIndicator.GetComponent<RectTransform>();
            stealthRect.anchorMin = new Vector2(0.7f, 0.9f);
            stealthRect.anchorMax = new Vector2(0.98f, 0.98f);
            stealthRect.offsetMin = Vector2.zero;
            stealthRect.offsetMax = Vector2.zero;
            stealthIndicator.gameObject.SetActive(false);
            
            // Instrucciones de controles (esquina inferior izquierda)
            GameObject controlsObj = new GameObject("Controls");
            controlsObj.transform.SetParent(hudPanel.transform);
            Text controlsText = controlsObj.AddComponent<Text>();
            controlsText.text = "WASD: Mover | SHIFT: Sigilo | R: Reiniciar";
            controlsText.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
            controlsText.fontSize = 16;
            controlsText.color = new Color(0.5f, 0.5f, 0.5f);
            controlsText.alignment = TextAnchor.LowerLeft;
            RectTransform controlsRect = controlsText.GetComponent<RectTransform>();
            controlsRect.anchorMin = new Vector2(0.02f, 0.02f);
            controlsRect.anchorMax = new Vector2(0.5f, 0.08f);
            controlsRect.offsetMin = Vector2.zero;
            controlsRect.offsetMax = Vector2.zero;
            
            hudPanel.SetActive(false);
        }
    }
    
    private GameObject CreatePanel(Transform parent, string name, Color bgColor)
    {
        GameObject panel = new GameObject(name);
        panel.transform.SetParent(parent);
        
        RectTransform rect = panel.AddComponent<RectTransform>();
        rect.anchorMin = Vector2.zero;
        rect.anchorMax = Vector2.one;
        rect.offsetMin = Vector2.zero;
        rect.offsetMax = Vector2.zero;
        
        Image img = panel.AddComponent<Image>();
        img.color = bgColor;
        
        return panel;
    }
    
    private Text CreateText(Transform parent, string content, int fontSize, Color color, TextAnchor anchor)
    {
        GameObject textObj = new GameObject("Text");
        textObj.transform.SetParent(parent);
        
        Text text = textObj.AddComponent<Text>();
        text.text = content;
        text.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
        text.fontSize = fontSize;
        text.color = color;
        text.alignment = anchor;
        text.fontStyle = FontStyle.Bold;
        
        RectTransform rect = text.GetComponent<RectTransform>();
        rect.anchorMin = new Vector2(0, 0.4f);
        rect.anchorMax = new Vector2(1, 0.7f);
        rect.offsetMin = Vector2.zero;
        rect.offsetMax = Vector2.zero;
        
        return text;
    }
}
