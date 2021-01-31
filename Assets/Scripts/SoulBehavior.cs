using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class SoulBehavior : MonoBehaviour
{
    public enum TYPE
    {
        RED = 0,
        GREEN,
        BLUE
    }

    [Serializable]
    public struct ColorSet
    {
        [ColorUsage(true, true)]
        public Color mainColor;
        [ColorUsage(true, true)]
        public Color emissiveColor;
    }
    
    private static readonly int BaseColor = Shader.PropertyToID("_BaseColor");
    private static readonly int AlbedoColor = Shader.PropertyToID("Color_408732C4");
    private static readonly int EmissionColor = Shader.PropertyToID("Color_C908B841");
    
    protected static PlayerController playerController;
    protected static ReaperController reaperController;
    
    protected bool lighton { get; private set; }

    [SerializeField]
    protected TYPE type;

    //====================================================================================================================//

    public float timeToChangeDirection;

    public GameObject particleEffectPrefab;
    public GameObject collectSoundEffectPrefab;


    private new Renderer renderer;

    [SerializeField]
    private ColorSet[] ColorSets;


    private bool _captured;

    //====================================================================================================================//
    
    // Start is called before the first frame update
    protected virtual void Start()
    {
        if (!playerController)
            playerController = FindObjectOfType<PlayerController>();

        if (!reaperController)
            reaperController = FindObjectOfType<ReaperController>();
        
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

    public void Init(in TYPE type)
    {
        //------------------------------------------------------------------------------------------------------------//
        
        //Find the renderer, and instance the material
        renderer = GetComponentInChildren<SkinnedMeshRenderer>();
        var shader = renderer.material.shader;
        renderer.material = new Material(shader);

        //------------------------------------------------------------------------------------------------------------//
        
        this.type = type;
        
        //TODO Get the Colors
        SetColor(ColorSets[(int)type]);
    }
    
    public virtual void CaptureSoul()
    {
        if (_captured)
            return;
        
        _captured = true;

        var currentPosition = transform.position;

        reaperController.InvestigatePoint(currentPosition);

        var sound = Instantiate(collectSoundEffectPrefab, currentPosition, Quaternion.identity);
        var effect = Instantiate(particleEffectPrefab, currentPosition, Quaternion.identity);
        
        Destroy(sound, 2f);
        Destroy(effect, 3f);

        playerController.CollectSoul(type);
        Destroy(gameObject);
        
    }


    protected void SetColor(in ColorSet colorSet)
    {
        SetColor(colorSet.mainColor, colorSet.emissiveColor);
    }
    protected void SetColor(in Color mainColor, in Color emissiveColor)
    {
        renderer.material.SetColor(AlbedoColor, mainColor);
        renderer.material.SetColor(EmissionColor, emissiveColor);
    }
}

