using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContractGenerator
{
    public class ContractItemFieldMappingViewModel
    {
        private List<int> fieldDataByteArrayIndexs = new List<int>();
        public string FieldName { get; set; }
        public List<int> FieldDataByteArrayIndexs { get=>this.fieldDataByteArrayIndexs; set=>this.fieldDataByteArrayIndexs = value; }
        public string FieldDataByteArrayIndexString
        {
            get {
                string indexString = "";

                for (int i = 0; i < this.fieldDataByteArrayIndexs.Count; i++)
                {
                    indexString += this.fieldDataByteArrayIndexs[i].ToString();

                    if (i != this.fieldDataByteArrayIndexs.Count - 1)
                    {
                        indexString += ",";
                    }
                }

                return indexString;
            }
            set {
                string indexString = value.ToString();
                string[] indexStrinValues = indexString.Split(new string[] { "," }, StringSplitOptions.None);
                int indexIntValue = -1;

                this.fieldDataByteArrayIndexs.Clear();

                foreach (var item in indexStrinValues)
                {
                    if (int.TryParse(item, out indexIntValue))
                    {
                        this.fieldDataByteArrayIndexs.Add(indexIntValue);
                    }
                }
            }
        }
    }
}
