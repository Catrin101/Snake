using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameover : MonoBehaviour
{
    private void OnMouseDown(){
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
}
