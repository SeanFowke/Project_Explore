using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
	private AudioManager au;
	private AudioSource ado;
    void Start()
    {
		au = GameObject.Find("AudioManager").GetComponent<AudioManager>();
		ado = GetComponent<AudioSource>();
    }


	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("SandMusic"))
		{
			ado.clip = au.sandMusic;
			ado.Play();
		}
		if (collision.CompareTag("MountainMusic"))
		{
			ado.clip = au.mountMusic;
			ado.Play();
		}
		if (collision.CompareTag("ForestMusic"))
		{
			ado.clip = au.forestMusic;
			ado.Play();
		}
		if (collision.CompareTag("CemetaryMusic"))
		{
			ado.clip = au.cemetaryMusic;
			ado.Play();
		}
		if (collision.CompareTag("SwampMusic"))
		{
			ado.clip = au.swampMusic;
			ado.Play();
		}
		if (collision.CompareTag("StartMusic"))
		{
			ado.clip = au.startMusic;
			ado.Play();
		}
	}
}
