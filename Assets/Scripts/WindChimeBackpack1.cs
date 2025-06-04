using System.Collections.Generic;
using UnityEngine;

public class WindChimeBackpack1 : MonoBehaviour
{
    public List<Transform> attachPoints; // 在 Inspector 中拖入所有 AttachPoint
    private List<GameObject> attachedChimes = new List<GameObject>();

    public bool TryAttach(GameObject chime)
    {
        // 找第一个空的挂点
        foreach (Transform point in attachPoints)
        {
            if (point.childCount == 0)
            {
                chime.transform.SetParent(point);
                chime.transform.localPosition = Vector3.zero;
                chime.transform.localRotation = Quaternion.identity;

                attachedChimes.Add(chime);
                return true; // 成功挂上
            }
        }

        Debug.LogWarning("No empty attach point available!");
        Destroy(chime); // 清理掉没地方挂的 wind chime
        return false;
    }

    // 用于 WindChimeSelector1 获取当前的风铃
    public List<GameObject> GetAttachedChimes()
    {
        return attachedChimes;
    }

    // 如果 match 成功，就调用此方法移除
    public void RemoveChime(GameObject chime)
    {
        if (attachedChimes.Contains(chime))
        {
            attachedChimes.Remove(chime);
            Destroy(chime); // 或执行动画后再销毁
        }
    }
}
