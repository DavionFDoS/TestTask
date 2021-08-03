using System.Collections.Generic;

namespace TestTask.Models
{
    /// <summary>
    /// Класс, представляющий собой модель дополнительного параметра
    /// </summary>
    public class AdditionalParameter
    {
        /// <summary>
        /// Название параметра
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// Тип параметра
        /// </summary>
        public AdditionalParameterType Type { get; set; }

        /// <summary>
        /// Список значений параметра
        /// </summary>
        public List<Values> ValuesList { get; set; }

        /// <summary>
        /// Метод для получения глубокого клона списка значений
        /// </summary>
        /// <param name="originalList"></param>
        /// <returns></returns>
        public IList<Values> CloneList(IList<Values> originalList)
        {
            if (originalList != null)
            {
                IList<Values> copyList = new List<Values>();
                foreach (Values value in originalList)
                {
                    copyList.Add(new Values { Name = (string)value.Name.Clone() });
                }
                return new List<Values>(copyList);
            }
            else
            {
                return new List<Values>();
            }
        }
        /// <summary>
        /// Метод для получения глубокого клона дополнительного параметра
        /// </summary>
        /// <returns></returns>
        public AdditionalParameter Clone()
        {
            List<Values> values = (List<Values>)CloneList(ValuesList);
            return new AdditionalParameter
            {
                Title = this.Title,
                Type = this.Type,
                ValuesList = values
            };
        }
    }
}
