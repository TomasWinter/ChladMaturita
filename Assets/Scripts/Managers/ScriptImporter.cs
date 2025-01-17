using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScriptImporter : MonoBehaviour
{
    [SerializeField] GameObject Scripts;
    [SerializeField] GameObject HUD;
    [SerializeField] GameObject MissionOverScreen;
    [SerializeField] GameObject Player;
    [SerializeField] Transform PlayerSpawn;
    private void Awake()
    {
        Instantiate(Scripts,transform);
        Instantiate(HUD);
        Instantiate(MissionOverScreen);
        Instantiate(Player,PlayerSpawn.position,PlayerSpawn.rotation);
    }
}
