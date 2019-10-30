using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class PlayerControl : MonoBehaviour
{
    float inputMoveX, inputMoveY;
    Animator animator;
    Player playerInput;

    int[] x = new int[4];

    public float speed;
    public Camera mainCamera;

    // Start is called before the first frame update
    private void Awake()
    {
        animator = GetComponent<Animator>();
        playerInput = ReInput.players.GetPlayer(0);
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        inputMoveX = Input.GetAxis("Move Horizontal");
        inputMoveY = Input.GetAxis("Move Vertical");
        Debug.Log(inputMoveX + " , " + inputMoveY);


        float inputV = inputMoveX*inputMoveX + inputMoveY* inputMoveY;
        
        if (inputV > 0.15f)
        {
            animator.SetFloat("moveSpeed", inputV);
            Vector3 moveForward = new Vector3(inputMoveX * mainCamera.transform.right.x, 0, inputMoveX * mainCamera.transform.right.z)
                                              + new Vector3(inputMoveY * mainCamera.transform.forward.x, 0, inputMoveY * mainCamera.transform.forward.z);
            transform.rotation = Quaternion.LookRotation(moveForward);
            //transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(moveForward), 0.8f);
            transform.position += Time.deltaTime * inputV * speed * transform.forward;
        }
        else animator.SetFloat("moveSpeed", 0);


    }

    private void FixedUpdate()
    {
        //float inputV = Mathf.Abs(inputMoveX) + Mathf.Abs(inputMoveY);
        //animator.SetFloat("moveSpeed", inputV);
        //if (inputV > 0.2f) {

        //    Vector3 moveForward = new Vector3(inputMoveX * mainCamera.transform.right.x, 0, inputMoveX * mainCamera.transform.right.z)
        //                                        + new Vector3(inputMoveY * mainCamera.transform.forward.x, 0, inputMoveY * mainCamera.transform.forward.z);

        //    transform.position += inputV * speed * moveForward;
        //    transform.rotation = Quaternion.LookRotation(moveForward);
        //    //transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(moveForward), 0.3f);
        //    //transform.forward = Vector3.Lerp(transform.forward, moveForward, 0.7f);
        //}




    }



}
