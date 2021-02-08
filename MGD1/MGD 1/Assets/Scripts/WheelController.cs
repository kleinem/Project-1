using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelController : MonoBehaviour
{

    public GameObject wedge;

    private Rigidbody2D rb;
    private GameObject currentWedge;
    private float lastSpawn = 180;
    private bool spawning = false;

    void Start()
    {

        rb = GetComponent<Rigidbody2D>();
        updateSpeed(60);

    }

    void Update()
    {

        foreach (Touch t in Input.touches)
        {

            if (t.phase == TouchPhase.Ended)
            {

                setSpawning(!spawning);

            }

        }
        if (spawning)
        {

            

        }


    }

    public void setSpawning(bool spawning_)
    {

        if (spawning_)
        {

            lastSpawn = 180 - transform.eulerAngles.z;
            spawning = true;

        }
        else
        {

            spawning = false;

        }

    }

    private void spawnWedge(Color col_, float size_)
    {

        GameObject tmp = Instantiate(wedge, transform);
        tmp.transform.localEulerAngles = new Vector3(0, 0, lastSpawn);
        tmp.GetComponent<WedgeController>().setColor(col_);
        tmp.GetComponent<WedgeController>().setSize(size_);
        lastSpawn += size_;

    }

    public void updateSpeed(float speed_)
    {

        rb.angularVelocity = -speed_;

    }

}
