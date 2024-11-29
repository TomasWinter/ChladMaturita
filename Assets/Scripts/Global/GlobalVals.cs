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

    private void Awake()
    {
        WaypointTypeDictionary = new Dictionary<WaypointType, Sprite>() {
            { WaypointType.Arrow, Resources.Load<Sprite>("Waypoints/Waypoint") }
        };

        Instance = this;
    }
}
