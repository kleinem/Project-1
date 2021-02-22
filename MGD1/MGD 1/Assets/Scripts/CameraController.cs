using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public float lerpSpeed = 0.1f;
    private Vector3[] positions = new Vector3[] {

        new Vector3(0, 4.5f,-10),
        new Vector3(0, 1, -10)

    };

    private Vector3 targetPos;

    void Start()
    {

        setPos(1);

    }

    void Update()
    {

        transform.position = Vector3.Lerp(transform.position, targetPos, lerpSpeed);

    }

    public void setPos(int index_)
    {

        targetPos = positions[index_];

    }

    private void FixedUpdate()
    {

        

    }
}
