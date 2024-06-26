using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

public class DialogueManager : MonoBehaviour
{
    public Dialogue currentSegment;

    private int currentIndex = -1;

    public TMP_Text dialogueTextBox;

    public Button continueButton;

    public KeyCode advanceKey = KeyCode.F; // Default key is Space

    public UnityEvent OnDialogueEnd;

    public UnityEvent OnDialogueTransition;

    ////////////////////////////////////////////// Unity Methods //////////////////////////////////////////////

    private void Start()
    {
        if (continueButton != null)
        {
            continueButton.onClick.AddListener(AdvanceDialogue);
        }

        //ActivateDialogueSegment(currentSegment);
    }

    private void Update()
    {
        if (Input.GetKeyDown(advanceKey))
        {
            AdvanceDialogue();
        }
    }
    
    ////////////////////////////////////////////// Dialogue Controls //////////////////////////////////////////////
    
   public void StartDialogue()
    {
        if (currentSegment == null)
        {
            Debug.LogWarning("No dialogue segment provided to start.");
            return;
        }

        ActivateDialogueSegment(currentSegment);
    }

    public void SwitchSegment(Dialogue segment)
    {
        currentSegment = segment;
        currentIndex = -1;
        AdvanceDialogue();
    }

    public void ActivateDialogueSegment(Dialogue segment)
    {
        OnDialogueTransition.Invoke();

        currentSegment = segment;
        currentIndex = -1;
        AdvanceDialogue();
    }

    void AdvanceDialogue()
    {
        currentIndex++;
        if (currentSegment != null && currentIndex < currentSegment.dialogueLines.Length)
        {
            DisplayCurrentLine();
        }
        else
        {
            if (currentIndex >= currentSegment.dialogueLines.Length - 1)
            {
                currentSegment.OnSegmentEnd.Invoke();
            }
            
            EndDialogue();
        }
    }

    void DisplayCurrentLine()
    {
        DialogueLine line = currentSegment.dialogueLines[currentIndex];
        dialogueTextBox.text = line.dialogueText;

        TriggerAnimation(line);

        // Uncomment and modify the below code if using character cameras
        // if (line.characterCamera != null)
        // {
        //     CinemachineCore.Instance.GetActiveBrain(0).SetActiveVirtualCamera(line.characterCamera, 1);
        // }
    }
    
    public void EndDialogue()
    {
        dialogueTextBox.text = "";

        OnDialogueEnd.Invoke();
        
        currentIndex = -1;

        // Optionally reset camera or UI elements here
    }
    
    ////////////////////////////////////////////// Animation Controls //////////////////////////////////////////////

    void TriggerAnimation(DialogueLine line)
    {
        Animator actorAnimator = FindAnimatorByIdentifier(line.actorIdentifier);
        if (actorAnimator != null && !string.IsNullOrEmpty(line.animationTrigger))
        {
            actorAnimator.SetTrigger(line.animationTrigger);
        }
    }

    Animator FindAnimatorByIdentifier(string identifier)
    {
        GameObject actor = GameObject.Find(identifier);
        if (actor != null)
        {
            return actor.GetComponent<Animator>();
        }
        Debug.LogWarning($"Animator for identifier '{identifier}' not found.");
        return null;
    }

    ////////////////////////////////////////////// Camera Controls //////////////////////////////////////////////

}
