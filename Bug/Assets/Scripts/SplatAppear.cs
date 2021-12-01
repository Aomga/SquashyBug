using UnityEngine;
using DG.Tweening;

public class SplatAppear : MonoBehaviour
{
    [SerializeField] float appearDuration = 0.1f;

    [SerializeField] Color[] colors;

    void Start()
    {
        float randomScaleAmount = Random.Range(0.2f, .8f);
        
        Vector3 randomScale = new Vector3(randomScaleAmount,randomScaleAmount, Random.Range(0.01f, 0.05f));
        transform.DOScale(randomScale, appearDuration);

        float randomRotationAmount = Random.Range(-360, 360);

        transform.DORotate(new Vector3(-90, randomRotationAmount, 0), appearDuration);

        GetComponent<MeshRenderer>().material.color = colors[Random.Range(0, colors.Length)];

        transform.DOScale(Vector3.zero, 10f).SetDelay(10f).OnComplete(() => {
            Destroy(gameObject);
        });
    }
}
