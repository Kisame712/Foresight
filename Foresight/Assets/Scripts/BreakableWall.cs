using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableWall : MonoBehaviour
{
   public void BreakOnAttack()
    {
        Destroy(gameObject);
    }
}
