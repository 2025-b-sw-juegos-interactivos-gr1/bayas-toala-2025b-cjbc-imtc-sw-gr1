using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Controlador del HUD - Threshold of Silence
/// Muestra información del estado del jugador y detección
/// </summary>
public class HUDController : MonoBehaviour
{
    [Header("Referencias UI")]
    [SerializeField] private Text stealthModeText;
    [SerializeField] private Text fpsText;
    [SerializeField] private Image detectionBar;
    [SerializeField] private Image[] detectionSegments;
    
    [Header("Colores de Detección")]
    [SerializeField] private Color safeColor = Color.green;
    [SerializeField] private Color warningColor = Color.yellow;
    [SerializeField] private Color dangerColor = new Color(1f, 0.5f, 0f); // Naranja
    [SerializeField] private Color criticalColor = Color.red;
    
    [Header("Referencias")]
    [SerializeField] private PlayerController playerController;
    
    private float fpsUpdateTimer = 0f;
    private float fpsUpdateInterval = 0.5f;
    
    private void Start()
    {
        // Buscar jugador si no está asignado
        if (playerController == null)
        {
            playerController = FindFirstObjectByType<PlayerController>();
        }
    }
    
    private void Update()
    {
        UpdateStealthModeUI();
        UpdateFPSCounter();
        UpdateDetectionBar();
    }
    
    private void UpdateStealthModeUI()
    {
        if (stealthModeText == null || playerController == null) return;
        
        if (playerController.IsInStealthMode)
        {
            stealthModeText.text = "[SIGILO (Movimiento preciso)]";
            stealthModeText.color = new Color(0f, 1f, 0.5f); // Verde brillante
        }
        else
        {
            stealthModeText.text = "";
        }
    }
    
    private void UpdateFPSCounter()
    {
        if (fpsText == null) return;
        
        fpsUpdateTimer += Time.unscaledDeltaTime;
        if (fpsUpdateTimer >= fpsUpdateInterval)
        {
            fpsUpdateTimer = 0f;
            int fps = Mathf.RoundToInt(1f / Time.unscaledDeltaTime);
            fpsText.text = fps + " FPS";
        }
    }
    
    private void UpdateDetectionBar()
    {
        // Buscar el enemigo más cercano y su nivel de detección
        EnemyAI[] enemies = FindObjectsByType<EnemyAI>(FindObjectsSortMode.None);
        
        float maxDetection = 0f;
        foreach (EnemyAI enemy in enemies)
        {
            if (enemy.DetectionPercentage > maxDetection)
            {
                maxDetection = enemy.DetectionPercentage;
            }
        }
        
        // Actualizar barra de detección
        if (detectionBar != null)
        {
            detectionBar.fillAmount = maxDetection;
            detectionBar.color = GetDetectionColor(maxDetection);
        }
        
        // Actualizar segmentos si se usan
        if (detectionSegments != null && detectionSegments.Length > 0)
        {
            UpdateDetectionSegments(maxDetection);
        }
    }
    
    private void UpdateDetectionSegments(float detectionLevel)
    {
        int activeSegments = Mathf.RoundToInt(detectionLevel * detectionSegments.Length);
        
        for (int i = 0; i < detectionSegments.Length; i++)
        {
            if (detectionSegments[i] != null)
            {
                detectionSegments[i].enabled = i < activeSegments;
                detectionSegments[i].color = GetDetectionColor((float)i / detectionSegments.Length);
            }
        }
    }
    
    private Color GetDetectionColor(float level)
    {
        if (level < 0.3f) return safeColor;
        if (level < 0.5f) return warningColor;
        if (level < 0.7f) return dangerColor;
        return criticalColor;
    }
}
