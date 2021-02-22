using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WedgeController : MonoBehaviour
{

    private Color col = new Color();
    private float angle = 10;
    private bool isLast = true;
    private int layer = 0;
    private bool isActive = false;

    private WheelController wc;

    void Start()
    {


        wc = transform.parent.GetComponent<WheelController>();

    }

    void FixedUpdate()
    {

        if (isLast && getCurrentAngle() >= angle)
        {

            wc.spawnNext();
            isLast = false;

        }
        if (getCurrentAngle() >= 270)
        {

            Destroy(gameObject);

        }
        if (!isActive && getCurrentAngle() > 90)
        {

            isActive = true;
            wc.setCurrentColor(col);

        }

    }

    public void initialize(float angle_, Color col_, int layer_)
    {

        col = col_;
        angle = angle_;
        layer = layer_;
        transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = col_;
        transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sortingOrder = layer_;

    }

    private float getCurrentAngle()
    {

        return (360 - transform.eulerAngles.z);

    }

}
