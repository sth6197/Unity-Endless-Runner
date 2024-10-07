using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObstaclePositionManager : MonoBehaviour
{
    [SerializeField] float[] randomPositionZ = new float[16];

    [SerializeField] int index = -1;
    [SerializeField] Transform[] parentRoads; 

    private void Awake()
    {
        for (int i = 0; i < randomPositionZ.Length; i++)
        {
            randomPositionZ[i] = i * 2.5f + -10.0f;
        }
    }

    public void InitializePosition()
    {
        index = (index + 1) % parentRoads.Length;

        transform.SetParent(parentRoads[index]);

        transform.localPosition += new Vector3(0, 0, 40);
    }
}
