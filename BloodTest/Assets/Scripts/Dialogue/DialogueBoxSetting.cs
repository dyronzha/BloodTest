using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueBoxSetting : MonoBehaviour{

    public TestPlayer _testplayer;
    public DialogueManager _dialoguemanager;

    public void LoadEmotion() {
        _dialoguemanager.CheckActorEmotion();
    }

    public void LoadDialogue() {
        _dialoguemanager.DisplayNextSentence();
    }

    public void EndorChange() {
        //if (_dialoguemanager.ActorwillChange == true) _dialoguemanager.ResumeDialogue();
        //else _dialoguemanager.DuringDialogue = false;
        _testplayer.SwitchDialogue(false);
        _dialoguemanager.DuringDialogue = false;
    }

}
