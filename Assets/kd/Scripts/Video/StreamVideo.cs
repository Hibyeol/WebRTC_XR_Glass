using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using OpenCVForUnity.CoreModule;
using OpenCVForUnity.ImgprocModule;
using OpenCVForUnity.ObjdetectModule;
using OpenCVForUnity.UnityUtils;
using UnityEngine.UI;

using System.Runtime.InteropServices;

public class StreamVideo : MonoBehaviour
{

    //캠 정보 보내기로 수정
    [DllImport("OpenCVDll")]
    private static extern void FlipImage(ref Color32[] rawImage, int widthm, int heigth);
    
    /*    Mat rgbaMat;
        Color32[] colors;
        webCamTexture.Play(); 
        if (webCamTexture.didUpdateThisFrame) {
        rgbaMat = new Mat(webCamTexture.height, webCamTexture.width, CvType.CV_8UC4);
        colors = new Color32[webCamTexture.width * webCamTexture.height];
        Utils.webCamTextureToMat(webCamTexture, rgbaMat, colors);*/

    public int frame;

    Color32[] data;

    private WebCamTexture webcamTexture;
    private string filename;
    private CascadeClassifier cascade;
    private MatOfRect faces;
    private Texture2D texture;
    private Mat rgbaMat;
    private Mat grayMat;
    void initializeData()
    {
        //rotate RawImage according to rotation of webcamtexture
        this.GetComponent<RectTransform>().Rotate(new Vector3(0, 0, 360 - webcamTexture.videoRotationAngle));
        //store name of xml file
        filename = "haarcascade_frontalface_default.xml";
        //initaliaze cascade classifier
        cascade = new CascadeClassifier();
        //load the xml file data
        cascade.load(Utils.getFilePath(filename));
        //initalize faces matofrect
        faces = new MatOfRect();
        //initialize rgb and gray Mats
        rgbaMat = new Mat(webcamTexture.height, webcamTexture.width, CvType.CV_8UC4);
        grayMat = new Mat(webcamTexture.height, webcamTexture.width, CvType.CV_8UC4);

        //initialize texture2d
        texture = new Texture2D(rgbaMat.cols(), rgbaMat.rows(), TextureFormat.RGBA32, false);
    }
    void Start()
    {
        // Start web cam feed
        WebCamDevice[] cam_devices = WebCamTexture.devices;
        webcamTexture = new WebCamTexture(cam_devices[0].name, (int)transform.GetComponent<RectTransform>().sizeDelta.x, (int)transform.GetComponent<RectTransform>().sizeDelta.y, 30);
        webcamTexture.Play();
        data = new Color32[webcamTexture.width * webcamTexture.height];
        initializeData();
    }

    void Update()
    {
        if (webcamTexture.didUpdateThisFrame)
        {
            ////webcamTexture.GetPixels32(data);
            // Do processing of data here.

            //convert webcamtexture to rgb mat
            Utils.webCamTextureToMat(webcamTexture, rgbaMat, true, -1); //flip code > 0 : 상하좌우반전, flip code == 0 : 상하반전, flip code < 0 : 좌우반전
            //convert rgbmat to grayscale
            Imgproc.cvtColor(rgbaMat, grayMat, Imgproc.COLOR_RGBA2GRAY);

            //extract faces
/*            cascade.detectMultiScale(grayMat, faces, 1.1, 4);
            //store faces in array
            OpenCVForUnity.CoreModule.Rect[] rects = faces.toArray();
            //draw rectangle over all faces
            for (int i = 0; i < rects.Length; i++)
            {
                Debug.Log("detect faces " + rects[i]);
                Imgproc.rectangle(rgbaMat, new Point(rects[i].x, rects[i].y), new Point(rects[i].x + rects[i].width, rects[i].y + rects[i].height), new Scalar(255, 0, 0, 255), 2);
            }*/

            //convert rgb mat back to texture
            Utils.fastMatToTexture2D(rgbaMat, texture);

            //set rawimage texture
            this.GetComponent<RawImage>().texture = texture;
        }
    }

    /*public double[] get(int row, int col)
    {
        ThrowIfDisposed();

        #if UNITY_PRO_LICENSE || ((UNITY_ANDROID || UNITY_IOS) && !UNITY_EDITOR) || UNITY_5
 
                                double[] tmpArray = new double[channels ()];
                                int result = core_Mat_nGet (nativeObj, row, col, tmpArray.Length, tmpArray);
 
                                if (result == 0) {
                                        return null;
                                } else {
                                        return tmpArray;
                                }
        #else
                return null;
        #endif
    }*/
}
