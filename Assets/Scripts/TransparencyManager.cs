using UnityEngine;

public class TransparencyManager : MonoBehaviour
{
    // Variables to adjust raycast size and direction from the Inspector
    public float raycastLength = 10f;
    public Vector3 raycastDirection = Vector3.forward;
    public LayerMask layerMask; // Set the layer mask to only hit certain layers if needed

    void Update()
    {
        // Perform the raycast
        RaycastHit hit;
        if (Physics.Raycast(transform.position, raycastDirection, out hit, raycastLength, layerMask))
        {
            // Check if the hit object has a renderer
            Renderer renderer = hit.collider.GetComponent<Renderer>();
            if (renderer != null)
            {
                // Set the object's transparency
                SetTransparency(renderer, 0.5f);
            }
        }
    }

    void SetTransparency(Renderer renderer, float transparency)
    {
        // Change the alpha value of the material
        foreach (Material mat in renderer.materials)
        {
            Color color = mat.color;
            color.a = transparency;
            mat.color = color;

            // Set the material to transparent rendering mode
            mat.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
            mat.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
            mat.SetInt("_ZWrite", 0);
            mat.DisableKeyword("_ALPHATEST_ON");
            mat.EnableKeyword("_ALPHABLEND_ON");
            mat.DisableKeyword("_ALPHAPREMULTIPLY_ON");
            mat.renderQueue = 3000;
        }
    }
}
