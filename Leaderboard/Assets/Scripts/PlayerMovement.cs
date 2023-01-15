using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private Transform leftPos,rightPos,defPos;

    private bool isPlayerOnLeft;
    private bool isPlayerOnRight;
    private float lerpTime;

    private Animator playerAnim;

    private void Start()
    {
        lerpTime = 2f;
        isPlayerOnLeft = false;
        isPlayerOnRight = false;
        playerAnim = GetComponent<Animator>();
        playerAnim.enabled = false;

    }
    private void Update()
    {
        if(GameManager.Instance.isGameOn)
        {
            playerAnim.enabled = true;
            if (Input.GetKeyDown(KeyCode.A))
            {
                if (!isPlayerOnLeft && isPlayerOnRight)
                {
                    transform.position = Vector3.Lerp(transform.position, defPos.position, lerpTime);
                    isPlayerOnRight = false;
                    isPlayerOnLeft = false;
                }
                else
                {
                    transform.position = Vector3.Lerp(transform.position, leftPos.position, lerpTime);
                    isPlayerOnLeft = true;
                    isPlayerOnRight = false;
                }

            }

            else if (Input.GetKeyDown(KeyCode.D))
            {
                if (!isPlayerOnRight && isPlayerOnLeft)
                {
                    transform.position = Vector3.Lerp(transform.position, defPos.position, lerpTime);
                    isPlayerOnRight = false;
                    isPlayerOnLeft = false;
                }
                else
                {
                    transform.position = Vector3.Lerp(transform.position, rightPos.position, lerpTime);
                    isPlayerOnRight = true;
                    isPlayerOnLeft = false;
                }
            }
        }
        else
        {
            playerAnim.enabled = false;
        }
        
    }
}
