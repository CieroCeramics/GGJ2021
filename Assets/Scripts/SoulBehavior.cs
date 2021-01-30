using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulBehavior : MonoBehaviour
{
   
      public float timeToChangeDirection;
 
   

   public ParticleSystem ps;
    public SkinnedMeshRenderer mr;

    // Start is called before the first frame update
public  void Start()
    {
       
       
       // ChangeDirection() ;
    }

    // Update is called once per frame


      void Update () {
         
     }
 
 
 


     private void OnTriggerEnter(Collider other) {
         {
             print("ontrigger");
             if (other.tag=="Player")
             {
                 mr.enabled=false;
                 ps.Play();
                 print("collided");
             }
         }
     }
 }

