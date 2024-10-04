using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIEffectController : MonoBehaviour
{

    #region Private Fields
     private bool isTextEffect = false;
    #endregion
    #region Public Fields
    public Text[] nameText;
    public Text[] valueText;
    #endregion

    private void OnEnable()
    {
        StartCoroutine(ShowTextEffect());
    }
    public IEnumerator ShowTextEffect()
    {

        for (int i = 0; i < nameText.Length; i++)
        {
            StartCoroutine(TextEffect(nameText[i], valueText[i]));
            yield return new WaitUntil(() => isTextEffect == false);
        }
    }

    private IEnumerator TextEffect(Text name, Text value)
    {
        isTextEffect = true;
        float time = 0;
        float duration = 0.5f;
        
        while (time <= duration)
        {
            name.color = new Color(1, 1, 1, 0 + time / duration);
            value.color = new Color(1, 1, 1, 0 + time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        isTextEffect = false;
    }

    public void ResetAlphaValue()
    {
        for (int i = 0; i < nameText.Length; i++)
        {
            nameText[i].color = new Color(1, 1, 1, 0);
            valueText[i].color = new Color(1, 1, 1, 0);
        }
    }
}
