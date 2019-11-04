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

    EnemyManager enemyManager;

    public float speed;
    public float rotateAngle;
    public float rotateSpeed;
    public Camera mainCamera;

    // Start is called before the first frame update
    private void Awake()
    {
        animator = GetComponent<Animator>();
        //playerInput = ReInput.players.GetPlayer(0);
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("LockTarget")) {
            if (!lockMod) lockMod = enemyManager.ChooseEnemyToLockTarget();
            else lockMod = false;
        }
        else if (lockMod) {
            if (Input.GetButtonDown("LeftTarget")) enemyManager.SwitchEnemyLockTarget(-1.0f);
            else if (Input.GetButtonDown("RightTarget")) enemyManager.SwitchEnemyLockTarget(1.0f);
        }
        Move();
        
        if(Input.GetKey(KeyCode.Space)){
            GetComponent<Rigidbody>().useGravity = false;
            transform.position += 15.0f*new Vector3(0,1,0)*Time.deltaTime;
        }
        else GetComponent<Rigidbody>().useGravity = true;
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

    public void SetEnemyManager(EnemyManager m) {
        enemyManager = m;
    }

    private void Move()
    {
        inputMoveX = Input.GetAxis("Move Horizontal");
        inputMoveY = Input.GetAxis("Move Vertical");
        Vector3 baseFWD, baseRight;
        if (!lockMod)
        {
            baseFWD = mainCamera.transform.forward;
            baseRight = mainCamera.transform.right;
        }
        else {
            baseFWD = new Vector3(enemyManager.LockTarget.transform.position.x - transform.position.x,0, enemyManager.LockTarget.transform.position.z - transform.position.z).normalized;
            baseRight = new Vector3(baseFWD.z, 0, -baseFWD.x);
        }

        float inputV = Mathf.Clamp01(inputMoveX * inputMoveX + inputMoveY * inputMoveY);
        if (inputV > 0.15f)
        {
            animator.SetFloat("moveSpeed", inputV);
            Vector3 moveForward = (new Vector3(inputMoveX * baseRight.x, 0, inputMoveX * baseRight.z)
                                              + new Vector3(inputMoveY * baseFWD.x, 0, inputMoveY * baseFWD.z)).normalized;

            float difAngle = Vector3.Angle(transform.forward, moveForward);

            //transform.rotation = Quaternion.LookRotation(moveForward);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(moveForward), Time.deltaTime * rotateSpeed);
            if (difAngle < rotateAngle) transform.position += Time.deltaTime * inputV * speed * transform.forward;


        }
        else {
            animator.SetFloat("moveSpeed", 0);
            if (lockMod) transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(baseFWD), Time.deltaTime * rotateSpeed);
        } 
    }

}
