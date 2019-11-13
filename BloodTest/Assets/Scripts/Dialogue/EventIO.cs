using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class EventIO : MonoBehaviour{

    string ReadPath_CSV;
    int RowCount = 0;

    public List<string> ReadNameList = new List<string>();
    public List<string> ReadEmotionList = new List<string>();
    public List<string> ReadEventList = new List<string>();

    public EventManager _eventmanager;

    void Awake(){
        ReadPath_CSV = Application.streamingAssetsPath + "/1_MainEvent.csv";//之後用switch case選擇章節載入
        ReadCSVFile(ReadPath_CSV);
        SetChapterEvent();
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
                ReadNameList.Add(data_values[0].ToString());
                ReadEmotionList.Add(data_values[1].ToString());
                ReadEventList.Add(data_values[2].ToString());
            }
            RowCount++;
        }
    }

    public void SetChapterEvent(){
        _eventmanager.EnqueueEvent(ReadNameList, ReadEmotionList, ReadEventList);
    }


    //public EventManager _eventmanager;

    //string ReadPath_Name;
    //string ReadPath_Event;

    //public List<string> ReadNameList = new List<string>();
    //public List<string> ReadEventList = new List<string>();

    //void Start(){
    //    ReadPath_Name = Application.streamingAssetsPath + "/Ch1_EventActor.txt";//之後這裡用switch case 分章節讀入
    //    ReadPath_Event = Application.streamingAssetsPath + "/Ch1_Event.txt";//同上

    //    ReadEventFile(ReadPath_Event);
    //    ReadNameFile(ReadPath_Name);
    //    SetChapterEvent();
    //}

    //void ReadNameFile(string filePath){
    //    StreamReader sReader = new StreamReader(filePath);
    //    while (!sReader.EndOfStream){
    //        string line = sReader.ReadLine();
    //        ReadNameList.Add(line);
    //    }
    //    sReader.Close();
    //}

    //void ReadEventFile(string filePath){
    //    StreamReader sReader = new StreamReader(filePath);
    //    while (!sReader.EndOfStream){
    //        string line = sReader.ReadLine();
    //        ReadEventList.Add(line);
    //    }
    //    sReader.Close();
    //}

    //void SetChapterEvent(){
    //    _eventmanager.EnqueueEvent(ReadNameList, ReadEventList);
    //}
}
