using FTN.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTN.Services.NetworkModelService.DataModel.Core
{
    public class BidTimeSeries : TimeSeries
    {

        private bool blockBid;
        private string direction;
        private bool divisible; 
        private string linkedBidsIdentification;
        private float minimumActivationQuantity;
        private float stepIncrementQuantity; 
        public BidTimeSeries(long globalId) : base(globalId)
        {
        }

        public bool BlockBid { get => blockBid; set => blockBid = value; }
        public string Direction { get => direction; set => direction = value; }
        public bool Divisible { get => divisible; set => divisible = value; }
        public string LinkedBidsIdentification { get => linkedBidsIdentification; set => linkedBidsIdentification = value; }
        public float MinimumActivationQuantity { get => minimumActivationQuantity; set => minimumActivationQuantity = value; }
        public float StepIncrementQuantity { get => stepIncrementQuantity; set => stepIncrementQuantity = value; }

        public override bool Equals(object obj)
        {
            if (base.Equals(obj))
            {
                BidTimeSeries x = (BidTimeSeries)obj;
                return ((x.blockBid == this.blockBid) &&
                        (x.direction == this.direction) &&
                        (x.divisible == this.divisible) &&
                        (x.linkedBidsIdentification == this.linkedBidsIdentification) &&
                        (x.minimumActivationQuantity == this.minimumActivationQuantity) &&
                        (x.stepIncrementQuantity == this.stepIncrementQuantity));
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
                case ModelCode.BIDTIMESERIES_BLOCKBID:
                case ModelCode.BIDTIMESERIES_DIRECTION:
                case ModelCode.BIDTIMESERIES_DIVISIBLE:
                case ModelCode.BIDTIMESERIES_LINKEDBIDSIDENTIFICATION:
                case ModelCode.BIDTIMESERIES_MINIMUMACTIVATIONQUANTITY:
                case ModelCode.BIDTIMESERIES_STEPINCREMENTQUANTITY:

                    return true;

                default:
                    return base.HasProperty(t);
            }
        }

        public override void GetProperty(Property prop)
        {
            switch (prop.Id)
            {
                case ModelCode.BIDTIMESERIES_BLOCKBID:
                    prop.SetValue(blockBid);
                    break;
                case ModelCode.BIDTIMESERIES_DIRECTION:
                    prop.SetValue(direction);
                    break;
                case ModelCode.BIDTIMESERIES_DIVISIBLE:
                    prop.SetValue(divisible);
                    break;

                case ModelCode.BIDTIMESERIES_LINKEDBIDSIDENTIFICATION:
                    prop.SetValue(linkedBidsIdentification);
                    break;
                case ModelCode.BIDTIMESERIES_MINIMUMACTIVATIONQUANTITY:
                    prop.SetValue(minimumActivationQuantity);
                    break;
                case ModelCode.BIDTIMESERIES_STEPINCREMENTQUANTITY:
                    prop.SetValue(stepIncrementQuantity);
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
                case ModelCode.BIDTIMESERIES_BLOCKBID:
                    blockBid = property.AsBool();
                    break;
                case ModelCode.BIDTIMESERIES_DIRECTION:
                    direction = property.AsString();
                    break;
                case ModelCode.BIDTIMESERIES_DIVISIBLE:
                    divisible = property.AsBool();
                    break;
                case ModelCode.BIDTIMESERIES_LINKEDBIDSIDENTIFICATION:
                    linkedBidsIdentification = property.AsString();
                    break;
                case ModelCode.BIDTIMESERIES_MINIMUMACTIVATIONQUANTITY:
                    minimumActivationQuantity = property.AsFloat();
                    break;
                case ModelCode.BIDTIMESERIES_STEPINCREMENTQUANTITY:
                    stepIncrementQuantity = property.AsFloat();
                    break;

                default:
                    base.SetProperty(property);
                    break;
            }
        }

        #endregion IAccess implementation


    }
}
