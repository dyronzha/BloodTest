using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    
    EnemyNode targetLockEnemy;

    EnemyBase lockTarget;
    public EnemyBase LockTarget {
        get { return lockTarget; }
    }
    List<EnemyBase> freeEnemy, usedEnemy;
    Heap<EnemyNode> targetHeap;

    PlayerControl player;
    Transform playerTransform;

    // Start is called before the first frame update
    private void Awake()
    {
        usedEnemy = new List<EnemyBase>();
        for (int i = 0; i < transform.childCount; i++) {
            Transform enemyT = transform.GetChild(i);
            EnemyBase enemy = new EnemyBase();
            enemy.Init(enemyT);
            usedEnemy.Add(enemy);
        }

    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetPlayer(PlayerControl p) {
        player = p;
        playerTransform = p.transform;
    }

    public bool ChooseEnemyToLockTarget() {
        bool findOne = false;
        float weight = .0f;
        float lessAngle = 500.0f;
        int count = 0;
        for (int i = 0; i < usedEnemy.Count; i++) {
            if (count >= 20) break;
            Vector3 diffV = new Vector3(usedEnemy[i].transform.position.x - playerTransform.position.x, 0, usedEnemy[i].transform.position.z - playerTransform.position.z);
            float dist = diffV.sqrMagnitude;
            float angle = Vector3.SignedAngle(playerTransform.forward, diffV, Vector3.up);
            if (dist < 100.0f && Mathf.Abs(angle) < 180.0f) {
                if (Mathf.Abs(angle) > 0.5f) weight = angle + Mathf.Sign(angle) * dist * 0.1f;
                else weight = angle + dist * 0.05f;
                Debug.Log(usedEnemy[i].transform.name + "  angle:" + angle + "  dist:" + dist*0.1f);
                if (Mathf.Abs(lessAngle) > Mathf.Abs(weight)) {
                    lockTarget = usedEnemy[i];
                    lessAngle = weight;
                    findOne = true;
                }
                count++;
            }
        }
        return findOne;
    }

    public bool SwitchEnemyLockTarget(float dir) {
        bool findOne = false;
        float weight = .0f;
        float lessAngle = 500.0f;
        int count = 0;
        for (int i = 0; i < usedEnemy.Count; i++)
        {
            if (count >= 20) break;
            Vector3 diffV = new Vector3(usedEnemy[i].transform.position.x - playerTransform.position.x, 0, usedEnemy[i].transform.position.z - playerTransform.position.z);
            float dist = diffV.sqrMagnitude;
            float angle = Vector3.SignedAngle(playerTransform.forward, diffV, Vector3.up);
            if (dist < 100.0f && (Mathf.Abs(angle) > 0.5f && dir*Mathf.Sign(angle) >= .0f) && Mathf.Abs(angle) < 180.0f)
            {
                weight = angle + Mathf.Sign(angle) * dist * 0.1f;
                Debug.Log(usedEnemy[i].transform.name + "  angle:" + angle + "  dist:" + dist*0.1);
                if (Mathf.Abs(lessAngle) > Mathf.Abs(weight))
                {
                    lockTarget = usedEnemy[i];
                    lessAngle = weight;
                    findOne = true;
                }
                count++;
            }
        }
        return findOne;
    }

    public bool ChooseEnemyToLockTargetHeap() {
        bool findOne = false;
        float lessAngle = 500.0f;
        EnemyNode node;
        targetHeap = new Heap<EnemyNode>(20);

        for (int i = 0; i < usedEnemy.Count; i++) {
            if (i == 20) break;
            Vector3 diffV = new Vector3(usedEnemy[i].transform.position.x - playerTransform.position.x, 0, usedEnemy[i].transform.position.z - playerTransform.position.z);
            float dist = diffV.sqrMagnitude;
            if (dist < 100.0f) {

                float angle = Vector3.Angle(playerTransform.forward, diffV);

                if (Mathf.Abs(angle) > 0.1f) node = new EnemyNode(usedEnemy[i], angle + Mathf.Sign(angle) * dist * 0.05f);
                else node = new EnemyNode(usedEnemy[i], angle + dist * 0.05f);

                if (Mathf.Abs(lessAngle) > Mathf.Abs(node.weight)) {
                    lessAngle = angle;
                    targetLockEnemy = node;
                    findOne = true;
                }
                targetHeap.Add(targetLockEnemy);
            }
        }

        return findOne;
    }


}
