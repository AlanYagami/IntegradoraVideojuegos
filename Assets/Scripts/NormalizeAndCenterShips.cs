using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class NormalizeAndCenterShips : MonoBehaviour
{
    [Tooltip("Tamaño objetivo aproximado (en unidades Unity) para la dimensión más grande de cada nave.")]
    public float targetMaxSize = 2.0f;

    [Tooltip("Si true, se ajustará la escala de las naves para igualar tamaños.")]
    public bool normalizeScale = true;

    [ContextMenu("Run NormalizeAndCenter on children")]
    public void Run()
    {
        // Recorre cada hijo directo (cada 'nave' esperada)
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform ship = transform.GetChild(i);

            // Si ya es un pivot creado por esto (nombre empieza con Pivot_), saltarlo
            if (ship.name.StartsWith("Pivot_")) continue;

            // Crear pivot vacío en la misma posición/rotación del ship
            GameObject pivotGO = new GameObject("Pivot_" + ship.name);
            Transform pivot = pivotGO.transform;
            pivot.SetParent(transform, false);

            // Colocar pivot en la misma posición y rotación local que el ship
            pivot.localPosition = ship.localPosition;
            pivot.localRotation = ship.localRotation;
            pivot.localScale = Vector3.one;

            // Reparentear ship al pivot, manteniendo transform global
            Vector3 oldWorldPos = ship.position;
            Quaternion oldWorldRot = ship.rotation;
            Vector3 oldLocalScale = ship.localScale;

            ship.SetParent(pivot, true);

            // Obtener renderer para calcular bounds en world space
            Renderer[] renderers = ship.GetComponentsInChildren<Renderer>();
            if (renderers.Length == 0)
            {
                Debug.LogWarning($"[Normalize] {ship.name} no tiene Renderer (no se puede centrar automáticamente).");
                continue;
            }

            // Calcular bounds combinados
            Bounds b = renderers[0].bounds;
            for (int r = 1; r < renderers.Length; r++)
                b.Encapsulate(renderers[r].bounds);

            // Centro en world space
            Vector3 worldCenter = b.center;

            // Queremos que ese centro coincida con pivot.position (en world)
            Vector3 delta = pivot.position - worldCenter;

            // Mover ship en world space por delta
            ship.position += delta;

            // Ahora opcionalmente normalizar escala: hacemos que la dimensión máxima -> targetMaxSize
            if (normalizeScale)
            {
                // Recalcular bounds en caso de que la posición haya cambiado
                Bounds newBounds = renderers[0].bounds;
                for (int r = 1; r < renderers.Length; r++)
                    newBounds.Encapsulate(renderers[r].bounds);

                float maxDim = Mathf.Max(newBounds.size.x, Mathf.Max(newBounds.size.y, newBounds.size.z));
                if (maxDim > 0.0001f)
                {
                    float scaleFactor = targetMaxSize / maxDim;
                    // Aplicar escala al pivot (así no alteramos la rotación local del modelo)
                    pivot.localScale = pivot.localScale * scaleFactor;
                }
            }

            // Ajuste final: poner ship.localPosition relativo al pivot (para evitar drift por rounding)
            // ya que movimos en world space, convertimos a local
            ship.localPosition = pivot.InverseTransformPoint(ship.position);
            ship.localRotation = Quaternion.Inverse(pivot.rotation) * ship.rotation;

            // opcional: resetear escala local del ship si era diferente (para mantener proporciones)
            ship.localScale = oldLocalScale;

            Debug.Log($"[Normalize] Pivot creado para {ship.name}, pivot: {pivot.name}");
        }

        Debug.Log("[Normalize] Proceso completado.");
    }
}
