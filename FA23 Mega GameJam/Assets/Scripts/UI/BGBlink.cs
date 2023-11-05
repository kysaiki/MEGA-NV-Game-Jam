using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BGBlink : MonoBehaviour
{
    [SerializeField]
    public float blinckSpeed;

    [SerializeField]
    public float minAlpha;

    [SerializeField]
    public Image StarImg;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UIBlinck(StarImg);
    }

    private void UIBlinck(Image img)
    {
        float _a = Mathf.Lerp(minAlpha, 1f, Mathf.PingPong(Time.time * blinckSpeed, 1));
        Color c = img.color;
        c.a = _a;
        img.color = c;
    }
}
