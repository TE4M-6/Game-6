using UnityEngine;

public class EndOfGameScript : MonoBehaviour
{
    [SerializeField] private GameObject gameWonCanvas;
    [SerializeField] private GameObject gameOverCanvas;
    
    public void GameWon()
    {
        CustomCursor.SetDefaultCursor(); // 131222 @Nuutti J.
        gameWonCanvas.SetActive(true);
        Time.timeScale = 0;
    }

    public void GameOver()
    {
        CustomCursor.SetDefaultCursor(); // 131222 @Nuutti J.
        gameOverCanvas.SetActive(true);
        Time.timeScale = 0;
    }
    
}
