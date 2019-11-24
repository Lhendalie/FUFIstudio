using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Image_Loader : MonoBehaviour
{
    public string url = "https://tinyjpg.com/images/social/website.jpg";
    public Renderer thisRenderer;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LoadFromLikeCorouting());
        thisRenderer.material.color = Color.red;
    }

    private IEnumerator LoadFromLikeCorouting()
    {
        Debug.Log("Loading...");
        WWW wwwLoader = new WWW(url);
        yield return wwwLoader;

        Debug.Log("Loaded");
        thisRenderer.material.color = Color.white;
        thisRenderer.material.mainTexture = wwwLoader.texture;
    }
}
