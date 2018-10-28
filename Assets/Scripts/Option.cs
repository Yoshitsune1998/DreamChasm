using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Option : MonoBehaviour {

    public static Option option;
    public GameObject mainOption;
    public GameObject volumeOption;
    public GameObject backButton;
    //public GameObject controlOption;

    private void Awake()
    {
        if (option == null)
        {
            option = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public void Start()
    {
        volumeOption.SetActive(false);
        //controlOption.SetActive(false);
        mainOption.SetActive(true);
        backButton.SetActive(false);
    }

    public void OpenVolume()
    {
        volumeOption.SetActive(true);
        mainOption.SetActive(false);
        backButton.SetActive(true);
    }

    //public void OpenControl()
    //{
    //  controlOption.SetActive(true);
    //  mainOption.SetActive(false);
    //}

    public void CloseVolume()
    {
        volumeOption.SetActive(false);
        mainOption.SetActive(true);
        backButton.SetActive(false);
    }

    public void SetToNormal()
    {
        mainOption.SetActive(true);
        volumeOption.SetActive(false);
        backButton.SetActive(false);
        //controlOption.SetActive(false);
    }

}
