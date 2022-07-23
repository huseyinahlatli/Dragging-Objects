using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private FloatingJoystick floatingJoystick;
    [SerializeField] private float speed;
    [SerializeField] private float turnSpeed;
    [SerializeField] private Animator animator;
    [SerializeField] private ParticleSystem particleSystem;
    private float vertical, horizontal;
    
    [Header ("Animator States")]
    [HideInInspector] public bool idleState = true;
    [HideInInspector] public bool runState = false;
    [HideInInspector] public bool pushState = false;

    #region Singleton Class: PlayerMovement

    public static PlayerMovement Instance;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    #endregion

    private void Start()
    {
        particleSystem = GetComponentInChildren<ParticleSystem>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            idleState = false;
            if (!pushState)
                runState = true;

            JoystickMove();
        }

        else
        {
            idleState = true;
            runState = false;
            pushState = false;
        }
       
        AnimationStates();
    }

    public void JoystickMove()
    {
        horizontal = floatingJoystick.Horizontal;
        vertical = floatingJoystick.Vertical;

        Vector3 addedPosition = new Vector3(
            horizontal * speed * Time.deltaTime,
            0,
            vertical * speed * Time.deltaTime
        );

        var newTransform = transform;
        newTransform.position += addedPosition;

        Vector3 direction = (Vector3.forward * vertical) + (Vector3.right * horizontal);
        transform.rotation = Quaternion.Slerp(
            newTransform.rotation,
            Quaternion.LookRotation(direction),
            turnSpeed * Time.deltaTime
        );
    }

    private void AnimationStates()
    {
        if (idleState)
        {
            animator.SetBool("Idle", true);
            animator.SetBool("Run", false);
            animator.SetBool("Push", false);
            particleSystem.Stop();
        }
        
        else if (runState)
        {
            animator.SetBool("Idle", false);
            animator.SetBool("Run", true);
            animator.SetBool("Push", false);
            particleSystem.Play();
        }
        
        else if (pushState)
        {
            animator.SetBool("Idle", false);
            animator.SetBool("Run", false);
            animator.SetBool("Push", true);
            particleSystem.Play();
        }
    }
}
