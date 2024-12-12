using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static BallerAnimator;

public class Shooting : MonoBehaviour

{

    public float shootingDuration = 1.0f; // Duration of the rotation

    public Transform shootingTarget;

    public float layupRange = 10.0f;

    private float totalElapsedTime = 0f;

    private Animator animator;
    public enum ShotType
    {
        Jumpshot,
        Layup
    }

    internal ShotType shotType;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad7) && !PlayerBehavior.shooting && Vector3.Distance(transform.position, shootingTarget.position) > layupRange && PlayerBehavior.onBall)
        {
            PlayerBehavior.dribbling = false;
            PlayerBehavior.shooting = true;
            shotType = ShotType.Jumpshot;
            animator.ResetTrigger("stopShoot");
            animator.SetTrigger("shoot");
            transform.LookAt(shootingTarget);
            transform.Rotate(15, 0, 0);

        }
        else if (Input.GetKeyUp(KeyCode.Keypad7))
        {
            PlayerBehavior.shooting = false;
            animator.ResetTrigger("shoot");
            animator.SetTrigger("stopShoot");
            if (totalElapsedTime > shootingDuration)
            {
                Release();
                PlayerBehavior.onBall = false;
            }
            else
            {
                //Fake();
                animator.SetTrigger("fake");
                PlayerBehavior.onBall = true;
                Ball.rb.useGravity = false;
            }
            totalElapsedTime = 0f;
        }
        if (Input.GetKeyDown(KeyCode.Keypad9) && !PlayerBehavior.shooting && Vector3.Distance(transform.position, shootingTarget.position) <= layupRange) //Layup
        {
            PlayerBehavior.dribbling = false;
            PlayerBehavior.shooting = true;
            shotType = ShotType.Layup;
            animator.ResetTrigger("stopShoot");
            animator.SetTrigger("rightLayup");

        }
        

        if (PlayerBehavior.shooting)
        {
            totalElapsedTime += Time.deltaTime;
            if (totalElapsedTime > shootingDuration * 1.2 && shotType == ShotType.Layup || (totalElapsedTime > shootingDuration * 2))
            {
                Release();
                PlayerBehavior.shooting = false;
                PlayerBehavior.onBall = false;
                totalElapsedTime = 0f;
                animator.ResetTrigger("shoot");
                animator.SetTrigger("stopShoot");
            }
        }
    }

    private void Release()
    {
        GameObject ball = GameObject.Find("Ball1");
        if (ball != null)
        {
            BallAirMovement ballParabola = ball.GetComponent<BallAirMovement>();
            if (ballParabola == null)
            {
                Debug.LogWarning("Target script not found on the target object.");
                return;
            }
            if (shotType == ShotType.Jumpshot)
            {
                ballParabola.Jumpshot();
            }
            else if (shotType == ShotType.Layup)
            {
                ballParabola.Layup(); 
            }
            Ball.rb.useGravity = true;
        }
        else
        {
            Debug.LogWarning("Ball not found.");
        }

    }


}
