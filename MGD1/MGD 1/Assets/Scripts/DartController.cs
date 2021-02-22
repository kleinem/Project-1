using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DartController : MonoBehaviour
{


    private Transform[] dartPositions;
    private Rigidbody2D rb;
    private float elapseTime = 0.15f;
    public int dartPosition = 0;
    private Color col;
    private bool fired = false;
    private bool ready = true;
    private bool started = false;
    private AudioSource thrown;

    void Start()
    {

        thrown = GameObject.FindGameObjectWithTag("Thrown").GetComponent<AudioSource>();

    }

    void Update()
    {
        

    }

    private void init()
    {

        dartPositions = new Transform[5] {

            GameObject.FindGameObjectWithTag("Pos0").transform,
            GameObject.FindGameObjectWithTag("Pos1").transform,
            GameObject.FindGameObjectWithTag("Pos2").transform,
            GameObject.FindGameObjectWithTag("Pos3").transform,
            GameObject.FindGameObjectWithTag("Pos4").transform

        };
        transform.position = dartPositions[0].position;
        transform.localScale = dartPositions[0].localScale;
        rb = GetComponent<Rigidbody2D>();
        started = true;

    }

    public void startShift()
    {
        if (ready)
        {
        
            gameObject.SetActive(true);
            StartCoroutine(shift());
        
        }

    }

    private IEnumerator shift()
    {

        ready = false;
        if (!started)
        {

            init();

        }

        if (dartPosition < 4)
        {

            float startTime = Time.time;
            float endTime = Time.time + elapseTime;
            while (Time.time < endTime)
            {

                transform.position = Vector3.Lerp(dartPositions[dartPosition].transform.position, dartPositions[dartPosition + 1].transform.position, (Time.time - startTime) / (endTime - startTime));
                transform.localScale = Vector3.Lerp(dartPositions[dartPosition].transform.localScale, dartPositions[dartPosition + 1].transform.localScale, (Time.time - startTime) / (endTime - startTime));
                yield return new WaitForEndOfFrame();

            }
            transform.position = dartPositions[dartPosition + 1].transform.position;
            transform.localScale = dartPositions[dartPosition + 1].transform.localScale;
            dartPosition++;
            ready = true;

        }
        else
        {

            rb.velocity = new Vector2(0,-10);
            yield return new WaitForEndOfFrame();
            thrown.PlayOneShot(thrown.clip);

        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.name == "Wheel")
        {

            Destroy(GetComponent<Rigidbody2D>());
            transform.SetParent(collision.gameObject.transform);
            Destroy(gameObject, 3);
            collision.gameObject.GetComponent<WheelController>().landDart(col);

        }

    }

    public void initialize(Color col_)
    {

        transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = col_;
        col = col_;

    }

}
