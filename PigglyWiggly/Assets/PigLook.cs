using UnityEngine;
using System.Collections;

public class PigLook : MonoBehaviour {
    public Texture norm;
    public Texture normDity;
    public Texture normHung;
    public Texture normHungDirty;
    public Texture sick;
    public Texture sickHung;
    public Texture sickDirty;
    public Texture sickDirtHung;
    public Texture halfSick;
    public Texture halfSickHung;
    public Texture halfSickDirt;
    public Texture halfSickDirtHung;

    public Texture ChangePigLook(bool dirty, int hunger, float sickness)
    {
        if (!dirty && hunger <=5 && sickness <= 5)
        {
            return norm;
        }
        else if (dirty)
        {
            if (hunger > 5)
            {
                if (sickness > 5 && sickness < 8)
                {
                    return halfSickDirtHung;
                }
                else if (sickness > 8)
                {
                    return sickDirtHung;
                }

                return normHungDirty;
            }
            else
            {
                if (sickness > 5 && sickness < 8)
                {
                    return halfSickDirt;
                }
                else if (sickness > 8)
                {
                    return sickDirty;
                }
            }

            return normDity;

        }
        else
        {
            if (hunger > 5)
            {
                if (sickness > 5 && sickness < 8)
                {
                    return halfSickHung;
                }
                else if (sickness > 8)
                {
                    return sickHung;
                }
            }
            else
            {
                if (sickness > 5 && sickness < 8)
                {
                    return halfSick;
                }
                else if (sickness > 8)
                {
                    return sick;
                }
            }
        }

        return norm;
    }
}
