using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    [SerializeField]
    GameObject Login, SignUp;

    private Transform pencil;


    private void Awake()
    {
        instance = this;
        Setup();
    }

    void Start()
    {
        Login.SetActive(true);
        SignUp.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void IsLogin()
    {
        Login.SetActive(true);
        SignUp.SetActive(false);
    }

    public void IsSignUp()
    {
        Login.SetActive(false);
        SignUp.SetActive(true);
    }

    public void Setup(){
        pencil = GameObject.Find("Pencil_Option").transform.GetChild(0);
	}

    public void ChangeColor(){
        pencil.GetComponent<Image>().color = EventSystem.current.currentSelectedGameObject.transform.GetChild(0).GetComponent<Image>().color;
	}

    public void ChangeThick()
    {
        pencil.GetChild(0).GetComponent<Slider>().value = EventSystem.current.currentSelectedGameObject.GetComponent<Slider>().value;
    }
}
