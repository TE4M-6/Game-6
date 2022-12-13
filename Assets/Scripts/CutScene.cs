using System.Collections;
using UnityEngine;

public class CutScene : MonoBehaviour
{
    private GameObject [] cutScenes;
    [SerializeField] private GameObject black;
    
    // Update is called once per frame
    void Update()
    {
        PlayCutscenes();
        SkipCutScenes();
    }

    private void PlayCutscenes()
    {
        StartCoroutine(BlackScreen());
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
            SceneLoader.LoadDemoGame();
        }
    }

    private IEnumerator BlackScreen()
    {
        yield return new WaitForSeconds(15f);
        black.SetActive(true);
    }
}
