using UnityEngine;

/// <summary>
/// Obstáculo del nivel - Threshold of Silence
/// Bloquea movimiento y línea de visión
/// </summary>
[RequireComponent(typeof(BoxCollider2D))]
public class Obstacle : MonoBehaviour
{
    [Header("Configuración Visual")]
    [SerializeField] private Color obstacleColor = new Color(0.25f, 0.25f, 0.3f);  // Gris oscuro
    [SerializeField] private Color borderColor = new Color(0.15f, 0.15f, 0.18f);   // Borde más oscuro
    [SerializeField] private bool showBorder = true;
    [SerializeField] private float borderWidth = 0.05f;
    
    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider;
    
    private void Awake()
    {
        // Configurar SpriteRenderer
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
        }
        spriteRenderer.color = obstacleColor;
        spriteRenderer.sortingOrder = 0;
        
        // Configurar Collider (NO es trigger - bloquea movimiento)
        boxCollider = GetComponent<BoxCollider2D>();
        if (boxCollider == null)
        {
            boxCollider = gameObject.AddComponent<BoxCollider2D>();
        }
        boxCollider.isTrigger = false;
        
        // Crear borde visual
        if (showBorder)
        {
            CreateBorder();
        }
        
        // Asegurar que está en la capa correcta para bloquear visión
        if (LayerMask.NameToLayer("Obstacles") != -1)
        {
            gameObject.layer = LayerMask.NameToLayer("Obstacles");
        }
    }
    
    private void CreateBorder()
    {
        // Crear objeto hijo para el borde
        GameObject border = new GameObject("Border");
        border.transform.parent = transform;
        border.transform.localPosition = Vector3.zero;
        border.transform.localScale = Vector3.one;
        
        // Añadir LineRenderer para el borde
        LineRenderer lr = border.AddComponent<LineRenderer>();
        lr.useWorldSpace = false;
        lr.loop = true;
        lr.positionCount = 4;
        
        // Calcular esquinas del cuadrado (en espacio local)
        float halfWidth = 0.5f;
        float halfHeight = 0.5f;
        
        lr.SetPosition(0, new Vector3(-halfWidth, -halfHeight, -0.01f));
        lr.SetPosition(1, new Vector3(halfWidth, -halfHeight, -0.01f));
        lr.SetPosition(2, new Vector3(halfWidth, halfHeight, -0.01f));
        lr.SetPosition(3, new Vector3(-halfWidth, halfHeight, -0.01f));
        
        lr.startWidth = borderWidth;
        lr.endWidth = borderWidth;
        lr.sortingOrder = 1;
        
        // Material del borde
        lr.material = new Material(Shader.Find("Sprites/Default"));
        lr.startColor = borderColor;
        lr.endColor = borderColor;
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(0.3f, 0.3f, 0.3f, 0.8f);
        Gizmos.DrawCube(transform.position, transform.localScale);
    }
}
