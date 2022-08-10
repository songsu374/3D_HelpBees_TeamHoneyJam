using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlinkAnim : MonoBehaviour
{
    public Image image;
    //public GameObject toastPanal;
    public static BlinkAnim instance;
    private void Awake()
    {
        instance = this;
    }

    public IEnumerator Blink()
    {
        int count = 0;
        this.gameObject.SetActive(true);

        while (count < 3)
        {
            this.image.gameObject.SetActive(false);
            yield return new WaitForSeconds(1f);
            this.image.gameObject.SetActive(true);
            yield return new WaitForSeconds(1f);
            count++;
        }
    }

}
