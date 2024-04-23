using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SkillsInfo : MonoBehaviour
{
    public GameObject Canvas;

    public void ShowInfo()
    {
        GameObject obj = GetOverUI(Canvas);
        switch (obj.name)
        {
            
        }
    }

    /// <summary>
    /// 获取鼠标停留处的UI
    /// </summary>
    /// <param name="canvas"></param>
    /// <returns></returns>
    private GameObject GetOverUI(GameObject canvas)
    {
        PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
        pointerEventData.position = Input.mousePosition;
        GraphicRaycaster gr = canvas.GetComponent<GraphicRaycaster>();
        List<RaycastResult> results = new List<RaycastResult>();
        gr.Raycast(pointerEventData, results);
        if (results.Count != 0)
        {
            return results[0].gameObject;
        }
        return null;
    }
}
