using UnityEngine;

public class unlock : MonoBehaviour {
    public static int Unlocked = 0;
    public int ll = Unlocked;
    public static int Unlocked1 = 0;
    public int aa = Unlocked1;
    public GameObject RPanel;
    public GameObject SPanel;
    void Start () {
        Unlocked = PlayerPrefs.GetInt("rankedUnlock");
        Unlocked1 = PlayerPrefs.GetInt("steadyUnlock");
    }
	void Update () {
		if(Unlocked >= 1)
        {
            RPanel.SetActive(false);
        }
        if (Unlocked1 >= 1)
        {
            SPanel.SetActive(false);
        }

    }
}
