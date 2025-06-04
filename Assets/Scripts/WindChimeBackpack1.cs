using UnityEngine;

public class WindChimeBackpack1 : MonoBehaviour
{
    [Header("Attach Points")]
    public Transform[] attachPoints;

    public GameObject FindAttachedChime(int melodyID)
    {
        foreach (Transform slot in attachPoints)
        {
            if (slot.childCount > 0)
            {
                Transform chime = slot.GetChild(0);
                WindChimeData1 data = chime.GetComponent<WindChimeData1>();
                if (data != null && data.melodyID == melodyID)
                {
                    return chime.gameObject;
                }
            }
        }
        return null;
    }
}
