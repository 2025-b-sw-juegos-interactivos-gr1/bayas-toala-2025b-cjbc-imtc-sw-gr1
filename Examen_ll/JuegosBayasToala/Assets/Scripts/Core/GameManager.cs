using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

/// <summary>
/// GameManager - Threshold of Silence
/// Controla el estado del juego, Game Over y reinicio
/// </summary>
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    
    [Header("Estado del Juego")]
    [SerializeField] private bool isGameOver = false;
    [SerializeField] private bool isGameWon = false;
    [SerializeField] private bool isPaused = false;
    
    [Header("Referencias UI")]
    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private GameObject victoryUI;
    [SerializeField] private GameObject pauseUI;
    [SerializeField] private GameObject hudUI;
    
    // Propiedades públicas
    public bool IsGameOver => isGameOver;
    public bool IsGameWon => isGameWon;
    public bool IsPaused => isPaused;
    
    // Input Actions
    private InputAction restartAction;
    private InputAction pauseAction;
    
    // Eventos
    public delegate void GameStateChanged(string state);
    public static event GameStateChanged OnGameStateChanged;
    
    private void Awake()
    {
        // Singleton pattern
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        
        // Configurar Input Actions
        SetupInputActions();
    }
    
    private void SetupInputActions()
    {
        // Acción de reinicio (R)
        restartAction = new InputAction("Restart", InputActionType.Button);
        restartAction.AddBinding("<Keyboard>/r");
        restartAction.Enable();
        
        // Acción de pausa (Escape)
        pauseAction = new InputAction("Pause", InputActionType.Button);
        pauseAction.AddBinding("<Keyboard>/escape");
        pauseAction.Enable();
    }
    
    private void Start()
    {
        // Inicializar estado
        ResetGameState();
        
        // Suscribirse a eventos
        PlayerController.PlayerDetected += TriggerGameOver;
    }
    
    private void OnDestroy()
    {
        // Desuscribirse de eventos
        PlayerController.PlayerDetected -= TriggerGameOver;
        
        // Limpiar Input Actions
        if (restartAction != null) restartAction.Disable();
        if (pauseAction != null) pauseAction.Disable();
    }
    
    private void Update()
    {
        // Detectar input de reinicio (R)
        if (restartAction != null && restartAction.WasPressedThisFrame())
        {
            RestartLevel();
        }
        
        // Detectar input de pausa (Esc)
        if (pauseAction != null && pauseAction.WasPressedThisFrame())
        {
            TogglePause();
        }
    }
    
    private void ResetGameState()
    {
        isGameOver = false;
        isGameWon = false;
        isPaused = false;
        Time.timeScale = 1f;
        
        // Ocultar UI
        if (gameOverUI != null) gameOverUI.SetActive(false);
        if (victoryUI != null) victoryUI.SetActive(false);
        if (pauseUI != null) pauseUI.SetActive(false);
        if (hudUI != null) hudUI.SetActive(true);
    }
    
    /// <summary>
    /// Activa el estado de Game Over
    /// </summary>
    public void TriggerGameOver()
    {
        if (isGameOver || isGameWon) return;
        
        isGameOver = true;
        Debug.Log("GAME OVER - ¡Has sido detectado!");
        
        // Usar UIManager si existe
        if (UIManager.Instance != null)
        {
            UIManager.Instance.ShowGameOver();
        }
        else
        {
            // Fallback: Mostrar UI de Game Over antigua
            if (gameOverUI != null) gameOverUI.SetActive(true);
            if (hudUI != null) hudUI.SetActive(false);
            Time.timeScale = 0.1f;
        }
        
        OnGameStateChanged?.Invoke("GameOver");
    }
    
    /// <summary>
    /// Activa el estado de Victoria
    /// </summary>
    public void TriggerVictory()
    {
        if (isGameOver || isGameWon) return;
        
        isGameWon = true;
        Debug.Log("¡VICTORIA! - Has completado el nivel");
        
        // Usar UIManager si existe
        if (UIManager.Instance != null)
        {
            UIManager.Instance.ShowVictory();
        }
        else
        {
            // Fallback: Mostrar UI de Victoria antigua
            if (victoryUI != null) victoryUI.SetActive(true);
            if (hudUI != null) hudUI.SetActive(false);
            Time.timeScale = 0f;
        }
        
        OnGameStateChanged?.Invoke("Victory");
    }
    
    /// <summary>
    /// Reinicia el nivel actual
    /// </summary>
    public void RestartLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
    /// <summary>
    /// Alterna el estado de pausa
    /// </summary>
    public void TogglePause()
    {
        if (isGameOver || isGameWon) return;
        
        isPaused = !isPaused;
        
        if (isPaused)
        {
            Time.timeScale = 0f;
            if (pauseUI != null) pauseUI.SetActive(true);
            OnGameStateChanged?.Invoke("Paused");
        }
        else
        {
            Time.timeScale = 1f;
            if (pauseUI != null) pauseUI.SetActive(false);
            OnGameStateChanged?.Invoke("Playing");
        }
    }
    
    /// <summary>
    /// Carga el siguiente nivel
    /// </summary>
    public void LoadNextLevel()
    {
        Time.timeScale = 1f;
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            Debug.Log("¡Has completado todos los niveles!");
            // Volver al menú principal o al primer nivel
            SceneManager.LoadScene(0);
        }
    }
    
    /// <summary>
    /// Sale del juego
    /// </summary>
    public void QuitGame()
    {
        Debug.Log("Saliendo del juego...");
        Application.Quit();
        
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
