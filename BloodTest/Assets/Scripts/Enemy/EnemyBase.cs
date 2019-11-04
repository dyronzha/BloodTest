using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase
{

    Transform _transform;
    public Transform transform {
        get { return _transform; }
    }

    // Start is called before the first frame update
    public virtual void Init(Transform t)
    {
        _transform = t;

    }

    // Update is called once per frame
    public virtual void Update()
    {
        
    }
}
