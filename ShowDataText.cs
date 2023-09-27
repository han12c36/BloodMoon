using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;
using TMPro;
using Interfaces;

//userdata 또는 userinfo를 text형식으로 보여줍니다.

public class ShowDataText : MonoBehaviour , IObserver
{
    TMP_Text dataText;
    public DataType type;

    public void UpdateData() => dataText.text = DataManager.Inst.DataText(type);
    void Start()
    {
        if (dataText == null) dataText = GetComponent<TMP_Text>();



        if(type == DataType.Nickname) DataManager.UserInfo.RegisterObserver(this);
        else DataManager.UserData.RegisterObserver(this);
    }
}