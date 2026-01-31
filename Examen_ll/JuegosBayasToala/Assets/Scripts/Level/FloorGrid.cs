using UnityEngine;

/// <summary>
/// Genera un piso con patrón de grid para el nivel
/// Threshold of Silence
/// </summary>
public class FloorGrid : MonoBehaviour
{
    [Header("Configuración del Grid")]
    [SerializeField] private int gridWidth = 20;
    [SerializeField] private int gridHeight = 15;
    [SerializeField] private float cellSize = 1f;
    
    [Header("Colores")]
    [SerializeField] private Color floorColor = new Color(0.06f, 0.06f, 0.08f);   // Casi negro uniforme
    [SerializeField] private Color lineColor = new Color(0.15f, 0.15f, 0.18f);    // Líneas del grid sutiles
    
    [Header("Configuración Visual")]
    [SerializeField] private bool showGridLines = false;  // Desactivado por defecto
    [SerializeField] private float lineWidth = 0.02f;
    
    private void Start()
    {
        GenerateFloor();
    }
    
    public void GenerateFloor()
    {
        // Limpiar hijos existentes
        foreach (Transform child in transform)
        {
            if (Application.isPlaying)
                Destroy(child.gameObject);
            else
                DestroyImmediate(child.gameObject);
        }
        
        // Crear tiles del piso
        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                CreateTile(x, y);
            }
        }
        
        // Crear líneas del grid
        if (showGridLines)
        {
            CreateGridLines();
        }
    }
    
    private void CreateTile(int x, int y)
    {
        GameObject tile = new GameObject($"Tile_{x}_{y}");
        tile.transform.parent = transform;
        tile.transform.position = new Vector3(
            x * cellSize - (gridWidth * cellSize / 2f) + cellSize / 2f,
            y * cellSize - (gridHeight * cellSize / 2f) + cellSize / 2f,
            1f // Detrás de todo
        );
        tile.transform.localScale = new Vector3(cellSize, cellSize, 1f);
        
        SpriteRenderer sr = tile.AddComponent<SpriteRenderer>();
        sr.sprite = CreateSquareSprite();
        sr.sortingOrder = -100; // Muy atrás
        
        // Color uniforme para todo el piso
        sr.color = floorColor;
    }
    
    private void CreateGridLines()
    {
        // Líneas verticales
        for (int x = 0; x <= gridWidth; x++)
        {
            CreateLine(
                new Vector3(x * cellSize - (gridWidth * cellSize / 2f), -gridHeight * cellSize / 2f, 0.5f),
                new Vector3(x * cellSize - (gridWidth * cellSize / 2f), gridHeight * cellSize / 2f, 0.5f),
                $"VLine_{x}"
            );
        }
        
        // Líneas horizontales
        for (int y = 0; y <= gridHeight; y++)
        {
            CreateLine(
                new Vector3(-gridWidth * cellSize / 2f, y * cellSize - (gridHeight * cellSize / 2f), 0.5f),
                new Vector3(gridWidth * cellSize / 2f, y * cellSize - (gridHeight * cellSize / 2f), 0.5f),
                $"HLine_{y}"
            );
        }
    }
    
    private void CreateLine(Vector3 start, Vector3 end, string name)
    {
        GameObject line = new GameObject(name);
        line.transform.parent = transform;
        
        LineRenderer lr = line.AddComponent<LineRenderer>();
        lr.positionCount = 2;
        lr.SetPosition(0, start);
        lr.SetPosition(1, end);
        lr.startWidth = lineWidth;
        lr.endWidth = lineWidth;
        lr.sortingOrder = -99;
        
        // Material simple
        lr.material = new Material(Shader.Find("Sprites/Default"));
        lr.startColor = lineColor;
        lr.endColor = lineColor;
    }
    
    private Sprite CreateSquareSprite()
    {
        // Crear una textura blanca simple
        Texture2D texture = new Texture2D(4, 4);
        Color[] colors = new Color[16];
        for (int i = 0; i < 16; i++) colors[i] = Color.white;
        texture.SetPixels(colors);
        texture.Apply();
        
        return Sprite.Create(texture, new Rect(0, 0, 4, 4), new Vector2(0.5f, 0.5f), 4);
    }
    
    private void OnDrawGizmos()
    {
        // Visualizar el área del grid en el editor
        Gizmos.color = new Color(0.3f, 0.3f, 0.3f, 0.3f);
        Gizmos.DrawWireCube(transform.position, new Vector3(gridWidth * cellSize, gridHeight * cellSize, 0));
    }
}
