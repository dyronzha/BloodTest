using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class PlayerControl : MonoBehaviour
{
    bool lockMod = false;
    float inputMoveX, inputMoveY, lastSpeed, curSpeed;
    float lastLockAngle;
    Animator animator;
    Player playerInput;

    int[] x = new int[4];

    EnemyManager enemyManager;


    Vector3 cameraOffset;
    Cinemachine.CinemachineOrbitalTransposer orbital;

    PlayerState playerState = PlayerState.Move;
    enum PlayerState { 
        Move, Attack
    }

    public float speed;
    public float rotateAngle;
    public float rotateSpeed;
    public Camera mainCamera;

    public Cinemachine.CinemachineVirtualCamera vcam;
    public Cinemachine.CinemachineVirtualCamera vcam2;
    public UnityEngine.Playables.PlayableDirector timeline;

    // Start is called before the first frame update
    private void Awake()
    {
        animator = GetComponent<Animator>();
        //playerInput = ReInput.players.GetPlayer(0);

        orbital = vcam.GetCinemachineComponent<Cinemachine.CinemachineOrbitalTransposer>();
        cameraOffset = mainCamera.transform.position - transform.position;
        cameraOffset = new Vector3(cameraOffset.x,0,cameraOffset.z);
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (playerState == PlayerState.Move)
        {
            if (Input.GetButtonDown("LockTarget"))
            {
                if (!lockMod) lockMod = enemyManager.ChooseEnemyToLockTarget();
                else lockMod = false;
            }
            else if (lockMod)
            {
                if (Input.GetButtonDown("LeftTarget")) enemyManager.SwitchEnemyLockTarget(-1.0f);
                else if (Input.GetButtonDown("RightTarget")) enemyManager.SwitchEnemyLockTarget(1.0f);
            }
            Move();
            if(Input.GetKeyDown(KeyCode.Space)){
                animator.Play("Attack");
                playerState = PlayerState.Attack;
            } 
        }
        else if (playerState == PlayerState.Attack) {
            //transform.position += speed * animator.deltaPosition.normalized;
            Debug.Log("attack move  " + animator.deltaPosition.normalized);
            AnimatorStateInfo aniInfo = animator.GetCurrentAnimatorStateInfo(0);
            if (aniInfo.normalizedTime > 0.95f) {
                playerState = PlayerState.Move;
            }
        }


        //if (transform.position.x < -3.5f)
        //{
        //    vcam.gameObject.SetActive(false);
        //    vcam2.gameObject.SetActive(true);
        //}
        //else {
        //    vcam.gameObject.SetActive(true);
        //    vcam2.gameObject.SetActive(false);
        //}
        //if (Input.GetKeyDown(KeyCode.Z)) {
        //    timeline.Play();
        //}
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
        if (Input.GetKey(KeyCode.A)) inputMoveX = -1.0f;
        else if (Input.GetKey(KeyCode.D)) inputMoveX = 1.0f;
        if (Input.GetKey(KeyCode.W)) inputMoveY = 1.0f;
        else if (Input.GetKey(KeyCode.S)) inputMoveY = -1.0f;
        Vector3 baseFWD, baseRight;

        float inputV = Mathf.Clamp01(inputMoveX * inputMoveX + inputMoveY * inputMoveY);

        baseFWD = mainCamera.transform.forward;
        baseRight = mainCamera.transform.right;
        if (inputV > 0.15f)
        {
            if (curSpeed < 1.0f) curSpeed += Time.deltaTime*4.0f;
            else curSpeed = 1.0f;

            animator.SetFloat("moveSpeed", curSpeed);
            Vector3 moveForward = (new Vector3(inputMoveX * baseRight.x, 0, inputMoveX * baseRight.z)
                                              + new Vector3(inputMoveY * baseFWD.x, 0, inputMoveY * baseFWD.z)).normalized;

            float difAngle = Vector3.Angle(transform.forward, moveForward);

            //transform.rotation = Quaternion.LookRotation(moveForward);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(moveForward), Time.deltaTime * rotateSpeed);
            if (difAngle < rotateAngle) transform.position += Time.deltaTime * curSpeed * speed * transform.forward;

            Debug.Log("last speeeeeeeed" + lastSpeed);

        }
        else
        {
            if (curSpeed > .0f) curSpeed -= Time.deltaTime * 5.0f;
            else curSpeed = .0f;
            animator.SetFloat("moveSpeed", curSpeed);
            if (lockMod) {
                baseFWD = new Vector3(enemyManager.LockTarget.transform.position.x - transform.position.x, 0, enemyManager.LockTarget.transform.position.z - transform.position.z).normalized;
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(baseFWD), Time.deltaTime * rotateSpeed * 0.45f);
            } 
        }
        #region 鎖定繞圈備份
        //if (!lockMod)
        //{
        //    baseFWD = mainCamera.transform.forward;
        //    baseRight = mainCamera.transform.right;
        //    if (inputV > 0.15f)
        //    {
        //        animator.SetFloat("moveSpeed", inputV);
        //        Vector3 moveForward = (new Vector3(inputMoveX * baseRight.x, 0, inputMoveX * baseRight.z)
        //                                          + new Vector3(inputMoveY * baseFWD.x, 0, inputMoveY * baseFWD.z)).normalized;

        //        float difAngle = Vector3.Angle(transform.forward, moveForward);

        //        //transform.rotation = Quaternion.LookRotation(moveForward);
        //        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(moveForward), Time.deltaTime * rotateSpeed);
        //        if (difAngle < rotateAngle) transform.position += Time.deltaTime * inputV * speed * transform.forward;

        //        //mainCamera.transform.position = transform.position + cameraOffset;
        //        lastSpeed = inputV * speed;
        //        Debug.Log("last speeeeeeeed" + lastSpeed);

        //    }
        //    else
        //    {
        //        lastSpeed = Mathf.Lerp(lastSpeed, 0, Time.deltaTime * 20.0f);
        //        animator.SetFloat("moveSpeed", lastSpeed);
        //    }
        //    //float a = Vector3.SignedAngle(new Vector3(vcam.transform.forward.x, 0, vcam.transform.forward.z), baseFWD, Vector3.up);
        //    //if (Mathf.Abs(a) > 0.1f) orbital.m_XAxis.Value = Mathf.Lerp(orbital.m_XAxis.Value, orbital.m_XAxis.Value + a, Time.deltaTime * 5.0f);
        //}
        //else {
        //    baseFWD = new Vector3(enemyManager.LockTarget.transform.position.x - transform.position.x,0, enemyManager.LockTarget.transform.position.z - transform.position.z).normalized;
        //    baseRight = new Vector3(baseFWD.z, 0, -baseFWD.x);
        //    if (inputV > 0.15f)
        //    {
        //        animator.SetFloat("moveSpeed", inputV);
        //        Vector3 moveForward = (new Vector3(inputMoveX * baseRight.x, 0, inputMoveX * baseRight.z)
        //                                          + new Vector3(inputMoveY * baseFWD.x, 0, inputMoveY * baseFWD.z)).normalized;

        //        float difAngle = Vector3.Angle(transform.forward, moveForward);

        //        //transform.rotation = Quaternion.LookRotation(moveForward);
        //        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(moveForward), Time.deltaTime * rotateSpeed);
        //        if (difAngle < rotateAngle) transform.position += Time.deltaTime * inputV * speed * transform.forward;

        //        //mainCamera.transform.position = transform.position + cameraOffset;
        //        lastSpeed = inputV * speed;

        //    }
        //    else
        //    {
        //        lastSpeed = Mathf.Lerp(lastSpeed, 0, Time.deltaTime * 10.0f);
        //        animator.SetFloat("moveSpeed", lastSpeed);
        //        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(baseFWD), Time.deltaTime * rotateSpeed);
        //    }

        //    //float a = Vector3.SignedAngle(new Vector3(vcam.transform.forward.x,0, vcam.transform.forward.z), baseFWD, Vector3.up);
        //    //if(Mathf.Abs(a) > 0.1f)orbital.m_XAxis.Value = Mathf.Lerp(orbital.m_XAxis.Value, orbital.m_XAxis.Value + a, Time.deltaTime*1.0f);
        //}
        #endregion  

    }

}
