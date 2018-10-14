using System;
using System.Collections;
using System.Text.RegularExpressions;
using UnityEngine;

public class ChessAI
{
    public static int computer = 2;
    public static int size = 15;
    public static int[,] board = new int[size, size];
    public static ArrayList blackNo = new ArrayList();
    static System.Random random = new System.Random();

    public enum Score
    {
        ONE = 0,
        TWO = 20,
        THREE = 80,
        FOUR = 320,
        FIVE = 1280,
        BLOCKED_ONE = 0,
        BLOCKED_TWO = 5,
        BLOCKED_THREE = 20,
        BLOCKED_FOUR = 80,
        TWO_THREE = 310,
        THREE_THREE = 270,
        TWO_TWO = 40
    }

    public static bool HasNeighbour(int x, int y, int[,] chessBoard, int distance = 2)
    {
        for (int i = x - distance; i < x + distance; i++)
        {
            if (i < 0 || i >= size)
            {
                continue;
            }
            for (int j = y - distance; j < y + distance; j++)
            {
                if (j < 0 || j >= size)
                {
                    continue;
                }
                if (i == x && j == y)
                {
                    continue;
                }
                if (chessBoard[i, j] != 0)
                {
                    return true;
                }
            }
        }
        return false;
    }

    public static int GetTotalScoreModel(int x, int y, int role, int[,] chessBoard)
    {
        int totalScore = 0;
        string model1 = "3";
        string model2 = "3";
        string model3 = "3";
        string model4 = "3";
        blackNo.Clear();

        //水平分数
        for (int i = 1; i < 5; i++)
        {
            if (x + i < size)
            {
                if (chessBoard[x + i, y] == role && model1.IndexOf("00") == -1)
                {
                    model1 += '1';
                }
                else if (chessBoard[x + i, y] == 0 && model1.IndexOf("00") == -1)
                {
                    model1 += '0';
                }
                else
                {
                    model1 += '2';
                    break;
                }
                if (model1.IndexOf("00") != -1)
                {
                    model1 = model1.Substring(0, model1.Length - 1);
                    break;
                }
            }
            else
            {
                model1 += '2';
                break;
            }
        }
        for (int i = 1; i < 5; i++)
        {
            if (x - i >= 0)
            {
                if (chessBoard[x - i, y] == role && model1.IndexOf("00") == -1)
                {
                    model1 = '1' + model1;
                }
                else if (chessBoard[x - i, y] == 0 && model1.IndexOf("00") == -1)
                {
                    model1 = '0' + model1;
                }
                else
                {
                    model1 = '2' + model1;
                    break;
                }
                if (model1.IndexOf("00") != -1)
                {
                    model1 = model1.Substring(1, model1.Length - 1);
                    break;
                }
            }
            else
            {
                model1 = '2' + model1;
                break;
            }
        }

        //垂直分数
        for (int i = 1; i < 5; i++)
        {
            if (y + i < size)
            {
                if (chessBoard[x, y + i] == role && model2.IndexOf("00") == -1)
                {
                    model2 += '1';
                }
                else if (chessBoard[x, y + i] == 0 && model2.IndexOf("00") == -1)
                {
                    model2 += '0';
                }
                else
                {
                    model2 += '2';
                    break;
                }
                if (model2.IndexOf("00") != -1)
                {
                    model2 = model2.Substring(0, model2.Length - 1);
                    break;
                }
            }
            else
            {
                model2 += '2';
                break;
            }
        }
        for (int i = 1; i < 5; i++)
        {
            if (y - i >= 0)
            {
                if (chessBoard[x, y - i] == role && model2.IndexOf("00") == -1)
                {
                    model2 = '1' + model2;
                }
                else if (chessBoard[x, y - i] == 0 && model2.IndexOf("00") == -1)
                {
                    model2 = '0' + model2;
                }
                else
                {
                    model2 = '2' + model2;
                    break;
                }
                if (model2.IndexOf("00") != -1)
                {
                    model2 = model2.Substring(1, model2.Length - 1);
                    break;
                }
            }
            else
            {
                model2 = '2' + model2;
                break;
            }
        }

        //斜线分数
        for (int i = 1; i < 5; i++)
        {
            if (x + i < size && y + i < size)
            {
                if (chessBoard[x + i, y + i] == role && model3.IndexOf("00") == -1)
                {
                    model3 += '1';
                }
                else if (chessBoard[x + i, y + i] == 0 && model3.IndexOf("00") == -1)
                {
                    model3 += '0';
                }
                else
                {
                    model3 += '2';
                    break;
                }
                if (model3.IndexOf("00") != -1)
                {
                    model3 = model3.Substring(0, model3.Length - 1);
                    break;
                }
            }
            else
            {
                model3 += '2';
                break;
            }
        }
        for (int i = 1; i < 5; i++)
        {
            if (x - i >= 0 && y - i >= 0)
            {
                if (chessBoard[x - i, y - i] == role && model3.IndexOf("00") == -1)
                {
                    model3 = '1' + model3;
                }
                else if (chessBoard[x - i, y - i] == 0 && model3.IndexOf("00") == -1)
                {
                    model3 = '0' + model3;
                }
                else
                {
                    model3 = '2' + model3;
                    break;
                }
                if (model3.IndexOf("00") != -1)
                {
                    model3 = model3.Substring(1, model3.Length - 1);
                    break;
                }
            }
            else
            {
                model3 = '2' + model3;
                break;
            }
        }

        //斜线分数
        for (int i = 1; i < 5; i++)
        {
            if (x + i < size && y - i >= 0)
            {
                if (chessBoard[x + i, y - i] == role && model4.IndexOf("00") == -1)
                {
                    model4 += '1';
                }
                else if (chessBoard[x + i, y - i] == 0 && model4.IndexOf("00") == -1)
                {
                    model4 += '0';
                }
                else
                {
                    model4 += '2';
                    break;
                }
                if (model4.IndexOf("00") != -1)
                {
                    model4 = model4.Substring(0, model4.Length - 1);
                    break;
                }
            }
            else
            {
                model4 += '2';
                break;
            }
        }
        for (int i = 1; i < 5; i++)
        {
            if (x - i >= 0 && y + i < size)
            {
                if (chessBoard[x - i, y + i] == role && model4.IndexOf("00") == -1)
                {
                    model4 = '1' + model4;
                }
                else if (chessBoard[x - i, y + i] == 0 && model4.IndexOf("00") == -1)
                {
                    model4 = '0' + model4;
                }
                else
                {
                    model4 = '2' + model4;
                    break;
                }
                if (model4.IndexOf("00") != -1)
                {
                    model4 = model4.Substring(1, model4.Length - 1);
                    break;
                }
            }
            else
            {
                model4 = '2' + model4;
                break;
            }
        }

        string[] models = new string[] { model1, model2, model3, model4 };
        string cPattern = "(^0*(1*31+|1+31*)0*2*$)|(^2*0*(1*31+|1+31*)0*$)";
        string bPattern = "(^2+0*(1*31+|1+31*)0*2*$)|(^2*0*(1*31+|1+31*)0*2+$)";
        string ncPattern = "[2]*[0]*(((1110|1101|1011|110|101)?3(0111|1011|1101|011|101))|((1110|1101|1011|110|101)3(0111|1011|1101|011|101)?)|((1110|1101|1011|110|101)3(0111|1011|1101|011|101)))[0]*[2]*";
        string n2Pattern = "[2]*[0]*10301[0]*[2]*";
        string n3Pattern = "[2]*[0]*(1301|1031)[0]*[2]*";
        string n4Pattern = "[2]*[0]*(11301|10311|10131|13101)[0]*[2]*";
        string nd4Pattern = "[2]*[0]*(1013101|111030111|11013011|11031011)[0]*[2]*";

        foreach (string model in models)
        {
            int twoFour = 0;
            int twoThree = 0;
            int blockThree = 0;
            if (Regex.IsMatch(model, nd4Pattern))
            {
                //Debug.Log(String.Format("ND4:{0},{1}", model, totalScore));
                if (role == 1)
                {
                    blackNo.Add(new int[x, y]);
                    totalScore = 0;
                    break;
                }
                totalScore += (int)Score.FOUR;
            }
            else if (Regex.IsMatch(model, n4Pattern))
            {
                //Debug.Log(String.Format("N4:{0},{1}", model, totalScore));
                twoFour++;
                if (twoFour >= 2 && role == 1)
                {
                    blackNo.Add(new int[x, y]);
                    totalScore = 0;
                    break;
                }
                totalScore += (int)Score.FOUR;
            }
            else if (Regex.IsMatch(model, n3Pattern))
            {
                //Debug.Log(String.Format("N3:{0},{1}", model, totalScore));
                if (model.Trim('2').Length >= 5)
                {
                    twoThree++;
                    if (twoThree >= 2 && role == 1)
                    {
                        blackNo.Add(new int[x, y]);
                        totalScore = 0;
                        break;
                    }
                    else if (twoThree >= 2 && role == 2)
                    {
                        totalScore += (int)Score.TWO_THREE;
                        twoThree--;
                    }
                    else if (twoThree == 1 && blockThree >= 1)
                    {
                        totalScore += (int)Score.THREE_THREE;
                        twoThree--;
                        blockThree--;
                    }
                    else
                    {
                        totalScore += (int)Score.THREE;
                    }
                }
                else
                {
                    blockThree++;
                    if (twoThree == 1 && blockThree >= 1 && role == 1)
                    {
                        totalScore += (int)Score.THREE_THREE;
                        blockThree--;
                    }
                    else if (twoThree == 1 && blockThree >= 1 && role == 2)
                    {
                        totalScore += (int)Score.THREE_THREE;
                        twoThree--;
                        blockThree--;
                    }
                    else
                    {
                        totalScore += (int)Score.BLOCKED_THREE;
                    }
                }
            }
            else if (Regex.IsMatch(model, ncPattern))
            {
                //Debug.Log(String.Format("NC:{0},{1}", model, totalScore));
                if (model.Trim('2').Trim('0').Length == 9)
                {
                    twoFour++;
                    if (twoFour >= 2 && role == 1)
                    {
                        blackNo.Add(new int[x, y]);
                        totalScore = 0;
                        break;
                    }
                    totalScore += (int)Score.FOUR;
                }
                else if (model.Trim('2').Trim('0').Length == 8)
                {
                    twoFour++;
                    twoThree++;
                    if ((twoThree >= 2 || twoFour >= 2) && role == 1)
                    {
                        blackNo.Add(new int[x, y]);
                        totalScore = 0;
                        break;
                    }
                    totalScore += (int)Score.FOUR;
                }
                else if (model.Trim('2').Trim('0').Length == 7)
                {
                    if (model.Trim('2').Trim('0').Length == model.Trim('0').Length)
                    {
                        totalScore += (int)Score.TWO_THREE;
                    }
                    else
                    {
                        totalScore += (int)Score.THREE_THREE;
                    }
                }
            }
            else if (Regex.IsMatch(model, n2Pattern))
            {
                //Debug.Log(String.Format("N2:{0},{1}", model, totalScore));
                if (model.Trim('2').Trim('0').Length == 5)
                {
                    if (model.Trim('2').Trim('0').Length == model.Trim('0').Length)
                    {
                        totalScore += (int)Score.TWO_TWO;
                    }
                    else
                    {
                        totalScore += (int)Score.BLOCKED_THREE;
                    }
                }
            }
            else if (Regex.IsMatch(model, bPattern))
            {
                if (model.Trim('2').Trim('0').Length > 5 && role == 1)
                {
                    blackNo.Add(new int[x, y]);
                    totalScore = 0;
                    break;
                }
                if (model.Trim('2').Trim('0').Length >= 5)
                {
                    totalScore += (int)Score.FIVE;
                    //Debug.Log(String.Format("B:{0},{1}", model, totalScore));
                }
                else if (model.Trim('2').Trim('0').Length == 4)
                {
                    twoFour++;
                    if (twoFour >= 2 && role == 1)
                    {
                        blackNo.Add(new int[x, y]);
                        totalScore = 0;
                        break;
                    }
                    totalScore += (int)Score.BLOCKED_FOUR;
                }
                else if (model.Trim('2').Trim('0').Length == 3)
                {
                    totalScore += (int)Score.BLOCKED_THREE;
                }
                else if (model.Trim('2').Trim('0').Length == 2)
                {
                    totalScore += (int)Score.BLOCKED_TWO;
                }
                else
                {
                    totalScore += (int)Score.BLOCKED_ONE;
                    //Debug.Log(String.Format("B:{0},{1}", model, totalScore));
                }
            }
            else if (Regex.IsMatch(model, cPattern))
            {
                if (model.Trim('0').Length > 5 && role == 1)
                {
                    blackNo.Add(new int[x, y]);
                    totalScore = 0;
                    break;
                }
                if (model.Trim('0').Length >= 5)
                {
                    totalScore += (int)Score.FIVE;
                    //Debug.Log(String.Format("C:{0},{1}", model, totalScore));
                }
                else if (model.Trim('0').Length == 4)
                {
                    twoFour++;
                    if (twoFour >= 2 && role == 1)
                    {
                        blackNo.Add(new int[x, y]);
                        totalScore = 0;
                        break;
                    }
                    totalScore += (int)Score.FOUR;
                }
                else if (model.Trim('0').Length == 3)
                {
                    twoThree++;
                    if (twoThree >= 2 && role == 1)
                    {
                        blackNo.Add(new int[x, y]);
                        totalScore = 0;
                        break;
                    }
                    totalScore += (int)Score.THREE;
                }
                else if (model.Trim('0').Length == 2)
                {
                    totalScore += (int)Score.TWO;
                }
                else
                {
                    totalScore += (int)Score.ONE;
                    //Debug.Log(String.Format("C:{0},{1}", model, totalScore));
                }
            }
            //if (model.IndexOf("1") != -1)
            //{
            //Debug.Log(String.Format("0:{0},{1}", model, totalScore));
            //}
        }
        return totalScore;
    }

    public static ArrayList FindMaxModel(int role, int[,] chessBoard)
    {
        ArrayList bestPoints = new ArrayList();
        int max_score = -1;
        int max_x = -1;
        int max_y = -1;

        for (int x = 0; x < size; x++)
        {
            for (int y = 0; y < size; y++)
            {
                if (HasNeighbour(x, y, chessBoard) && chessBoard[x, y] == 0)
                {
                    int score = GetTotalScoreModel(x, y, role, chessBoard);
                    if (max_x < 3 || max_y < 3 || max_x > 11 || max_y > 11)
                    {
                        score = score / 5 * 4;
                    }
                    if (score > max_score)
                    {
                        bestPoints.Clear();
                        max_score = score;
                        max_x = x;
                        max_y = y;
                    }
                    if (score == max_score)
                    {
                        bestPoints.Add(new int[] { max_x, max_y, role == computer ? max_score : -max_score });
                    }
                }
            }
        }
        return bestPoints;
    }

    public static int[] MaxModelPoints(int role, int[,] chessBoard)
    {
        ArrayList localBestPoints = FindMaxModel(role, chessBoard);
        ArrayList counterBestPoints = FindMaxModel(role == 1 ? 2 : 1, chessBoard);
        if (Math.Abs(((int[])localBestPoints[0])[2]) > Math.Abs(((int[])counterBestPoints[0])[2] / 5 * 4))
        {
            return (int[])localBestPoints[random.Next(localBestPoints.Count)];
        }
        return (int[])counterBestPoints[random.Next(counterBestPoints.Count)];
    }

    public static ArrayList Gen(int[,] chessBoard)
    {
        ArrayList points = new ArrayList();
        for (int x = 0; x < size; x++)
        {
            for (int y = 0; y < size; y++)
            {
                if (HasNeighbour(x, y, chessBoard) && chessBoard[x, y] == 0)
                {
                    points.Add(new int[] { x, y });
                }
            }
        }
        return points;
    }

    public static int[] AlphaBetaMin(int[,] chessBoard, int deep, int alpha, int beta, int role)
    {
        int[] v = MaxModelPoints(role, chessBoard);
        if (deep <= 0)
        {
            return v;
        }
        int[] best = new int[] { -1, -1, 10000 };
        ArrayList points = Gen(chessBoard);

        for (int i = 0; i < points.Count; i++)
        {
            int[] p = (int[])points[i];
            chessBoard[p[0], p[1]] = role == 1 ? 2 : 1;
            v = AlphaBetaMax(chessBoard, deep - 1, best[2] < alpha ? best[2] : alpha, beta, role);
            chessBoard[p[0], p[1]] = 0;
            if (v[2] < best[2])
            {
                best = v;
            }
            if (v[2] < beta)
            {  //AB剪枝
                break;
            }
        }
        return best;
    }

    public static int[] AlphaBetaMax(int[,] chessBoard, int deep, int alpha, int beta, int role)
    {
        int[] v = MaxModelPoints(role, chessBoard);
        if (deep <= 0)
        {
            return v;
        }
        int[] best = new int[] { -1, -1, -10000 };
        ArrayList points = Gen(chessBoard);

        for (int i = 0; i < points.Count; i++)
        {
            int[] p = (int[])points[i];
            chessBoard[p[0], p[1]] = role == 1 ? 2 : 1;
            v = AlphaBetaMin(chessBoard, deep - 1, best[2] > alpha ? best[2] : alpha, beta, role);
            chessBoard[p[0], p[1]] = 0;
            if (v[2] > best[2])
            {
                best = v;
            }
            if (v[2] > beta)
            {  //AB剪枝
                break;
            }
        }
        return best;
    }

    public static int[] NextMove(int role)
    {
        return AlphaBetaMax(board, 0, -10000, 10000, role);
    }

    public static bool CheckWinner(int x, int y, int role)
    {
        //水平分数
        int score = 1;
        for (int i = 1; i < 5; i++)
        {
            if (x + i < size)
            {
                if (board[x + i, y] == role)
                {
                    score++;
                }
                else
                {
                    break;
                }
            }
        }
        if (score >= 5)
        {
            return true;
        }
        for (int i = 1; i < 5; i++)
        {
            if (x - i >= 0)
            {
                if (board[x - i, y] == role)
                {
                    score++;
                }
                else
                {
                    break;
                }
            }
        }
        if (score >= 5)
        {
            return true;
        }

        //垂直分数
        score = 1;
        for (int i = 1; i < 5; i++)
        {
            if (y + i < size)
            {
                if (board[x, y + i] == role)
                {
                    score++;
                }
                else
                {
                    break;
                }
            }
        }
        if (score >= 5)
        {
            return true;
        }
        for (int i = 1; i < 5; i++)
        {
            if (y - i >= 0)
            {
                if (board[x, y - i] == role)
                {
                    score++;
                }
                else
                {
                    break;
                }
            }
        }
        if (score >= 5)
        {
            return true;
        }

        //斜线分数
        score = 1;
        for (int i = 1; i < 5; i++)
        {
            if (x + 1 < size && y + i < size)
            {
                if (board[x + i, y + i] == role)
                {
                    score++;
                }
                else
                {
                    break;
                }
            }
        }
        if (score >= 5)
        {
            return true;
        }
        for (int i = 1; i < 5; i++)
        {
            if (x - i >= 0 && y - i >= 0)
            {
                if (board[x - i, y - i] == role)
                {
                    score++;
                }
                else
                {
                    break;
                }
            }
        }
        if (score >= 5)
        {
            return true;
        }

        //斜线分数
        score = 1;
        for (int i = 1; i < 5; i++)
        {
            if (x + i < size && y - i >= 0)
            {
                if (board[x + i, y - i] == role)
                {
                    score++;
                }
                else
                {
                    break;
                }
            }
        }
        if (score >= 5)
        {
            return true;
        }
        for (int i = 1; i < 5; i++)
        {
            if (x - i >= 0 && y + i < size)
            {
                if (board[x - i, y + i] == role)
                {
                    score++;
                }
                else
                {
                    break;
                }
            }
        }
        if (score >= 5)
        {
            return true;
        }
        return false;
    }
}
