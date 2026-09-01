using UnityEngine;
using UnityEngine.UI;

public class JetpackHeatMeter : MonoBehaviour
{
    [Header("Hõmérséklet beállítások")]
    [SerializeField] private float heatIncreaseRate = 0.5f;
    [SerializeField] private float heatDecreaseRate = 0.25f;
    [SerializeField] private float overheatExplodeDelay = 1.5f;
    [SerializeField] private float resumeThreshold = 0.1f;
    [SerializeField] private float shakeThreshold = 0.95f;

    [Header("UI")]
    [SerializeField] private Image heatFillImage;
    [SerializeField] private RectTransform meterRect;

    [Header("Hang")]
    [SerializeField] private AudioClip warningSound;
    [SerializeField] private AudioSource warningAudioSource;

    private float heat = 0f;
    private bool isOverheated = false;
    private float overheatTimer = 0f;
    private Vector2 originalPos;

    public bool IsOverheated => isOverheated;

    void Start()
    {
        if (meterRect != null) originalPos = meterRect.anchoredPosition;
    }

    public void UpdateHeat(bool isThrusting, JetpackController controller)
    {
        heat += (isThrusting ? heatIncreaseRate : -heatDecreaseRate) * Time.deltaTime;
        heat = Mathf.Clamp01(heat);

        if (heat >= 1f) isOverheated = true;
        if (heat <= resumeThreshold) isOverheated = false;

        UpdateVisuals();

        if (isOverheated && isThrusting)
        {
            overheatTimer += Time.deltaTime;
            if (overheatTimer >= overheatExplodeDelay)
            {
                controller.Explode();
            }
        }
        else
        {
            overheatTimer = 0f;
        }
    }

    void UpdateVisuals()
    {
        if (heatFillImage == null) return;

        heatFillImage.fillAmount = heat;
        heatFillImage.color = heat < 0.5f
            ? Color.Lerp(Color.green, Color.yellow, heat / 0.5f)
            : Color.Lerp(Color.yellow, Color.red, (heat - 0.5f) / 0.5f);

        bool shouldShake = heat >= shakeThreshold;

        if (shouldShake && meterRect != null)
        {
            meterRect.anchoredPosition = originalPos + new Vector2(Random.Range(-3f, 3f), Random.Range(-3f, 3f));

            if (warningAudioSource != null && !warningAudioSource.isPlaying)
            {
                warningAudioSource.Play();
            }
        }
        else
        {
            if (meterRect != null) meterRect.anchoredPosition = originalPos;

            if (warningAudioSource != null && warningAudioSource.isPlaying)
            {
                warningAudioSource.Stop();
            }
        }
    }
}