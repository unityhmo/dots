using UnityEngine;
using UnityEngine.UI;

public class TextTranslator : MonoBehaviour {
    [SerializeField] private string textId;

    void Start() {
        Text text = GetComponent<Text>();

        if (text == null) {
            return;
        }

        if (textId == "ISOCode") {
            text.text = I18n.GetLanguage();
        } else {
            text.text = I18n.Fields[textId];
        }
    }
}