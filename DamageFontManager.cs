using UnityEngine;
using TMPro;
using DG.Tweening;




// 다음 몬스터를 타격할 때 밀려서 출력이 되어야 정상입니다.
// 해당 공격 모션이 진행중이여도 타겟이 사라지면 모션은 나가지만 데미지 폰트와 데미지 처리는 적용되지 않습니다.


public class DamageFontManager : MonoBehaviour
{
    public Transform canvas; // UI 캔버스

    public Transform targetFieldParent;
    public Transform targetField;

    private TMP_Text[] activeDamageFonts;

    private float timeBetweenDamageFonts = 1.0f;
    private int maxActiveFonts = 10;
    private int indexer = 0;
    private float step = 0.4f;

    private Vector3 fontPostion;     //show를 출력 시 받습니다.
    public bool IsNewPosition => timer >= timeBetweenDamageFonts || indexer > (maxActiveFonts - 1);

    private float timer = .0f;

    private Sequence GetAnimation(TMP_Text text)
    {
        return DOTween.Sequence().
           SetAutoKill(false).
           OnStart(() => { text.alpha = 1f; }).
           Prepend(text.rectTransform.DOScale(2.0f, 0.5f)).
           Append(text.rectTransform.DOScale(1.0f, 0.5f)).
           Join(text.DOFade(.0f, 1.0f)).
           AppendCallback(() => { PoolingManager.Return(text.gameObject); }).
           Pause();
    }

    private void Start() 
    {
        activeDamageFonts = new TMP_Text[maxActiveFonts * 2];
    }
    public void Update() 
    {
        if (timer < timeBetweenDamageFonts) timer += Time.deltaTime;

        //transform.position = Camera.main.WorldToScreenPoint(targetFieldParent.TransformPoint(targetField.position));
    }


    public void ShowDamageFont(int dmg, Vector3 position)
    {
        if (IsNewPosition)
        {
            fontPostion = position;

            for (int i = 0; i < indexer; i++) activeDamageFonts[i] = null;

            indexer = 0;

            timer -= timeBetweenDamageFonts;
        }

        GameObject obj = PoolingManager.Lental("DamageFont");
        obj.transform.SetParent(transform);

        DamageFont font = obj.GetComponent<DamageFont>();

        if (font.seq == null) font.SetSeqeunce = GetAnimation(font.text);

        activeDamageFonts[indexer] = font.text;

        if (font != null) indexer++;

        font.text.text = dmg.ToString();

        //font.gameObject.transform.position = Camera.main.WorldToScreenPoint(fontPostion + new Vector3(Random.Range(.0f, .2f), indexer * step, 0));
        font.gameObject.transform.position = Camera.main.WorldToScreenPoint(fontPostion + new Vector3(Random.Range(.0f, .2f), indexer * step, 0));

        font.gameObject.SetActive(true);

        font.seq?.Restart();
    }

    public Vector3 TargetWorldPostion() => targetFieldParent.TransformPoint(targetField.position + fontPostion);
}