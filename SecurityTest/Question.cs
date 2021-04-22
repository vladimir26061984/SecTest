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
        /// Картинка
        /// </summary>
        public string PictureQ;
        /// <summary>
        /// Варианты ответов
        /// </summary>
        public List<Answers> Answer;
        /// <summary>
        /// Список ссылок на файлы рисунков
        /// </summary>
        public List<string> PathFile;

        /// <summary>
        /// Вариант правильного ответа
        /// </summary>
        public int ValidAnswer;

        /// <summary>
        /// Ответ
        /// </summary>
        public int UserAnswer;
        /// <summary>
        /// признак того, что на вопрос был получен правильный ответ
        /// </summary>
        public bool isOk
        {
            get { return UserAnswer == ValidAnswer; }
        }

        public Question()
        {
            Answer = new List<Answers>();

        }

        public string getAnswer()
        {
            string res = "";
            foreach (Answers SSS in Answer)
            {
                if (res.Length == 0)
                    res = SSS.Text; 
                else
                    res += "@" + SSS.Text.Trim().Replace("\n","").Replace("\r", "");
            }
            return res;
        }

        public void setAnswer(string SSS)
        {
            List<string>  res = SSS.Split('@').ToList();
            res.RemoveAll(ss => ss.Equals(string.Empty));
            for (int i = 0; i < res.Count; i++)
            {
                Answers newOb = new Answers();
                newOb.Text = res[i];
                newOb.originIndex = i + 1;
                Answer.Add(newOb);
            }
        }

        public void setAnswer(Answers[] Arr)
        {
            Answer.AddRange(Arr);
        }

        public override string ToString()
        {
            int maxLen = Text.Length;
            if (maxLen > 28)
                maxLen = 25;
            string SSS = Num.ToString() + ". ";
            if (Text.Length > 0)
                SSS += Text.Substring(0, maxLen) + "... ";
            if (PictureQ.Length > 0)
                SSS += " [Изображение]";
            return SSS;
        }
    }
}
