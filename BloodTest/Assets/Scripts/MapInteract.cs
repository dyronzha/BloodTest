using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapInteract : MonoBehaviour
{
    [HideInInspector]
    public Drink[] Drinks = new Drink[0];

    [System.Serializable]
    public struct Drink
    {
        public string Name;
        public float Price;
        public Color Color;
        public int aaa;
    }

    [HideInInspector]
    public VCameraPoint[] VCameraPoints = new VCameraPoint[0];

    [System.Serializable]
    public struct VCameraPoint {
        public Collider colliderPoint;
        public GameObject vCamera;
    }


    [HideInInspector]
    public interactPoint[] interactPoints = new interactPoint[0];

    [System.Serializable]
    public struct interactPoint
    {
        public Collider colliderPoint;
        public InteractType interactType;
        public UnityEngine.Playables.PlayableDirector timeline;
        public float textShowTime;
        public string infoText;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}

public enum InteractType
{
    CameraMove, InfoText, MoveAndText
}
