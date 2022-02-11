//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FTN {
    using System;
    using FTN;
    
    
    /// The formal specification of specific characteristics related to a bid.
    public class BidTimeSeries : TimeSeries {
        
        /// Indication that  the values in the period are considered as a whole. They cannot be changed or subdivided.
        private System.Boolean? cim_blockBid;
        
        private const bool isBlockBidMandatory = false;
        
        private const string _blockBidPrefix = "cim";
        
        /// The coded identification of the energy flow.
        private string cim_direction;
        
        private const bool isDirectionMandatory = false;
        
        private const string _directionPrefix = "cim";
        
        /// An indication whether or not each element of the bid may be partially accepted or not.
        private System.Boolean? cim_divisible;
        
        private const bool isDivisibleMandatory = false;
        
        private const string _divisiblePrefix = "cim";
        
        /// Unique identification associated with all linked bids.
        private string cim_linkedBidsIdentification;
        
        private const bool isLinkedBidsIdentificationMandatory = false;
        
        private const string _linkedBidsIdentificationPrefix = "cim";
        
        public virtual bool BlockBid {
            get {
                return this.cim_blockBid.GetValueOrDefault();
            }
            set {
                this.cim_blockBid = value;
            }
        }
        
        public virtual bool BlockBidHasValue {
            get {
                return this.cim_blockBid != null;
            }
        }
        
        public static bool IsBlockBidMandatory {
            get {
                return isBlockBidMandatory;
            }
        }
        
        public static string BlockBidPrefix {
            get {
                return _blockBidPrefix;
            }
        }
        
        public virtual string Direction {
            get {
                return this.cim_direction;
            }
            set {
                this.cim_direction = value;
            }
        }
        
        public virtual bool DirectionHasValue {
            get {
                return this.cim_direction != null;
            }
        }
        
        public static bool IsDirectionMandatory {
            get {
                return isDirectionMandatory;
            }
        }
        
        public static string DirectionPrefix {
            get {
                return _directionPrefix;
            }
        }
        
        public virtual bool Divisible {
            get {
                return this.cim_divisible.GetValueOrDefault();
            }
            set {
                this.cim_divisible = value;
            }
        }
        
        public virtual bool DivisibleHasValue {
            get {
                return this.cim_divisible != null;
            }
        }
        
        public static bool IsDivisibleMandatory {
            get {
                return isDivisibleMandatory;
            }
        }
        
        public static string DivisiblePrefix {
            get {
                return _divisiblePrefix;
            }
        }
        
        public virtual string LinkedBidsIdentification {
            get {
                return this.cim_linkedBidsIdentification;
            }
            set {
                this.cim_linkedBidsIdentification = value;
            }
        }
        
        public virtual bool LinkedBidsIdentificationHasValue {
            get {
                return this.cim_linkedBidsIdentification != null;
            }
        }
        
        public static bool IsLinkedBidsIdentificationMandatory {
            get {
                return isLinkedBidsIdentificationMandatory;
            }
        }
        
        public static string LinkedBidsIdentificationPrefix {
            get {
                return _linkedBidsIdentificationPrefix;
            }
        }
    }
}