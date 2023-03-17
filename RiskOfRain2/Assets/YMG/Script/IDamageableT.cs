using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageableT
{
    void TakeHit(float damage, RaycastHit hit);
}
