using Godot;
using System;

public partial class Letters : Node2D
{
	// Called when the node enters the scene tree for the first time.

	private Lamp _lamp;

	private string targetText;
	private string fullText;

	private Random random = new Random();

	private static readonly string[] Dictionary =
	{
		"planet",
		"satellite",
		"moon",
		"galaxy",
		"telescope",
		"bright",
		"dark"
	};

	public void GenerateText()
	{
		targetText = Dictionary[random.Next(Dictionary.Length)];

		var alphabet = "abcdefghijklmnopqrstuvwxyz";
		foreach (char c in targetText)
		{
			alphabet = alphabet.Replace(c.ToString(), "");
		}

		var alphabetChars = alphabet.ToCharArray();
		
		var n = alphabetChars.Length;
		while (n > 1)
		{
			n--;
			var k = random.Next(n + 1);
			(alphabetChars[k], alphabetChars[n]) = (alphabetChars[n], alphabetChars[k]);
		}

		var idx = 0;
		fullText = targetText;
		while (fullText.Length < 10)
		{
			var insertionPoint = random.Next(fullText.Length + 1);
			fullText = fullText.Insert(insertionPoint, alphabetChars[idx].ToString());
			idx++;
		}
	}
	
	public override void _Ready()
	{
		GenerateText();
		for (int i = 0; i < 10; i++)
		{
			var label = new Label();
			label.HorizontalAlignment = HorizontalAlignment.Center;
			label.Position = new Vector2(300 + i * 50, 360);
			label.Text = fullText[i].ToString();
			label.Name = "Char" + i;
			label.LabelSettings = new LabelSettings();
			label.LabelSettings.FontSize = 36;
			AddChild(label);

			_lamp = GetNode<Lamp>("../Lamps/Lamp" + i);
			_lamp.Lbl = label;
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
