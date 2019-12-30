using System.Collections;
using UnityEngine;
using UnityEngine.Events;

/**
 * =======================================
 * Simple procedural map generator v0.1.2...
 * Made with ♥ By qBite 2019
 * 
 * =======================================
 * 
 * Pixel Art source: https://www.reddit.com/r/PixelArt/comments/4ms6mn/newbieccoc_tree_in_a_grass_field/
 * =======================================
 **/

[DisallowMultipleComponent]
public class WorldGenerator : MonoBehaviour
{
    public enum GenerationType {
        Box = 0,
        Circle = 1
    };

    public GenerationType Type = GenerationType.Circle;

    [Header("Area")]
    public Bounds Area = new Bounds(Vector2.zero, Vector2.one);

    [Space]
    public float Distance = 1f;

    [Header("Box")]
    public Vector2 BoxSize = Vector2.one;

    [Header("Circle")]
    public float CircleRadius = 0.5f;

    [Space]
    public GameObject[] objects;

    [Header("Debug")]
    public bool Draw = true;

    [Header("Events")]
    public UnityEvent OnGenerationEnd;

    private Coroutine coroutine;

    public GameObject GetRandomObject() {
        return objects[Mathf.RoundToInt(Random.Range(0, objects.Length))];
    }

    public static Vector2 GetRandomCirclePoint(float radius) {
        return Random.insideUnitCircle * radius;
    }

    public static Vector2 GetRandomBoxPoint(Vector2 size)
    {
        return new Vector2(Random.Range(-size.x, size.x), Random.Range(-size.y, size.y)) / 2;
    }

    public void Generate() {
        if (coroutine == null)
        {
            switch (Type)
            {
                case GenerationType.Circle:
                    coroutine = StartCoroutine(Circle());
                    break;
                case GenerationType.Box:
                    coroutine = StartCoroutine(Box());
                    break;
            }
        } else
        {
            Debug.LogError("Generation already running!");
        }
    }

    public void StopGenerate() {
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
            coroutine = null;
        }
    }

    private IEnumerator Circle()
    {
        Vector2 Current = (Vector2)Area.min + (Vector2.one * CircleRadius);

        while (Current.y < Area.max.y)
        {
            while (Current.x < Area.max.x)
            {
                Instantiate(GetRandomObject(), Current + GetRandomCirclePoint(CircleRadius), Quaternion.identity);
                Current.x += (CircleRadius * 2) + Distance;
                yield return null;
            }
            Current.x = Area.min.x + CircleRadius;
            Current.y += (CircleRadius * 2) + Distance;
        }

        coroutine = null;
        OnGenerationEnd.Invoke();
    }

    private IEnumerator Box()
    {
        Vector2 Current = (Vector2)Area.min + (BoxSize / 2);

        while (Current.y < Area.max.y)
        {
            while (Current.x < Area.max.x)
            {
                Instantiate(GetRandomObject(), Current + GetRandomBoxPoint(BoxSize), Quaternion.identity);
                Current.x += BoxSize.x + Distance;
                yield return null;
            }
            Current.x = Area.min.x + (BoxSize.x / 2);
            Current.y += BoxSize.y + Distance;
        }

        coroutine = null;
        OnGenerationEnd.Invoke();
    }
}
