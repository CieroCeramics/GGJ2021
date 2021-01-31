using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class SoulBehavior : MonoBehaviour
{
    private static readonly int BaseColor = Shader.PropertyToID("_BaseColor");
    private static readonly int AlbedoColor = Shader.PropertyToID("AlbedoColor");
    private static readonly int EmissionColor = Shader.PropertyToID("EmissionColor");


    
    protected static PlayerController playerController;
    
    protected bool lighton { get; private set; }

    //====================================================================================================================//

    public int type;
    public float timeToChangeDirection;

    [FormerlySerializedAs("soulCol")] 
    public Color soulColor;
    
    public GameObject particleEffectPrefab;


    private new Renderer renderer;


    //====================================================================================================================//
    
    // Start is called before the first frame update
    protected virtual void Start()
    {
        //Find the renderer, and instance the material
        renderer = GetComponentInChildren<Renderer>();
        var shader = renderer.material.shader;
        renderer.material = new Material(shader);

        SetColor(soulColor);

        if (!playerController)
            playerController = FindObjectOfType<PlayerController>();


        lighton = playerController.FlashLightOn;
    }

    private void OnTriggerEnter(Collider other)
    {
        print("ontrigger");

        if (!other.CompareTag("CaptureLight")) 
            return;

        CaptureSoul();
    }

    //====================================================================================================================//
    
    public virtual void CaptureSoul()
    {
        print("collided");

        Instantiate(particleEffectPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    protected void SetColor(in Color color)
    {
        renderer.material.SetColor(AlbedoColor, color);
    }
}

