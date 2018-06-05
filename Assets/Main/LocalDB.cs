using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LocalDB
{
    // ����� �� ������ ����������� ��������������� ��������� __def_ColorPlayer1 � __def_ColorPlayer2, �� ��� �������� GameManager
    private static int __def_ColorPlayer1;
    private static int __def_ColorPlayer2;

    private static int __def_CountPlayers;
    private static int __def_CountRealPlayers;

    private static int __def_ModeGame;

    private static int __def_GameInWin;

    private static int __def_GexagonsOrientation;

    private static int __def_OpenedRecordsInMain;



    public static int _def_ColorPlayer1         { get { return __def_ColorPlayer1; }        set { PlayerPrefs.SetInt("_def_ColorPlayer1", value); PlayerPrefs.Save();        __def_ColorPlayer1 = value; } }
    public static int _def_ColorPlayer2         { get { return __def_ColorPlayer2; }        set { PlayerPrefs.SetInt("_def_ColorPlayer2", value); PlayerPrefs.Save();        __def_ColorPlayer2 = value; } }

    public static int _def_CountPlayers         { get { return __def_CountPlayers; }        set { PlayerPrefs.SetInt("_def_CountPlayers", value); PlayerPrefs.Save();        __def_CountPlayers = value; } }
    public static int _def_CountRealPlayers     { get { return __def_CountRealPlayers; }    set { PlayerPrefs.SetInt("_def_CountRealPlayers", value); PlayerPrefs.Save();    __def_CountRealPlayers = value; } }

    public static int _def_ModeGame             { get { return __def_ModeGame; }            set { PlayerPrefs.SetInt("_def_ModeGame", value); PlayerPrefs.Save();            __def_ModeGame = value; } }

    public static int _def_GameInWin            { get { return __def_GameInWin; }           set { PlayerPrefs.SetInt("_def_GameInWin", value); PlayerPrefs.Save();           __def_GameInWin = value; } }

    public static int _def_GexagonsOrientation  { get { return __def_GexagonsOrientation; } set { PlayerPrefs.SetInt("_def_GexagonsOrientation", value); PlayerPrefs.Save(); __def_GexagonsOrientation = value; } }

    public static int _def_OpenedRecordsInMain  { get { return __def_OpenedRecordsInMain; } set { PlayerPrefs.SetInt("_def_OpenedRecordsInMain", value); PlayerPrefs.Save(); __def_OpenedRecordsInMain = value; } }


    static LocalDB()
    {
        // �������� ��-��������� �������� �����!
        __def_ColorPlayer1 = PlayerPrefs.GetInt("_def_ColorPlayer1", 1);
        __def_ColorPlayer2 = PlayerPrefs.GetInt("_def_ColorPlayer2", 2);

        __def_CountPlayers = PlayerPrefs.GetInt("_def_CountPlayers", 3);
        __def_CountRealPlayers = PlayerPrefs.GetInt("_def_CountRealPlayers", 1);

        __def_ModeGame = PlayerPrefs.GetInt("_def_ModeGame", 0);

        __def_GameInWin = PlayerPrefs.GetInt("_def_GameInWin", 1);

        __def_GexagonsOrientation = PlayerPrefs.GetInt("_def_GexagonsOrientation", 1);

        __def_OpenedRecordsInMain = PlayerPrefs.GetInt("_def_OpenedRecordsInMain", 0);
    }
}