using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    public Dropdown resolutionDropdown;
    public Toggle fullScreenToggle;
    private Resolution[] resolutions;
    void Start()
    {
        InitResolutions();
    }

    private void InitResolutions()
    {
        SetFullScreen(fullScreenToggle.isOn);
        resolutionDropdown.ClearOptions();
        
        // Clean the original resolutions from duplicate dimensions w/ different hz rates
        Dictionary<string, int> resolutionDict = new Dictionary<string, int>();
        StringBuilder option = new StringBuilder();
        for (int i = 0; i < Screen.resolutions.Length; i++)
        {
            Resolution currResolution = Screen.resolutions[i];
            option.Clear();
            option.Append(currResolution.width);
            option.Append(" x ");
            option.Append(currResolution.height);
            string resolutionKey = option.ToString();
            if (!resolutionDict.ContainsKey(resolutionKey))
            {
                resolutionDict.Add(resolutionKey, i);
            }
        }

        resolutions = new Resolution[resolutionDict.Count];
        int index = 0;
        foreach (string key in resolutionDict.Keys)
        {
            int resolutionsIndex = resolutionDict[key];
            resolutions[index] = Screen.resolutions[resolutionsIndex];
            index++;
        }

        int currentResolutionIndex = 0;
        List<string> optionsList = new List<string>();
        for (int i = 0; i < resolutions.Length; i++)
        {
            option.Clear();
            option.Append(resolutions[i].width);
            option.Append(" x ");
            option.Append(resolutions[i].height);
            optionsList.Add(option.ToString());

            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }
        resolutionDropdown.AddOptions(optionsList);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }
    public void SetFullScreen (bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
        resolutionDropdown.interactable = !isFullScreen;
    }

    /*
     * Bit of an issue. (work around to use Toggle bool)
     * the Screen.SetResolution is taking in the bool from the Toggle object in unity
     * which is referenced in the Canvas component script, OptionsMenu.cs, as the toggle object:
     * FullScreenToggle
     * 
     * We should techinically be using Screen.fullScreen for consistency sake.
     * However, 
    A full-screen switch does not happen immediately; 
    it will actually happen when the current frame is finished.
     * due to this, the Screen.fullScreen bool cannot be set until the frame finishes
     * which is too late on start up, since the bool switch update will occur after
     * the update/frame-finish
     */
    public void SetResolution(int resolutionIndex) 
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, fullScreenToggle.isOn);
    }
}
