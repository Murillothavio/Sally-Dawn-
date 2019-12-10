using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public Camera mainCam;
    public float shakeAmount = 0;
    [Range(0, 1)]
    public float dragShake;
    private float MxAntiFuga = 0.5f, TAntiFuga;

    void Awake()
    {
        if (mainCam == null)
            mainCam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        shakeAmount *= dragShake;
        if (shakeAmount > 0.01)
        {
            Vector3 camPosi = mainCam.transform.position;

            float offsetX = Random.value * shakeAmount * 2 - shakeAmount;
            float offsetY = Random.value * shakeAmount * 2 - shakeAmount;

            camPosi.x += offsetX;
            camPosi.y += offsetY;

            mainCam.transform.position = camPosi;
        }
        TAntiFuga += Time.deltaTime;
        if (TAntiFuga > MxAntiFuga)
        {
            TAntiFuga = 0;
            shakeAmount = 0;
            mainCam.transform.localPosition = Vector3.zero;
        }
    }
}
