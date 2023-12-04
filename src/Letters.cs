using Godot;
using System;

public partial class Letters : Node2D
{
	// Called when the node enters the scene tree for the first time.

	private Lamp _lamp;
	public override void _Ready()
	{
		var text = "ligma";
		for (int i = 0; i < 10; i++)
		{
			var label = new Label();
			label.HorizontalAlignment = HorizontalAlignment.Center;
			label.Position = new Vector2(300 + i * 50, 360);
			label.Text = i < text.Length ? text[i].ToString() : "E";
			label.Name = "Char" + i;
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
