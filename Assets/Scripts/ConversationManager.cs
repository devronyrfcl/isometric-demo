using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;
using System.Collections;

public class ConversationManager : MonoBehaviour
{
    [System.Serializable]
    public class Dialogue
    {
        public string speakerName;
        public Sprite speakerAvatar;
        [TextArea(3, 10)]
        public string dialogueText;
    }

    public GameObject conversationUI; // Reference to the ConversationUI GameObject
    public Image avatarImage;
    public TMP_Text nameText;
    public TMP_Text dialogueText;

    public Dialogue[] dialogues;
    public UnityEvent onConversationEnd; // UnityEvent to be triggered at the end of the conversation

    private int currentDialogueIndex = 0;
    private bool isTyping = false;
    private Coroutine typingCoroutine;

    void Start()
    {
        if (dialogues.Length > 0)
        {
            UpdateDialogue();
        }
        else
        {
            Debug.LogError("No dialogues assigned to the ConversationManager.");
        }
    }

    public void Next()
    {
        currentDialogueIndex++;
        if (currentDialogueIndex < dialogues.Length)
        {
            UpdateDialogue();
        }
        else
        {
            EndConversation();
        }
    }

    void UpdateDialogue()
    {
        Dialogue currentDialogue = dialogues[currentDialogueIndex];
        avatarImage.sprite = currentDialogue.speakerAvatar;
        nameText.text = currentDialogue.speakerName;

        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }

        typingCoroutine = StartCoroutine(DecryptDialogue(currentDialogue.dialogueText));
    }

    IEnumerator DecryptDialogue(string dialogue)
    {
        isTyping = true;
        dialogueText.text = "";
        for (int i = 0; i < dialogue.Length; i++)
        {
            dialogueText.text += dialogue[i];
            yield return new WaitForSeconds(0.02f); // Adjust decryption speed here
        }
        isTyping = false;
    }

    public void ResetConversation()
    {
        currentDialogueIndex = 0;
        UpdateDialogue();
        EnableConversationUI(); // Ensure the UI is enabled after resetting
    }

    void EndConversation()
    {
        // Disable the conversation UI
        conversationUI.SetActive(false);
        Debug.Log("End of conversation.");

        // Invoke the UnityEvent when the conversation ends
        onConversationEnd?.Invoke();
    }

    void EnableConversationUI()
    {
        conversationUI.SetActive(true);
    }

    public bool IsTyping
    {
        get { return isTyping; }
    }
}
