using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Runtime.InteropServices;
using UnityEngine.UI;

public class Test : MonoBehaviour
{
    [DllImport("OpenCDl_Setup")]
    private static extern void FlipImage(ref Color32[] rawImage, int width, int height);
    
    // Start is called before the first frame update
    void Start()
    {
        Color32[] image = GetComponent<Image>().sprite.texture.GetPixels32();
        FlipImage(ref image, 500, 500);
        GetComponent<Image>().sprite.texture.SetPixels32(image);
        GetComponent<Image>().sprite.texture.Apply();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
