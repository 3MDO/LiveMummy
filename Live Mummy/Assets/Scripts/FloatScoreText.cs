using TMPro;
using UnityEngine;
using DG.Tweening;
public class FloatScoreText : MonoBehaviour
{
    [SerializeField] float _floatSpeed = 5f;

    public void SetScoreValue(int multiplier)
    {
        var text = GetComponent<TMP_Text>();
        text.SetText("x " + multiplier);

        if (multiplier < 3)
            text.color = Color.white;
        if (multiplier < 10)
            text.color = Color.green;
        if (multiplier < 3)
            text.color = Color.yellow;
        if (multiplier < 3)
            text.color = Color.red;

        Destroy(gameObject, 1f);
        text.DOFade(0.0F, 2.0F);
    }
    private void Update()
    {

        transform.position += transform.up * Time.deltaTime * _floatSpeed;
    }
    
}