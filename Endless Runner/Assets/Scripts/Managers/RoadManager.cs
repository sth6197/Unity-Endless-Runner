using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class RoadManager : State
{
    [SerializeField] List<GameObject> roads;

    [SerializeField] float speed = 25.0f;
    [SerializeField] float offset = 40.0f;
    
    void Start()
    {
        roads.Capacity = 10;
    }

    void Update()
    {
        if (state == false) return;

        for (int i = 0; i < roads.Count; i++)
        {
            roads[i].transform.Translate(Vector3.back * speed * Time.deltaTime);
        }
    }

    public void InitializePosition()
    {
         GameObject gameObject = roads[0];

         roads.Remove(gameObject);
         
         float newZ = roads[roads.Count - 1].transform.position.z + offset;

         gameObject.transform.position = new Vector3(0, 0, newZ);

         roads.Add(gameObject);
    }
}
