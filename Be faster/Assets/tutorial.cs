using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tutorial : MonoBehaviour {
    public int ft = 0;
    public GameObject Panel;
	// Use this for initialization
	void Start () {
        ft = PlayerPrefs.GetInt("thisisfirsttime");
        if(ft == 0)
        {
            Panel.SetActive(true);
        }
        if(ft >= 1)
        {
            Panel.SetActive(false);
        }
	}
	
	public void Tutorial()
    {
        PlayerPrefs.SetInt("thisisfirsttime", 1);
        Panel.SetActive(false);
    }
}
