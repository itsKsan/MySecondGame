using UnityEngine;

public static class UtilsClass
{
    public static Vector2 GetRandomPosition(float radius)
    {
        var randomPosition = Random.insideUnitCircle * radius;
        return randomPosition;
    }
}