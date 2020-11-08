using UnityEngine;
public class sceneManagerR : MonoBehaviour {
	public void SceneCHange(int scene)
    {
        Application.LoadLevel(scene);
    }
}