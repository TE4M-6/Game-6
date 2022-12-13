using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageScript : MonoBehaviour
{
    [SerializeField] private Material flashMaterial;

    private SpriteRenderer spriteRender;
    private Material ogMaterial;
    private Coroutine flashRoutine;
    // Start is called before the first frame update
    void Start()
    {
        spriteRender = GetComponent<SpriteRenderer>();
        ogMaterial = spriteRender.material;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Flash()
    {
        if (flashRoutine != null)
        {
            StopCoroutine(flashRoutine);
        }

        flashRoutine = StartCoroutine(FlashRoutine());
    }

    private IEnumerator FlashRoutine()
    {
        spriteRender.material = flashMaterial;
        yield return new WaitForSeconds(0.125f);
        spriteRender.material = ogMaterial;
        flashRoutine = null;
    }
}
