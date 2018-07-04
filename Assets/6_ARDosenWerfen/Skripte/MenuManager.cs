using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {

    // Klick auf Start Game Button
	public void OnPlayButtonClick()
    {
        SceneManager.LoadScene("Testscene");
    }
}
