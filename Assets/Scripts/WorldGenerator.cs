using UnityEngine;

/**
 * =======================================
 * Simple procedural map generator v0.0.1...
 * Made with ♥ By qBite 2019
 * 
 * =======================================
 * 
 * Pixel Art source: https://www.reddit.com/r/PixelArt/comments/4ms6mn/newbieccoc_tree_in_a_grass_field/
 * =======================================
 **/

public class WorldGenerator : MonoBehaviour
{
    [Header("Area")]
    public Vector2 from;
    public Vector2 to;

    [Header("Circle")]
    public float distance;
    public float radius;

    [Space]
    public GameObject[] objects;

    private float curX;
    private float curY;

    GameObject GetRandomObject() {
        return objects[Mathf.RoundToInt(Random.Range(0, objects.Length))];
    }

    Vector2 GetRandomPoint(float x, float y) {
        return new Vector2(x, y) + Random.insideUnitCircle * radius;
    }

    void Start()
    {
        curX = from.x;
        curY = from.y;

        while (curY < to.y)
        {
            while (curX < to.x)
            {
                Instantiate(GetRandomObject(), GetRandomPoint(curX, curY), Quaternion.identity);
                curX += distance;
            }
            curX = from.x;
            curY += distance;
        }
    }
}
