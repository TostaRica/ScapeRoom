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

    private List<Message> messageList = new List<Message>();
    private Decision treeRoot;
    private Decision currentDecision;
    private string code_1;

    // Start is called before the first frame update
    private void Start()
    {
        InitFirstCodeDialog();
        CheckDecision("");
        code_1 = "";
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            //SendMessageToChat("hey, who are you?", true);
            //SendMessageToChat("112", false);
            //SendMessageToChat("wtf?", true);
            //SendMessageToChat("091", false);
            //SendMessageToChat("oh, okay you are also locked in here right? ", true);
            //SendMessageToChat("1", false);
            code_1 = Main_sc.GetKey(code1);
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
        StartCoroutine(MakeDecision(currentDecision.time, code));
    }

    private IEnumerator MakeDecision(float seconds, string code)
    {
        yield return new WaitForSeconds(seconds);
        if (currentDecision.anwser == code)
        {
            SendMessageToChat(currentDecision.script, true);
            currentDecision = currentDecision.goodStep;
        }
        else
        {
            SendMessageToChat(currentDecision.script, true);
            currentDecision = currentDecision.badStep;
        }
    }

    public void InitFirstCodeDialog()
    {
        treeRoot = new Decision();
        treeRoot.anwser = null;
        treeRoot.script = "Is someone there?";
        currentDecision = treeRoot;

        Decision dec0 = new Decision();
        dec0.anwser = "123";
        dec0.script = "Are you a real person? If you are, type 123.";

        treeRoot.goodStep = dec0;
        treeRoot.badStep = null;

        Decision dec1 = new Decision();
        dec1.time = 5.0f;
        dec1.transition = true;
        dec1.anwser = null;
        dec1.script = "I'm guessing you can't type. I woke up trapped in here. It's some sort of document room full of papers marked with numbers. The door is locked with a 4 digit code.";

        Decision dec2 = new Decision();
        dec2.time = 5.0f;
        dec2.transition = true;
        dec2.anwser = null;
        dec2.script = "I assume you're trapped too. This seems like some psycho's messed up game. They want to toy with us.";

        Decision dec3 = new Decision();
        dec3.time = 5.0f;
        dec3.anwser = null;
        dec3.script = "I assume you're trapped too. This seems like some psycho's messed up game. They want to toy with us.";


        dec0.goodStep = dec1;
        dec0.badStep = null;
        dec1.goodStep = dec2;
        dec1.badStep = null;
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
        public bool transition = false;
        public float time = 2.0f;
        public string anwser;
        public string script;
        public Decision goodStep;
        public Decision badStep;
    }
}