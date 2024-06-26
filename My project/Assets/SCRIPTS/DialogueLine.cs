using UnityEngine;

[CreateAssetMenu(fileName = "New Dialogue Line", menuName = "Dialogue/Dialogue Line")]
public class DialogueLine : ScriptableObject
{
    [TextArea(3, 10)]
    public string dialogueText;

    public string actorIdentifier; // Identifier to find the actor's animator in the scene
    public string animationTrigger; // Animation trigger to be called

    // Add any additional properties or methods needed for dialogue execution here
}