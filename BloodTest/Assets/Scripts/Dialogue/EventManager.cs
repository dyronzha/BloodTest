using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//只負責處理事件播出的總佇列
//負責計時，若總佇列仍有元素則根據計時逐漸推出(Unarus作法)

public class EventManager : MonoBehaviour{
    public EmotionPack _emotionpack;
    public Queue<string> AllActorNames = new Queue<string>();
    public Queue<string> AllEmotions = new Queue<string>();
    public Queue<string> AllEventSentences = new Queue<string>();

    public Text nameText;
    public Text EventText;

    float TriggerMoment = 0.0f;
    bool Showing_Event = false;
    public float StayTime = 3.0f;
   public  bool PausePoint = true;

    public Queue<string> AwaitActorName = new Queue<string>();
    public Queue<string> AwaitEmotion = new Queue<string>();
    public Queue<string> AwaitSentence = new Queue<string>();


    void Update(){
        if (Showing_Event == true && Time.time > TriggerMoment + StayTime) {
            Showing_Event = false;
            _emotionpack.EventLeave();
        }
    }

    public void EnqueueEvent(List<string> NameList, List<string> EmotionList, List<string> EventList){
        //預先清空
        AllActorNames.Clear();
        AllEmotions.Clear();
        AllEventSentences.Clear();

        //推入本章節事件角色名與內容
        foreach (string name in NameList) { AllActorNames.Enqueue(name); }
        foreach (string emotion in EmotionList) { AllEmotions.Enqueue(emotion); }
        foreach (string sentence in EventList) { AllEventSentences.Enqueue(sentence); }
    }

    public void EventStillGoingorNot() {
        if (Showing_Event == false && PausePoint == false) DisplayNextEvent();
    }

    public void DisplayNextEvent() {
        if (AllEventSentences.Count == 0) {
            return;
        }

        if (AllActorNames.Peek() == "") {
            PausePoint = true;
            AllActorNames.Dequeue();
            AllEmotions.Dequeue();
            AllEventSentences.Dequeue();
            return;
        }

        nameText.text = "";
        EventText.text = "";
        _emotionpack.FindEventEmotion(AllEmotions.Dequeue());
        nameText.text = AllActorNames.Dequeue();
        EventText.text = AllEventSentences.Dequeue();
        PausePoint = false;
        Showing_Event = true;
        TriggerMoment = Time.time;
    }

    public void PushEventToQueue(string ActorName,string Emotion,string Sentence) {

        AwaitActorName.Enqueue(ActorName);
        AwaitEmotion.Enqueue(Emotion);
        AwaitSentence.Enqueue(Sentence);
        //可能追加什麼東西，留位

        //下面註解應為Display部分
        //nameText.text = "";
        //EventText.text = "";
        //_emotionpack.FindEventEmotion(Emotion);
        //nameText.text = ActorName;
        //EventText.text = Sentence;
    }



}
