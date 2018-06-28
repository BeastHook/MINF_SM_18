using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour {

    // Klick auf Start Game Button
	public void OnPlayButtonClick()
    {
       GameplayManager.Instance.ChangeScene("Testscene");
    }
}
