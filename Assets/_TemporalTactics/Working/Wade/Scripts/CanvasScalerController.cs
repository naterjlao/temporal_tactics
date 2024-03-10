using UnityEngine;
using UnityEngine.UI;

public class CanvasScalerController : UnityEngine.EventSystems.UIBehaviour
{
    // Reference resolution
    public Vector2 referenceResolution = new Vector2(1600f, 900f);

    // Match value at reference resolution
    public float matchValueAtReference = 0.5f;

    // Reference to the Canvas Scaler component
    private CanvasScaler canvasScaler;

    // Reference to the RectTransform
    private RectTransform rectTransform;

    protected override void Start()
    {
        base.Start();

        // Get the Canvas Scaler component attached to the Canvas
        canvasScaler = GetComponent<CanvasScaler>();

        // Set the scale mode to scale with screen size
        canvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;

        // Set the reference resolution
        canvasScaler.referenceResolution = referenceResolution;

        // Get the RectTransform component
        rectTransform = GetComponent<RectTransform>();

        // Calculate and set the match value based on the aspect ratio
        UpdateMatchValue();
    }

    protected override void OnRectTransformDimensionsChange()
    {
        base.OnRectTransformDimensionsChange();

        // Calculate and set the match value based on the aspect ratio
        UpdateMatchValue();
    }

    void UpdateMatchValue()
    {
        // Calculate the aspect ratio
        float aspectRatio = (float)Screen.width / Screen.height;

        // Calculate the match value based on the aspect ratio and reference resolution
        float matchValue = matchValueAtReference * (referenceResolution.y / referenceResolution.x) / aspectRatio;

        // Set the match value
        if (!canvasScaler) canvasScaler = GetComponent<CanvasScaler>();
        canvasScaler.matchWidthOrHeight = (matchValue > 1) ? 1 : matchValue;
    }
}
