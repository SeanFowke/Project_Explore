using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
	public void Begin()
	{
		SceneManager.LoadScene("World");
	}
	public void Exit()
	{
		Application.Quit();
	}
}
