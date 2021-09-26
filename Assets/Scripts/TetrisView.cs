using Unity.Mathematics;
using UnityEngine;

public class TetrisView : MonoBehaviour
{
	[SerializeField] private SpriteRenderer _blockTpl;
	[SerializeField] private float2 _margin;
	[SerializeField] private Color[] _colors =
	{
		new Color32(120, 37, 179, 128),
		new Color32(100, 179, 179, 128),
		new Color32(80, 34, 22, 128),
		new Color32(80, 134, 22, 128),
		new Color32(180, 34, 22, 128),
		new Color32(180, 34, 122, 128),
	};

	private TetrisModel _model;
	private SpriteRenderer[,] _blocks;

	public void Init(TetrisModel model)
	{
		_model = model;
		
		if (_blocks != null) 
			ResetField();
		CreateField();
	}

	private void ResetField()
	{
		foreach (var image in _blocks)
			Destroy(image.gameObject);
	}

	private void CreateField()
	{
		var width = _model.Dimensions.x;
		var height = _model.Dimensions.y;
		_blocks = new SpriteRenderer[width, height];
		var tplExtents = _blockTpl.sprite.bounds.size;
		float2 offset = new float2(tplExtents.x, tplExtents.y) + _margin;
		for (int y = 0; y < height; y++)
		{
			for (int x = 0; x < width; x++)
			{
				var instance = Instantiate(_blockTpl, transform);
				var pos2d = offset * new float2(x, -y);
				instance.transform.localPosition = new Vector3(pos2d.x, pos2d.y, 0);
				_blocks[x, y] = instance;
			}
		}
	}

	private void Update()
	{
		if(_model == null)
			return;
		
		var viewData = _model.GetViewData();
		var width = _model.Dimensions.x;
		var height = _model.Dimensions.y;

		for (int y = 0; y < height; y++)
		{
			for (int x = 0; x < width; x++)
			{
				SpriteRenderer image = _blocks[x, y];
				FieldElement block = viewData[x, y];
				var color = block.Occupied ? _colors[block.Color] : Color.white;
				color.a = 0.5f;
				image.color = color;
			}
		}
	}
}