using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HW_Week12 : MonoBehaviour
{
    public static HW_Week12 instance;
    public List<Image> puzzlePieceImages;
    public List<Vector2> startingPositions = new List<Vector2>();
    public Dictionary<int, List<Sprite>> puzzlePieces;
    public GameObject puzzleButtonPrefab;
    public GameObject puzzleButtonParent;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        StartCoroutine(LoadSprites());
        foreach (var image in puzzlePieceImages)
        {
            startingPositions.Add(image.GetComponent<RectTransform>().anchoredPosition);
        }
    }

    public IEnumerator LoadSprites()
    {
        yield return StartCoroutine(IMG2Sprites.LoadAndCropAllFilesToSprites(Application.streamingAssetsPath + "/images"));
        puzzlePieces = IMG2Sprites.returnResults_Sprites;
        InstantiatePuzzleButtons();

        yield return "loaded Sprites";
    }

    private void InstantiatePuzzleButtons()
    {
        for (int i = 0; i < puzzlePieces.Count; i++)
        {
            var puzzleButtonGO = Instantiate(puzzleButtonPrefab, puzzleButtonParent.transform);
            PuzzleSwitchButton puzzleSwitchButton = puzzleButtonGO.GetComponent<PuzzleSwitchButton>();
            puzzleSwitchButton.buttonText.text = "Puzzle: " + i;
            puzzleSwitchButton.puzzleNum = i;
        }
    }

}
