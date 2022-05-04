using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class PuzzleSwitchButton : MonoBehaviour
{
    public TMP_Text buttonText;
    
    [HideInInspector]
    public int puzzleNum;

    public void OnPuzzleButtonClick()
    {
        for (int i = 0; i < HW_Week12.instance.puzzlePieceImages.Count; i++)
        {
            HW_Week12.instance.puzzlePieceImages[i].sprite = HW_Week12.instance.puzzlePieces[puzzleNum][i];
        }

        List<Vector2> tempPositions = new List<Vector2>();
        for (int i = 0; i < HW_Week12.instance.startingPositions.Count; i++)
        {
            tempPositions.Add(HW_Week12.instance.startingPositions[i]);
        }

        foreach (var image in HW_Week12.instance.puzzlePieceImages)
        {
            int randomNum = Random.Range(0, tempPositions.Count);
            image.GetComponent<RectTransform>().anchoredPosition = tempPositions[randomNum];
            tempPositions.Remove(tempPositions[randomNum]);
        }
    }
}
