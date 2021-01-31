/*
 * author : jiankaiwang
 * description : The script provides you with basic operations of first personal control.
 * platform : Unity
 * date : 2017/12
 */
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerController : MonoBehaviour
{
    private int[] _collected;

    public bool FlashLightOn { get; private set; }
    public TMP_Text soulCount;

    public float speed = 10.0f;
    private float translation;
    private float straffe;

    [SerializeField, Header("Flash Light")]
    private GameObject lightSourceObject;

    private Light spotLight;

    [SerializeField] private bool setFlashLightOnStart;

    private BoxCollider Caputure;

    //====================================================================================================================//

    // Use this for initialization
    private void Start()
    {
        // turn off the cursor
        Cursor.lockState = CursorLockMode.Locked;
        spotLight = lightSourceObject.GetComponentInChildren<Light>();
        Caputure = lightSourceObject.GetComponent<BoxCollider>();
        SetLightState(setFlashLightOnStart);
        Caputure.enabled = false;

        _collected = new int[3];

        UpdateUI();
    }

    // Update is called once per frame
    private void Update()
    {
        // Input.GetAxis() is used to get the user's input
        // You can further set it on Unity. (Edit, Project Settings, Input)
        translation = Input.GetAxis("Vertical") * speed * Time.deltaTime;
        straffe = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        transform.Translate(straffe, 0, translation);

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // turn on the cursor
            Cursor.lockState = CursorLockMode.None;
        }

        if (Input.GetKey(KeyCode.Mouse1))
        {
            spotLight.color = Color.red;
            Caputure.enabled = true;
        }
        else
        {
            spotLight.color = Color.white;
            Caputure.enabled = false;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            SetLightState(!FlashLightOn);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Portal"))
        {
            SceneManager.LoadScene("LobbyScene");
        }
    }

    //====================================================================================================================//
    public int GetTypeCount(in SoulBehavior.TYPE type)
    {
        return _collected[(int) type];
    }

    public void CollectSoul(in SoulBehavior.TYPE type)
    {
        _collected[(int) type]++;

        UpdateUI();
    }

    private void SetLightState(bool state)
    {
        lightSourceObject.SetActive(state);
        FlashLightOn = state;
    }

    private void UpdateUI()
    {
        var sum = _collected.Sum();
        soulCount.text = $"{sum}";
    }

    //====================================================================================================================//

}
