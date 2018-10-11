using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using benedictCumberbatch;

public static class Extensions
{
    private static System.Random rng = new System.Random();
    public static void Shuffle<T>(this IList<T> list)
    {
        int n = list.Count;
        while (n > 1) {
            n--;
            int k = rng.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
}

public class benedictCumberbatchScript : MonoBehaviour
{
    public KMBombInfo Bomb;
    public KMAudio Audio;

    public KMSelectable leftUp;
    public KMSelectable leftDown;
    public KMSelectable rightUp;
    public KMSelectable rightDown;
    public KMSelectable submit;
    private bool buttonLock;
    int totalModules = 0;
    int solvedModules = 0;
    bool solveAddition;
    int totalDigits = 0;
    int totalLetters = 0;

    public TextMesh leftScreen;
    public string[] leftScreenOptions;
    private int leftDisplayedScreen = 0;
    public Animator[] leftCards;
    private bool leftMovingUp;
    public TextMesh[] leftCardsText;
    public string displayedLeftWord = "";
    private int leftBase;
    public string[] prefixes;
    public string[] shuffledPrefixes;
    private int benIndex = 0;
    private string correctLeftAnswer = "";

    public TextMesh rightScreen;
    public string[] rightScreenOptions;
    private int rightDisplayedScreen = 0;
    public Animator[] rightCards;
    private bool rightMovingUp;
    public TextMesh[] rightCardsText;
    public string displayedRightWord = "";
    private int rightBase;
    public string[] suffixes;
    public string[] shuffledSuffixes;
    private int cumIndex = 0;
    private string correctRightAnswer = "";

    string leftScreenSaved = "";
    string rightScreenSaved = "";
    private int leftIndex = 0;
    private int rightIndex = 0;


    //Logging
    static int moduleIdCounter = 1;
    int moduleId;

    void Awake()
    {
        moduleId = moduleIdCounter++;
        leftUp.OnInteract += delegate () { OnLeftUpButton(); return false; };
        leftDown.OnInteract += delegate () { OnLeftDownButton(); return false; };
        rightUp.OnInteract += delegate () { OnRightUpButton(); return false; };
        rightDown.OnInteract += delegate () { OnRightDownButton(); return false; };
        submit.OnInteract += delegate () { OnSubmitButton(); return false; };
    }

    void Start()
    {
        totalModules = Bomb.GetSolvableModuleNames().Count;
        benIndex = UnityEngine.Random.Range(0,29);
        if(benIndex == 0 || benIndex == 1 || benIndex == 2 || benIndex == 3 || benIndex == 4)
        {
            for(int i = 0; i <= 9; i++)
            {
                shuffledPrefixes[i] = prefixes[i];
            }
            shuffledPrefixes.Shuffle();
            for(int i = 0; i <= 9; i++)
            {
                leftCardsText[i].text = shuffledPrefixes[i];
            }
            leftScreen.text = leftScreenOptions[0];
            leftBase = 0;
        }
        else if(benIndex == 5 || benIndex == 6 || benIndex == 7 || benIndex == 8 || benIndex == 9)
        {
            for(int i = 0; i <= 9; i++)
            {
                shuffledPrefixes[i] = prefixes[i+10];
            }
            shuffledPrefixes.Shuffle();
            for(int i = 0; i <= 9; i++)
            {
                leftCardsText[i].text = shuffledPrefixes[i];
            }
            leftScreen.text = leftScreenOptions[1];
            leftBase = 10;
        }
        else if(benIndex == 10 || benIndex == 11 || benIndex == 12 || benIndex == 13 || benIndex == 14)
        {
            for(int i = 0; i <= 9; i++)
            {
                shuffledPrefixes[i] = prefixes[i+20];
            }
            shuffledPrefixes.Shuffle();
            for(int i = 0; i <= 9; i++)
            {
                leftCardsText[i].text = shuffledPrefixes[i];
            }
            leftScreen.text = leftScreenOptions[2];
            leftBase = 20;
        }
        else if(benIndex == 15 || benIndex == 16 || benIndex == 17 || benIndex == 18 || benIndex == 19)
        {
            for(int i = 0; i <= 9; i++)
            {
                shuffledPrefixes[i] = prefixes[i+30];
            }
            shuffledPrefixes.Shuffle();
            for(int i = 0; i <= 9; i++)
            {
                leftCardsText[i].text = shuffledPrefixes[i];
            }
            leftScreen.text = leftScreenOptions[3];
            leftBase = 30;
        }
        else if(benIndex == 20 || benIndex == 21 || benIndex == 22 || benIndex == 23 || benIndex == 24)
        {
            for(int i = 0; i <= 9; i++)
            {
                shuffledPrefixes[i] = prefixes[i+40];
            }
            shuffledPrefixes.Shuffle();
            for(int i = 0; i <= 9; i++)
            {
                leftCardsText[i].text = shuffledPrefixes[i];
            }
            leftScreen.text = leftScreenOptions[4];
            leftBase = 40;
        }
        else if(benIndex == 25)
        {
            int listDeterminer = UnityEngine.Random.Range(0,5);
            if(listDeterminer == 0)
            {
                for(int i = 0; i <= 9; i++)
                {
                    shuffledPrefixes[i] = prefixes[i];
                }
                leftBase = 0;
            }
            else if(listDeterminer == 1)
            {
                for(int i = 0; i <= 9; i++)
                {
                    shuffledPrefixes[i] = prefixes[i+10];
                }
                leftBase = 10;
            }
            else if(listDeterminer == 2)
            {
                for(int i = 0; i <= 9; i++)
                {
                    shuffledPrefixes[i] = prefixes[i+20];
                }
                leftBase = 20;
            }
            else if(listDeterminer == 3)
            {
                for(int i = 0; i <= 9; i++)
                {
                    shuffledPrefixes[i] = prefixes[i+30];
                }
                leftBase = 30;
            }
            else if(listDeterminer == 4)
            {
                for(int i = 0; i <= 9; i++)
                {
                    shuffledPrefixes[i] = prefixes[i+40];
                }
                leftBase = 40;
            }
            shuffledPrefixes.Shuffle();
            for(int i = 0; i <= 9; i++)
            {
                leftCardsText[i].text = shuffledPrefixes[i];
            }
            leftScreen.text = leftScreenOptions[5];
        }
        else if(benIndex == 26)
        {
            int listDeterminer = UnityEngine.Random.Range(0,5);
            if(listDeterminer == 0)
            {
                for(int i = 0; i <= 9; i++)
                {
                    shuffledPrefixes[i] = prefixes[i];
                }
                leftBase = 0;
            }
            else if(listDeterminer == 1)
            {
                for(int i = 0; i <= 9; i++)
                {
                    shuffledPrefixes[i] = prefixes[i+10];
                }
                leftBase = 10;
            }
            else if(listDeterminer == 2)
            {
                for(int i = 0; i <= 9; i++)
                {
                    shuffledPrefixes[i] = prefixes[i+20];
                }
                leftBase = 20;
            }
            else if(listDeterminer == 3)
            {
                for(int i = 0; i <= 9; i++)
                {
                    shuffledPrefixes[i] = prefixes[i+30];
                }
                leftBase = 30;
            }
            else if(listDeterminer == 4)
            {
                for(int i = 0; i <= 9; i++)
                {
                    shuffledPrefixes[i] = prefixes[i+40];
                }
                leftBase = 40;
            }
            shuffledPrefixes.Shuffle();
            for(int i = 0; i <= 9; i++)
            {
                leftCardsText[i].text = shuffledPrefixes[i];
            }
            leftScreen.text = leftScreenOptions[6];
        }
        else if(benIndex == 27)
        {
            int listDeterminer = UnityEngine.Random.Range(0,5);
            if(listDeterminer == 0)
            {
                for(int i = 0; i <= 9; i++)
                {
                    shuffledPrefixes[i] = prefixes[i];
                }
                leftBase = 0;
            }
            else if(listDeterminer == 1)
            {
                for(int i = 0; i <= 9; i++)
                {
                    shuffledPrefixes[i] = prefixes[i+10];
                }
                leftBase = 10;
            }
            else if(listDeterminer == 2)
            {
                for(int i = 0; i <= 9; i++)
                {
                    shuffledPrefixes[i] = prefixes[i+20];
                }
                leftBase = 20;
            }
            else if(listDeterminer == 3)
            {
                for(int i = 0; i <= 9; i++)
                {
                    shuffledPrefixes[i] = prefixes[i+30];
                }
                leftBase = 30;
            }
            else if(listDeterminer == 4)
            {
                for(int i = 0; i <= 9; i++)
                {
                    shuffledPrefixes[i] = prefixes[i+40];
                }
                leftBase = 40;
            }
            shuffledPrefixes.Shuffle();
            for(int i = 0; i <= 9; i++)
            {
                leftCardsText[i].text = shuffledPrefixes[i];
            }
            leftScreen.text = leftScreenOptions[7];
        }
        else if(benIndex == 28)
        {
            int listDeterminer = UnityEngine.Random.Range(0,5);
            if(listDeterminer == 0)
            {
                for(int i = 0; i <= 9; i++)
                {
                    shuffledPrefixes[i] = prefixes[i];
                }
                leftBase = 0;
            }
            else if(listDeterminer == 1)
            {
                for(int i = 0; i <= 9; i++)
                {
                    shuffledPrefixes[i] = prefixes[i+10];
                }
                leftBase = 10;
            }
            else if(listDeterminer == 2)
            {
                for(int i = 0; i <= 9; i++)
                {
                    shuffledPrefixes[i] = prefixes[i+20];
                }
                leftBase = 20;
            }
            else if(listDeterminer == 3)
            {
                for(int i = 0; i <= 9; i++)
                {
                    shuffledPrefixes[i] = prefixes[i+30];
                }
                leftBase = 30;
            }
            else if(listDeterminer == 4)
            {
                for(int i = 0; i <= 9; i++)
                {
                    shuffledPrefixes[i] = prefixes[i+40];
                }
                leftBase = 40;
            }
            shuffledPrefixes.Shuffle();
            for(int i = 0; i <= 9; i++)
            {
                leftCardsText[i].text = shuffledPrefixes[i];
            }
            leftScreen.text = leftScreenOptions[8];
        }


        cumIndex = UnityEngine.Random.Range(0,29);
        if(cumIndex == 0 || cumIndex == 1 || cumIndex == 2 || cumIndex == 3 || cumIndex == 4)
        {
            for(int i = 0; i <= 9; i++)
            {
                shuffledSuffixes[i] = suffixes[i];
            }
            shuffledSuffixes.Shuffle();
            for(int i = 0; i <= 9; i++)
            {
                rightCardsText[i].text = shuffledSuffixes[i];
            }
            rightScreen.text = rightScreenOptions[0];
            rightBase = 0;
        }
        else if(cumIndex == 5 || cumIndex == 6 || cumIndex == 7 || cumIndex == 8 || cumIndex == 9)
        {
            for(int i = 0; i <= 9; i++)
            {
                shuffledSuffixes[i] = suffixes[i+10];
            }
            shuffledSuffixes.Shuffle();
            for(int i = 0; i <= 9; i++)
            {
                rightCardsText[i].text = shuffledSuffixes[i];
            }
            rightScreen.text = rightScreenOptions[1];
            rightBase = 10;
        }
        else if(cumIndex == 10 || cumIndex == 11 || cumIndex == 12 || cumIndex == 13 || cumIndex == 14)
        {
            for(int i = 0; i <= 9; i++)
            {
                shuffledSuffixes[i] = suffixes[i+20];
            }
            shuffledSuffixes.Shuffle();
            for(int i = 0; i <= 9; i++)
            {
                rightCardsText[i].text = shuffledSuffixes[i];
            }
            rightScreen.text = rightScreenOptions[2];
            rightBase = 20;
        }
        else if(cumIndex == 15 || cumIndex == 16 || cumIndex == 17 || cumIndex == 18 || cumIndex == 19)
        {
            for(int i = 0; i <= 9; i++)
            {
                shuffledSuffixes[i] = suffixes[i+30];
            }
            shuffledSuffixes.Shuffle();
            for(int i = 0; i <= 9; i++)
            {
                rightCardsText[i].text = shuffledSuffixes[i];
            }
            rightScreen.text = rightScreenOptions[3];
            rightBase = 30;
        }
        else if(cumIndex == 20 || cumIndex == 21 || cumIndex == 22 || cumIndex == 23 || cumIndex == 24)
        {
            for(int i = 0; i <= 9; i++)
            {
                shuffledSuffixes[i] = suffixes[i+40];
            }
            shuffledSuffixes.Shuffle();
            for(int i = 0; i <= 9; i++)
            {
                rightCardsText[i].text = shuffledSuffixes[i];
            }
            rightScreen.text = rightScreenOptions[4];
            rightBase = 40;
        }
        else if(cumIndex == 25)
        {
            int listDeterminer = UnityEngine.Random.Range(0,5);
            if(listDeterminer == 0)
            {
                for(int i = 0; i <= 9; i++)
                {
                    shuffledSuffixes[i] = suffixes[i];
                }
                rightBase = 0;
            }
            else if(listDeterminer == 1)
            {
                for(int i = 0; i <= 9; i++)
                {
                    shuffledSuffixes[i] = suffixes[i+10];
                }
                rightBase = 10;
            }
            else if(listDeterminer == 2)
            {
                for(int i = 0; i <= 9; i++)
                {
                    shuffledSuffixes[i] = suffixes[i+20];
                }
                rightBase = 20;
            }
            else if(listDeterminer == 3)
            {
                for(int i = 0; i <= 9; i++)
                {
                    shuffledSuffixes[i] = suffixes[i+30];
                }
                rightBase = 30;
            }
            else if(listDeterminer == 4)
            {
                for(int i = 0; i <= 9; i++)
                {
                    shuffledSuffixes[i] = suffixes[i+40];
                }
                rightBase = 40;
            }
            shuffledSuffixes.Shuffle();
            for(int i = 0; i <= 9; i++)
            {
                rightCardsText[i].text = shuffledSuffixes[i];
            }
            rightScreen.text = rightScreenOptions[5];
        }
        else if(cumIndex == 26)
        {
            int listDeterminer = UnityEngine.Random.Range(0,5);
            if(listDeterminer == 0)
            {
                for(int i = 0; i <= 9; i++)
                {
                    shuffledSuffixes[i] = suffixes[i];
                }
                rightBase = 0;
            }
            else if(listDeterminer == 1)
            {
                for(int i = 0; i <= 9; i++)
                {
                    shuffledSuffixes[i] = suffixes[i+10];
                }
                rightBase = 10;
            }
            else if(listDeterminer == 2)
            {
                for(int i = 0; i <= 9; i++)
                {
                    shuffledSuffixes[i] = suffixes[i+20];
                }
                rightBase = 20;
            }
            else if(listDeterminer == 3)
            {
                for(int i = 0; i <= 9; i++)
                {
                    shuffledSuffixes[i] = suffixes[i+30];
                }
                rightBase = 30;
            }
            else if(listDeterminer == 4)
            {
                for(int i = 0; i <= 9; i++)
                {
                    shuffledSuffixes[i] = suffixes[i+40];
                }
                rightBase = 40;
            }
            shuffledSuffixes.Shuffle();
            for(int i = 0; i <= 9; i++)
            {
                rightCardsText[i].text = shuffledSuffixes[i];
            }
            rightScreen.text = rightScreenOptions[6];
        }
        else if(cumIndex == 27)
        {
            int listDeterminer = UnityEngine.Random.Range(0,5);
            if(listDeterminer == 0)
            {
                for(int i = 0; i <= 9; i++)
                {
                    shuffledSuffixes[i] = suffixes[i];
                }
                rightBase = 0;
            }
            else if(listDeterminer == 1)
            {
                for(int i = 0; i <= 9; i++)
                {
                    shuffledSuffixes[i] = suffixes[i+10];
                }
                rightBase = 10;
            }
            else if(listDeterminer == 2)
            {
                for(int i = 0; i <= 9; i++)
                {
                    shuffledSuffixes[i] = suffixes[i+20];
                }
                rightBase = 20;
            }
            else if(listDeterminer == 3)
            {
                for(int i = 0; i <= 9; i++)
                {
                    shuffledSuffixes[i] = suffixes[i+30];
                }
                rightBase = 30;
            }
            else if(listDeterminer == 4)
            {
                for(int i = 0; i <= 9; i++)
                {
                    shuffledSuffixes[i] = suffixes[i+40];
                }
                rightBase = 40;
            }
            shuffledSuffixes.Shuffle();
            for(int i = 0; i <= 9; i++)
            {
                rightCardsText[i].text = shuffledSuffixes[i];
            }
            rightScreen.text = rightScreenOptions[7];
        }
        else if(cumIndex == 28)
        {
            int listDeterminer = UnityEngine.Random.Range(0,5);
            if(listDeterminer == 0)
            {
                for(int i = 0; i <= 9; i++)
                {
                    shuffledSuffixes[i] = suffixes[i];
                }
                rightBase = 0;
            }
            else if(listDeterminer == 1)
            {
                for(int i = 0; i <= 9; i++)
                {
                    shuffledSuffixes[i] = suffixes[i+10];
                }
                rightBase = 10;
            }
            else if(listDeterminer == 2)
            {
                for(int i = 0; i <= 9; i++)
                {
                    shuffledSuffixes[i] = suffixes[i+20];
                }
                rightBase = 20;
            }
            else if(listDeterminer == 3)
            {
                for(int i = 0; i <= 9; i++)
                {
                    shuffledSuffixes[i] = suffixes[i+30];
                }
                rightBase = 30;
            }
            else if(listDeterminer == 4)
            {
                for(int i = 0; i <= 9; i++)
                {
                    shuffledSuffixes[i] = suffixes[i+40];
                }
                rightBase = 40;
            }
            shuffledSuffixes.Shuffle();
            for(int i = 0; i <= 9; i++)
            {
                rightCardsText[i].text = shuffledSuffixes[i];
            }
            rightScreen.text = rightScreenOptions[8];
        }
        displayedLeftWord = leftScreen.text + shuffledPrefixes[leftDisplayedScreen];
        displayedRightWord = rightScreen.text + shuffledSuffixes[rightDisplayedScreen];
        leftScreenSaved = leftScreen.text;
        rightScreenSaved = rightScreen.text;
        Debug.LogFormat("[Benedict Cumberbatch #{0}] The left screen prefix is {1}.", moduleId, leftScreenSaved);
        Debug.LogFormat("[Benedict Cumberbatch #{0}] The right screen prefix is {1}.", moduleId, rightScreenSaved);
        CalculateLeftSuffix();
        CalculateRightSuffix();
        StartCoroutine(ScreenBlink());
    }

    IEnumerator ScreenBlink()
    {
        int counter = 0;
        yield return new WaitForSeconds(0.5f);
        while(counter < 20)
        {
            leftScreen.text = "";
            rightScreen.text = "";
            yield return new WaitForSeconds (0.05f);
            leftScreen.text = leftScreenSaved;
            rightScreen.text = rightScreenSaved;
            counter++;
            yield return new WaitForSeconds (0.05f);
        }
    }

    void Update()
    {
        solvedModules = Bomb.GetSolvedModuleNames().Count;
        if(solvedModules > (totalModules / 2) && !solveAddition && !buttonLock)
        {
            Debug.LogFormat("[Benedict Cumberbatch #{0}] More than half of the modules have now been solved. The left suffix is being recalculated.", moduleId);
            solveAddition = true;
            leftIndex = 0;
            CalculateLeftSuffix();
        }
    }

    void CalculateLeftSuffix()
    {
        if(benIndex < 25)
        {
            if(Bomb.GetPortCount(Port.Parallel) >= 1)
            {
                leftIndex++;
                Debug.LogFormat("[Benedict Cumberbatch #{0}] There is a parallel port. +1. The left answer is now {1}.", moduleId, leftIndex % 10);
            }
            if(Bomb.GetSerialNumberLetters().Any(x => x == 'A' || x == 'E' || x == 'I' || x == 'O' || x == 'U'))
            {
                leftIndex++;
                Debug.LogFormat("[Benedict Cumberbatch #{0}] There is a vowel. +1. The left answer is now {1}.", moduleId, leftIndex % 10);
            }
            if(solveAddition)
            {
                leftIndex++;
                Debug.LogFormat("[Benedict Cumberbatch #{0}] More than half of the bomb's modules are solved. +1. The left answer is now {1}.", moduleId, leftIndex % 10);
            }
            if(Bomb.GetPortCount(Port.Serial) >= 1)
            {
                leftIndex += 2;
                Debug.LogFormat("[Benedict Cumberbatch #{0}] There is a serial port. +2. The left answer is now {1}.", moduleId, leftIndex % 10);
            }
            if(Bomb.GetSerialNumberLetters().All(x => x != 'A' && x != 'E' && x != 'I' && x != 'O' && x != 'U'))
            {
                leftIndex += 2;
                Debug.LogFormat("[Benedict Cumberbatch #{0}] There is no vowel. +2. The left answer is now {1}.", moduleId, leftIndex % 10);
            }
            if(!solveAddition)
            {
                leftIndex += 2;
                Debug.LogFormat("[Benedict Cumberbatch #{0}] Half or fewer of the bomb's modules are solved. +2. The left answer is now {1}.", moduleId, leftIndex % 10);
            }
            if(Bomb.IsIndicatorOn("FRK"))
            {
                leftIndex = leftIndex * 2;
                leftIndex += 3;
                Debug.LogFormat("[Benedict Cumberbatch #{0}] There is a lit FRK port. x2, +3. The left answer is now {1}.", moduleId, leftIndex % 10);
            }
            totalDigits = Bomb.GetSerialNumberNumbers().Count();
            totalLetters = Bomb.GetSerialNumberLetters().Count();
            if(totalDigits > totalLetters)
            {
                leftIndex = leftIndex * 2;
                leftIndex += 4;
                Debug.LogFormat("[Benedict Cumberbatch #{0}] There are more digits than letters in the serial number. x2, +4. The left answer is now {1}.", moduleId, leftIndex % 10);
            }
            if (Bomb.GetSerialNumberNumbers().Last() % 2 == 0)
            {
                leftIndex = leftIndex * 2;
                leftIndex += 5;
                Debug.LogFormat("[Benedict Cumberbatch #{0}] The last digit of the serial number is even. x2, +5. The left answer is now {1}.", moduleId, leftIndex % 10);
            }
            if (Bomb.IsIndicatorOff("NSA"))
            {
                leftIndex = leftIndex * 3;
                leftIndex += 6;
                Debug.LogFormat("[Benedict Cumberbatch #{0}] There is an unlit NSA. x3, +6. The left answer is now {1}.", moduleId, leftIndex % 10);
            }
            if(totalLetters > totalDigits)
            {
                leftIndex = leftIndex * 3;
                leftIndex += 7;
                Debug.LogFormat("[Benedict Cumberbatch #{0}] There are more letters than digits in the serial number. x3, +7. The left answer is now {1}.", moduleId, leftIndex % 10);
            }
            if (Bomb.GetSerialNumberNumbers().Last() % 2 == 1)
            {
                leftIndex = leftIndex * 3;
                leftIndex += 8;
                Debug.LogFormat("[Benedict Cumberbatch #{0}] The last digit of the serial number is odd. x3, +8. The left answer is now {1}.", moduleId, leftIndex % 10);
            }
            leftIndex = leftIndex % 10;
        }
        else
        {
            benIndex = 0;
        }
        Debug.LogFormat("[Benedict Cumberbatch #{0}] YOUR FINAL ANSWER FOR THE LEFT SUFFIX IS {1}.", moduleId, leftIndex);
        if(solveAddition)
        {
            correctLeftAnswer = leftScreenSaved + prefixes[leftIndex + leftBase];
            correctRightAnswer = rightScreenSaved + suffixes[rightIndex + rightBase];
            Debug.LogFormat("[Benedict Cumberbatch #{0}] THE CORRECT ANSWER IS NOW {1} {2}.", moduleId, correctLeftAnswer, correctRightAnswer);
        }
    }

    void CalculateRightSuffix()
    {
        if(cumIndex < 25)
        {
            if(Bomb.GetPortCount(Port.StereoRCA) >= 1)
            {
                rightIndex++;
                Debug.LogFormat("[Benedict Cumberbatch #{0}] There is a Stereo RCA port. +1. The right answer is now {1}.", moduleId, rightIndex % 10);
            }
            if(Bomb.GetBatteryCount() > 2)
            {
                rightIndex++;
                Debug.LogFormat("[Benedict Cumberbatch #{0}] There are more than two batteries. +1. The right answer is now {1}.", moduleId, rightIndex % 10);
            }
            if(Bomb.GetPorts().Distinct().Count() != Bomb.GetPorts().Count())
            {
                rightIndex++;
                Debug.LogFormat("[Benedict Cumberbatch #{0}] Not all ports are unique. +1. The right answer is now {1}.", moduleId, rightIndex % 10);
            }
            if(Bomb.GetPortCount(Port.RJ45) >= 1)
            {
                rightIndex += 2;
                Debug.LogFormat("[Benedict Cumberbatch #{0}] There is an RJ-45 port. +2. The right answer is now {1}.", moduleId, rightIndex % 10);
            }
            if(Bomb.GetBatteryCount() < 2)
            {
                rightIndex += 2;
                Debug.LogFormat("[Benedict Cumberbatch #{0}] There are fewer than two batteries. +2. The right answer is now {1}.", moduleId, rightIndex % 10);
            }
            if(Bomb.GetPorts().Distinct().Count() == Bomb.GetPorts().Count())
            {
                rightIndex += 2;
                Debug.LogFormat("[Benedict Cumberbatch #{0}] All ports are unique or no ports are present. +2. The right answer is now {1}.", moduleId, rightIndex % 10);
            }
            if(Bomb.IsIndicatorOn("FRQ"))
            {
                rightIndex = rightIndex * 2;
                rightIndex += 3;
                Debug.LogFormat("[Benedict Cumberbatch #{0}] There is a lit FRQ port. x2, +3. The answer is now {1}.", moduleId, rightIndex % 10);
            }
            if(Bomb.GetPorts().Distinct().Count() > 1)
            {
                rightIndex = rightIndex * 2;
                rightIndex += 4;
                Debug.LogFormat("[Benedict Cumberbatch #{0}] There is more than one type of port. x2, +4. The right answer is now {1}.", moduleId, rightIndex % 10);
            }
            if (Bomb.GetBatteryHolderCount() > 1)
            {
                rightIndex = rightIndex * 2;
                rightIndex += 5;
                Debug.LogFormat("[Benedict Cumberbatch #{0}] There is more than one battery holder. x2, +5. The right answer is now {1}.", moduleId, rightIndex % 10);
            }
            if (Bomb.IsIndicatorOff("MSA"))
            {
                rightIndex = rightIndex * 3;
                rightIndex += 6;
                Debug.LogFormat("[Benedict Cumberbatch #{0}] There is an unlit MSA. x3, +6. The right answer is now {1}.", moduleId, rightIndex % 10);
            }
            if(Bomb.GetPorts().Distinct().Count() == 1)
            {
                rightIndex = rightIndex * 3;
                rightIndex += 7;
                Debug.LogFormat("[Benedict Cumberbatch #{0}] There is only one type of port. x3, +7. The right answer is now {1}.", moduleId, rightIndex % 10);
            }
            if (Bomb.GetBatteryHolderCount() < 1)
            {
                rightIndex = rightIndex * 3;
                rightIndex += 8;
                Debug.LogFormat("[Benedict Cumberbatch #{0}] There are no battery holders on the bomb. x3, +8. The right answer is now {1}.", moduleId, rightIndex % 10);
            }
            rightIndex = rightIndex % 10;
        }
        else
        {
            cumIndex = 0;
        }
        Debug.LogFormat("[Benedict Cumberbatch #{0}] YOUR FINAL ANSWER FOR THE RIGHT SUFFIX IS {1}.", moduleId, rightIndex);
        correctLeftAnswer = leftScreenSaved + prefixes[leftIndex + leftBase];
        correctRightAnswer = rightScreenSaved + suffixes[rightIndex + rightBase];
        Debug.LogFormat("[Benedict Cumberbatch #{0}] THE CORRECT ANSWER IS {1} {2}.", moduleId, correctLeftAnswer, correctRightAnswer);

    }

    public void OnLeftUpButton()
    {
        Audio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, leftUp.transform);
        leftUp.AddInteractionPunch(0.5f);
        if(leftDisplayedScreen >= 0 && leftDisplayedScreen < 10 && !buttonLock)
        {
            if(leftDisplayedScreen == 0)
            {
                return;
            }
            buttonLock = true;
            leftCards[leftDisplayedScreen].SetBool("move", true);
            Audio.PlaySoundAtTransform("pageTurn", transform);
            StartCoroutine(stopLeftMove());
            leftMovingUp = true;
        }
    }

    public void OnLeftDownButton()
    {
        Audio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, leftDown.transform);
        leftDown.AddInteractionPunch(0.5f);
        if(leftDisplayedScreen >= 0 && leftDisplayedScreen < 10 && !buttonLock)
        {
            if(leftDisplayedScreen == 9)
            {
                return;
            }
            buttonLock = true;
            leftCards[leftDisplayedScreen + 1].SetBool("move", true);
            Audio.PlaySoundAtTransform("pageTurn", transform);
            StartCoroutine(stopLeftMove());
        }
    }

    IEnumerator stopLeftMove()
    {
        yield return new WaitForSeconds(0.2f);
        if(leftMovingUp)
        {
            leftCards[leftDisplayedScreen].SetBool("move", false);
            leftMovingUp = false;
            leftDisplayedScreen--;
            if(leftDisplayedScreen < 0)
            {
                leftDisplayedScreen = 0;
            }
            else if (leftDisplayedScreen > 9)
            {
                leftDisplayedScreen = 9;
            }
        }
        else
        {
            leftCards[leftDisplayedScreen + 1].SetBool("move", false);
            leftDisplayedScreen++;
            if(leftDisplayedScreen < 0)
            {
                leftDisplayedScreen = 0;
            }
            else if (leftDisplayedScreen > 9)
            {
                leftDisplayedScreen = 9;
            }
        }
        displayedLeftWord = leftScreen.text + shuffledPrefixes[leftDisplayedScreen];
        yield return new WaitForSeconds(0.3f);
        buttonLock = false;
    }


    public void OnRightUpButton()
    {
        Audio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, rightUp.transform);
        rightUp.AddInteractionPunch(0.5f);
        if(rightDisplayedScreen >= 0 && rightDisplayedScreen < 10 && !buttonLock)
        {
            if(rightDisplayedScreen == 0)
            {
                return;
            }
            buttonLock = true;
            rightCards[rightDisplayedScreen].SetBool("move", true);
            Audio.PlaySoundAtTransform("pageTurn", transform);
            StartCoroutine(stopRightMove());
            rightMovingUp = true;
        }
    }

    public void OnRightDownButton()
    {
        Audio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, rightDown.transform);
        rightDown.AddInteractionPunch(0.5f);
        if(rightDisplayedScreen >= 0 && rightDisplayedScreen < 10 && !buttonLock)
        {
            if(rightDisplayedScreen == 9)
            {
                return;
            }
            buttonLock = true;
            rightCards[rightDisplayedScreen + 1].SetBool("move", true);
            Audio.PlaySoundAtTransform("pageTurn", transform);
            StartCoroutine(stopRightMove());
        }
    }

    IEnumerator stopRightMove()
    {
        yield return new WaitForSeconds(0.2f);
        if(rightMovingUp)
        {
            rightCards[rightDisplayedScreen].SetBool("move", false);
            rightMovingUp = false;
            rightDisplayedScreen--;
            if(rightDisplayedScreen < 0)
            {
                rightDisplayedScreen = 0;
            }
            else if (rightDisplayedScreen > 9)
            {
                rightDisplayedScreen = 9;
            }
        }
        else
        {
            rightCards[rightDisplayedScreen + 1].SetBool("move", false);
            rightDisplayedScreen++;
            if(rightDisplayedScreen < 0)
            {
                rightDisplayedScreen = 0;
            }
            else if (rightDisplayedScreen > 9)
            {
                rightDisplayedScreen = 9;
            }
        }
        displayedRightWord = rightScreen.text + shuffledSuffixes[rightDisplayedScreen];
        yield return new WaitForSeconds(0.3f);
        buttonLock = false;
    }

    public void OnSubmitButton()
    {
        Audio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, submit.transform);
        submit.AddInteractionPunch();
        if(buttonLock)
        {
            return;
        }
        if((displayedLeftWord == correctLeftAnswer) && (displayedRightWord == correctRightAnswer))
        {
            GetComponent<KMBombModule>().HandlePass();
            Debug.LogFormat("[Benedict Cumberbatch #{0}] You submitted {1} {2}. That is correct. Module disarmed.", moduleId, displayedLeftWord, displayedRightWord);
            Audio.PlaySoundAtTransform("pass", transform);
            buttonLock = true;
        }
        else
        {
            GetComponent<KMBombModule>().HandleStrike();
            Debug.LogFormat("[Benedict Cumberbatch #{0}] Strike! You submitted {1} {2}. That is incorrect.", moduleId, displayedLeftWord, displayedRightWord);
            int strikeSound = UnityEngine.Random.Range(0,2);
            if(strikeSound == 0)
            {
                Audio.PlaySoundAtTransform("strike1", transform);
            }
            else
            {
                Audio.PlaySoundAtTransform("strike2", transform);
            }
        }
    }

    private string TwitchHelpMessage = @"Use '!{0} submit bee horn' to submit the prefixes of the names. Use '!{0} left down' to flip the left page down. Use '!{0} submit to submit the current names. Make sure you include punctuation if there is any.";

    IEnumerator ProcessTwitchCommand(string command)
    {
        var parts = command.ToLowerInvariant().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

        if (parts.Length == 3 && parts[0] == "submit")
        {
            if (!shuffledPrefixes.Contains(parts[1]) || !shuffledSuffixes.Contains(parts[2])) { yield return "sendtochaterror I don't understand what you are trying to enter!"; yield break; }

            yield return null;

            while (shuffledPrefixes[leftDisplayedScreen] != parts[1])
            {
                if (Array.IndexOf(shuffledPrefixes, parts[1]) > leftDisplayedScreen) { OnLeftDownButton(); }
                else { OnLeftUpButton(); }
                yield return new WaitForSeconds(.3f);
            }
            while (shuffledSuffixes[rightDisplayedScreen] != parts[2])
            {
                if (Array.IndexOf(shuffledSuffixes, parts[2]) > rightDisplayedScreen) { OnRightDownButton(); }
                else { OnRightUpButton(); }
                yield return new WaitForSeconds(.3f);
            }
            yield return new WaitForSeconds(.3f); // This delay is needed, idk why...
            OnSubmitButton();
        }
        else if (parts.Length == 2 && new[]{ "left", "right", "l", "r"}.Any(w => w == parts[0]) && new[] { "up", "down", "u", "d" }.Any(w => w == parts[1]))
        {
            yield return null;

            if (parts[0] == "left" || parts[0] == "l")
            {
                if (parts[1] == "down" || parts[1] == "d") { OnLeftDownButton(); }
                else { OnLeftUpButton(); }
            }
            else if (parts[0] == "right" || parts[0] == "r")
            {
                if (parts[1] == "down" || parts[1] == "d") { OnRightDownButton(); }
                else { OnRightUpButton(); }
            }
        }
        else if (parts.Length == 1 && parts[0] == "submit")
        {
            OnSubmitButton();
        }
    }
}
