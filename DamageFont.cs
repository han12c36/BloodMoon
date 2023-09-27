using UnityEngine;
using TMPro;
using DG.Tweening;


public class DamageFont : MonoBehaviour
{
    public TMP_Text text;
    public Sequence seq;
    public Sequence SetSeqeunce { set { seq = value; } }
}