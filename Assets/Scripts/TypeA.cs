using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TypeA : SoulBehavior
{
     public Vector3 point; 
    public float range; 
    public float speed; 
    public float amp;
    public float per;
     private Vector3  dest;
    private Vector3 curPos;


    
       private float movementDuration = 3.0f;
     private float waitBeforeMoving = 0.0f;
     private bool hasArrived = false;
 
    // Start is called before the first frame update
    public void Start()
    {
         mr=GetComponentInChildren<SkinnedMeshRenderer>();
        ps = GetComponentInChildren<ParticleSystem>();
     //    dest = newSpot();
        curPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {

           if(!hasArrived)
         {
             hasArrived = true;

             float randX = Random.Range(point.x-range, point.x+range);
             float randZ = Random.Range(point.z-range, point.z+range);
             dest = new Vector3 (randX, 0, randZ);
             Vector3 dir = dest - transform.position;
            float angle = Mathf.Atan2(dir.y, dir.z) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, angle, 0));
             StartCoroutine(MoveToPoint(new Vector3(randX, 17.4f, randZ)));
         }




        // timeToChangeDirection -= Time.deltaTime;
        
        //  if (timeToChangeDirection <= 0) 
        //  {
        //      //ChangeDirection();
        //    dest = newSpot();
        //    float a = amp;
        //   // float b = 2*Mathf.PI*2/per;
        //    Vector3 dir = dest - transform.position;
        //    float angle = Mathf.Atan2(dir.y, dir.z) * Mathf.Rad2Deg;
        //    float singleStep = speed * Time.deltaTime;

        //     Vector3 newDirection = Vector3.RotateTowards(transform.forward, dest, singleStep, 0.0f);
        //    transform.rotation = Quaternion.Euler(newDirection);
        //  }
 
        // transform.Translate (new Vector3(-speed, amp*Mathf.Sin(per*Time.time),0));
    }

//         public Vector3 newSpot()
//     {
//         float newx = Random.Range(xLimit*-1, xLimit);
//         float newy = Random.Range(yLimit*-1, yLimit);
// timeToChangeDirection = 1.5f;
// print(newx + " " + newy);
//         return new Vector3 (newx, 0,newy );
         
//     }













 
     private IEnumerator MoveToPoint(Vector3 targetPos)
     {
         float timer = 0.0f;
         Vector3 startPos = transform.position;
 
         while (timer < movementDuration)
         {
             timer += Time.deltaTime;
             float t = timer / movementDuration;
             t = t * t * t * (t * (6f * t - 15f) + 10f);
             transform.position = Vector3.Lerp(startPos, targetPos, t);
 
             yield return null;
         }
 
         yield return new WaitForSeconds(waitBeforeMoving);
         hasArrived = false;
     }

}
