using UnityEngine;
using TMPro;

public class RaycastPlacer : MonoBehaviour
{
    [Header("Paramètres du Raycast")]
    [Tooltip("Distance maximale du raycast")]
    public float rayDistance = 10f;

    public TextMeshProUGUI tmpDebug;

    [Tooltip("Couches à détecter (laisser sur Everything pour tout détecter)")]
    public LayerMask layerMask = Physics.DefaultRaycastLayers;

    void Update()
    {
        // Point de départ : la position de l'objet
        Vector3 origin = transform.position;
        // Direction : tout droit par rapport à l'objet
        Vector3 direction = transform.forward;

        // On fait le raycast
        if (Physics.Raycast(origin, direction, out RaycastHit hitInfo, rayDistance, layerMask))
        {
            // Dessine le rayon en vert jusqu'à l'impact dans la Scene View
            Debug.DrawRay(origin, direction * hitInfo.distance, Color.green);

            // Log dans la console
            Debug.Log(
                $"[Raycast] Touché : {hitInfo.collider.gameObject.name} " +
                $"à une distance de {hitInfo.distance:F2} " +
                $"au point {hitInfo.point}",
                hitInfo.collider.gameObject
            );

            tmpDebug.text = hitInfo.collider.gameObject.name;
        }
        else
        {
            // Si rien touché, dessin d'un rayon rouge pour visualiser la direction
            Debug.DrawRay(origin, direction * rayDistance, Color.red);
        }
    }

    // Optionnel : visualisation dans l'éditeur quand l'objet est sélectionné
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position + transform.forward * rayDistance);
    }
}
