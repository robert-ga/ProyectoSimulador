using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetBouns : MonoBehaviour
{
    public static TargetBouns Instance;
    void Awake()
    {
        Instance = this;
    }
    [SerializeField] BoxCollider col;
    public Vector3 GetRamdomPosition()
    {
        Vector3 center = col.center + transform.position;
        float minX = center.x - col.size.x / 2f;
        float maxX = center.x + col.size.x / 2f;

        float minY = center.y - col.size.y / 2f;
        float maxY = center.y + col.size.y / 2f;

        float minZ = center.z - col.size.z / 2f;
        float maxZ = center.z + col.size.z / 2f;

        float ramdomX = Random.Range(minX, maxX);
        float ramdomY = Random.Range(minY, maxY);
        float ramdomZ = Random.Range(minZ, maxZ);

        Vector3 ramdomposition = new Vector3(ramdomX, ramdomY, ramdomZ);

        return ramdomposition;

    }
}
