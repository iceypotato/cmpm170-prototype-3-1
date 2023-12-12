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
			label.LabelSettings.FontSize = 100;
			label.LabelSettings.Font = GD.Load<FontFile>("res://assets/Constellation-Awlx.ttf");
			AddChild(label);

			_lamp = GetNode<Lamp>("../Lamps/Lamp" + i);
			_lamp.Lbl = label;
		}
	}

	private const double AnimTime = 1.0;
	private double animLeft = 0.0;
	
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		string enabledText = "";
		for (int i = 0; i < 10; i++)
		{
			var lamp = GetNode<Lamp>("../Lamps/Lamp" + i);

			if (!lamp.IsLampOn)
				enabledText += lamp.Lbl.Text;
		}

		if (enabledText == targetText)
		{
			animLeft = AnimTime;
			
			GenerateText();
			
			for (int i = 0; i < 10; i++)
			{
				var lamp = GetNode<Lamp>("../Lamps/Lamp" + i);
				lamp.IsLampOn = false;
				lamp.Lbl.Text = fullText[i].ToString();
			}
		}

		double redBlue = Math.Clamp((AnimTime - animLeft) / AnimTime, 0.0, 1.0);
		
		for (int i = 0; i < 10; i++)
		{
			var lamp = GetNode<Lamp>("../Lamps/Lamp" + i);
			lamp.Lbl.LabelSettings.FontColor = new Color((float)redBlue, 1.0F, (float)redBlue);
		}

		animLeft -= delta;
	}
}
