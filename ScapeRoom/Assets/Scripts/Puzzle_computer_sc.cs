using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Puzzle_computer_sc : Puzzle_sc
{
    [SerializeField] private GameObject yourText;
    [SerializeField] private GameObject strangerText;
    [SerializeField] private GameObject chatPanel;
    [SerializeField] private InputField inputField;

    List<Message> messageList = new List<Message>();
    Decision treeRoot;
    Decision currentDecision;
    int i = 0;

    // Start is called before the first frame update
    void Start()
    {
        InitFirstCodeDialog();
        CheckDecision("");

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            //SendMessageToChat("hey, who are you?", true);
            //SendMessageToChat("112", false);
            //SendMessageToChat("wtf?", true);
            //SendMessageToChat("091", false);
            //SendMessageToChat("oh, okay you are also locked in here right? ", true);
            //SendMessageToChat("1", false);
            CheckDecision(inputField.text);
        }
    }

    public override void Activate()
    {
        CheckDecision("");
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

    public void SendMessageToChat(string text, bool stranger)
    {

        Message newMessage = new Message();
        newMessage.text = text;
        GameObject newText = (stranger) ? Instantiate(strangerText, chatPanel.transform) : Instantiate(yourText, chatPanel.transform);
        newMessage.textObject = newText.GetComponent<Text>();
        newMessage.textObject.text = ((stranger) ? "<stranger> " : "<you> ") + newMessage.text;
        messageList.Add(newMessage);

    }
    public void CheckDecision(string code)
    {
        if (code != "")
        {
            SendMessageToChat(code, false);
            inputField.text = "";
        }
        StartCoroutine(MakeDecision(2, code));

    }
    IEnumerator MakeDecision(int seconds, string code)
    {
        yield return new WaitForSeconds(seconds);
        if (currentDecision.code == code)
        {
            SendMessageToChat(currentDecision.goodAnswerd, true);
            currentDecision = currentDecision.nextStep;
        }
        else
        {
            SendMessageToChat(currentDecision.badAnswerd, true);
        }

    }
    public void InitFirstCodeDialog()
    {
        treeRoot = new Decision();
        treeRoot.code = "";
        treeRoot.goodAnswerd = "There are someone there?";
        treeRoot.badAnswerd = "";
        currentDecision = treeRoot;

        Decision dec0 = new Decision();
        dec0.code = "1";
        dec0.goodAnswerd = "You are the fucking master of the creation";
        dec0.badAnswerd = "badAnswerd";
        treeRoot.nextStep = dec0;

        Decision dec1 = new Decision();
        dec1.code = "2";
        dec1.goodAnswerd = "goodAnswerd";
        dec1.badAnswerd = "badAnswerd";
        dec0.nextStep = dec1;
    }


    [System.Serializable]
    public class Message
    {
        public string text;
        public Text textObject;
    }
    [System.Serializable]
    public class Decision
    {
        public string code;
        public string goodAnswerd;
        public string badAnswerd;
        public Decision nextStep;
    }
}
