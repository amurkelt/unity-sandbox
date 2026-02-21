using System.Collections;
using UnityEngine;

public class Dissolve : MonoBehaviour
{
    [SerializeField] private float dissolveTime = 0.75f;

    private SpriteRenderer[] spriteRenderers;
    private Material[] materials;

    private int dissolveAmount = Shader.PropertyToID("_DissolveAmount");
    private int verticalDissolveAmount = Shader.PropertyToID("_VerticalDissolve");

    private void Start()
    {
        spriteRenderers = GetComponentsInChildren<SpriteRenderer>();

        materials = new Material[spriteRenderers.Length];
        for (int i = 0; i < spriteRenderers.Length; i++)
        {
            materials[i] = spriteRenderers[i].material;
        }
    }

    [ContextMenu("Vanish")]
    public void Vanish()
    {
        StartCoroutine(Vanish(true, false));
    }

    [ContextMenu("Appear")]
    public void Appear()
    {
        StartCoroutine(Appear(true, false));
    }

    private IEnumerator Vanish(bool useDissolve, bool useVertical)
    {
        float elapsedTime = 0f;
        while (elapsedTime < dissolveTime)
        {
            elapsedTime += Time.deltaTime;

            float lerpedDissolve = Mathf.Lerp(0f, 1.1f, (elapsedTime / dissolveTime));
            float lerpedVerticalDissolve = Mathf.Lerp(0f, 1.1f, (elapsedTime / dissolveTime));

            for (int i = 0; i < materials.Length; i++)
            {
                if (useDissolve)
                    materials[i].SetFloat(dissolveAmount, lerpedDissolve);

                if (useVertical)
                    materials[i].SetFloat(verticalDissolveAmount, lerpedVerticalDissolve);
            }

            yield return null;
        }
    }

    private IEnumerator Appear(bool useDissolve, bool useVertical)
    {
        float elapsedTime = 0f;
        while (elapsedTime < dissolveTime)
        {
            elapsedTime += Time.deltaTime;

            float lerpedDissolve = Mathf.Lerp(1.1f, 0f, (elapsedTime / dissolveTime));
            float lerpedVerticalDissolve = Mathf.Lerp(1.1f, 0f, (elapsedTime / dissolveTime));

            for (int i = 0; i < materials.Length; i++)
            {
                if (useDissolve)
                    materials[i].SetFloat(dissolveAmount, lerpedDissolve);

                if (useVertical)
                    materials[i].SetFloat(verticalDissolveAmount, lerpedVerticalDissolve);
            }

            yield return null;
        }
    }
}
