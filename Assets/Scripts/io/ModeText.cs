using UnityEngine;
using UnityEngine.UI;

public class ModeText : MonoBehaviour {
	public Text heading;
	public Text subheading;

	public void Clear() {
		Print ("", "");
	}

	public void Print(string headingText, string subheadingText) {
		heading.text = headingText;
		subheading.text = subheadingText;
	}
}
