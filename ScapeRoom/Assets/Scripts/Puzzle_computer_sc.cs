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
        treeRoot.script = "Is someone there?";
        treeRoot.anwser = null;

        Decision dec0 = new Decision();
        dec0.script = "Are you a real person? If you are, type 123.";
        dec0.anwser = "123";
        
        Decision dec1 = new Decision();
        dec1.time = 5.0f;
        dec1.transition = true;
        dec1.script = "I'm guessing you can't type. I woke up trapped in here. It's some sort of document room full of papers marked with numbers. The door is locked with a 4 digit code.";
        dec1.anwser = null;
     
        Decision dec2 = new Decision();
        dec2.time = 5.0f;
        dec2.transition = true;
        dec2.script = "I assume you're trapped too. This seems like some psycho's messed up game. They want to toy with us.";
        dec2.anwser = null;
       
        Decision dec3 = new Decision();
        dec3.time = 5.0f;
        dec3.script = "If you find a number tell me and I might find something useful. That can help us both get out of here.";
        dec3.anwser = null;

        Decision dec4 = new Decision();
        dec4.time = 5.0f;
        dec4.transition = true;
        dec4.script = "The document marked with that number is a blueprint of a combination lock with the numbers 247.";
        dec4.anwser = null;

        Decision dec5 = new Decision();
        dec5.time = 5.0f;
        dec5.script = "I hope that worked. Please, if you find a four digit code somewhere tell me, it might save my life.";
        dec5.anwser = null;

        Decision dec6 = new Decision();
        dec6.time = 5.0f;
        dec6.script = "It's a picture of a hallway with broken windows. There's a green exit sign with a red circle around it.";
        dec6.anwser = null;

        Decision dec7 = new Decision();
        dec7.time = 5.0f;
        dec7.script = "Its a recipe for some sort of injection. Here are the instructions \n - Place one vial \n -Spin once \n - Place two more vials. \n - Spin once. \n - Mix.";
        dec7.anwser = null;

        Decision dec8 = new Decision();
        dec8.time = 5.0f;
        dec8.script = "The door opened! Thank you, you saved my life. I hope you can escape too.";
        dec8.anwser = null;

        //init
        treeRoot.goodStep = dec0;
        treeRoot.badStep = null;
        currentDecision = treeRoot;

        //steps
        dec0.goodStep = dec1;
        dec0.badStep = null;
        dec1.goodStep = dec2;
        dec1.badStep = null;
        dec2.goodStep = dec3;
        dec2.badStep = null;
        dec3.goodStep = dec4;
        dec3.badStep = null;
        dec4.goodStep = dec5;
        dec4.badStep = null;
        dec5.goodStep = dec6;
        dec5.badStep = null;
        dec6.goodStep = dec7;
        dec6.badStep = null;
        dec7.goodStep = dec8;
        dec7.badStep = null;

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