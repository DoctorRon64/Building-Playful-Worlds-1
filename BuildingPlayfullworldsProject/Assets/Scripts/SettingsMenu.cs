using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class SettingsMenu : MonoBehaviour
{
	public AudioMixer AudioMixer;
	public TMP_Dropdown DropdownResolutions;
	private Resolution[] resolutions;

	private void Start()
	{
		resolutions = Screen.resolutions;
		
		List<string> resolutionList = new List<string>();
		int currentResolution = 0;

		for (int i = 0; i < resolutions.Length; i++)
		{
			string option = resolutions[i].width + " x " + resolutions[i].height;
			resolutionList.Add(option);
			if (resolutions[i].width == Screen.currentResolution.width &&
				resolutions[i].height == Screen.currentResolution.height)
			{
				currentResolution = i;
			}
		}
		DropdownResolutions.AddOptions(resolutionList);
		DropdownResolutions.value = currentResolution;
		DropdownResolutions.RefreshShownValue();
	}

	public void SetResolution(int ResolutionIndex)
	{
		Resolution _resolution = resolutions[ResolutionIndex];
		Screen.SetResolution(_resolution.width, _resolution.height, Screen.fullScreen);
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
