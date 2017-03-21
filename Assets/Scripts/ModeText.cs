using UnityEngine.UI;

public class ModeText {
	private Text heading;
	private Text subheading;

	public ModeText(Text heading, Text subheading) {
		this.heading = heading;
		this.subheading = subheading;
	}

	public void Clear() {
		Print ("", "");
	}

	public void Print(string headingText, string subheadingText) {
		heading.text = headingText;
		subheading.text = subheadingText;
	}
}
