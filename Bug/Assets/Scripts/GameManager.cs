using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    private void Awake() {
        DOTween.SetTweensCapacity(1000, 50);
    }
}
