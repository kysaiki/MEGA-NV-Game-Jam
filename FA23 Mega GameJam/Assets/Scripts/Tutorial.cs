using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Tutorial : MonoBehaviour
{
    [SerializeField] private Animator mouseAnim;
    [SerializeField] private Animator spaceBarAnim;
    [SerializeField] private Animator arrowAnim;
    [SerializeField] private Image mouse;
    [SerializeField] private Image spaceBar;
    [SerializeField] private Image arrows;
    [SerializeField] private bool showAimTutorial; 
    [SerializeField] private bool showLaunchTutorial;
    [SerializeField] private bool showSteerTutorial;
    [SerializeField] private TMP_Text spaceTxt;
    [SerializeField] private TMP_Text launchText;

    private void Start()
    {
    }

    private void Update()
    {
        if (showAimTutorial)
        {
            mouseAnim.Play("MouseAnim");
            showAimTutorial = false;
        }
        else if (showLaunchTutorial)
        {
            spaceBarAnim.Play("FadeIn");
            showLaunchTutorial = false;
        }
        else if (showSteerTutorial)
        {
            arrowAnim.Play("FadeIn");
            showSteerTutorial = false;
        }

        if(Input.GetKeyDown(KeyCode.Space) && spaceBar.enabled)
        {
            spaceBarAnim.Play("FadeOut");
            spaceTxt.enabled = false;
            launchText.enabled = false;
        }
        if (Input.GetAxis("Horizontal") > 0 && arrows.enabled && !arrowAnim.GetCurrentAnimatorStateInfo(0).IsName("FadeOut"))
        {
            arrowAnim.Play("FadeOut");
        }
    }

    private IEnumerator ShowLaunch()
    {

        yield return new WaitForSeconds(3); ;

    }

    private IEnumerator ShowArrows()
    {
        yield return new WaitForSeconds(3); ;
    }

    public void SetAimTutorial()
    {
        showAimTutorial = true;
    }

    public void SetLaunchTutorial()
    {
        showLaunchTutorial = true;
    }

    public void SetSteerTutorial()
    {
        showSteerTutorial = true;
    }
}
