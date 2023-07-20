using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayNowButton : MonoBehaviour
{
    public void PlayNow()
    {
        SceneManager.LoadScene("TeenPatti"); 
    }
}
