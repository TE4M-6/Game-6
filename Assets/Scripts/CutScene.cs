using UnityEngine;

public class CutScene : MonoBehaviour
{
    private GameObject [] cutScenes;
    
    // Update is called once per frame
    void Update()
    {
        PlayCutscenes();
        SkipCutScenes();
    }

    private void PlayCutscenes()
    {
        cutScenes = GameObject.FindGameObjectsWithTag("Cutscene Canvas");
        if (cutScenes.Length == 0)
        {
            SceneLoader.LoadDemoGame();
        }
    }

    private void SkipCutScenes()
    {
        if (Input.anyKey)
        {
            Debug.Log("any key pressed");
            SceneLoader.LoadDemoGame();
        }
    }
}
