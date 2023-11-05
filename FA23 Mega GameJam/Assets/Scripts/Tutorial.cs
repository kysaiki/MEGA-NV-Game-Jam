/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    [SerializeField] private GameObject mouse;
    [SerializeField] private GameObject spaceBar;
    [SerializeField] private bool showAimTutorial; 
    [SerializeField] private bool showLaunchTutorial;

    private void Start()
    {
        showAimTutorial = true;
    }

    private void Update()
    {
        if (showAimTutorial)
        {            
        }
        if (showLaunchTutorial)
        {
            StartCoroutine(ShowLaunch());
        }
    }

    private IEnumerator ShowLaunch()
    {
        juvenileFishText.SetActive(true);
        juvenileFishText.GetComponent<Animator>().SetBool("visible", true);

        yield return new WaitForSeconds(3); ;

        juvenileFishText.GetComponent<Animator>().SetBool("visible", false);
    }

    public void RevealCatHorizontalControls()
    {
        catHorizontalArrows.SetActive(true);
        catHorizontalArrows.GetComponent<Animator>().SetBool("visible", true);
    }

    public void RevealCatVerticalControls()
    {
        catVerticalArrows.SetActive(true);
        catVerticalArrows.GetComponent<Animator>().SetBool("visible", true);
    }

    public void RevealSpotlightSpaceControl()
    {
        spotlightSpace.SetActive(true);
        spotlightSpace.GetComponent<Animator>().SetBool("visible", true);
    }

    public void RevealSpotlightMoveControls()
    {
        spotlightMoveArrows.SetActive(true);
        spotlightMoveArrows.GetComponent<Animator>().SetBool("visible", true);
    }

    public void RevealBoatSpaceControl()
    {
        boatSpace.SetActive(true);
        boatSpace.GetComponent<Animator>().SetBool("visible", true);
    }

    public void HideBoatSpaceControl()
    {
        if (boatSpace.activeSelf)
        {
            boatSpace.GetComponent<Animator>().SetBool("visible", false);
        }
    }

    public void HideCatHorizontalControls()
    {
        if (catHorizontalArrows.activeSelf)
        {
            catHorizontalArrows.GetComponent<Animator>().SetBool("visible", false);
        }
    }

    public void HideCatVerticalControls()
    {
        if (catVerticalArrows.activeSelf)
        {
            catVerticalArrows.GetComponent<Animator>().SetBool("visible", false);
        }
    }

    public void HideSpotlightSpaceControl()
    {
        if (spotlightSpace.activeSelf)
        {
            spotlightSpace.GetComponent<Animator>().SetBool("visible", false);
        }
    }

    public void HideSpotlightMoveControls()
    {
        if (spotlightMoveArrows.activeSelf)
        {
            spotlightMoveArrows.GetComponent<Animator>().SetBool("visible", false);
        }
    }
}*/
