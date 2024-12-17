using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GlobalVals : MonoBehaviour
{
    public static GlobalVals Instance { get; private set; }

    public static Dictionary<WaypointType, Sprite> WaypointTypeDictionary;

    public GameObject Bullet;
    public GameObject BulletParent;
    public GameObject Grave;
    public GameObject WaypointPrefab;
    public GameObject TextBillboardPrefab;
    [HideInInspector]public int RaycastLayermask;

    [Header("Splats")]
    [SerializeField] Sprite[] Splats;
    [SerializeField] GameObject SplatPrefab;

    private void Awake()
    {
        WaypointTypeDictionary = new Dictionary<WaypointType, Sprite>() {
            { WaypointType.Arrow, Resources.Load<Sprite>("Waypoints/Waypoint") }
        };

        Instance = this;

        RaycastLayermask = ~LayerMask.GetMask("Ignore Raycast", "Water", "UI", "Bullets", "PlayerHands", "Invisible");
    }

    public GameObject Splat { 
        get {
            GameObject x = Instantiate(SplatPrefab);
            x.GetComponent<SpriteRenderer>().sprite = Splats[Random.Range(0, Splats.Length - 1)];
            return x; 
        } }
}
