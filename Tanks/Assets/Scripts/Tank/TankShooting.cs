using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TankShooting : MonoBehaviour
{
    public int playerNumber = 1;
    public Rigidbody shellRb;
    public Transform fireTransform;
    public Slider aimSlider;
    public AudioSource shootingAudio;
    public AudioClip chargingClip;
    public AudioClip fireClip;
    public float minLaunchForce = 15f;
    public float maxLaunchForce = 30f;
    public float maxChargetTime = 0.75f;

    private string fireButton;
    private float currentLaunchForce;
    private float chargeSpeed;
    private bool fired;

    private void OnEnable()
    {
        currentLaunchForce = minLaunchForce;
        aimSlider.value = minLaunchForce;
    }

    private void Start()
    {
        fireButton = "Fire" + playerNumber;

        chargeSpeed = (maxLaunchForce - minLaunchForce) / maxChargetTime;
    }

    private void Update()
    {
        aimSlider.value = minLaunchForce;

        if (currentLaunchForce >= maxLaunchForce && !fired)
        {
            currentLaunchForce = maxLaunchForce;
            Fire();
        }
        else if (Input.GetButtonDown(fireButton))
        {
            fired = false;
            currentLaunchForce = minLaunchForce;

            shootingAudio.clip = chargingClip;
            shootingAudio.Play();

        }
        else if (Input.GetButton(fireButton) && !fired)
        {
            currentLaunchForce += chargeSpeed * Time.deltaTime;

            aimSlider.value = currentLaunchForce;
        }
        else if (Input.GetButtonUp(fireButton) && !fired)
        {
            Fire();
        }
    }

    private void Fire()
    {
        fired = true;

        Rigidbody shellInstance = Instantiate(shellRb, fireTransform.position, fireTransform.rotation) as Rigidbody;

        shellInstance.velocity = currentLaunchForce * fireTransform.forward;

        shootingAudio.clip = fireClip;
        shootingAudio.Play();

        currentLaunchForce = minLaunchForce;
    }
}
