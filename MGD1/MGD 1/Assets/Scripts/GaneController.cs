using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GaneController : MonoBehaviour
{

    public GameObject dart;
    public WheelController wc;
    public CameraController cc;

    private UIController UICon;
    private bool canFire = true;
    private bool inGame = true;
    private int score = 0;
    private bool lost = false;

    void Start()
    {

        UICon = GameObject.FindGameObjectWithTag("UIController").GetComponent<UIController>();
        StartCoroutine(commence());

    }

    private IEnumerator commence()
    {

        inGame = false;
        advance();
        yield return new WaitForSeconds(0.5f);
        advance();
        yield return new WaitForSeconds(0.5f);
        advance();
        yield return new WaitForSeconds(0.5f);
        advance();
        yield return new WaitForSeconds(0.5f);
        UICon.setText("Tap the screen to fire a dart.\nMake sure to land it on the correct color!");
        inGame = true;

    }

    void Update()
    {
        
        foreach(Touch touch in Input.touches)
        {

            if (touch.phase == TouchPhase.Began)
            {

                tap(false);

            }

        }

    }

    public void quit()
    {

        Application.Quit();

    }

    private IEnumerator delay()
    {

        canFire = false;
        yield return new WaitForSeconds(0.22f);
        canFire = true;

    }

    private void tap(bool override_)
    {

        if (lost)
        {

            Time.timeScale = 1;
            SceneManager.LoadScene(0);

        }

        if (inGame)
        {

            if (canFire || override_)
            {

                advance();
                UICon.dartThrown();
                StartCoroutine(delay());
                UICon.setText("");

            }

        }

    }

    private void advance()
    {

        GameObject tmp = Instantiate(dart, Vector3.zero, Quaternion.identity);
        tmp.GetComponent<DartController>().initialize(wc.getColor());

        GameObject[] tmp2 = GameObject.FindGameObjectsWithTag("Dart");
        for (int a = 0; a < tmp2.Length; a++)
        {

            tmp2[a].GetComponent<DartController>().startShift();

        }

    }

    public void addScore(int score_)
    {

        score += score_;
        UICon.setScore(score);

    }

    public void triggerLoss(string cause_)
    {

        Debug.Log("lost due to " + cause_);
        Time.timeScale = 0;
        lost = true;
        UICon.setText("Game Over!\nYour final score was " + score + "\nTap anywhere to restart.");

    }

}
