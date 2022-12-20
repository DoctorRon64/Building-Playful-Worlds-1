using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour
{
	public AudioMixer AudioMixer;
	private Resolution[] resolutions;


	private void Start()
	{
		resolutions = Screen.resolutions;
	}

	public void ChangeVolume(float _Volume)
	{
		AudioMixer.SetFloat("volume", _Volume);
	}

	public void SetFullscreen(bool _Fullscreen)
	{
		Screen.fullScreen = _Fullscreen;
	}
}
