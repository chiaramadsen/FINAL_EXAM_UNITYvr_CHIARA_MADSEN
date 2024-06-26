using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class KeyPressStateChangeTrigger : MonoBehaviour
{
    public KeyCode KeyToPress = KeyCode.G;
    public bool isPressed = false;
    
    public UnityEvent OnKeyPressTrue;
    public UnityEvent OnKeyPressFalse;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        KeyPressStateCheck();
    }

    void KeyPressStateCheck()
    {
        if (Input.GetKeyDown(KeyToPress))
        {
            isPressed = !isPressed;
        
            if (isPressed)
            {
                OnKeyPressTrue.Invoke();    
            }
            else
            {
                OnKeyPressFalse.Invoke();
            }
        }

    }
}
