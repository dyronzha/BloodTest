using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    float lessAngle = 500.0f;
    EnemyNode targetLockEnemy;

    List<EnemyBase> freeEnemy, usedEnemy;
    Heap<EnemyNode> targetHeap;

    Transform player;

    // Start is called before the first frame update
    private void Awake()
    {
        for (int i = 0; i < transform.childCount; i++) {
            Transform enemyT = transform.GetChild(i);
            EnemyBase enemy = new EnemyBase();
            enemy.Init(enemyT);
            usedEnemy = new List<EnemyBase>();
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

    public bool ChooseEnemyToLockTarget() {
        bool findOne = false;
        EnemyNode node;
        targetHeap = new Heap<EnemyNode>(20);

        for (int i = 0; i < usedEnemy.Count; i++) {
            if (i == 20) break;
            Vector3 diffV = new Vector3(usedEnemy[i].transform.position.x - player.position.x, 0, usedEnemy[i].transform.position.z - player.position.z);
            float dist = diffV.sqrMagnitude;
            if (dist < 100.0f) {

                float angle = Vector3.Angle(player.forward, diffV);

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


    public void Test(PlayerControl p) {
        EnemyNode e1 = new EnemyNode(20);
        EnemyNode e2 = new EnemyNode(-100);
        EnemyNode e3 = new EnemyNode(50);
        EnemyNode[] ns = new EnemyNode[3] { e1, e2, e3 };
        p.nodes = ns;
    }
    public void Test2(EnemyNode[] ns) {
        EnemyNode[] n = ns;
        n[0].weight = -1000;
    }
}
