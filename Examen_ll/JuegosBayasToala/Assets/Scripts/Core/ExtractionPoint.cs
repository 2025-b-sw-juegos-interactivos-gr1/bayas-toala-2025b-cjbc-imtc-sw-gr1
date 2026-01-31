using UnityEngine;

/// <summary>
/// Punto de Extracción - Threshold of Silence
/// El jugador debe llegar aquí para ganar el nivel
/// </summary>
public class ExtractionPoint : MonoBehaviour
{
    [Header("Configuración Visual")]
    [SerializeField] private Color extractionColor = new Color(1f, 0.8f, 0f); // Amarillo/Naranja
    [SerializeField] private bool pulseEffect = true;
    [SerializeField] private float pulseSpeed = 2f;
    [SerializeField] private float pulseMinAlpha = 0.5f;
    [SerializeField] private float pulseMaxAlpha = 1f;
    
    private SpriteRenderer spriteRenderer;
    
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        
        if (spriteRenderer == null)
        {
            spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
        }
        
        // Configurar color inicial
        spriteRenderer.color = extractionColor;
        
        // Asegurar que tiene un collider trigger
        Collider2D col = GetComponent<Collider2D>();
        if (col == null)
        {
            BoxCollider2D boxCol = gameObject.AddComponent<BoxCollider2D>();
            boxCol.isTrigger = true;
        }
        else
        {
            col.isTrigger = true;
        }
    }
    
    private void Update()
    {
        if (pulseEffect && spriteRenderer != null)
        {
            // Efecto de pulso para indicar el objetivo
            float alpha = Mathf.Lerp(pulseMinAlpha, pulseMaxAlpha, 
                (Mathf.Sin(Time.time * pulseSpeed) + 1f) / 2f);
            
            Color currentColor = extractionColor;
            currentColor.a = alpha;
            spriteRenderer.color = currentColor;
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Verificar si es el jugador
        if (other.CompareTag("Player"))
        {
            Debug.Log("¡Punto de extracción alcanzado!");
            
            // Notificar al GameManager
            GameManager gameManager = GameManager.Instance;
            if (gameManager == null)
            {
                gameManager = FindFirstObjectByType<GameManager>();
            }
            
            if (gameManager != null)
            {
                gameManager.TriggerVictory();
            }
        }
    }
    
    private void OnDrawGizmos()
    {
        // Dibujar el punto de extracción en el editor
        Gizmos.color = new Color(1f, 0.8f, 0f, 0.5f);
        Gizmos.DrawCube(transform.position, transform.localScale);
        Gizmos.color = new Color(1f, 0.8f, 0f, 1f);
        Gizmos.DrawWireCube(transform.position, transform.localScale);
    }
}
