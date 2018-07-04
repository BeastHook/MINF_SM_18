﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinigameCanvas : MonoBehaviour
{
	[SerializeField] private Text _minigameName;
	[SerializeField] private Text _descriptionText;
	[SerializeField] private Text _startsInText;
	[SerializeField] private Image _gameImage;
	[SerializeField] private GameObject _winObject;
	[SerializeField] private GameObject _failureObject;

	private MultisceneManager.Minigame _minigame;

	public void InitiliazeWithMinigame(MultisceneManager.Minigame minigame)
	{
		_minigameName.text = minigame.Name;
		_descriptionText.text = minigame.Description;
		_gameImage.sprite = minigame.Sprite;

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

		_startsInText.gameObject.SetActive(false);
	}
}
