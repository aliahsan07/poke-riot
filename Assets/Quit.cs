using UnityEngine;
using UnityEngine.UI;// we need this namespace in order to access UI elements within our script
using System.Collections;
using UnityEngine.SceneManagement; // neded in order to load scenes

public class Quit : MonoBehaviour
{
    public void exitGame() //this function will be used on our Exit button

    {
        //SceneManager.LoadScene(3); //this will load our menu from our build settings. "0" is the second scene in our game
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;

#else
        Application.Quit(); 
#endif
    }

}