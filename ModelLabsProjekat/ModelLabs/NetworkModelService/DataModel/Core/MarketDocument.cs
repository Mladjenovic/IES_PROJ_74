using FTN.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTN.Services.NetworkModelService.DataModel.Core
{
    public class MarketDocument : Document
    {
        private long process;
        private List<long> periods = new List<long>();
        private List<long> timeSeries = new List<long>();

        public MarketDocument(long globalId) : base(globalId)
        {
        }

        public long Process { get => process; set => process = value; }
        public List<long> Periods { get => periods; set => periods = value; }
        public List<long> TimeSeries { get => timeSeries; set => timeSeries = value; }

        public override bool Equals(object obj)
        {
            if (base.Equals(obj))
            {
                MarketDocument x = (MarketDocument)obj;
                return ((x.process == this.process) &&
                    (CompareHelper.CompareLists(x.periods, this.periods) &&
                    (CompareHelper.CompareLists(x.timeSeries, this.timeSeries))));
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
                case ModelCode.MARKETDOCUMENT_PROCESS:
                case ModelCode.MARKETDOCUMENT_PERIODS:
                case ModelCode.MARKETDOCUMENT_TIMESERIES:
                    return true;

                default:
                    return base.HasProperty(t);
            }
        }
        public override void GetProperty(Property prop)
        {
            switch (prop.Id)
            {
                case ModelCode.MARKETDOCUMENT_PROCESS:
                    prop.SetValue(process);
                    break;
                case ModelCode.MARKETDOCUMENT_PERIODS:
                    prop.SetValue(periods);
                    break;
                case ModelCode.MARKETDOCUMENT_TIMESERIES:
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
                case ModelCode.MARKETDOCUMENT_PROCESS:
                    process = property.AsReference();
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
                return periods.Count > 0 || timeSeries.Count > 0 || base.IsReferenced;
            }
        }
        public override void AddReference(ModelCode referenceId, long globalId)
        {
            switch (referenceId)
            {
                case ModelCode.PERIOD_MARKETDOCUMENT:
                    periods.Add(globalId);
                    break;
                case ModelCode.TIMESERIES_MARKETDOCUMENT:
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
                case ModelCode.PERIOD_MARKETDOCUMENT:

                    if (periods.Contains(globalId))
                    {
                        periods.Remove(globalId);
                    }
                    else
                    {
                        CommonTrace.WriteTrace(CommonTrace.TraceWarning, "Entity (GID = 0x{0:x16}) doesn't contain reference 0x{1:x16}.", this.GlobalId, globalId);
                    }

                    break;


                case ModelCode.TIMESERIES_MARKETDOCUMENT:

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
