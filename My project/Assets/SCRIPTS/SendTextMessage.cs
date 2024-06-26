using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SendTextMessage : MonoBehaviour
{
    public TMP_Text textMessage;

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void SendText(string message)
    {
        textMessage.text = message;
    }
}
