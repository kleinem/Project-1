using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelController : MonoBehaviour
{

    public GameObject wedge;
    private GameObject indicator;
    public UIController UICon;

    private float[] wedgeSize = new float[2] { 40, 60 };
    private Color[] colors = new Color[6]
    {

        new Color(1.0f, 0.5f, 0.5f),
        new Color(0.5f, 1.0f, 0.5f),
        new Color(0.5f, 0.5f, 1.0f),
        new Color(1.0f, 1.0f, 0.5f),
        new Color(0.5f, 1.0f, 1.0f),
        new Color(1.0f, 0.5f, 1.0f)

    };
    private int colorCount = 2;
    private Color lastCol;
    private int layerCounter = 0;
    private Rigidbody2D rb;
    private Color currentColor;
    private GaneController gc;
    private int currentLevel = 0;
    private int dartCount = 0;
    public AudioSource hit;
    public AudioSource miss;

    void Start()
    {

        gc = GameObject.FindGameObjectWithTag("GameController").GetComponent<GaneController>();
        indicator = transform.GetChild(0).gameObject;
        rb = GetComponent<Rigidbody2D>();
        rb.angularVelocity = -60;
        spawnNext();
        setLevel(0);

    }

    void Update()
    {



    }

    public void setLevel(int level_)
    {

        currentLevel = level_;
        switch (level_)
        {


            case 0:
                setStats(40, 60, 2);

                break;
            case 1:
                setStats(38, 58, 2);

                break;
            case 2:
                setStats(35, 53, 2);

                break;
            case 3:
                setStats(33, 50, 3);

                break;
            case 4:
                setStats(32, 47, 3);

                break;
            case 5:
                setStats(30, 44, 3);

                break;
            case 6:
                setStats(29, 41, 3);

                break;
            case 7:
                setStats(27, 38, 4);

                break;
            case 8:
                setStats(25, 33, 4);

                break;
            case 9:
                setStats(23, 30, 4);

                break;
            case 10:
                setStats(21, 28, 4);

                break;
            case 11:
                setStats(18, 26, 4);

                break;
            case 12:
                setStats(15, 23, 5);

                break;
            case 13:
                setStats(13, 21, 5);

                break;
            case 14:
                setStats(12, 20, 5);

                break;
            case 15:
                setStats(10, 19, 5);

                break;
            case 16:
                setStats(9, 18, 6);

                break;
            case 17:
                setStats(8, 17, 6);

                break;
            case 18:
                setStats(7, 16, 6);

                break;
            case 19:
                setStats(4, 12, 6);

                break;

        }

    }

    public void setCurrentColor(Color col_)
    {

        currentColor = col_;
        indicator.GetComponent<SpriteRenderer>().color = col_;

    }

    public Color getCurrentColor()
    {

        return currentColor;

    }

    public Color getColor()
    {

        return (colors[Random.Range(0, colorCount)]);

    }

    private void setStats(float min_, float max_, int colorCount_)
    {

        wedgeSize[0] = min_;
        wedgeSize[1] = max_;
        colorCount = colorCount_;

    }

    public void landDart(Color col_)
    {

        if (col_ == currentColor)
        {

            gc.addScore(currentLevel + 1);
            dartCount++;
            if (dartCount >= 12)
            {

                currentLevel++;
                setLevel(currentLevel);
                UICon.setText("Level : " + (currentLevel + 1));
                UICon.clearAfterTime(1.5f);
                dartCount = 0;

            }
            hit.PlayOneShot(hit.clip);

        }
        else
        {

            gc.triggerLoss("mismatch");
            miss.PlayOneShot(miss.clip);

        }

    }

    public void spawnNext()
    {

        GameObject tmp = Instantiate(wedge, transform);
        tmp.transform.eulerAngles = new Vector3(0, 0, 0);
        Color tmp2 = colors[Random.Range(0, colorCount)];
        while (tmp2 == lastCol)
        {

            tmp2 = colors[Random.Range(0, colorCount)];

        }
        tmp.GetComponent<WedgeController>().initialize(Random.Range(wedgeSize[0], wedgeSize[1]), tmp2, layerCounter);
        layerCounter++;
        lastCol = tmp2;

    }

}
