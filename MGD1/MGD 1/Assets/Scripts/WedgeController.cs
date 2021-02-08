using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WedgeController : MonoBehaviour
{

    public Color col;

    void Start()
    {

        setColor(new Color(Random.Range(0, 1.0f), Random.Range(0, 1.0f), Random.Range(0, 1.0f)));

    }

    void Update()
    {



    }

    public void setColor(Color col_)
    {

        col = col_;
        transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = col_;

    }

    public void setSize(float size_)
    {

        transform.GetChild(0).localEulerAngles = new Vector3(0, 0, size_ - 90);
        Debug.Log("setting to " + size_);

    }

}
