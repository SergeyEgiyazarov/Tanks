using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankMovement : MonoBehaviour
{
    public int playerNumber = 1;
    public float speed = 12f;
    public float turnSpeed = 180f;
    public AudioSource movementAudio;
    public AudioClip engineIdling;
    public AudioClip engineDriving;
    public float pitchRange = 0.2f;

    private string movementAxisName;
    private string turnAxisName;
    private Rigidbody tankRigidbody;
    private float movementInputValue;
    private float turnInputValue;
    private float originalPitch;

    private void Awake()
    {
        tankRigidbody = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        tankRigidbody.isKinematic = false;
        movementInputValue = 0f;
        turnInputValue = 0f;
    }

    private void OnDisable()
    {
        tankRigidbody.isKinematic = true;
    }

    private void Start()
    {
        movementAxisName = "Vertical" + playerNumber;
        turnAxisName = "Horizontal" + playerNumber;

        originalPitch = movementAudio.pitch;
    }

    private void Update()
    {
        movementInputValue = Input.GetAxis(movementAxisName);
        turnInputValue = Input.GetAxis(turnAxisName);

        EngineAudio();
    }

    private void EngineAudio()
    {
        if (Mathf.Abs(movementInputValue) < 0.1f && Mathf.Abs(turnInputValue) < 0.1f)
        {
            if (movementAudio.clip == engineDriving)
            {
                movementAudio.clip = engineIdling;
                movementAudio.pitch = Random.Range(originalPitch - pitchRange, originalPitch + pitchRange);
                movementAudio.Play();
            }
        }
        else
        {
            if (movementAudio.clip == engineIdling)
            {
                movementAudio.clip = engineDriving;
                movementAudio.pitch = Random.Range(originalPitch - pitchRange, originalPitch + pitchRange);
                movementAudio.Play();
            }
        }
    }

    private void FixedUpdate()
    {
        Move();
        Turn();
    }

    private void Move()
    {
        Vector3 movement = transform.forward * movementInputValue * speed * Time.deltaTime;

        tankRigidbody.MovePosition(tankRigidbody.position + movement);
    }

    private void Turn()
    {
        float turn = turnInputValue * turnSpeed * Time.deltaTime;

        Quaternion turnRotation = Quaternion.Euler(0.0f, turn, 0.0f);

        tankRigidbody.MoveRotation(tankRigidbody.rotation * turnRotation);
    }
}
