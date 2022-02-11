using FTN.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTN.Services.NetworkModelService.DataModel.Core
{
    public class TimeSeries : IdentifiedObject
    {
        private string objectAggregation;
        private string product;
        private string version;
        private long marketDocument;
        private long period;
        private List<long> measurementPoints = new List<long>();

        public TimeSeries(long globalId) : base(globalId)
        {
        }

        public string ObjectAggregation { get => objectAggregation; set => objectAggregation = value; }
        public string Product { get => product; set => product = value; }
        public string Version { get => version; set => version = value; }
        public long MarketDocument { get => marketDocument; set => marketDocument = value; }
        public long Period { get => period; set => period = value; }
        public List<long> MeasurementPoints { get => measurementPoints; set => measurementPoints = value; }


        public override bool Equals(object obj)
        {
            if (base.Equals(obj))
            {
                TimeSeries x = (TimeSeries)obj;
                return ((x.objectAggregation == this.objectAggregation) &&
                        (x.product == this.product) &&
                        (x.version == this.version) &&
                        (x.marketDocument == this.marketDocument) &&
                        (x.period == this.period) &&
                        (CompareHelper.CompareLists(x.measurementPoints, this.measurementPoints)));
            }
            else
            {
                return false;
            }
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }


        #region IAccess implementation

        public override bool HasProperty(ModelCode t)
        {
            switch (t)
            {
                case ModelCode.TIMESERIES_OBJECTAGGREGATION:
                case ModelCode.TIMESERIES_PRODUCT:
                case ModelCode.TIMESERIES_VERSION:
                case ModelCode.TIMESERIES_MARKETDOCUMENT:
                case ModelCode.TIMESERIES_PERIOD:
                case ModelCode.TIMESERIES_MEASUREMENTPOINTS:

                    return true;

                default:
                    return base.HasProperty(t);
            }
        }

        public override void GetProperty(Property prop)
        {
            switch (prop.Id)
            {
                case ModelCode.TIMESERIES_OBJECTAGGREGATION:
                    prop.SetValue(objectAggregation);
                    break;
                case ModelCode.TIMESERIES_PRODUCT:
                    prop.SetValue(product);
                    break;
                case ModelCode.TIMESERIES_VERSION:
                    prop.SetValue(version);
                    break;
                case ModelCode.TIMESERIES_MARKETDOCUMENT:
                    prop.SetValue(marketDocument);
                    break;
                case ModelCode.TIMESERIES_PERIOD:
                    prop.SetValue(period);
                    break;
                case ModelCode.TIMESERIES_MEASUREMENTPOINTS:
                    prop.SetValue(measurementPoints);
                    break;

                default:
                    base.GetProperty(prop);
                    break;
            }
        }

        public override void SetProperty(Property property)
        {
            switch (property.Id)
            {
                case ModelCode.TIMESERIES_OBJECTAGGREGATION:
                    objectAggregation = property.AsString();
                    break;
                case ModelCode.TIMESERIES_PRODUCT:
                    product = property.AsString();
                    break;
                case ModelCode.TIMESERIES_VERSION:
                    version = property.AsString();
                    break;
                case ModelCode.TIMESERIES_MARKETDOCUMENT:
                    marketDocument = property.AsReference();
                    break;
                case ModelCode.TIMESERIES_PERIOD:
                    period = property.AsReference();
                    break;


                default:
                    base.SetProperty(property);
                    break;
            }
        }

        #endregion IAccess implementation


        #region IReference implementation
        public override bool IsReferenced
        {
            get
            {
                return measurementPoints.Count > 0 || base.IsReferenced;
            }
        }
        public override void AddReference(ModelCode referenceId, long globalId)
        {
            switch (referenceId)
            {
                case ModelCode.MEASUREMENTPOINT_TIMESERIES:
                    measurementPoints.Add(globalId);
                    break;

                default:
                    base.AddReference(referenceId, globalId);
                    break;
            }
        }


        public override void RemoveReference(ModelCode referenceId, long globalId)
        {
            switch (referenceId)
            {
                case ModelCode.MEASUREMENTPOINT_TIMESERIES:

                    if (measurementPoints.Contains(globalId))
                    {
                        measurementPoints.Remove(globalId);
                    }
                    else
                    {
                        CommonTrace.WriteTrace(CommonTrace.TraceWarning, "Entity (GID = 0x{0:x16}) doesn't contain reference 0x{1:x16}.", this.GlobalId, globalId);
                    }

                    break;

                default:
                    base.RemoveReference(referenceId, globalId);
                    break;
            }
        }

        #endregion IReference implementation	


    }
}
