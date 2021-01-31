using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TextProgression : MonoBehaviour
{
    public Text mText;
    public RawImage charImg;
    public RawImage bgImg;

    
    private bool openscene =false;
    private string[] textArray = new string[]{"Welcome to GAME TITLE\nYou are a \"SOUL RETREIVER\" you're sworn duty in life is to return lost souls to those who have fallen victim to \n \"THE REAPER\".\n Find the lost soul fragments and return them to their rightful HUMAN...\ndon't get caught by \"THE REAPER\"",
    "Skills of the trade:\nUse WASD to move around in \"SOUL SPACE\" \nPress Space bar to turn flash light On/Off.\nLeft Click + Move Mouse to rotate the camera.\nRight Click to Capture Souls. ",
    "I hardly got any sleep last night. THE REAPER just kept chasing me\n\"Maybe this time I should USE MY FLASHLIGHT LESS\"\n...\n*KNOCK *KNOCK",
    "Hello, \nPlease, can you retreive the 5 lost fragments of my soul?\"\nyou must go to the soul realm and find the soul fragments OF THE CORRECT NATURE to help the client.",""};
    // Start is called before the first frame update

    int t;
    void Start()
    {
        t=0;
        bgImg.color = Color.black;
        charImg.enabled= false;
         mText.text = textArray[0];
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            mText.text = textArray[t];

            if(t==2)
            {
                 bgImg.color = Color.white;
            }
            if(t==3)
            {
                charImg.enabled = true;
            }

            if (t==4)
            {
                SceneManager.LoadScene("gameScene");
            }
            t++;

        }

  
    }
}
