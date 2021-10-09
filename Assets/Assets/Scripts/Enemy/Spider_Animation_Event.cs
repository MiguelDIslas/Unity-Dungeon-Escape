using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider_Animation_Event : MonoBehaviour
{

    private Spider _spider;

    private void Awake() => _spider = transform.parent.GetComponent<Spider>();

    public void Fire() => _spider.Attack();

}
