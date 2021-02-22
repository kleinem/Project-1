using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{

    public Text score;
    public GameObject cooldown;
    public Text information;


    private GaneController gc;
    private float cooldownTimer = 6.0f;
    private float cooldownTime = 0;
    private float scaler = 9.25f;
    private bool playing = false;

    void Start()
    {

        gc = GameObject.FindGameObjectWithTag("GameController").GetComponent<GaneController>();

    }

    void Update()
    {
        
        if (playing)
        {

            cooldownTime -= Time.deltaTime;
            cooldown.transform.localScale = new Vector3(1, scaler * Mathf.Clamp(cooldownTime / cooldownTimer, 0, 1), 1);

            if (cooldownTime <= 0)
            {

                gc.triggerLoss("cooldown");
                playing = false;

            }

        }

    }

    public void dartThrown()
    {

        cooldownTime = cooldownTimer;
        playing = true;

    }

    public void setText(string text_)
    {

        information.text = text_;

    }

    public IEnumerator clearAfterTime(float time_)
    {

        yield return new WaitForSeconds(time_);
        information.text = "";

    }

    public void setScore(int score_)
    {

        string tmp = "";
        if (score_ / 1000 < 1)
        {

            tmp += "0";

        }
        if (score_ / 100 < 1)
        {

            tmp += "0";

        }
        if (score_ / 10 < 1)
        {

            tmp += "0";

        }
        tmp += score_;
        score.text = "SCORE : " + tmp;

    }

}
