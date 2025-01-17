using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassScript : BillboardingScript
{
    [SerializeField] List<Sprite> Sprites = new();
    private void Awake()
    {
        transform.localScale = transform.localScale + (Random.Range(-0.5f, 0.5f) * Vector3.one);
        GetComponent<SpriteRenderer>().sprite = Sprites[Random.Range(0,Sprites.Count)];
    }
}
