using UnityEngine;
using System.Collections.Generic;

public class PathWithTriggers : MonoBehaviour
{
    public GameObject player;
    public BoxCollider[] points;
    public GameObject arrowPrefab;
    public float arrowSpacing = 1.0f; // Adjust this to set the spacing between arrows

    private int currentSegment = -1; // Start before the first segment
    private List<List<GameObject>> arrows = new List<List<GameObject>>();

    void Start()
    {
        // Check if points and arrowPrefab are assigned
        if (points != null && points.Length > 1 && arrowPrefab != null && player != null)
        {
            DrawArrows();
        }
        else
        {
            Debug.LogError("Points array must contain at least 2 points, ArrowPrefab and Player must be assigned.");
        }
    }

    void DrawArrows()
    {
        for (int i = 0; i < points.Length - 1; i++)
        {
            Vector3 startPoint = points[i].transform.position;
            Vector3 endPoint = points[i + 1].transform.position;
            Vector3 direction = (endPoint - startPoint).normalized;
            float distance = Vector3.Distance(startPoint, endPoint);
            int arrowCount = Mathf.FloorToInt(distance / arrowSpacing);

            List<GameObject> segmentArrows = new List<GameObject>();

            for (int j = 0; j <= arrowCount; j++)
            {
                Vector3 position = startPoint + direction * (j * arrowSpacing);
                GameObject arrow = Instantiate(arrowPrefab, position, Quaternion.LookRotation(direction));
                arrow.transform.SetParent(transform); // Optional: parent the arrows to keep the hierarchy clean
                segmentArrows.Add(arrow);
            }

            arrows.Add(segmentArrows);

            // Make sure the collider is set as a trigger
            points[i].isTrigger = true;
        }
    }

    void Update()
    {
        if (currentSegment < points.Length - 1)
        {
            // Check if player entered the next trigger point
            if (currentSegment + 1 < points.Length && points[currentSegment + 1].bounds.Intersects(player.GetComponent<Collider>().bounds))
            {
                // Disable arrows of the previous segment
                if (currentSegment >= 0)
                {
                    foreach (GameObject arrow in arrows[currentSegment])
                    {
                        Destroy(arrow);
                    }
                }

                currentSegment++;
            }
        }
    }
}

