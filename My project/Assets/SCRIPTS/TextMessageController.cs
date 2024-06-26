using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class TextMessageController : MonoBehaviour
{
    public TMP_Text MessageTextBox;
    public float TweenDuration = 1.0f;
    public float ClearDelay = 3.0f;

    void Start()
    {

    }

    public void SendText(string message)
    {
        MessageTextBox.text = message;
    }
    
    public void ClearText()
    {
        MessageTextBox.text = "";
    }

    public void SendTextWithTween(string message)
    {
        MessageTextBox.text = message;
        MessageTextBox.color = new Color(MessageTextBox.color.r, MessageTextBox.color.g, MessageTextBox.color.b, 0);
        DOTween.To(() => MessageTextBox.color.a, x => MessageTextBox.color = new Color(MessageTextBox.color.r, MessageTextBox.color.g, MessageTextBox.color.b, x), 1, TweenDuration);
    }
    
    // public void SendTextWithTween(string message)
    // {
    //     MessageTextBox.text = "";
    //     MessageTextBox.DOText(message, TweenDuration, true);
    // }
    
    public void SendAndClearTextWithTween(string message)
    {
        SendTextWithTween(message);
        StartCoroutine(ClearAfterDelay());
    }

    private IEnumerator ClearAfterDelay()
    {
        yield return new WaitForSeconds(ClearDelay);
        ClearTextWithTween();
    }
    
    public void ClearTextWithTween()
    {
        DOTween.To(() => MessageTextBox.color.a, x => MessageTextBox.color = new Color(MessageTextBox.color.r, MessageTextBox.color.g, MessageTextBox.color.b, x), 0, TweenDuration);
    }

}
