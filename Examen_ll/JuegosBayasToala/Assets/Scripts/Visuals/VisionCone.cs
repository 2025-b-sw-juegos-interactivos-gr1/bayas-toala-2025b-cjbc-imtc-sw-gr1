using UnityEngine;

/// <summary>
/// Cono de Visión Visual - Threshold of Silence
/// Renderiza el cono de visión del enemigo
/// </summary>
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class VisionCone : MonoBehaviour
{
    [Header("Configuración del Cono")]
    [SerializeField] private float visionAngle = 60f;
    [SerializeField] private float visionDistance = 6f;
    [SerializeField] private int segments = 20;
    
    [Header("Colores por Estado")]
    [SerializeField] private Color patrolColor = new Color(1f, 0f, 0f, 0.2f);
    [SerializeField] private Color suspiciousColor = new Color(1f, 0.5f, 0f, 0.3f);
    [SerializeField] private Color alertColor = new Color(1f, 0f, 0f, 0.5f);
    
    [Header("Referencias")]
    [SerializeField] private EnemyAI enemyAI;
    
    private Mesh mesh;
    private MeshFilter meshFilter;
    private MeshRenderer meshRenderer;
    private Material coneMaterial;
    
    private void Awake()
    {
        meshFilter = GetComponent<MeshFilter>();
        meshRenderer = GetComponent<MeshRenderer>();
        
        // Crear material transparente
        coneMaterial = new Material(Shader.Find("Sprites/Default"));
        coneMaterial.color = patrolColor;
        meshRenderer.material = coneMaterial;
        meshRenderer.sortingOrder = -1; // Detrás del enemigo
        
        // Crear mesh inicial
        mesh = new Mesh();
        meshFilter.mesh = mesh;
        
        GenerateConeMesh();
    }
    
    private void Start()
    {
        if (enemyAI == null)
        {
            enemyAI = GetComponentInParent<EnemyAI>();
        }
    }
    
    private void Update()
    {
        UpdateConeColor();
    }
    
    private void GenerateConeMesh()
    {
        Vector3[] vertices = new Vector3[segments + 2];
        int[] triangles = new int[segments * 3];
        
        // Vértice central (origen del cono)
        vertices[0] = Vector3.zero;
        
        // Generar vértices del arco
        float angleStep = visionAngle / segments;
        float startAngle = -visionAngle / 2f;
        
        for (int i = 0; i <= segments; i++)
        {
            float currentAngle = startAngle + (angleStep * i);
            float rad = currentAngle * Mathf.Deg2Rad;
            
            vertices[i + 1] = new Vector3(
                Mathf.Cos(rad) * visionDistance,
                Mathf.Sin(rad) * visionDistance,
                0
            );
        }
        
        // Generar triángulos
        for (int i = 0; i < segments; i++)
        {
            triangles[i * 3] = 0;
            triangles[i * 3 + 1] = i + 1;
            triangles[i * 3 + 2] = i + 2;
        }
        
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
    }
    
    private void UpdateConeColor()
    {
        if (enemyAI == null || coneMaterial == null) return;
        
        Color targetColor = enemyAI.CurrentState switch
        {
            EnemyAI.EnemyState.Patrol => patrolColor,
            EnemyAI.EnemyState.Suspicious => suspiciousColor,
            EnemyAI.EnemyState.Alert => alertColor,
            EnemyAI.EnemyState.Confirmed => alertColor,
            _ => patrolColor
        };
        
        coneMaterial.color = Color.Lerp(coneMaterial.color, targetColor, Time.deltaTime * 5f);
    }
    
    /// <summary>
    /// Actualiza los parámetros del cono en runtime
    /// </summary>
    public void UpdateConeParameters(float angle, float distance)
    {
        visionAngle = angle;
        visionDistance = distance;
        GenerateConeMesh();
    }
}
