using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class MainEventIO : MonoBehaviour{

    string ReadPath_CSV;
    int RowCount = 0;
    public Queue<string> AllActorNames = new Queue<string>();
    public Queue<string> AllEmotions = new Queue<string>();
    public Queue<string> AllEventSentences = new Queue<string>();
    public EventManager _eventmanager;

    void Awake(){
        AllActorNames.Clear();
        AllEmotions.Clear();
        AllEventSentences.Clear();
        ReadPath_CSV = Application.streamingAssetsPath + "/1_MainEvent.csv";//之後用switch case選擇章節載入
        ReadCSVFile(ReadPath_CSV);
    }

    void ReadCSVFile(string filePath){
        StreamReader sReader = new StreamReader(filePath);

        bool EndofFile = false;

        while (!EndofFile){
            string line = sReader.ReadLine();
            if (line == null){
                EndofFile = true;
                break;
            }
            if (RowCount != 0){
                var data_values = line.Split(',');
                AllActorNames.Enqueue(data_values[0]);
                AllEmotions.Enqueue(data_values[1]);
                AllEventSentences.Enqueue(data_values[2]);
            }
            RowCount++;
        }
    }

    public void TriggerMainEvent() {

        while (AllActorNames.Peek() != "") {
            _eventmanager.PushEventToQueue(AllActorNames.Dequeue(), AllEmotions.Dequeue(), AllEventSentences.Dequeue());
            //留位，要確認此段事件對話有幾句，直到全部推入為止
        }

        if (AllActorNames.Peek() == "") {
            AllActorNames.Dequeue();
            AllEmotions.Dequeue();
            AllEventSentences.Dequeue();
        }

    }

}


//public EmotionPack _emotionpack;
//public Queue<string> AllActorNames = new Queue<string>();
//public Queue<string> AllEmotions = new Queue<string>();
//public Queue<string> AllEventSentences = new Queue<string>();
//public Text nameText;
//public Text EventText;
//float TriggerMoment = 0.0f;
//bool Showing_Event = false;
//public float StayTime = 3.0f;

//public bool PausePoint = true;

//void Update()
//{
//    if (Showing_Event == true && Time.time > TriggerMoment + StayTime)
//    {
//        Showing_Event = false;
//        _emotionpack.EventLeave();
//    }
//}

//public void EnqueueEvent(List<string> NameList, List<string> EmotionList, List<string> EventList)
//{
//    //預先清空
//    AllActorNames.Clear();
//    AllEmotions.Clear();
//    AllEventSentences.Clear();

//    //推入本章節事件角色名與內容
//    foreach (string name in NameList) { AllActorNames.Enqueue(name); }
//    foreach (string emotion in EmotionList) { AllEmotions.Enqueue(emotion); }
//    foreach (string sentence in EventList) { AllEventSentences.Enqueue(sentence); }
//}

//public void EventStillGoingorNot()
//{
//    if (Showing_Event == false && PausePoint == false) DisplayNextEvent();
//}

//public void DisplayNextEvent()
//{
//    if (AllEventSentences.Count == 0)
//    {
//        return;
//    }

//    if (AllActorNames.Peek() == "")
//    {
//        PausePoint = true;
//        AllActorNames.Dequeue();
//        AllEmotions.Dequeue();
//        AllEventSentences.Dequeue();
//        return;
//    }

//    nameText.text = "";
//    EventText.text = "";
//    _emotionpack.FindEventEmotion(AllEmotions.Dequeue());
//    nameText.text = AllActorNames.Dequeue();
//    EventText.text = AllEventSentences.Dequeue();
//    PausePoint = false;
//    Showing_Event = true;
//    TriggerMoment = Time.time;
//}