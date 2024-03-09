using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

using UnityEngine;

public class GradientToTexture : MonoBehaviour
{
    public Gradient gradient;

    [Header("Params")]
    public string textureName = "GradientTexture";
    public Vector2 Dimensions = new Vector2(100, 10);

    public Texture2D texture;
    [SerializeField] Material materialToApply;


    [Button]
    private void CreateTextureFromGradient(bool saveToAssets = false)
    {
        // Create a new texture with dimensions 100x1
        texture = new Texture2D((int)Dimensions.x, (int)Dimensions.y);
        texture.name = textureName;

        // Loop through each pixel and set its color based on the gradient
        for (int x = 0; x < texture.width; x++)
        {
            float t = (float)x / (float)texture.width;
            Color color = gradient.Evaluate(t);

            for (int i = 0; i < texture.height; i++)
            {
                texture.SetPixel(x, i, color);
            }
        }

        // Apply changes and save the texture
        texture.Apply();

        if (saveToAssets) SaveTexture();
        if (materialToApply) materialToApply.mainTexture = texture;
    }

    [Button]
    void SaveTexture()
    {
        // Encode the texture into PNG format
        byte[] bytes = texture.EncodeToPNG();

        // Define the file path
        string filePath = Application.dataPath + "/" + textureName + ".png";

        // Write the PNG file to disk
        System.IO.File.WriteAllBytes(filePath, bytes);

        Debug.Log("Texture saved at: " + filePath);
    }
}
