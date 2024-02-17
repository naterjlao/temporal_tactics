using UnityEngine;
using UnityEditor;

public class TextureToSpritePostProcessor : AssetPostprocessor
{
    void OnPreprocessTexture()
    {
        // Check if the texture is being imported into a folder named "UI", "Sprite", or "Sprites"
        string folderPath = System.IO.Path.GetDirectoryName(assetPath);
        string folderName = System.IO.Path.GetFileName(folderPath);
        if (folderName.Equals("UI", System.StringComparison.OrdinalIgnoreCase) ||
            folderName.Equals("Sprite", System.StringComparison.OrdinalIgnoreCase) ||
            folderName.Equals("Sprites", System.StringComparison.OrdinalIgnoreCase))
        {
            TextureImporter textureImporter = (TextureImporter)assetImporter;
            textureImporter.textureType = TextureImporterType.Sprite;
            textureImporter.spriteImportMode = SpriteImportMode.Single;
            textureImporter.spritePixelsPerUnit = 100; // You can adjust this value as per your requirement
            textureImporter.mipmapEnabled = false; // Mipmaps are generally not needed for sprites
        }
    }
}
