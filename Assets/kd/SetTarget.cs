using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using OpenCVForUnityExample;
using OpenCVForUnity.CoreModule;

public class SetTarget : MonoBehaviour
{
    public FaceDetectorYNWebCamTextureExample a;
    int max = 3;
    int num = 0;
    public Vector2 t;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        FaceDetectorYNWebCamTextureExample.Detection[] detections = a.det(a.bgrMat);
        t = detections[0].xy;


        foreach (var d in detections)
        {
            if (Vector3.Distance(transform.GetChild(num).position, t) > Vector3.Distance(transform.GetChild(num).position, d.xy))
            {
                t = d.xy;
            }
            if (Input.GetKeyDown("a"))
            {
                transform.GetChild(num).gameObject.SetActive(false);
                num++;
                num %= max;
                transform.GetChild(num).gameObject.SetActive(true);
            }
            if (Input.GetKeyDown("b"))
            {
                transform.GetChild(num).gameObject.SetActive(false);
                num--;
                if (num == -1) num = 2;
                transform.GetChild(num).gameObject.SetActive(true);
            }
        }

        transform.GetChild(num).LookAt(new Vector3(t.x - 260, 0.0f, -t.y + 130));

        if (Vector3.Distance(transform.GetChild(num).position, new Vector3(t.x - 260, 0.0f, -t.y + 130)) >= 3)
        {

            //transform.GetChild(num).GetComponent<Animator>().SetBool("Forward", true);
            transform.GetChild(num).position = Vector3.MoveTowards(transform.GetChild(num).position, new Vector3(t.x - 260, 0.0f, -t.y + 130), 40f * Time.deltaTime);

        }
        /*else
        {
            transform.GetChild(num).GetComponent<Animator>().SetBool("Forward", false);
        }*/
    }
}

/*if ((target.position - transform.position).magnitude <= 3) // 플레이어
{
    Enemyanimator.SetBool("Bite Attack", true); // 랜덤으로 공격 모션 설정 및 공격

}
*/