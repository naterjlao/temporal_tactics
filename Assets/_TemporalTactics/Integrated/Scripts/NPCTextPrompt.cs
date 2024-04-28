using System.Collections;
using System.Collections.Generic;
using TMPro;
using NaughtyAttributes;
using UnityEngine;

[System.Serializable]
public class prompt_text
{
    public string Text;
    public float LineDelay; 
};

[System.Serializable]
public class NPCTextPrompt : MonoBehaviour
{
    public new GameObject camera;
    public float CharacterDelay;
    public List<prompt_text> Prompt;
    public GameObject TextObject;
    public GameObject PanelObject;
    private TextMeshProUGUI textMeshPro;
    private bool writing_text;

    // Start is called before the first frame update
    void Start()
    {
        textMeshPro = TextObject.GetComponent<TextMeshProUGUI>();
        this.Hide();
        writing_text = false;
#if false
        Debug.Log(textMeshPro);
        Component[] components = TextObject.GetComponents(typeof(Component));
        foreach(Component component in components) {
            Debug.Log(component.ToString());
        }
#endif
    }

    void Update()
    {
        transform.rotation = Quaternion.LookRotation(transform.position - camera.transform.position);
        if (!writing_text)
        {
            textMeshPro.text = "";
        }
    }

    [Button]
    public void Hide()
    {
        //textMeshPro.text = "";
        writing_text = false;
        TextObject.SetActive(false);
        PanelObject.SetActive(false);
    }

    [Button]
    public void Show()
    {
        writing_text = true;
        TextObject.SetActive(true);
        PanelObject.SetActive(true);
        StartCoroutine(typeout());
    }

    IEnumerator typeout()
    {
        foreach (prompt_text line in Prompt)
        {
            foreach(char c in line.Text)
            {
                textMeshPro.text += c;
                yield return new WaitForSeconds(CharacterDelay);
            }
            textMeshPro.text += "\n";
            yield return new WaitForSeconds(line.LineDelay);
        }
    }
}
