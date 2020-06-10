using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using Rally.Lib.Protocol.Contract;

namespace ContractGenerator
{
    public partial class UCContractItem : UserControl
    {
        public UCContractItem()
        {
            InitializeComponent();
        }

        private IDictionary<string, int[]> fieldMappings = null;

        private List<ContractItemFieldMappingViewModel> contractItemFieldMappingViewModels = null;
        BindingSource bindingSourceContractItemFieldMappingViewModel = null;

        private Rally.Lib.Protocol.Contract.ContractGenerator contractGenerator = new Rally.Lib.Protocol.Contract.ContractGenerator();

        public string ClassName { get; set; }
        public string Namespace { get; set; }

        public void Populate(IDictionary<string, int[]> Mappings)
        {
            this.fieldMappings = Mappings != null ? Mappings : new Dictionary<string, int[]>() { { "Item1", new int[] { 0,1,2} }, { "Item2", new int[] { 3, 4, 5 } } };

            this.contractItemFieldMappingViewModels = new List<ContractItemFieldMappingViewModel>() {
                new ContractItemFieldMappingViewModel() {  FieldName ="Item1", FieldDataByteArrayIndexs = new List<int>() { 0, 1, 2 } },
                new ContractItemFieldMappingViewModel() {  FieldName ="Item2", FieldDataByteArrayIndexs = new List<int>() { 3, 4, 5, 6 } }
            };

            if (fieldMappings != null)
            {
                this.contractItemFieldMappingViewModels.Clear();

                foreach (var item in this.fieldMappings)
                {
                    this.contractItemFieldMappingViewModels.Add(new ContractItemFieldMappingViewModel() {
                        FieldName = item.Key,
                        FieldDataByteArrayIndexs = new List<int>(item.Value)
                    });
                }
            }

            this.bindingSourceContractItemFieldMappingViewModel = new BindingSource(this.contractItemFieldMappingViewModels, "");

            this.dataGridViewContract.DataSource = this.bindingSourceContractItemFieldMappingViewModel;
        }

        public string Generate(out string Meta)
        {
            Meta = this.getMetaJson(this.contractItemFieldMappingViewModels);

            string json = this.getDescriptorJson(this.contractItemFieldMappingViewModels);

            string  contractCode =  this.contractGenerator.GenerateContract(this.Namespace, this.ClassName, json);

            return contractCode;
        }

        private string getMetaJson(List<ContractItemFieldMappingViewModel> mappings)
        {
            string json = "{";

            foreach (var mapping in mappings)
            {
                json += $"\"{mapping.FieldName}\" : [{mapping.FieldDataByteArrayIndexString}]";

                if (mappings.IndexOf(mapping) != (mappings.Count -1))
                {
                    json += ",\r\n";
                }
            }

            json += "}";

            return json;
        }

        private string getDescriptorJson(List<ContractItemFieldMappingViewModel> mappings)
        {
            string json = "{";

            foreach (var mapping in mappings)
            {
                json += $"\"{mapping.FieldName}\" : \"\"";

                if (mappings.IndexOf(mapping) != (mappings.Count - 1))
                {
                    json += ",\r\n";
                }
            }

            json += "}";

            return json;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            //this.Populate(null);
        }
    }
}
