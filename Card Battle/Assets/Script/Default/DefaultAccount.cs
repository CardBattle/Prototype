using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultAccount
{

    private int accountNum;//���� �ѹ�
    private string userName;//���� �̸�
    private string email;//���� �̸�
    private int characterCount;//���� ĳ����
    private int houseCount;//���� ����ó
    private int studyCount;//���� �н�
    private int trainingCount;//���� �ܷ�

    public DefaultAccount(int accNum, string userNameTyping, string userEmail, int c, int h, int s, int t)
    {
        this.accountNum = accNum;
        this.userName = userNameTyping;
        this.email = userEmail;
        this.characterCount = c;//���� ĳ����
        this.houseCount = h;//���� ����ó
        this.studyCount = s;//���� �н�
        this.trainingCount = t; //���� �ܷ�
    }

    /*public int SetAccountNumber(int userNumber)
    {
        if (userNumber==0)
        {
            return 1;
        }
        else
        {
            accountNum = userNumber;
            return 0;
        }

    }

    public int SetUserName(string userNameTyping)
    {
        if (userNameTyping == null)
        {
            return 1;
        }
        else
        {
            userName = userNameTyping;
            return 0;
        }
    }
    public int SetCharacterCount(int charaterNumber)
    {
        if (charaterNumber == 0)
        {
            return 1;
        }
        else
        {
            characterCount.Add(charaterNumber);//ĳ���Ͱ� ������ ����Ǹ� ����� ĳ���� ��ȣ�� ������ �߰��Ѵ�.
            return 0;
        }
    }

    public int SetHouseCount(int houseNumber)
    {
        if (houseNumber == 0)
        {
            return 1;
        }
        else
        {
            houseCount.Add(houseNumber);
            return 0;
        }
    }

    public int SetStudyCount(int studyNumber)
    {
        if (studyNumber == 0)
        {
            return 1;
        }
        else
        {
            studyCount.Add(studyNumber);
            return 0;
        }
    }

    public int SetTrainingCount(int trainingNumber)
    {
        if (trainingNumber == 0)
        {
            return 1;
        }
        else
        {
            trainingCount.Add(trainingNumber);
            return 0;
        }
    }

    public int GetAccountNumber()
    {
        if (accountNum == 0)
        {
            return 1;
        }
        else
        {
            return accountNum;
        }
    }

    public string GetUserName()
    {
        if (userName == null)
        {
            return null;
        }
        else
        {
            return userName;
        }
    }
    public List<int> GetCharacterCount()
    {
        List<int>charaterResult = new List<int>();
        if (characterCount.Count== 0)
        {
            return charaterResult;
        }
        else
        {
            for(int i=0; i<characterCount.Count;i++)
            {
                charaterResult.Add(characterCount[i]);
            }
            return charaterResult;
        }
    }

    public List<int> GetHouseCount(int houseNumber)
    {
        List<int> houseResult = new List<int>();
        if (houseCount.Count == 0)
        {
            return houseResult;
        }
        else
        {
            for (int i = 0; i < houseCount.Count; i++)
            {
                houseResult.Add(houseCount[i]);
            }
            return houseResult;
        }
    }

    public List<int> GetStudyCount()
    {
        List<int> studyResult = new List<int>();
        if (characterCount.Count == 0)
        {
            return studyResult;
        }
        else
        {
            for (int i = 0; i < studyCount.Count; i++)
            {
                studyResult.Add(studyCount[i]);
            }
            return studyResult;
        }
    }

    public List<int> GetTrainingCount()
    {
        List<int> trainingResult = new List<int>();
        if (characterCount.Count == 0)
        {
            return trainingResult;
        }
        else
        {
            for (int i = 0; i < trainingCount.Count; i++)
            {
                trainingResult.Add(trainingCount[i]);
            }
            return trainingResult;
        }
    }*/
}