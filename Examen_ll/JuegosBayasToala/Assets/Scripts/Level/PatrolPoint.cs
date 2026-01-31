using UnityEngine;

/// <summary>
/// Punto de Patrullaje - Threshold of Silence
/// Marcador visual para las rutas de patrullaje de enemigos
/// </summary>
public class PatrolPoint : MonoBehaviour
{
    [Header("Configuración Visual (Solo Editor)")]
    [SerializeField] private Color gizmoColor = Color.blue;
    [SerializeField] private float gizmoRadius = 0.3f;
    
    [Header("Configuración")]
    [SerializeField] private float waitTime = 1.0f; // Tiempo de espera en este punto
    
    public float WaitTime => waitTime;
    
    private void OnDrawGizmos()
    {
        Gizmos.color = gizmoColor;
        Gizmos.DrawSphere(transform.position, gizmoRadius);
        
        // Dibujar número del punto
        #if UNITY_EDITOR
        UnityEditor.Handles.Label(transform.position + Vector3.up * 0.5f, gameObject.name);
        #endif
    }
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, gizmoRadius * 1.5f);
    }
}
