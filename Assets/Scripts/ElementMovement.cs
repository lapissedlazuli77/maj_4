using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementMovement : MonoBehaviour
{
    public float speed = 0.02f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 currentPosition = transform.position;
        currentPosition.x -= speed;
        transform.position = currentPosition;
        if (currentPosition.x < -6) { Destroy(gameObject); }
    }
}
