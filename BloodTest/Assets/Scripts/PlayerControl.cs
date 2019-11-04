using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class PlayerControl : MonoBehaviour
{
    bool lockMod = false;
    float inputMoveX, inputMoveY;
    Animator animator;
    Player playerInput;

    int[] x = new int[4];

    public EnemyManager enemyManager;
    public EnemyNode[] nodes;

    public float speed;
    public float rotateAngle;
    public float rotateSpeed;
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
        float inputV = Mathf.Clamp01(inputMoveX * inputMoveX + inputMoveY * inputMoveY);
        if (inputV > 0.15f)
        {
            animator.SetFloat("moveSpeed", inputV);
            Vector3 moveForward = (new Vector3(inputMoveX * mainCamera.transform.right.x, 0, inputMoveX * mainCamera.transform.right.z)
                                              + new Vector3(inputMoveY * mainCamera.transform.forward.x, 0, inputMoveY * mainCamera.transform.forward.z)).normalized;

            float difAngle = Vector3.Angle(transform.forward, moveForward);

            //transform.rotation = Quaternion.LookRotation(moveForward);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(moveForward), Time.deltaTime * rotateSpeed);
            if (difAngle < rotateAngle) transform.position += Time.deltaTime * inputV * speed * transform.forward;


            Debug.Log("angleeeee " + Vector3.SignedAngle(Vector3.forward, moveForward,Vector3.up));
        }
        else animator.SetFloat("moveSpeed", 0);

        if (Input.GetButtonDown("LockTarget")) { 
            
        }

        if (Input.GetKeyDown(KeyCode.Space)) {
            enemyManager.Test(this);
        }
        if (Input.GetKeyDown(KeyCode.Z)) {
            Debug.Log("yeeeeeeeeeeeee  " + nodes[0].weight);
            enemyManager.Test2(nodes);
        }

    }

    private void FixedUpdate()
    {
        //float inputV = Mathf.Clamp01(inputMoveX * inputMoveX + inputMoveY * inputMoveY);

        //if (inputV > 0.15f)
        //{
        //    animator.SetFloat("moveSpeed", inputV);
        //    Vector3 moveForward = (new Vector3(inputMoveX * mainCamera.transform.right.x, 0, inputMoveX * mainCamera.transform.right.z)
        //                                      + new Vector3(inputMoveY * mainCamera.transform.forward.x, 0, inputMoveY * mainCamera.transform.forward.z)).normalized;

        //    float difAngle = Vector3.Angle(transform.forward, moveForward);

        //    //transform.rotation = Quaternion.LookRotation(moveForward);
        //    transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(moveForward), Time.deltaTime * rotateSpeed);
        //    if (difAngle < rotateAngle) transform.position += Time.deltaTime * inputV * speed * transform.forward;
        //}
        //else animator.SetFloat("moveSpeed", 0);




    }



}
