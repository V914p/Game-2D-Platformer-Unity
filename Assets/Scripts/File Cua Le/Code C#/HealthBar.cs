using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthBar : MonoBehaviour
{
    public Transform target;       // Nhân vật hoặc quái
    public Vector3 offset;         // Độ lệch để hiển thị trên đầu
    public Image fillBar;
    public TextMeshProUGUI valueText;

    

    public void UpdateBar(int currentValue, int maxValue)
    {
        fillBar.fillAmount = (float)currentValue / (float)maxValue;
        valueText.text = currentValue.ToString() + " / " + maxValue.ToString();
    }
    void LateUpdate()
    {
        if (target != null)
        {
            // Gán vị trí thanh máu = vị trí target + offset
            transform.position = target.position + offset;
        }
    }
}
