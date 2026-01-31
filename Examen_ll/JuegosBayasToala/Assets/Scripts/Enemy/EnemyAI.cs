using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// IA del Enemigo - Threshold of Silence
/// FSM: Patrol → Suspicious → Alert → Confirmed (Game Over)
/// </summary>
public class EnemyAI : MonoBehaviour
{
    // Estados de la FSM
    public enum EnemyState
    {
        Patrol,         // Patrullaje normal
        Suspicious,     // Sospecha (escuchó algo)
        Alert,          // Alerta (vio algo)
        Confirmed       // Confirmado (Game Over)
    }
    
    [Header("Configuración de Patrullaje")]
    [SerializeField] private List<Transform> patrolPoints = new List<Transform>();
    [SerializeField] private float patrolSpeed = 2.0f;
    [SerializeField] private float waitTimeAtPoint = 1.0f;
    
    [Header("Configuración de Detección")]
    [SerializeField] private float proximityRadius = 4.0f;      // Radio de proximidad
    [SerializeField] private float visionAngle = 60f;           // Ángulo del cono de visión
    [SerializeField] private float visionDistance = 6.0f;       // Distancia de visión
    [SerializeField] private LayerMask obstacleLayer;           // Capa de obstáculos
    [SerializeField] private LayerMask playerLayer;             // Capa del jugador
    
    [Header("Configuración de Estados")]
    [SerializeField] private float suspiciousTime = 2.0f;       // Tiempo en estado sospechoso
    [SerializeField] private float alertTime = 3.0f;            // Tiempo en estado alerta
    [SerializeField] private float cooldownTime = 1.5f;         // Tiempo de enfriamiento
    
    [Header("Sistema de Detección Acumulativo")]
    [SerializeField] private float maxDetectionLevel = 100f;
    [SerializeField] private float detectionIncreaseRate = 70f;  // Por segundo cuando ve al jugador
    [SerializeField] private float detectionDecreaseRate = 5f;   // Por segundo cuando no ve
    
    [Header("Referencias Visuales")]
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private SpriteRenderer visionConeRenderer;
    
    // Estado actual
    private EnemyState currentState = EnemyState.Patrol;
    private float currentDetectionLevel = 0f;
    private int currentPatrolIndex = 0;
    private float waitTimer = 0f;
    private float stateTimer = 0f;
    private Vector2 facingDirection = Vector2.right;
    private Transform playerTransform;
    private PlayerController playerController;
    
    // Propiedades públicas
    public EnemyState CurrentState => currentState;
    public float DetectionLevel => currentDetectionLevel;
    public float DetectionPercentage => currentDetectionLevel / maxDetectionLevel;
    
    private void Start()
    {
        // Buscar al jugador
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
            playerController = player.GetComponent<PlayerController>();
        }
        
        if (spriteRenderer == null)
            spriteRenderer = GetComponent<SpriteRenderer>();
            
        // Inicializar dirección hacia el primer punto de patrulla
        if (patrolPoints.Count > 0)
        {
            facingDirection = ((Vector2)patrolPoints[0].position - (Vector2)transform.position).normalized;
        }
    }
    
    private void Update()
    {
        if (playerTransform == null) return;
        
        // Actualizar detección
        UpdateDetection();
        
        // Actualizar FSM
        UpdateStateMachine();
        
        // Actualizar visuales
        UpdateVisuals();
    }
    
    private void UpdateDetection()
    {
        bool canSeePlayer = CanSeePlayer();
        bool canHearPlayer = CanHearPlayer();
        bool playerInProximity = IsPlayerInProximity();
        
        // Incrementar o decrementar nivel de detección
        if (canSeePlayer)
        {
            // Detección visual es la más rápida
            currentDetectionLevel += detectionIncreaseRate * 1.5f * Time.deltaTime;
        }
        else if (canHearPlayer || playerInProximity)
        {
            // Detección por ruido o proximidad es más lenta
            currentDetectionLevel += detectionIncreaseRate * 0.5f * Time.deltaTime;
        }
        else
        {
            // Decrementar cuando no detecta nada
            currentDetectionLevel -= detectionDecreaseRate * Time.deltaTime;
        }
        
        // Clampear valores
        currentDetectionLevel = Mathf.Clamp(currentDetectionLevel, 0f, maxDetectionLevel);
        
        // Verificar Game Over
        if (currentDetectionLevel >= maxDetectionLevel)
        {
            currentState = EnemyState.Confirmed;
            TriggerGameOver();
        }
    }
    
    private void UpdateStateMachine()
    {
        // Transiciones de estado basadas en nivel de detección
        EnemyState previousState = currentState;
        
        if (currentDetectionLevel >= maxDetectionLevel)
        {
            currentState = EnemyState.Confirmed;
        }
        else if (currentDetectionLevel >= 70f)
        {
            currentState = EnemyState.Alert;
        }
        else if (currentDetectionLevel >= 30f)
        {
            currentState = EnemyState.Suspicious;
        }
        else
        {
            currentState = EnemyState.Patrol;
        }
        
        // Ejecutar comportamiento según estado
        switch (currentState)
        {
            case EnemyState.Patrol:
                DoPatrol();
                break;
            case EnemyState.Suspicious:
                DoSuspicious();
                break;
            case EnemyState.Alert:
                DoAlert();
                break;
            case EnemyState.Confirmed:
                // Game Over ya fue disparado
                break;
        }
    }
    
    private void DoPatrol()
    {
        if (patrolPoints.Count == 0) return;
        
        Transform targetPoint = patrolPoints[currentPatrolIndex];
        Vector2 direction = ((Vector2)targetPoint.position - (Vector2)transform.position);
        float distance = direction.magnitude;
        
        if (distance < 0.1f)
        {
            // Llegó al punto, esperar
            waitTimer += Time.deltaTime;
            if (waitTimer >= waitTimeAtPoint)
            {
                waitTimer = 0f;
                currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Count;
            }
        }
        else
        {
            // Moverse hacia el punto
            facingDirection = direction.normalized;
            transform.position = Vector2.MoveTowards(transform.position, targetPoint.position, patrolSpeed * Time.deltaTime);
        }
    }
    
    private void DoSuspicious()
    {
        // En estado sospechoso, el enemigo reduce velocidad y mira alrededor
        // Por ahora, continúa patrullando pero más lento
        if (patrolPoints.Count == 0) return;
        
        Transform targetPoint = patrolPoints[currentPatrolIndex];
        Vector2 direction = ((Vector2)targetPoint.position - (Vector2)transform.position);
        float distance = direction.magnitude;
        
        if (distance >= 0.1f)
        {
            facingDirection = direction.normalized;
            transform.position = Vector2.MoveTowards(transform.position, targetPoint.position, patrolSpeed * 0.5f * Time.deltaTime);
        }
    }
    
    private void DoAlert()
    {
        // En estado alerta, el enemigo se detiene y mira hacia el jugador
        if (playerTransform != null)
        {
            facingDirection = ((Vector2)playerTransform.position - (Vector2)transform.position).normalized;
        }
    }
    
    private bool CanSeePlayer()
    {
        if (playerTransform == null) return false;
        
        Vector2 directionToPlayer = ((Vector2)playerTransform.position - (Vector2)transform.position);
        float distanceToPlayer = directionToPlayer.magnitude;
        
        // Verificar distancia
        if (distanceToPlayer > visionDistance) return false;
        
        // Verificar ángulo
        float angleToPlayer = Vector2.Angle(facingDirection, directionToPlayer);
        if (angleToPlayer > visionAngle / 2f) return false;
        
        // Verificar línea de visión (raycast)
        RaycastHit2D hit = Physics2D.Raycast(transform.position, directionToPlayer.normalized, distanceToPlayer, obstacleLayer);
        if (hit.collider != null) return false; // Hay un obstáculo
        
        return true;
    }
    
    private bool CanHearPlayer()
    {
        if (playerTransform == null || playerController == null) return false;
        
        // Solo escucha si el jugador se está moviendo
        if (!playerController.IsMoving) return false;
        
        float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);
        float hearingRadius = proximityRadius + playerController.CurrentNoiseRadius;
        
        return distanceToPlayer <= hearingRadius;
    }
    
    private bool IsPlayerInProximity()
    {
        if (playerTransform == null) return false;
        
        float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);
        return distanceToPlayer <= proximityRadius;
    }
    
    private void TriggerGameOver()
    {
        // Notificar al jugador
        if (playerController != null)
        {
            playerController.OnDetected();
        }
        
        // Notificar al GameManager
        GameManager gameManager = FindFirstObjectByType<GameManager>();
        if (gameManager != null)
        {
            gameManager.TriggerGameOver();
        }
    }
    
    private void UpdateVisuals()
    {
        if (spriteRenderer == null) return;
        
        // Cambiar color según estado
        Color stateColor = currentState switch
        {
            EnemyState.Patrol => Color.red,
            EnemyState.Suspicious => new Color(1f, 0.5f, 0f), // Naranja
            EnemyState.Alert => new Color(1f, 0.2f, 0f),      // Rojo-naranja
            EnemyState.Confirmed => Color.magenta,
            _ => Color.red
        };
        
        spriteRenderer.color = stateColor;
        
        // Rotar el sprite según la dirección
        float angle = Mathf.Atan2(facingDirection.y, facingDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
    
    private void OnDrawGizmosSelected()
    {
        // Dibujar radio de proximidad
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, proximityRadius);
        
        // Dibujar cono de visión
        Gizmos.color = Color.red;
        Vector3 leftBoundary = Quaternion.Euler(0, 0, visionAngle / 2f) * (Vector3)facingDirection * visionDistance;
        Vector3 rightBoundary = Quaternion.Euler(0, 0, -visionAngle / 2f) * (Vector3)facingDirection * visionDistance;
        
        Gizmos.DrawLine(transform.position, transform.position + leftBoundary);
        Gizmos.DrawLine(transform.position, transform.position + rightBoundary);
        Gizmos.DrawLine(transform.position + leftBoundary, transform.position + rightBoundary);
        
        // Dibujar puntos de patrullaje
        Gizmos.color = Color.blue;
        for (int i = 0; i < patrolPoints.Count; i++)
        {
            if (patrolPoints[i] != null)
            {
                Gizmos.DrawSphere(patrolPoints[i].position, 0.2f);
                if (i < patrolPoints.Count - 1 && patrolPoints[i + 1] != null)
                {
                    Gizmos.DrawLine(patrolPoints[i].position, patrolPoints[i + 1].position);
                }
            }
        }
        if (patrolPoints.Count > 1 && patrolPoints[0] != null && patrolPoints[patrolPoints.Count - 1] != null)
        {
            Gizmos.DrawLine(patrolPoints[patrolPoints.Count - 1].position, patrolPoints[0].position);
        }
    }
}
