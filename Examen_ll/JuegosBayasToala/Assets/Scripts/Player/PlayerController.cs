using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Controlador del jugador - Threshold of Silence
/// Movimiento en 8 direcciones con modo sigilo
/// Compatible con Legacy Input y New Input System
/// </summary>
public class PlayerController : MonoBehaviour
{
    [Header("Configuración de Movimiento")]
    [SerializeField] private float normalSpeed = 3.5f;      // Velocidad normal
    [SerializeField] private float stealthSpeed = 2.0f;     // Velocidad en modo sigilo
    
    [Header("Configuración de Ruido")]
    [SerializeField] private float normalNoiseRadius = 1.5f;    // Radio de ruido normal
    [SerializeField] private float stealthNoiseRadius = 0.5f;   // Radio de ruido en sigilo
    
    [Header("Referencias")]
    [SerializeField] private SpriteRenderer spriteRenderer;
    
    // Estado actual
    private bool isInStealthMode = false;
    private Vector2 movementInput;
    private Rigidbody2D rb;
    
    // New Input System
    private PlayerInput playerInput;
    private InputAction moveAction;
    private InputAction stealthAction;
    
    // Propiedades públicas para otros sistemas
    public bool IsMoving => movementInput.magnitude > 0.1f;
    public bool IsInStealthMode => isInStealthMode;
    public float CurrentNoiseRadius => isInStealthMode ? stealthNoiseRadius : normalNoiseRadius;
    public float CurrentSpeed => isInStealthMode ? stealthSpeed : normalSpeed;
    
    // Eventos
    public delegate void OnPlayerDetected();
    public static event OnPlayerDetected PlayerDetected;
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody2D>();
        }
        
        // Configurar Rigidbody2D para top-down
        rb.gravityScale = 0f;
        rb.freezeRotation = true;
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        
        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }
        
        // Configurar New Input System
        SetupInputSystem();
    }
    
    private void SetupInputSystem()
    {
        // Crear input actions programáticamente
        var inputActionAsset = ScriptableObject.CreateInstance<InputActionAsset>();
        
        // Crear action map
        var actionMap = new InputActionMap("Player");
        
        // Crear acción de movimiento
        moveAction = actionMap.AddAction("Move", InputActionType.Value);
        moveAction.AddCompositeBinding("2DVector")
            .With("Up", "<Keyboard>/w")
            .With("Down", "<Keyboard>/s")
            .With("Left", "<Keyboard>/a")
            .With("Right", "<Keyboard>/d");
        moveAction.AddCompositeBinding("2DVector")
            .With("Up", "<Keyboard>/upArrow")
            .With("Down", "<Keyboard>/downArrow")
            .With("Left", "<Keyboard>/leftArrow")
            .With("Right", "<Keyboard>/rightArrow");
        
        // Crear acción de sigilo
        stealthAction = actionMap.AddAction("Stealth", InputActionType.Button);
        stealthAction.AddBinding("<Keyboard>/leftShift");
        stealthAction.AddBinding("<Keyboard>/rightShift");
        
        // Habilitar acciones
        moveAction.Enable();
        stealthAction.Enable();
    }
    
    private void OnDestroy()
    {
        // Limpiar acciones
        if (moveAction != null) moveAction.Disable();
        if (stealthAction != null) stealthAction.Disable();
    }
    
    private void Update()
    {
        // Capturar input de movimiento (New Input System)
        if (moveAction != null)
        {
            movementInput = moveAction.ReadValue<Vector2>();
        }
        
        // Detectar modo sigilo
        if (stealthAction != null)
        {
            isInStealthMode = stealthAction.IsPressed();
        }
        
        // Normalizar para movimiento diagonal consistente
        if (movementInput.magnitude > 1f)
        {
            movementInput.Normalize();
        }
        
        // Actualizar visual del modo sigilo
        UpdateVisuals();
    }
    
    private void FixedUpdate()
    {
        // Aplicar movimiento
        float currentSpeed = isInStealthMode ? stealthSpeed : normalSpeed;
        Vector2 newVelocity = movementInput * currentSpeed;
        rb.linearVelocity = newVelocity;
        
        // Debug para verificar que funciona
        if (movementInput.magnitude > 0.1f)
        {
            Debug.Log($"Input: {movementInput}, Velocity: {newVelocity}");
        }
    }
    
    private void UpdateVisuals()
    {
        // Cambiar color según el modo (cyan normal, cyan oscuro en sigilo)
        if (spriteRenderer != null)
        {
            Color playerColor = isInStealthMode ? 
                new Color(0f, 0.8f, 0.8f, 0.7f) :  // Cyan semi-transparente en sigilo
                new Color(0f, 1f, 1f, 1f);          // Cyan brillante normal
            spriteRenderer.color = playerColor;
        }
    }
    
    /// <summary>
    /// Llamado cuando el jugador es detectado por un enemigo
    /// </summary>
    public void OnDetected()
    {
        PlayerDetected?.Invoke();
    }
    
    /// <summary>
    /// Dibuja el radio de ruido en el editor (para debug)
    /// </summary>
    private void OnDrawGizmosSelected()
    {
        // Dibujar radio de ruido
        Gizmos.color = isInStealthMode ? Color.green : Color.yellow;
        Gizmos.DrawWireSphere(transform.position, CurrentNoiseRadius);
    }
}
