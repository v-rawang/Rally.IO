using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestCaching
{
    public class RadiationMeasurementDataItem
    {
        public Guid Id { get; set; }
        public string InstrumentID { get; set; }
        //public Detector Detector { get; set; }
        //public Vehicle Vehicle { get; set; }
        public string OperatorID { get; set; }
        public string CameraNo { get; set; }
        public decimal? BackgroundValue { get; set; }
        public decimal? AlarmThresholdOne { get; set; }
        public decimal? AlarmThresholdTwo { get; set; }
        public decimal? MeasuredValue { get; set; }
        //public Nuclide Nuclide { get; set; }
        public DateTime? MeasurementTime { get; set; }
        public string State { get; set; }
        public string Result { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public decimal? WindSpeed { get; set; }
        public decimal? VehicleSpeed { get; set; }  
        public string Remarks { get; set; }
    }
}
