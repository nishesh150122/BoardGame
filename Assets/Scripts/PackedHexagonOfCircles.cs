using System;
using UnityEngine;

public class PackedHexagonOfCircles : MonoBehaviour
{
    public int numberOfCircles = 18; // Adjust the number of circles as needed
    public float circleRadius = 1f;
    public GameObject redCirclePrefab;
    public GameObject blackCirclePrefab;
    public GameObject whiteCirclePrefab;
    public PhysicsMaterial2D bounceMaterial;
    public float padding = 0.1f; // Adjust the padding to prevent overlap

    private void Awake()
    {
        CreateFilledHexagon();
    }


    void CreateFilledHexagon()
    {
        // Instantiate the center red circle at (0, 0)
        GameObject queen = Instantiate(redCirclePrefab,Vector2.zero, Quaternion.identity);
        queen.transform.SetParent(transform);
        CircleCollider2D redCircleCollider = queen.AddComponent<CircleCollider2D>();
        Rigidbody2D rigidbody2DRed = queen.AddComponent<Rigidbody2D>();
        rigidbody2DRed.angularDrag = 5f;
        rigidbody2DRed.drag = 3f;
        rigidbody2DRed.gravityScale = 0f;
        rigidbody2DRed.mass = 1f;
        rigidbody2DRed.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        rigidbody2DRed.sharedMaterial = bounceMaterial;

        for (int ring = 1; ring <= numberOfCircles / 6; ring++)
        {
            for (int i = 0; i < ring * 6; i++)
            {
                float angle = i * 2 * Mathf.PI / (ring * 6);
                float x = Mathf.Cos(angle) * (circleRadius + padding) * ring * 1.5f; // Add padding to prevent overlap
                float y = Mathf.Sin(angle) * (circleRadius + padding) * ring * 1.5f; // Add padding to prevent overlap

                // Instantiate black and white circles alternatively
                GameObject circlePrefab = i % 2 == 0 ? blackCirclePrefab : whiteCirclePrefab;
                GameObject circle = Instantiate(circlePrefab, new Vector2(x, y), Quaternion.identity);


                // Make the current script's GameObject (PackedHexagonOfCircles) the parent of the circle
                circle.transform.SetParent(transform);

                CircleCollider2D circleCollider = circle.AddComponent<CircleCollider2D>();
                Rigidbody2D rigidbody2D = circle.AddComponent<Rigidbody2D>();
                rigidbody2D.angularDrag = 5f;
                rigidbody2D.drag = 3f;
                rigidbody2D.gravityScale = 0f;
                rigidbody2D.mass = 1f;
                rigidbody2D.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
                rigidbody2D.sharedMaterial = bounceMaterial;
            }
        }
    }
}