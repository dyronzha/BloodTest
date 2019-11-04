using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyNode : IHeapItem<EnemyNode>
{
    public int listID;
    public float weight;
    EnemyBase enemy;
    


    public bool walkable;
    public Vector3 worldPosition;
    public int gridX;
    public int gridY;
    public int movementPenalty;
    public int extentPenalty = 0;

    public int gCost;
    public int hCost;
    public EnemyNode parent;
    int heapIndex;

    public EnemyNode(float _weight)
    {
        weight = _weight;
    }
    public EnemyNode(EnemyBase _enemy, float _weight)
    {
        enemy = _enemy;
        weight = _weight;
    }

    public EnemyNode(bool _walkable, Vector3 _worldPos, int _gridX, int _gridY, int _penalty)
    {
        walkable = _walkable;
        worldPosition = _worldPos;
        gridX = _gridX;
        gridY = _gridY;
        movementPenalty = _penalty;
    }

    public int fCost
    {
        get
        {
            return gCost + hCost;
        }
    }

    public int HeapIndex
    {
        get
        {
            return heapIndex;
        }
        set
        {
            heapIndex = value;
        }
    }

    public int CompareTo(EnemyNode nodeToCompare)
    {
        int compare = fCost.CompareTo(nodeToCompare.fCost);
        if (compare == 0)
        {
            compare = hCost.CompareTo(nodeToCompare.hCost);
        }
        return -compare;
    }

    public void AddPenalty(int value) {

        if (extentPenalty + value + movementPenalty <= 90) {
            extentPenalty += value;
        }
    }

}
