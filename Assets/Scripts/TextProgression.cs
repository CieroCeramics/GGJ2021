using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TextProgression : MonoBehaviour
{
    public Text mText;
    public Sprite[] charImgArray;
    
    public Image bgImg;
    public Image charImg;
    int stage = 0;

    private bool openscene = false;

    private string[] textArray1 = new string[]
    {
        "Welcome to GAME TITLE\nYou are a \"SOUL RETREIVER\" you're sworn duty in life is to return lost souls to those who have fallen victim to \n \"THE REAPER\".\n Find the lost soul fragments and return them to their rightful HUMAN...\ndon't get caught by \"THE REAPER\"",
        "Skills of the trade:\nUse WASD to move around in \"SOUL SPACE\" \nPress Space bar to turn flash light On/Off.\nLeft Click + Move Mouse to rotate the camera.\nRight Click to Capture Souls. ",
        "I hardly got any sleep last night. THE REAPER just kept chasing me\n\"Maybe this time I should USE MY FLASHLIGHT LESS\"\n...\n*KNOCK *KNOCK",
        "Hello, \nPlease, can you retreive the 5 lost fragments of my soul?\"\nyou must go to the soul realm and find the soul fragments OF THE CORRECT NATURE to help the client.",
        ""
    };
    // Start is called before the first frame update

    private string[] textArray2 = new string[]
    {
        "... Analyzing soul data \n",
        "\"Great job you found my lost soul!\"\n \nall in a hard days work for a SOUL RETREIvER now for the next challenge.",
        "\"back in the SOUL OFFICE with another client, \n hope I don't meet THE REAPER today\"",
        "Hello,\n will you bring back the 10 fragments of my lost soul."
    };

    private string[] textArray3 = new string[]
    {
        "... Analyzing soul data \n",
        "\"Great job you found my lost soul!\"\n \nall in a hard days work for a SOUL RETREIvER now for the next challenge.",
        "\"back in the SOUL OFFICE with another client, \n hope I don't meet THE REAPER today\"",
        "Hello,\n will you bring back the 15 fragments of my lost soul."
    };

    private string[] textArrayBAD = new string[]
    {
        "... Analyzing soul data \n",
        "\"... you did not collect the rigt souls!\"\n \nGo Back and get the right ones!",
        "make sure you have the right color and number",
        "you did not collect sould OF THE CORRECT NATURE\n re enter the soul world and try again. \n",
        "tap space to retry"
    };

    int t;


    //====================================================================================================================//
    

    void Start()
    {
        if (GameSettings.FirstMissionComplete == false)
        {
            stage = 0;
        }

        charImg.sprite = charImgArray[stage];
        t = 0;
        bgImg.color = Color.black;
        charImg.enabled = false;
        mText.text = "[tap space bar]";


    }

    // Update is called once per frame
    void Update()
    {
if (!GameSettings.FirstMissionComplete)
{
     stager(textArray1);
}
else if (GameSettings.MissionSuccess)
{
        switch (stage)
        {
            case 0:
                stager(textArray1);
                break;
            case 1:
                stager(textArray2);
                break;
            case 2:
                stager(textArray3);
                break;
            case 3:
            //ty for playing;
            break;
           

        }
}
else stager(textArrayBAD);


    }

    //====================================================================================================================//
    

    void stager(string[] textArray)
    {
        charImg.sprite = charImgArray[0];
        if (Input.GetKeyDown(KeyCode.Space))
        {
            mText.text = textArray[t];

            if (t == 2)
            {
                bgImg.color = Color.white;
            }

            if (t == 3)
            {
                charImg.enabled = true;
            }

            if (t == 4)
            {
                SceneManager.LoadScene("gameScene");
            }

            t++;

        }
    }


    void stage2(string[] textArray)
    {
        charImg.sprite = charImgArray[1];
        if (Input.GetKeyDown(KeyCode.Space))
        {
            mText.text = textArray[t];


            if (t == 2)
            {
                bgImg.color = Color.white;
            }

            if (t == 3)
            {
                charImg.enabled = true;
            }

            if (t == 4)
            {
                SceneManager.LoadScene("gameScene");
            }

            t++;

        }
    }
}
