using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Puzzle_computer_sc : Puzzle_sc
{
    [SerializeField] private GameObject yourText;
    [SerializeField] private GameObject strangerText;
    [SerializeField] private GameObject chatPanel;
    List<Message> messageList = new List<Message>();

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            SendMessageToChat("hey, who are you?", true);
            SendMessageToChat("112", false);
            SendMessageToChat("wtf?", true);
            SendMessageToChat("091", false);
            SendMessageToChat("oh, okay you are also locked in here right? ", true);
            SendMessageToChat("1", false);
        }
    }

    public override void Activate()
    {
        throw new System.NotImplementedException();
    }

    public override void Deactivate()
    {
        throw new System.NotImplementedException();
    }

    public override void OnResolve()
    {
        throw new System.NotImplementedException();
    }

    public override void OnFail()
    {
        throw new System.NotImplementedException();
    }

    public void SendMessageToChat(string text, bool stranger) {

        Message newMessage = new Message();
        newMessage.text = text;
        GameObject newText = (stranger)?Instantiate(strangerText, chatPanel.transform): Instantiate(yourText, chatPanel.transform);
        newMessage.textObject = newText.GetComponent<Text>();
        newMessage.textObject.text = ((stranger) ? "<stranger> " : "<you> ") + newMessage.text;
        messageList.Add(newMessage);

    }

    [System.Serializable]
    public class Message{
        public string text;
        public Text textObject;
    }
}
