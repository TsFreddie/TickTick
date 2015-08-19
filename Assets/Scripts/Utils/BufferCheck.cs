namespace TTUtils
{
    /// <summary>
    /// 网络传输Buffer处理静态方法类
    /// </summary>
    public static class BufferCheck
    {
        /// <summary>
        /// 字符串长度按Buffer大小限制【按UTF-8】
        /// </summary>
        /// <param name="str">原字符串</param>
        /// <param name="bufferSize">Buffer大小</param>
        /// <returns>返回的新字符串</returns>
        public static string CharacterLimit(string str, int bufferSize)
        {
            if (System.Text.Encoding.UTF8.GetByteCount(str) > bufferSize)
            {
                string newString = "";
                int bytes = 0; //位置
                System.Globalization.TextElementEnumerator textEnumerator = System.Globalization.StringInfo.GetTextElementEnumerator(str); //字符位移器
                while (textEnumerator.MoveNext())
                {
                    string textElement = textEnumerator.GetTextElement();
                    bytes += System.Text.Encoding.UTF8.GetByteCount(textElement);
                    if (bytes <= bufferSize)
                    {
                        newString += textElement;
                    }
                    else
                    {
                        break;
                    }
                }
                return newString;
            }
            return str; //无需修改原路返回
        }
    }
}
