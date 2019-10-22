using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextTranslator : MonoBehaviour
{
  [SerializeField]
  private string textId;

  void Start()
  {
    Text text = GetComponent<Text>();
    TextMeshProUGUI textMesh = GetComponent<TextMeshProUGUI>();

    if (text == null && textMesh == null)
    {
      return;
    }

    string textValue;

    if (textId == "ISOCode")
    {
      textValue = I18n.GetLanguage();
    }
    else
    {
      textValue = I18n.Fields[textId];
    }

    if (textMesh != null)
    {
      textMesh.text = textValue;
    }
    else if (text != null)
    {
      text.text = textValue;
    }
  }
}