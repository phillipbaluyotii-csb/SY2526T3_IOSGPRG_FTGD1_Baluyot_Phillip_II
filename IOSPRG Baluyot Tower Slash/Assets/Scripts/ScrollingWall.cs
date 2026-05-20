using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingWall : MonoBehaviour
{
    public float speed;

    [SerializeField] private Renderer wallRenderer;

    private void Update()
    {
        wallRenderer.material.mainTextureOffset += new Vector2(0, speed * Time.deltaTime);
    }
}
