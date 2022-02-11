using FTN.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTN.Services.NetworkModelService.DataModel.Core
{
    public class Period : IdentifiedObject
    {
        private long marketDocument;
        List<long> points = new List<long>();
        List<long> timeSeries = new List<long>();

        public Period(long globalId) : base(globalId)
        {
        }

        public long MarketDocument { get => marketDocument; set => marketDocument = value; }
        public List<long> Points { get => points; set => points = value; }
        public List<long> TimeSeries { get => timeSeries; set => timeSeries = value; }

        public override bool Equals(object obj)
        {
            if (base.Equals(obj))
            {
                Period x = (Period)obj;
                return ((x.marketDocument == this.marketDocument) &&
                        (CompareHelper.CompareLists(x.points, this.points)) &&
                        (CompareHelper.CompareLists(x.timeSeries, this.timeSeries)));
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
                case ModelCode.PERIOD_MARKETDOCUMENT:
                case ModelCode.PERIOD_POINTS:
                case ModelCode.PERIOD_TIMESERIES:
                    return true;

                default:
                    return base.HasProperty(t);
            }
        }

        public override void GetProperty(Property prop)
        {
            switch (prop.Id)
            {
                case ModelCode.PERIOD_MARKETDOCUMENT:
                    prop.SetValue(marketDocument);
                    break;
                case ModelCode.PERIOD_POINTS:
                    prop.SetValue(points);
                    break;
                case ModelCode.PERIOD_TIMESERIES:
                    prop.SetValue(timeSeries);
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
                case ModelCode.PERIOD_MARKETDOCUMENT:
                    marketDocument = property.AsReference();
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
                return points.Count > 0 || timeSeries.Count > 0 || base.IsReferenced;
            }
        }
        public override void AddReference(ModelCode referenceId, long globalId)
        {
            switch (referenceId)
            {
                case ModelCode.POINT_PERIOD:
                    points.Add(globalId);
                    break;
                case ModelCode.TIMESERIES_PERIOD:
                    timeSeries.Add(globalId);
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
                case ModelCode.POINT_PERIOD:

                    if (points.Contains(globalId))
                    {
                        points.Remove(globalId);
                    }
                    else
                    {
                        CommonTrace.WriteTrace(CommonTrace.TraceWarning, "Entity (GID = 0x{0:x16}) doesn't contain reference 0x{1:x16}.", this.GlobalId, globalId);
                    }

                    break;

                case ModelCode.TIMESERIES_PERIOD:

                    if (timeSeries.Contains(globalId))
                    {
                        timeSeries.Remove(globalId);
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
