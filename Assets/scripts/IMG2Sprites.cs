using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class IMG2Sprites
{
	//Results from LoadAndCropAllFilesToSprites()
	//public static List<Sprite> returnResults_Sprites = new List<Sprite>();
	public static Dictionary<int, List<Sprite>> returnResults_Sprites = new Dictionary<int, List<Sprite>>();
	//Crop & return all sprites in the folder
	public static IEnumerator LoadAndCropAllFilesToSprites(string FilePath, float PixelsPerUnit = 100.0f, SpriteMeshType spriteType = SpriteMeshType.Tight, float CropX = 512, float CropY = 512)
	{
		returnResults_Sprites.Clear();
		//List<Sprite> NewSprites = new List<Sprite>();
		Dictionary<int, List<Sprite>> NewSprites = new Dictionary<int, List<Sprite>>();

		if (Directory.Exists(FilePath))
		{
			var fileInfo = Directory.GetFiles(FilePath);

			int puzzleNum = 0;
			foreach (string fileExactPath in fileInfo)
			{
				if (!fileExactPath.Contains(".meta"))
				{
					//Debug: Do not use ToString() to convert the path that contains StreamingAssets folder 
					List<Sprite> tempSprites = new List<Sprite>();
					Texture2D SpriteTexture = LoadTexture(fileExactPath);

					if (SpriteTexture != null)
					{
						int ReadXCount = Mathf.FloorToInt(SpriteTexture.width / CropX);
						int ReadYCount = Mathf.FloorToInt(SpriteTexture.height / CropY);
						if (ReadXCount > 0 && ReadYCount > 0)
						{
							for (int ReadY = ReadYCount - 1; ReadY >= 0; ReadY--)
							{
								for (int ReadX = 0; ReadX < ReadXCount; ReadX++)
								{
									tempSprites.Add(Sprite.Create(SpriteTexture, new Rect(ReadX * CropX, ReadY * CropY, CropX, CropY), new Vector2(0.5f, 0.5f), PixelsPerUnit, 0, spriteType));
									yield return new WaitForEndOfFrame();
								}
							}
						}
					}
					//End

					if (tempSprites.Count > 0)
					{
						List<Sprite> tempList = new List<Sprite>();
						for (int i = 0; i < tempSprites.Count; i++)
						{
							tempSprites[i].name = "puzzlePiece_" + puzzleNum + "_"+ i;
							tempList.Add(tempSprites[i]);
						}

						NewSprites.Add(puzzleNum, tempList);
						/*foreach (Sprite spriteElement in tempSprites)
						{
							NewSprites.Add(spriteElement);
						}*/
						//e.g. After loading a character clothes
						//Wait for a frame
						yield return new WaitForEndOfFrame();
					}

					puzzleNum++;
				}
			}
		}
		returnResults_Sprites = NewSprites;
		yield return "Crop all Sprites";
		//return NewSprites;
	}

	static Texture2D LoadTexture(string FilePath)
	{
		Texture2D Tex2D = null;
		byte[] FileData = null;

		if (File.Exists(FilePath))
		{
			FileData = File.ReadAllBytes(FilePath);
			Tex2D = new Texture2D(2, 2);
		}

		if (Tex2D.LoadImage(FileData))
			return Tex2D;

		return null;
	}
}
