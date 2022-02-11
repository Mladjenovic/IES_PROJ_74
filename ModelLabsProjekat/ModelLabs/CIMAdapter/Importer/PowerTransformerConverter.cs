namespace FTN.ESI.SIMES.CIM.CIMAdapter.Importer
{
	using FTN.Common;

	/// <summary>
	/// PowerTransformerConverter has methods for populating
	/// ResourceDescription objects using PowerTransformerCIMProfile_Labs objects.
	/// </summary>
	public static class PowerTransformerConverter
	{

        #region Populate ResourceDescription
        public static void PopulateIdentifiedObjectProperties(FTN.IdentifiedObject cimIdentifiedObject, ResourceDescription rd)
        {
            if ((cimIdentifiedObject != null) && (rd != null))
            {
                if (cimIdentifiedObject.MRIDHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.IDOBJ_MRID, cimIdentifiedObject.MRID));
                }
                if (cimIdentifiedObject.NameHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.IDOBJ_NAME, cimIdentifiedObject.Name));
                }
                if (cimIdentifiedObject.AliasNameHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.IDOBJ_ALIASNAME, cimIdentifiedObject.AliasName));
                }
            }
        }

        public static void PopulateProcessProperties(FTN.Process cimProcess, ResourceDescription rd)
        {
            if ((cimProcess != null) && (rd != null))
            {
                PowerTransformerConverter.PopulateIdentifiedObjectProperties(cimProcess, rd);

                if (cimProcess.ClassificationTypeHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.PROCESS_CLASIFICATIONTYPE, cimProcess.ClassificationType));
                }
                if (cimProcess.ProcessTypeHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.PROCESS_PROCESSTYPE, cimProcess.ProcessType));
                }
            }
        }

        public static void PopulateDocumentProperites(FTN.Document cimDocument, ResourceDescription rd)
        {
            if ((cimDocument != null) && (rd != null))
            {
                PowerTransformerConverter.PopulateIdentifiedObjectProperties(cimDocument, rd);

                if (cimDocument.CreatedDateTimeHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.DOCUMENT_CREATEDDATETIME, cimDocument.CreatedDateTime));
                }
                if (cimDocument.LastModifiedDateTimeHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.DOCUMENT_LASTMODIFIEDDATETIME, cimDocument.LastModifiedDateTime));
                }
                if (cimDocument.RevisionNumberHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.DOCUMENT_REVISIONNUMBER, cimDocument.RevisionNumber));
                }
                if (cimDocument.SubjectHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.DOCUMENT_SUBJECT, cimDocument.Subject));
                }
                if (cimDocument.TitleHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.DOCUMENT_TITLE, cimDocument.Title));
                }
                if (cimDocument.TypeHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.DOCUMENT_TYPE, cimDocument.Type));
                }
            }
        }

        public static void PopulateMarketDocumentProperties(FTN.MarketDocument cimMarketDocument, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
        {
            if ((cimMarketDocument != null) && (rd != null))
            {
                PowerTransformerConverter.PopulateDocumentProperites(cimMarketDocument, rd);

                if (cimMarketDocument.ProcessHasValue)
                {
                    long gid = importHelper.GetMappedGID(cimMarketDocument.Process.ID);
                    if (gid < 0)
                    {
                        report.Report.Append("WARNING: Convert ").Append(cimMarketDocument.GetType().ToString()).Append(" rdfID = \"").Append(cimMarketDocument.ID);
                        report.Report.Append("\" - Failed to set reference to MarketDocument: rdfID \"").Append(cimMarketDocument.Process.ID).AppendLine(" \" is not mapped to GID!");
                    }
                    rd.AddProperty(new Property(ModelCode.MARKETDOCUMENT_PROCESS, gid));
                }
            }
        }

        public static void PopulatePeriodProperties(FTN.Period cimPeriod, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
        {
            if ((cimPeriod != null) && (rd != null))
            {
                PowerTransformerConverter.PopulateIdentifiedObjectProperties(cimPeriod, rd);

                if (cimPeriod.MarketDocumentHasValue)
                {
                    long gid = importHelper.GetMappedGID(cimPeriod.MarketDocument.ID);
                    if (gid < 0)
                    {
                        report.Report.Append("WARNING: Convert ").Append(cimPeriod.GetType().ToString()).Append(" rdfID = \"").Append(cimPeriod.ID);
                        report.Report.Append("\" - Failed to set reference to MarketDocument: rdfID \"").Append(cimPeriod.MarketDocument.ID).AppendLine(" \" is not mapped to GID!");
                    }
                    rd.AddProperty(new Property(ModelCode.PERIOD_MARKETDOCUMENT, gid));
                }
            }
        }

        public static void PopulatePointProperties(FTN.Point cimPoint, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
        {
            if ((cimPoint != null) && (rd != null))
            {
                PowerTransformerConverter.PopulateIdentifiedObjectProperties(cimPoint, rd);

                if (cimPoint.PositionHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.POINT_POSITION, cimPoint.Position));
                }
                if (cimPoint.PeriodHasValue)
                {
                    long gid = importHelper.GetMappedGID(cimPoint.Period.ID);
                    if (gid < 0)
                    {
                        report.Report.Append("WARNING: Convert ").Append(cimPoint.GetType().ToString()).Append(" rdfID = \"").Append(cimPoint.ID);
                        report.Report.Append("\" - Failed to set reference to Period: rdfID \"").Append(cimPoint.Period.ID).AppendLine(" \" is not mapped to GID!");
                    }
                    rd.AddProperty(new Property(ModelCode.POINT_PERIOD, gid));
                }
            }
        }


        public static void PopulateTimeSeriesProperties(FTN.TimeSeries cimTimeSeries, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
        {
            if ((cimTimeSeries != null) && (rd != null))
            {
                PowerTransformerConverter.PopulateIdentifiedObjectProperties(cimTimeSeries, rd);

                if (cimTimeSeries.ObjectAggregationHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.TIMESERIES_OBJECTAGGREGATION, cimTimeSeries.ObjectAggregation));
                }
                if (cimTimeSeries.ProductHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.TIMESERIES_PRODUCT, cimTimeSeries.Product));
                }
                if (cimTimeSeries.VersionHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.TIMESERIES_VERSION, cimTimeSeries.Version));
                }

                if (cimTimeSeries.MarketDocumentHasValue)
                {
                    long gid = importHelper.GetMappedGID(cimTimeSeries.MarketDocument.ID);
                    if (gid < 0)
                    {
                        report.Report.Append("WARNING: Convert ").Append(cimTimeSeries.GetType().ToString()).Append(" rdfID = \"").Append(cimTimeSeries.ID);
                        report.Report.Append("\" - Failed to set reference to Period: rdfID \"").Append(cimTimeSeries.Period.ID).AppendLine(" \" is not mapped to GID!");
                    }
                    rd.AddProperty(new Property(ModelCode.TIMESERIES_MARKETDOCUMENT, gid));
                }
                if (cimTimeSeries.PeriodHasValue)
                {
                    long gid = importHelper.GetMappedGID(cimTimeSeries.Period.ID);
                    if (gid < 0)
                    {
                        report.Report.Append("WARNING: Convert ").Append(cimTimeSeries.GetType().ToString()).Append(" rdfID = \"").Append(cimTimeSeries.ID);
                        report.Report.Append("\" - Failed to set reference to Period: rdfID \"").Append(cimTimeSeries.Period.ID).AppendLine(" \" is not mapped to GID!");
                    }
                    rd.AddProperty(new Property(ModelCode.TIMESERIES_PERIOD, gid));
                }
            }

        }


        public static void PopulateBidTimeSeriesProperties(FTN.BidTimeSeries cimBidTimeSeries, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
        {
            if ((cimBidTimeSeries != null) && (rd != null))
            {
                PowerTransformerConverter.PopulateTimeSeriesProperties(cimBidTimeSeries, rd, importHelper, report);

                if (cimBidTimeSeries.BlockBidHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.BIDTIMESERIES_BLOCKBID, cimBidTimeSeries.BlockBid));
                }
                if (cimBidTimeSeries.DirectionHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.BIDTIMESERIES_DIRECTION, cimBidTimeSeries.Direction));
                }
                if (cimBidTimeSeries.DivisibleHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.BIDTIMESERIES_DIVISIBLE, cimBidTimeSeries.Divisible));
                }
                if (cimBidTimeSeries.LinkedBidsIdentificationHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.BIDTIMESERIES_LINKEDBIDSIDENTIFICATION, cimBidTimeSeries.LinkedBidsIdentification));
                }
            }
        }


        public static void PopulateMeasurementPointProperties(FTN.MeasurementPoint cimMeasurementPoint, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
        {
            if ((cimMeasurementPoint != null) && (rd != null))
            {
                PowerTransformerConverter.PopulateIdentifiedObjectProperties(cimMeasurementPoint, rd);

                if (cimMeasurementPoint.TimeSeriesHasValue)
                {
                    long gid = importHelper.GetMappedGID(cimMeasurementPoint.TimeSeries.ID);
                    if (gid < 0)
                    {
                        report.Report.Append("WARNING: Convert ").Append(cimMeasurementPoint.GetType().ToString()).Append(" rdfID = \"").Append(cimMeasurementPoint.ID);
                        report.Report.Append("\" - Failed to set reference to Period: rdfID \"").Append(cimMeasurementPoint.TimeSeries.ID).AppendLine(" \" is not mapped to GID!");
                    }
                    rd.AddProperty(new Property(ModelCode.MEASUREMENTPOINT_TIMESERIES, gid));
                }
            }
        }


        //public static void PopulateLocationProperties(FTN.Location cimLocation, ResourceDescription rd)
        //{
        //	if ((cimLocation != null) && (rd != null))
        //	{
        //		PowerTransformerConverter.PopulateIdentifiedObjectProperties(cimLocation, rd);

        //		if (cimLocation.CategoryHasValue)
        //		{
        //			rd.AddProperty(new Property(ModelCode.LOCATION_CATEGORY, cimLocation.Category));
        //		}
        //		if (cimLocation.CorporateCodeHasValue)
        //		{
        //			rd.AddProperty(new Property(ModelCode.LOCATION_CORPORATECODE, cimLocation.CorporateCode));
        //		}
        //	}
        //}

        //public static void PopulatePowerSystemResourceProperties(FTN.PowerSystemResource cimPowerSystemResource, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
        //{
        //	if ((cimPowerSystemResource != null) && (rd != null))
        //	{
        //		PowerTransformerConverter.PopulateIdentifiedObjectProperties(cimPowerSystemResource, rd);

        //		if (cimPowerSystemResource.CustomTypeHasValue)
        //		{
        //			rd.AddProperty(new Property(ModelCode.PSR_CUSTOMTYPE, cimPowerSystemResource.CustomType));
        //		}
        //		if (cimPowerSystemResource.LocationHasValue)
        //		{
        //			long gid = importHelper.GetMappedGID(cimPowerSystemResource.Location.ID);
        //			if (gid < 0)
        //			{
        //				report.Report.Append("WARNING: Convert ").Append(cimPowerSystemResource.GetType().ToString()).Append(" rdfID = \"").Append(cimPowerSystemResource.ID);
        //				report.Report.Append("\" - Failed to set reference to Location: rdfID \"").Append(cimPowerSystemResource.Location.ID).AppendLine(" \" is not mapped to GID!");
        //			}
        //			rd.AddProperty(new Property(ModelCode.PSR_LOCATION, gid));
        //		}
        //	}
        //}

        //public static void PopulateBaseVoltageProperties(FTN.BaseVoltage cimBaseVoltage, ResourceDescription rd)
        //{
        //	if ((cimBaseVoltage != null) && (rd != null))
        //	{
        //		PowerTransformerConverter.PopulateIdentifiedObjectProperties(cimBaseVoltage, rd);

        //		if (cimBaseVoltage.NominalVoltageHasValue)
        //		{
        //			rd.AddProperty(new Property(ModelCode.BASEVOLTAGE_NOMINALVOLTAGE, cimBaseVoltage.NominalVoltage));
        //		}
        //	}
        //}

        //public static void PopulateEquipmentProperties(FTN.Equipment cimEquipment, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
        //{
        //	if ((cimEquipment != null) && (rd != null))
        //	{
        //		PowerTransformerConverter.PopulatePowerSystemResourceProperties(cimEquipment, rd, importHelper, report);

        //		if (cimEquipment.PrivateHasValue)
        //		{
        //			rd.AddProperty(new Property(ModelCode.EQUIPMENT_ISPRIVATE, cimEquipment.Private));
        //		}
        //		if (cimEquipment.IsUndergroundHasValue)
        //		{
        //			rd.AddProperty(new Property(ModelCode.EQUIPMENT_ISUNDERGROUND, cimEquipment.IsUnderground));
        //		}
        //	}
        //}

        //public static void PopulateConductingEquipmentProperties(FTN.ConductingEquipment cimConductingEquipment, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
        //{
        //	if ((cimConductingEquipment != null) && (rd != null))
        //	{
        //		PowerTransformerConverter.PopulateEquipmentProperties(cimConductingEquipment, rd, importHelper, report);

        //		if (cimConductingEquipment.PhasesHasValue)
        //		{
        //			rd.AddProperty(new Property(ModelCode.CONDEQ_PHASES, (short)GetDMSPhaseCode(cimConductingEquipment.Phases)));
        //		}
        //		if (cimConductingEquipment.RatedVoltageHasValue)
        //		{
        //			rd.AddProperty(new Property(ModelCode.CONDEQ_RATEDVOLTAGE, cimConductingEquipment.RatedVoltage));
        //		}
        //		if (cimConductingEquipment.BaseVoltageHasValue)
        //		{
        //			long gid = importHelper.GetMappedGID(cimConductingEquipment.BaseVoltage.ID);
        //			if (gid < 0)
        //			{
        //				report.Report.Append("WARNING: Convert ").Append(cimConductingEquipment.GetType().ToString()).Append(" rdfID = \"").Append(cimConductingEquipment.ID);
        //				report.Report.Append("\" - Failed to set reference to BaseVoltage: rdfID \"").Append(cimConductingEquipment.BaseVoltage.ID).AppendLine(" \" is not mapped to GID!");
        //			}
        //			rd.AddProperty(new Property(ModelCode.CONDEQ_BASVOLTAGE, gid));
        //		}
        //	}
        //}

        //public static void PopulatePowerTransformerProperties(FTN.PowerTransformer cimPowerTransformer, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
        //{
        //	if ((cimPowerTransformer != null) && (rd != null))
        //	{
        //		PowerTransformerConverter.PopulateEquipmentProperties(cimPowerTransformer, rd, importHelper, report);

        //		if (cimPowerTransformer.FunctionHasValue)
        //		{
        //			rd.AddProperty(new Property(ModelCode.POWERTR_FUNC, (short)GetDMSTransformerFunctionKind(cimPowerTransformer.Function)));
        //		}
        //		if (cimPowerTransformer.AutotransformerHasValue)
        //		{
        //			rd.AddProperty(new Property(ModelCode.POWERTR_AUTO, cimPowerTransformer.Autotransformer));
        //		}
        //	}
        //}

        //public static void PopulateTransformerWindingProperties(FTN.TransformerWinding cimTransformerWinding, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
        //{
        //	if ((cimTransformerWinding != null) && (rd != null))
        //	{
        //		PowerTransformerConverter.PopulateConductingEquipmentProperties(cimTransformerWinding, rd, importHelper, report);

        //		if (cimTransformerWinding.ConnectionTypeHasValue)
        //		{
        //			rd.AddProperty(new Property(ModelCode.POWERTRWINDING_CONNTYPE, (short)GetDMSWindingConnection(cimTransformerWinding.ConnectionType)));
        //		}
        //		if (cimTransformerWinding.GroundedHasValue)
        //		{
        //			rd.AddProperty(new Property(ModelCode.POWERTRWINDING_GROUNDED, cimTransformerWinding.Grounded));
        //		}
        //		if (cimTransformerWinding.RatedSHasValue)
        //		{
        //			rd.AddProperty(new Property(ModelCode.POWERTRWINDING_RATEDS, cimTransformerWinding.RatedS));
        //		}
        //		if (cimTransformerWinding.RatedUHasValue)
        //		{
        //			rd.AddProperty(new Property(ModelCode.POWERTRWINDING_RATEDU, cimTransformerWinding.RatedU));
        //		}
        //		if (cimTransformerWinding.WindingTypeHasValue)
        //		{
        //			rd.AddProperty(new Property(ModelCode.POWERTRWINDING_WINDTYPE, (short)GetDMSWindingType(cimTransformerWinding.WindingType)));
        //		}
        //		if (cimTransformerWinding.PhaseToGroundVoltageHasValue)
        //		{
        //			rd.AddProperty(new Property(ModelCode.POWERTRWINDING_PHASETOGRNDVOLTAGE, cimTransformerWinding.PhaseToGroundVoltage));
        //		}
        //		if (cimTransformerWinding.PhaseToPhaseVoltageHasValue)
        //		{
        //			rd.AddProperty(new Property(ModelCode.POWERTRWINDING_PHASETOPHASEVOLTAGE, cimTransformerWinding.PhaseToPhaseVoltage));
        //		}
        //		if (cimTransformerWinding.PowerTransformerHasValue)
        //		{
        //			long gid = importHelper.GetMappedGID(cimTransformerWinding.PowerTransformer.ID);
        //			if (gid < 0)
        //			{
        //				report.Report.Append("WARNING: Convert ").Append(cimTransformerWinding.GetType().ToString()).Append(" rdfID = \"").Append(cimTransformerWinding.ID);
        //				report.Report.Append("\" - Failed to set reference to PowerTransformer: rdfID \"").Append(cimTransformerWinding.PowerTransformer.ID).AppendLine(" \" is not mapped to GID!");
        //			}
        //			rd.AddProperty(new Property(ModelCode.POWERTRWINDING_POWERTRW, gid));
        //		}
        //	}
        //}

        //public static void PopulateWindingTestProperties(FTN.WindingTest cimWindingTest, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
        //{
        //	if ((cimWindingTest != null) && (rd != null))
        //	{
        //		PowerTransformerConverter.PopulateIdentifiedObjectProperties(cimWindingTest, rd);

        //		if (cimWindingTest.LeakageImpedanceHasValue)
        //		{
        //			rd.AddProperty(new Property(ModelCode.WINDINGTEST_LEAKIMPDN, cimWindingTest.LeakageImpedance));
        //		}
        //		if (cimWindingTest.LoadLossHasValue)
        //		{
        //			rd.AddProperty(new Property(ModelCode.WINDINGTEST_LOADLOSS, cimWindingTest.LoadLoss));
        //		}
        //		if (cimWindingTest.NoLoadLossHasValue)
        //		{
        //			rd.AddProperty(new Property(ModelCode.WINDINGTEST_NOLOADLOSS, cimWindingTest.NoLoadLoss));
        //		}
        //		if (cimWindingTest.PhaseShiftHasValue)
        //		{
        //			rd.AddProperty(new Property(ModelCode.WINDINGTEST_PHASESHIFT, cimWindingTest.PhaseShift));
        //		}
        //		if (cimWindingTest.LeakageImpedance0PercentHasValue)
        //		{
        //			rd.AddProperty(new Property(ModelCode.WINDINGTEST_LEAKIMPDN0PERCENT, cimWindingTest.LeakageImpedance0Percent));
        //		}
        //		if (cimWindingTest.LeakageImpedanceMaxPercentHasValue)
        //		{
        //			rd.AddProperty(new Property(ModelCode.WINDINGTEST_LEAKIMPDNMAXPERCENT, cimWindingTest.LeakageImpedanceMaxPercent));
        //		}
        //		if (cimWindingTest.LeakageImpedanceMinPercentHasValue)
        //		{
        //			rd.AddProperty(new Property(ModelCode.WINDINGTEST_LEAKIMPDNMINPERCENT, cimWindingTest.LeakageImpedanceMinPercent));
        //		}

        //		if (cimWindingTest.From_TransformerWindingHasValue)
        //		{
        //			long gid = importHelper.GetMappedGID(cimWindingTest.From_TransformerWinding.ID);
        //			if (gid < 0)
        //			{
        //				report.Report.Append("WARNING: Convert ").Append(cimWindingTest.GetType().ToString()).Append(" rdfID = \"").Append(cimWindingTest.ID);
        //				report.Report.Append("\" - Failed to set reference to TransformerWinding: rdfID \"").Append(cimWindingTest.From_TransformerWinding.ID).AppendLine(" \" is not mapped to GID!");
        //			}
        //			rd.AddProperty(new Property(ModelCode.WINDINGTEST_POWERTRWINDING, gid));
        //		}
        //	}
        //}
        #endregion Populate ResourceDescription

        #region Enums convert
        //public static PhaseCode GetDMSPhaseCode(FTN.PhaseCode phases)
        //{
        //	switch (phases)
        //	{
        //		case FTN.PhaseCode.A:
        //			return PhaseCode.A;
        //		case FTN.PhaseCode.AB:
        //			return PhaseCode.AB;
        //		case FTN.PhaseCode.ABC:
        //			return PhaseCode.ABC;
        //		case FTN.PhaseCode.ABCN:
        //			return PhaseCode.ABCN;
        //		case FTN.PhaseCode.ABN:
        //			return PhaseCode.ABN;
        //		case FTN.PhaseCode.AC:
        //			return PhaseCode.AC;
        //		case FTN.PhaseCode.ACN:
        //			return PhaseCode.ACN;
        //		case FTN.PhaseCode.AN:
        //			return PhaseCode.AN;
        //		case FTN.PhaseCode.B:
        //			return PhaseCode.B;
        //		case FTN.PhaseCode.BC:
        //			return PhaseCode.BC;
        //		case FTN.PhaseCode.BCN:
        //			return PhaseCode.BCN;
        //		case FTN.PhaseCode.BN:
        //			return PhaseCode.BN;
        //		case FTN.PhaseCode.C:
        //			return PhaseCode.C;
        //		case FTN.PhaseCode.CN:
        //			return PhaseCode.CN;
        //		case FTN.PhaseCode.N:
        //			return PhaseCode.N;
        //		case FTN.PhaseCode.s12N:
        //			return PhaseCode.ABN;
        //		case FTN.PhaseCode.s1N:
        //			return PhaseCode.AN;
        //		case FTN.PhaseCode.s2N:
        //			return PhaseCode.BN;
        //		default: return PhaseCode.Unknown;
        //	}
        //}

        //public static TransformerFunction GetDMSTransformerFunctionKind(FTN.TransformerFunctionKind transformerFunction)
        //{
        //	switch (transformerFunction)
        //	{
        //		case FTN.TransformerFunctionKind.voltageRegulator:
        //			return TransformerFunction.Voltreg;
        //		default:
        //			return TransformerFunction.Consumer;
        //	}
        //}

        //public static WindingType GetDMSWindingType(FTN.WindingType windingType)
        //{
        //	switch (windingType)
        //	{
        //		case FTN.WindingType.primary:
        //			return WindingType.Primary;
        //		case FTN.WindingType.secondary:
        //			return WindingType.Secondary;
        //		case FTN.WindingType.tertiary:
        //			return WindingType.Tertiary;
        //		default:
        //			return WindingType.None;
        //	}
        //}

        //public static WindingConnection GetDMSWindingConnection(FTN.WindingConnection windingConnection)
        //{
        //	switch (windingConnection)
        //	{
        //		case FTN.WindingConnection.D:
        //			return WindingConnection.D;
        //		case FTN.WindingConnection.I:
        //			return WindingConnection.I;
        //		case FTN.WindingConnection.Z:
        //			return WindingConnection.Z;
        //		case FTN.WindingConnection.Y:
        //			return WindingConnection.Y;
        //		default:
        //			return WindingConnection.Y;
        //	}
        //}
        #endregion Enums convert
    }
}
