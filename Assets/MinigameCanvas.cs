using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MinigameCanvas : MonoBehaviour
{
	[SerializeField] private TextMeshPro _minigameName;
	[SerializeField] private TextMeshPro _descriptionText;
	[SerializeField] private Text _startsInText;
	[SerializeField] private Image _gameImage;
	[SerializeField] private GameObject _winObject;
	[SerializeField] private GameObject _failureObject;
	[SerializeField] private Transform[] _letters;

	private MultisceneManager.Minigame _minigame;

	public void InitiliazeWithMinigame(MultisceneManager.Minigame minigame)
	{
		_minigameName.text = minigame.Name;
		_descriptionText.text = minigame.Description;
		_gameImage.sprite = minigame.Sprite;

		if(minigame.SolutionLetters.Length > 0)
			for (int i = 0; i < minigame.SolutionLetters.Length; i++)
			{
				Instantiate(minigame.SolutionLetters[i], _letters[i]);
			}

		_minigame = minigame;
	}

	public void UpdateStartsIn(int startsIn)
	{
		_startsInText.text = "Starts in " + startsIn + "...";
	}

	public void UpdateCanvas(bool hasWon)
	{
		if (hasWon)
		{
			_winObject.SetActive(true);
		}
		else
		{
			_failureObject.SetActive(true);
		}

		_startsInText.text = "Game Already Played";
	}
}
