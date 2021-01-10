using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WordBank : MonoBehaviour
{
    // TODO: need to recycle the word list if it runs out. also need a random list every time.
    public WordBankUtil wordBankUtil;
    int wordListIndex;

    private List<string> wordList = new List<string>();

    private void Awake()
    {
        wordListIndex = 0;
        wordList.AddRange(wordBankUtil.GetWordBank());
        Shuffle(wordList);
    }

    private void Shuffle(List<string> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            int random = Random.Range(i, list.Count);
            string temporary = list[i];

            list[i] = list[random];
            list[random] = temporary;
        }
    }

    private void ConvertToLower(List<string> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            list[i] = list[i].ToLower();
        }
    }

    public string GetNextWord()
    {
        string newWord = string.Empty;
        if (wordList == null || wordList.Count == 0)        // null/empty check
            return newWord;

        newWord = wordList[wordListIndex];
        wordListIndex++;

        if (wordList.Count <= wordListIndex)
        {
            wordListIndex = 0;
            Shuffle(wordList);      // TODO see if we need to pre shuffle as we approach the end of the list. - maybe needed for massive word lists.
        }

        return newWord;
    }
}
