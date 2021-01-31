using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulBehavior : MonoBehaviour
{
  
  public int type;
    public float timeToChangeDirection;

    public ParticleSystem ps;
    public SkinnedMeshRenderer mr;
public GameObject ThePlayer;
    public Color soulCol;

    private bool lighton;
    // Start is called before the first frame update
    public void Start()
    {
        mr.material.SetColor("_BaseColor",soulCol);
        lighton=ThePlayer.GetComponent<PlayerController>().FlashLightOn;
    }

    // Update is called once per frame


    void Update()
    {
        // if(lighton)
        // {
        //      mr.material.SetColor("_BaseColor",soulCol);
        // }
        // else mr.material.SetColor("_BaseColor", Color.black);
    }





    private void OnTriggerEnter(Collider other)
    {
        {
            print("ontrigger");
            if (other.tag == "CaptureLight")
            {
                mr.enabled = false;
                ps.Play();
                print("collided");
            }
        }
    }
}

