using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputView : MonoBehaviour
{
    [SerializeField]
    InputField inputField;

    [SerializeField]
    ClothBlendShapeWeight clothColliderControl;

    [SerializeField]
    int inputValue;

    void Start()
    {
        inputField.onValueChanged.AddListener(InputCallback);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void InputCallback(string inputText)
    {
        int num = 0;//初始化

        if (IsNumberic(inputText))
        {
            clothColliderControl.SetValue(inputValue);
        }
        else
        {
            return;//跳出
        }
    }

    bool IsNumberic(string strText)
    {
        bool isNum;//初始化
        int result;
        isNum = int.TryParse(strText, out result);
        inputValue = result;
        return isNum;
    }
}
