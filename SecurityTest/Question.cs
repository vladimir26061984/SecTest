using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SecurityTest
{
    public class Question
    {
        /// <summary>
        /// Номер вопроса во входном наборе данных
        /// </summary>
        public int Num;
        /// <summary>
        /// Содержимое вопроса
        /// </summary>
        public string Text;
        /// <summary>
        /// Варианты ответов
        /// </summary>
        public List<String> Answer;

        /// <summary>
        /// Вариант правильного ответа
        /// </summary>
        public int ValidAnswer;

        public Question()
        {
            Answer = new List<string>();

        }

        public string getAnswer()
        {
            string res = "";
            foreach (string SSS in Answer)
            {
                if (res.Length == 0)
                    res = SSS; 
                else
                    res += "@" + SSS.Trim().Replace("\n","").Replace("\r", "");
            }
            return res;
        }

        public void setAnswer(string SSS)
        {
            string[] res = SSS.Split('@');
            for (int i = 0; i < res.Length; i++)
            {
                Answer.Add(res[i]);
            }
            
        }
    }
}
