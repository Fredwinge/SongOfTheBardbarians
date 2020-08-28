using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CGoblinHorn : MonoBehaviour
{
    void GoblinHorn()
    {
        CSoundBank.Instance.GoblinRangedAttack(gameObject);
    }
}
