using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroll : MonoBehaviour
{
    public float scrollSpeed;

    private MeshRenderer meshRender;

    // Start is called before the first frame update
    void Start()
    {
        meshRender = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        // Vertically scrolls the main texture on the object
        Vector2 offset = new Vector2(0, Time.time * scrollSpeed);
        meshRender.sharedMaterial.SetTextureOffset("_MainTex", offset);
    }
}
